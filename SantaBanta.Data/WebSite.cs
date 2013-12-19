using System.Collections.Generic;
using System.Linq;
using SantaBantaEntity;

namespace SantaBanta.Data
{
    public class WebSite
    {
        public FunctionCallStatus AddCategories(string categoryName, string categoryURL)
        {
            using (SantaBantaDataContext santaBantaDataContext = new SantaBantaDataContext())
            {
                List<Categories> categorieses = new List<Categories>();
                categorieses = WebSite.GetAllCategories();
                int categoriesExistanceCheck = categorieses.Where(i => i.CategoryName == categoryName).ToList().Count;


                if (categoriesExistanceCheck == 0)
                {
                    Category category = new Category();
                    category.CategoryName = categoryName;
                    category.CategoryURL = categoryURL;
                    santaBantaDataContext.Categories.InsertOnSubmit(category);
                    santaBantaDataContext.SubmitChanges();
                    if (category.Id > 0)
                    {
                        return FunctionCallStatus.Success;
                    }
                    else
                    {
                        return FunctionCallStatus.Error;
                    } 
                }
                else
                {
                    return FunctionCallStatus.DataAlreadyExists;
                }
            }
        }

        public static List<Categories> GetAllCategories()
        {
            using (SantaBantaDataContext santaBantaDataContext = new SantaBantaDataContext())
            {
                var categories = (from d in santaBantaDataContext.Categories
                                  select new Categories
                                  {
                                      CategoryName = d.CategoryName,
                                      CategoryURL = d.CategoryURL
                                  }
                                 ).ToList();
                return categories;
            }
        }

        public FunctionCallStatus AddSubCategoriesCategories(string categoryName, string categoryURL)
        {
            using (SantaBantaDataContext santaBantaDataContext = new SantaBantaDataContext())
            {
                List<SantaBantaEntity.CategoriesContent> categorieses = new List<SantaBantaEntity.CategoriesContent>();
                categorieses = WebSite.GetAllSubCategories();
                int categoriesExistanceCheck = categorieses.Where(i => i.URL == categoryURL).ToList().Count;


                if (categoriesExistanceCheck == 0)
                {
                    Subcategory category = new Subcategory();
                    category.SubcategoryName = categoryName;
                    category.SubcategoryURL = categoryURL;
                    santaBantaDataContext.Subcategories.InsertOnSubmit(category);
                    santaBantaDataContext.SubmitChanges();
                    if (category.Id > 0)
                    {
                        return FunctionCallStatus.Success;
                    }
                    else
                    {
                        return FunctionCallStatus.Error;
                    }
                }
                else
                {
                    return FunctionCallStatus.DataAlreadyExists;
                }
            }
        }

        public static List<SantaBantaEntity.CategoriesContent> GetAllSubCategories()
        {
            using (SantaBantaDataContext santaBantaDataContext = new SantaBantaDataContext())
            {
                var subCategories = (from d in santaBantaDataContext.Subcategories
                                     select new SantaBantaEntity.CategoriesContent
                                  {
                                      Name = d.SubcategoryName,
                                      URL = d.SubcategoryURL
                                  }
                                 ).ToList();
                return subCategories;
            }
        }

        //DownloadList

        public FunctionCallStatus AddDownloadLinks(string categoryName, string categoryURL)
        {
            using (SantaBantaDataContext santaBantaDataContext = new SantaBantaDataContext())
            {
                List<SantaBantaEntity.DownloadList> categorieses = new List<SantaBantaEntity.DownloadList>();
                categorieses = WebSite.GetAllDownloadLinks();
                int categoriesExistanceCheck = categorieses.Where(i => i.DownloadImageURL == categoryURL).ToList().Count;


                if (categoriesExistanceCheck == 0)
                {
                    DownloadInformation category = new DownloadInformation();
                    category.ImageName = categoryName;
                    category.ImageURL = categoryURL;
                    santaBantaDataContext.DownloadInformations.InsertOnSubmit(category);
                    santaBantaDataContext.SubmitChanges();
                    if (category.Id > 0)
                    {
                        return FunctionCallStatus.Success;
                    }
                    else
                    {
                        return FunctionCallStatus.Error;
                    }
                }
                else
                {
                    return FunctionCallStatus.DataAlreadyExists;
                }
            }
        }

        public static List<SantaBantaEntity.DownloadList> GetAllDownloadLinks()
        {
            using (SantaBantaDataContext santaBantaDataContext = new SantaBantaDataContext())
            {
                var subCategories = (from d in santaBantaDataContext.DownloadInformations
                                     select new DownloadList
                                     {
                                         DownloadImageName = d.ImageName,
                                         DownloadImageURL = d.ImageURL
                                     }
                                 ).ToList();
                return subCategories;
            }
        }

        public static List<SantaBantaEntity.DownloadList> GetAllDownloadedLinksByImageName(string imageName)
        {
            using (SantaBantaDataContext santaBantaDataContext = new SantaBantaDataContext())
            {
                var subCategories = (from d in santaBantaDataContext.DownloadInformations
                                     where d.ImageName == imageName
                                     select new DownloadList
                                     {
                                         DownloadImageName = d.ImageName,
                                         DownloadImageURL = d.ImageURL
                                     }
                                 ).ToList();
                return subCategories;
            }
        }
    }
}
