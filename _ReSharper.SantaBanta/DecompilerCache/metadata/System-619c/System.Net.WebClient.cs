// Type: System.Net.WebClient
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Client\System.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.Cache;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
    [ComVisible(true)]
    public class WebClient : Component
    {
        public Encoding Encoding { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }

        public string BaseAddress { get; set; }

        public ICredentials Credentials { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public bool UseDefaultCredentials { get; set; }

        public WebHeaderCollection Headers { get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public NameValueCollection QueryString { get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public WebHeaderCollection ResponseHeaders { get; }
        public IWebProxy Proxy { get; set; }

        public RequestCachePolicy CachePolicy { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public bool IsBusy { get; }
        protected virtual WebRequest GetWebRequest(Uri address);
        protected virtual WebResponse GetWebResponse(WebRequest request);
        protected virtual WebResponse GetWebResponse(WebRequest request, IAsyncResult result);
        public byte[] DownloadData(string address);
        public byte[] DownloadData(Uri address);
        public void DownloadFile(string address, string fileName);
        public void DownloadFile(Uri address, string fileName);
        public Stream OpenRead(string address);
        public Stream OpenRead(Uri address);
        public Stream OpenWrite(string address);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Stream OpenWrite(Uri address);

        public Stream OpenWrite(string address, string method);
        public Stream OpenWrite(Uri address, string method);
        public byte[] UploadData(string address, byte[] data);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public byte[] UploadData(Uri address, byte[] data);

        public byte[] UploadData(string address, string method, byte[] data);
        public byte[] UploadData(Uri address, string method, byte[] data);
        public byte[] UploadFile(string address, string fileName);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public byte[] UploadFile(Uri address, string fileName);

        public byte[] UploadFile(string address, string method, string fileName);
        public byte[] UploadFile(Uri address, string method, string fileName);
        public byte[] UploadValues(string address, NameValueCollection data);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public byte[] UploadValues(Uri address, NameValueCollection data);

        public byte[] UploadValues(string address, string method, NameValueCollection data);
        public byte[] UploadValues(Uri address, string method, NameValueCollection data);
        public string UploadString(string address, string data);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public string UploadString(Uri address, string data);

        public string UploadString(string address, string method, string data);
        public string UploadString(Uri address, string method, string data);
        public string DownloadString(string address);
        public string DownloadString(Uri address);
        protected virtual void OnOpenReadCompleted(OpenReadCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void OpenReadAsync(Uri address);

        public void OpenReadAsync(Uri address, object userToken);
        protected virtual void OnOpenWriteCompleted(OpenWriteCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void OpenWriteAsync(Uri address);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void OpenWriteAsync(Uri address, string method);

        public void OpenWriteAsync(Uri address, string method, object userToken);
        protected virtual void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void DownloadStringAsync(Uri address);

        public void DownloadStringAsync(Uri address, object userToken);
        protected virtual void OnDownloadDataCompleted(DownloadDataCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void DownloadDataAsync(Uri address);

        public void DownloadDataAsync(Uri address, object userToken);
        protected virtual void OnDownloadFileCompleted(AsyncCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void DownloadFileAsync(Uri address, string fileName);

        public void DownloadFileAsync(Uri address, string fileName, object userToken);
        protected virtual void OnUploadStringCompleted(UploadStringCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void UploadStringAsync(Uri address, string data);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void UploadStringAsync(Uri address, string method, string data);

        public void UploadStringAsync(Uri address, string method, string data, object userToken);
        protected virtual void OnUploadDataCompleted(UploadDataCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void UploadDataAsync(Uri address, byte[] data);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void UploadDataAsync(Uri address, string method, byte[] data);

        public void UploadDataAsync(Uri address, string method, byte[] data, object userToken);
        protected virtual void OnUploadFileCompleted(UploadFileCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void UploadFileAsync(Uri address, string fileName);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void UploadFileAsync(Uri address, string method, string fileName);

        public void UploadFileAsync(Uri address, string method, string fileName, object userToken);
        protected virtual void OnUploadValuesCompleted(UploadValuesCompletedEventArgs e);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void UploadValuesAsync(Uri address, NameValueCollection data);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void UploadValuesAsync(Uri address, string method, NameValueCollection data);

        public void UploadValuesAsync(Uri address, string method, NameValueCollection data, object userToken);
        public void CancelAsync();
        protected virtual void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e);
        protected virtual void OnUploadProgressChanged(UploadProgressChangedEventArgs e);

        public event OpenReadCompletedEventHandler OpenReadCompleted;
        public event OpenWriteCompletedEventHandler OpenWriteCompleted;
        public event DownloadStringCompletedEventHandler DownloadStringCompleted;
        public event DownloadDataCompletedEventHandler DownloadDataCompleted;
        public event AsyncCompletedEventHandler DownloadFileCompleted;
        public event UploadStringCompletedEventHandler UploadStringCompleted;
        public event UploadDataCompletedEventHandler UploadDataCompleted;
        public event UploadFileCompletedEventHandler UploadFileCompleted;
        public event UploadValuesCompletedEventHandler UploadValuesCompleted;
        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
        public event UploadProgressChangedEventHandler UploadProgressChanged;
    }
}
