using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace BingWallpaperBrowser
{
    class Wallpaper : IDisposable
    {
        public enum WallpaperStyle
        {
            Tile,
            Center,
            Stretch,
            Fit,
            Fill
        }

        [DllImport( "user32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
        [return: MarshalAs( UnmanagedType.Bool )]
        private static extern bool SystemParametersInfo( uint uiAction, uint uiParam,
            string pvParam, uint fWinIni );

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        public void Set( WallpaperStyle style, string url ) {
            Download d = new Download();
            string path = Path.Combine( Path.GetTempPath(), "bing_wallpaper.jpg");
            d.DownloadImage( url, path );

            RegistryKey key = Registry.CurrentUser.OpenSubKey( @"Control Panel\Desktop", true );

            switch ( style ) {
                case WallpaperStyle.Tile:
                    key.SetValue( @"WallpaperStyle", "0" );
                    key.SetValue( @"TileWallpaper", "1" );
                    break;
                case WallpaperStyle.Center:
                    key.SetValue( @"WallpaperStyle", "0" );
                    key.SetValue( @"TileWallpaper", "0" );
                    break;
                case WallpaperStyle.Stretch:
                    key.SetValue( @"WallpaperStyle", "2" );
                    key.SetValue( @"TileWallpaper", "0" );
                    break;
                case WallpaperStyle.Fit: // (Windows 7 and later)
                    key.SetValue( @"WallpaperStyle", "6" );
                    key.SetValue( @"TileWallpaper", "0" );
                    break;
                case WallpaperStyle.Fill: // (Windows 7 and later)
                    key.SetValue( @"WallpaperStyle", "10" );
                    key.SetValue( @"TileWallpaper", "0" );
                    break;
            }

            key.Close();


            SystemParametersInfo( SPI_SETDESKWALLPAPER,
                0,
                path,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE
                );
        }

        public void Dispose() { }
    }
}
