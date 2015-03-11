using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BingWallpaperBrowser
{
    class Program : System.Windows.Application
    {
        [STAThread]
        static void Main( string[] args ) {
            Program p = new Program();

            // if any args were given
            if ( args.Length > 0 ) {
                // ...do something
            } else {
                // show main window
                p.MainWindow = new View.Browser();
                p.MainWindow.Show();

                p.Run();
            }

            p.Shutdown();
        }
    }
}
