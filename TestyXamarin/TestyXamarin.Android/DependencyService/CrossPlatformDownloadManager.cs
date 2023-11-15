using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestyXamarin.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TestyXamarin.Droid.DependencyService.CrossPlatformDownloadManager))]
namespace TestyXamarin.Droid.DependencyService
{
    public class CrossPlatformDownloadManager : ICrossPlatformDownloadManager
    {
        public string getPathToDownload(string FileName)
        {
            return Path.Combine(Android.App.Application.Context.GetExternalFilesDir(null).Path, FileName);
        }
        public async Task<string> DownloadFileAsync(string url, string FileName)
        {
            try
            {
                string path = getPathToDownload(FileName);
                /*
                WebClient webClent = new WebClient();
                await Task.Run(() => webClent.DownloadFile(url, path + ".test")); //test download file
                await Task.Run(() => webClent.DownloadFile(url, path));
                */
                //https only
                HttpClient _httpClient = new HttpClient();
                byte[] fileBytes = await _httpClient.GetByteArrayAsync(url);
                File.WriteAllBytes(path, fileBytes);
                //
                return path;
            }       
            catch (Exception)
            {
                return null;
            }
        }
        public string DefaultPathToDownloadedFiles { get { return Android.App.Application.Context.GetExternalFilesDir(null).Path; } }
        public bool DownloadedFileExists(string path)
        {
            if (System.IO.File.Exists(path))
                return true;
            return false;
        }
    }
}