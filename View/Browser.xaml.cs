using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BingWallpaperBrowser.View
{
    public partial class Browser : Window
    {
        BingWallpaper bw;
        int count = 0;

        public Browser() {
            InitializeComponent();
            Change();
        }

        private void Previous( object sender, RoutedEventArgs rea ) {
            count++;
            Change();
        }

        private void Next( object sender, RoutedEventArgs rea ) {
            count--;
            Change();
        }

        private void Change() {
            Bing b = new Bing();
            bw = b.GetWallpaper( count );

            if ( count > 25 ) {
                BPrev.IsEnabled = false;
            } else if ( count < 0 ) {
                BNext.IsEnabled = false;
            } else {
                BPrev.IsEnabled = true;
                BNext.IsEnabled = true;
            }

            Title.Text = bw.Date.ToShortDateString() + " - " + bw.Copyright;

            BitmapImage bi = new BitmapImage( new System.Uri( bw.Url ) );
            GBg.Background = new ImageBrush( bi );
        }

        private void Set( object sender, RoutedEventArgs rea ) {
            Wallpaper w = new Wallpaper();
            w.Set( (Wallpaper.WallpaperStyle)CBWallpaperStyle.SelectedIndex, bw.Url );
        }

        private void Download( object sender, RoutedEventArgs rea ) {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = bw.Url.Replace( "http://bing.com/az/hprichbg/rb/", "" );
            dlg.DefaultExt = ".jpg"; // Default file extension
            dlg.Filter = "JPEG image (.jpg)|*.jpg"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if ( result == true ) {
                Download d = new Download();
                d.DownloadImage( bw.Url, dlg.FileName );
            }
        }
    }
}
