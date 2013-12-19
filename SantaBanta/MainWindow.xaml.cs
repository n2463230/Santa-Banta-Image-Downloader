using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using SantaBanta.Data;
using Path = System.IO.Path;
using SantaBantaEntity;

namespace SantaBanta
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region static declarations
        private static WebClient _wc = new WebClient();
        private const string Url = "http://www.santabanta.com/wallpapers/parents.asp";
        #endregion

        public MainWindow()
        {
            ServicePointManager.DefaultConnectionLimit = 100;
            InitializeComponent();
            string retrievedHTML = RetrieveData(Url);


            IEnumerable<Categories> categorieses = FetchCategoryNames(retrievedHTML);
            foreach (var categoriesContent in categorieses)
            {
                var webSite = new WebSite();
                webSite.AddCategories(categoriesContent.CategoryName, categoriesContent.CategoryURL);
            }
            cmbCategories.ItemsSource = categorieses;
        }

        #region Button Events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FetchSubCategories();
        }

        private void FetchSubCategories()
        {
            const int pagesToCheckForSubCategories = 200;
            var categoriesContents = new List<CategoriesContent>();

            string urlToFetch = cmbCategories.SelectedValue.ToString();
            prgCategory.Maximum = pagesToCheckForSubCategories;
            prgCategory.Value = 1;
            string retrievedHTML = RetrieveData(urlToFetch);

            categoriesContents.AddRange(FetchSelectedCategoryContent(retrievedHTML));

            for (int i = 2; i < pagesToCheckForSubCategories; i++)//Checks the no of pages where from different individual star celebrity names can be found.
            {
                string url = urlToFetch + "?order=n&page=" + i;
                retrievedHTML = RetrieveData(url);
                prgCategory.Value = i;
                categoriesContents.AddRange(FetchSelectedCategoryContent(retrievedHTML));
            }

            var categoriesContentsToBind = new List<CategoriesContent>();

            foreach (var categoriesContent in categoriesContents)
            {
                var content = categoriesContent;
                if (categoriesContentsToBind.Where(i => i.Name == content.Name).ToList().Count == 0)
                {
                    var webSite = new WebSite();
                    webSite.AddSubCategoriesCategories(categoriesContent.Name, categoriesContent.URL);
                    categoriesContentsToBind.Add(categoriesContent);
                }
            }


            cmbSubCategories.ItemsSource = categoriesContentsToBind.OrderBy(val => val.Name).ToList();
        }

        private void BtnDownloadAllClick(object sender, RoutedEventArgs e)
        {
            List<Categories> categorieses = cmbCategories.ItemsSource as List<Categories>;

            prgCategory.Maximum = categorieses.Count;

            for (int i = 6; i < categorieses.Count; i++)//Temporarily set to 1 otherwise default will be 0;
            {
                prgCategory.Value = i + 1;
                cmbCategories.SelectedIndex = i;
                Button_Click(sender, e);

                List<CategoriesContent> categoriesContents = cmbSubCategories.ItemsSource as List<CategoriesContent>;
                for (int j = 0; j < categoriesContents.Count; j++)
                {
                    cmbSubCategories.SelectedIndex = j;
                    BtnDownloadClick(sender, e);
                }
            }
        }

        private void BtnDownloadClick(object sender, RoutedEventArgs e)
        {

            string subcategoryURL = cmbSubCategories.SelectedValue.ToString();

            for (int i = 0; i < 200; i++)
            {
                try
                {
                    string rewrittenURL = subcategoryURL;
                    if (i > 0)
                    {
                        rewrittenURL = subcategoryURL + "?page=" + (i + 1);
                    }
                    string retrievedHTML = RetrieveData(rewrittenURL);

                    IEnumerable<DownloadList> downloadLists = FetchDownloadList(retrievedHTML);

                    foreach (var downloadList in downloadLists)
                    {
                        try
                        {
                            List<DownloadList> downloadListsList = WebSite.GetAllDownloadedLinksByImageName(cmbSubCategories.Text);

                            for (int k = 0; k < 10; k++)
                            {
                                if (k == 0)
                                {
                                    string downloadURL = FetchDownloadURL(downloadList.DownloadImageURL);
                                  
                                    var downloadListExists = downloadListsList.Where(d => d.DownloadImageURL == downloadURL).FirstOrDefault();

                                    if (downloadListExists != null)
                                    {
                                        k = 10;
                                    }
                                    else
                                    {
                                        DownloadImage(downloadURL);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        string downloadURL = FetchDownloadURL(downloadList.DownloadImageURL + "?high=" + k);
                                     
                                        var downloadListExists = downloadListsList.Where(d => d.DownloadImageURL == downloadURL).FirstOrDefault();
                                        if (downloadListExists != null)
                                        {
                                            k = 10;
                                        }
                                        else
                                        {
                                            DownloadImage(downloadURL);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            //Thread.Sleep(10000);
                        }
                        catch (Exception)
                        {

                        }
                    }

                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        private static IEnumerable<Categories> FetchCategoryNames(string retrievedHTML)
        {
            List<Categories> categoriesList = new List<Categories>();
            Categories category = new Categories();

            while (retrievedHTML.IndexOf("<div class=\"box-1-wb-2\">") > -1)
            {
                retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<div class=\"box-1-wb-2\">") + 1);
                retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<a href=\""));
                category = new Categories();
                category.CategoryURL = Url.Substring(0, Url.IndexOf("/wallpapers")) + RetrieveHREFFromAnchorTag(retrievedHTML);
                string startdiv = "<div class=\"box-1-wb-3\">";
                category.CategoryName = RetrieveDivData(retrievedHTML, startdiv);
                categoriesList.Add(category);
            }
            return categoriesList;
        }

        private static string RetrieveDivData(string retrievedHTML, string startDiv)
        {
            int startIndex = startDiv.ToCharArray().ToList().Count + retrievedHTML.IndexOf(startDiv);
            int endIndex = retrievedHTML.IndexOf("</div>") - startIndex;
            string retrievedData = retrievedHTML.Substring(startIndex, endIndex);
            return retrievedData;
        }

        private static string RetrieveHREFFromAnchorTag(string retrievedHTML)
        {
            string ahref = "<a href=\"";
            int startIndex = ahref.ToCharArray().ToList().Count;
            int endIndex = retrievedHTML.IndexOf("\">") - startIndex;
            string retrievedURL = retrievedHTML.Substring(startIndex, endIndex);
            return retrievedURL;
        }

        private static IEnumerable<CategoriesContent> FetchSelectedCategoryContent(string retrievedHTML)
        {
            List<string> strList = new List<string>();
            List<CategoriesContent> categoriesContents = new List<CategoriesContent>();
            CategoriesContent categoriesContent = new CategoriesContent();
            while (retrievedHTML.IndexOf("<div class=\"text\">") > -1)
            {
                categoriesContent = new CategoriesContent();
                retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<div class=\"text\">") + 1);
                retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<a href=\""));

                string partialHREF = RetrieveHREFFromAnchorTag(retrievedHTML);

                string closingAnchortag = "</a>";
                string startIndexValue = "\">";
                int startIndex = retrievedHTML.IndexOf(startIndexValue) + startIndexValue.ToCharArray().ToList().Count;
                int endIndex = retrievedHTML.IndexOf(closingAnchortag);
                categoriesContent.Name = retrievedHTML.Substring(startIndex, endIndex - startIndex);
                categoriesContent.URL = Url.Substring(0, Url.IndexOf("/wallpapers")) + partialHREF;
                categoriesContents.Add(categoriesContent);
                strList.Add(retrievedHTML);
            }
            return categoriesContents;
        }

        private static string RetrieveData(string url)
        {

            // used to build entire input
            var sb = new StringBuilder();

            // used on each read operation
            var buf = new byte[8192];
            try
            {
                // prepare the web page we will be asking for
                var request = (HttpWebRequest)
                                         WebRequest.Create(url);

                /* Using the proxy class to access the site
                 * Uri proxyURI = new Uri("http://proxy.com:80");
                 request.Proxy = new WebProxy(proxyURI);
                 request.Proxy.Credentials = new NetworkCredential("proxyuser", "proxypassword");*/

                // execute the request
                var response = (HttpWebResponse)
                                           request.GetResponse();

                // we will read data via the response stream
                Stream resStream = response.GetResponseStream();

                string tempString = null;
                int count = 0;

                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buf, 0, buf.Length);

                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        tempString = Encoding.ASCII.GetString(buf, 0, count);

                        // continue building the string
                        sb.Append(tempString);
                    }
                } while (count > 0); // any more data to read?

            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Failed to retrieve data from the network. Please check you internet connection: " +
                                exception);
            }
            return sb.ToString();
        }

        private static string FetchDownloadURL(string url)
        {
            string retrievedHTML = RetrieveData(url);

            while (retrievedHTML.IndexOf("<a") > 0)
            {
                string retrievedHTML1 = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<a") + 1);
                if (!retrievedHTML1.Contains("download="))
                {
                    break;
                }
                else
                {
                    retrievedHTML = retrievedHTML1;
                }
            }

            string startingPoint = "a href=\"";

            string endingPoint = "\" download";

            int startIndex = startingPoint.ToCharArray().ToList().Count;

            int endIndex = retrievedHTML.IndexOf(endingPoint);

            string html = retrievedHTML.Substring(startIndex, endIndex - startIndex);

            return html;
        }

        private void DownloadImage(string downloadImageUrl)
        {

            string filePath = GetFilePath(downloadImageUrl);
            try
            {
                WebSite webSite = new WebSite();

                if (filePath.Contains("jpg"))
                {
                    FunctionCallStatus functionCallStatus = webSite.AddDownloadLinks(cmbSubCategories.Text,
                                                                                     downloadImageUrl);
                    if (functionCallStatus != FunctionCallStatus.DataAlreadyExists)
                    {
                        _wc = new WebClient();
                        _wc.DownloadFile(new Uri(downloadImageUrl), filePath);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private string GetFilePath(string downloadImageUrl)
        {
            int i = 0;
            string filePath = @"e:\SantaBanta\" + cmbCategories.Text + "\\" + cmbSubCategories.Text + "\\" + i +
                                          Path.GetExtension(downloadImageUrl);

            if (!Directory.Exists(@"e:\SantaBanta\" + cmbCategories.Text + "\\" + cmbSubCategories.Text + "\\"))
            {
                Directory.CreateDirectory(@"e:\SantaBanta\" + cmbCategories.Text + "\\" + cmbSubCategories.Text + "\\");
            }

            while (File.Exists(filePath))
            {
                i++;
                filePath = @"e:\SantaBanta\" + cmbCategories.Text + "\\" + cmbSubCategories.Text + "\\" + i +
                           Path.GetExtension(downloadImageUrl);
            }
            return filePath;
        }

        //private static IEnumerable<DownloadList> FetchDownloadList(string retrievedHTML)
        //{
        //    List<string> strList = new List<string>();
        //    List<DownloadList> categoriesContents = new List<DownloadList>();
        //    DownloadList categoriesContent = new DownloadList();
        //    while (retrievedHTML.IndexOf("<div class=\"text\">") > -1)
        //    {
        //        categoriesContent = new DownloadList();
        //        retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<div class=\"text\">") + 1);
        //        retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<a href=\""));

        //        string partialHREF = RetrieveHREFFromAnchorTag(retrievedHTML);

        //        string closingAnchortag = "</a>";
        //        string startIndexValue = "\">";
        //        int startIndex = retrievedHTML.IndexOf(startIndexValue) + startIndexValue.ToCharArray().ToList().Count;
        //        int endIndex = retrievedHTML.IndexOf(closingAnchortag);
        //        categoriesContent.DownloadImageName = retrievedHTML.Substring(startIndex, endIndex - startIndex);
        //        categoriesContent.DownloadImageURL = Url.Substring(0, Url.IndexOf("/wallpapers")) + partialHREF;
        //        categoriesContents.Add(categoriesContent);
        //        strList.Add(retrievedHTML);
        //    }
        //    return categoriesContents;
        //}

        private List<DownloadList> FetchDownloadList(string retrievedHTML)
        {
            List<string> strList = new List<string>();
            List<DownloadList> categoriesContents = new List<DownloadList>();
            DownloadList categoriesContent = new DownloadList();
            while (retrievedHTML.IndexOf("<div class=\"text\">") > -1)
            //while (retrievedHTML.IndexOf("<div class=\"imgdiv1\">") > -1)
            {
                categoriesContent = new DownloadList();
                retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<div class=\"text\">") + 1);
                //retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<div class=\"imgdiv1\">") + 1);
                retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf("<a href=\""));

                string closingAnchortag = "</a>";
                string startIndexValue = "\">";
                int startIndex = retrievedHTML.IndexOf(startIndexValue) + startIndexValue.ToCharArray().ToList().Count;
                int endIndex = retrievedHTML.IndexOf(closingAnchortag);
                categoriesContent.DownloadImageName = retrievedHTML.Substring(startIndex, endIndex - startIndex);

                string imgDiv = "imgdiv1\">\r\n";
                retrievedHTML = retrievedHTML.Remove(0, retrievedHTML.IndexOf(imgDiv) + imgDiv.Length);
                string partialHREF = RetrieveHREFFromAnchorTag(retrievedHTML);


                categoriesContent.DownloadImageURL = Url.Substring(0, Url.IndexOf("/wallpapers")) + partialHREF;
                categoriesContents.Add(categoriesContent);
                strList.Add(retrievedHTML);
            }
            return categoriesContents;
        }
    }
}
