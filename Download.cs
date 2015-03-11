using System;
using System.Net;

namespace BingWallpaperBrowser
{
    class Download : IDisposable
    {
        public void DownloadImage( string url, string path ) {
            try {
                using ( WebClient wc = new WebClient() ) {
                    wc.DownloadFile( url, path );
                }
            } catch {
                throw;
            }
        }

        public void Dispose() { }
    }
}
