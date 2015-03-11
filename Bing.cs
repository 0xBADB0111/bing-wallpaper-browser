using System;
using System.Net;
using System.Xml;

namespace BingWallpaperBrowser
{
    struct BingWallpaper
    {
        public int Id;
        public string Url;
        public string Copyright;
        public string CopyrightLink;
        public DateTime Date;
    }

    class Bing
    {
        /// <summary>
        /// Gets BingWallpaper object
        /// </summary>
        /// <param name="idx">ID of image {-1,17}</param>
        /// <param name="mkt">Location</param>
        /// <returns>BingWallpaper object</returns>
        public BingWallpaper GetWallpaper( int idx, string mkt = "en_US" ) {
            string url = "http://www.bing.com/HPImageArchive.aspx?format=xml&n=1"
                + "&idx=" + idx
                + "&mkt" + mkt;

            return GetBingWallpaper( GetXmlDocument( url ), idx );
        }

        private XmlDocument GetXmlDocument( string url ) {
            XmlDocument xml = new XmlDocument();
            string xmlstr = "";

            try {
                using ( WebClient wc = new WebClient() ) {
                    xmlstr = wc.DownloadString( url );
                }
                xml.LoadXml( xmlstr );
            } catch (WebException) {
                System.Windows.MessageBox.Show( "Cannot connect to the internet. Application will now quit." );
                System.Environment.Exit(1);
            } catch {
                throw;
            } 

            return xml;
        }

        private BingWallpaper GetBingWallpaper( XmlDocument xml, int idx ) {
            BingWallpaper bw = new BingWallpaper();

            string strdate = xml.SelectSingleNode( "//images/image/startdate" ).InnerText;
            System.Diagnostics.Debug.WriteLine( strdate );

            // populate bingwallpaper
            bw.Id = idx;
            bw.Url = "http://bing.com" + xml.SelectSingleNode( "//images/image/urlBase" ).InnerText + "_1920x1080.jpg";
            bw.Copyright = xml.SelectSingleNode( "//images/image/copyright" ).InnerText;
            bw.CopyrightLink = xml.SelectSingleNode( "//images/image/copyrightlink" ).InnerText;
            bw.Date = new DateTime( 
                        Convert.ToInt32( strdate.Remove( 4, 4 ) ),
                        Convert.ToInt32( strdate.Remove( 6, 2 ).Remove( 0, 4 ) ),
                        Convert.ToInt32( strdate.Remove( 0, 6 ) )
                        );

            return bw;
        }
    }
}
