using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestyXamarin.Interfaces
{
    public interface ICrossPlatformDownloadManager
    {
        string DefaultPathToDownloadedFiles { get; }
        Task<string> DownloadFileAsync(string url, string fileName);
        bool DownloadedFileExists(string path);
    }
}
