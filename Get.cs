using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace MyDLL
{
    public class Get
    {
        /// <summary>
        /// Get Argb color for chosen pixel.
        /// 0,0 is top left of the primary monitor.
        /// Screens to the left of the primary screen is in negative.
        /// </summary>
        /// <param name="x">Left point</param>
        /// <param name="y">Top point</param>
        /// <returns>The 32-bit Argb value</returns>
        public int Pixel( int x, int y ) {
            var bmp = new Bitmap( 16, 16, PixelFormat.Format32bppArgb );
            var grp = Graphics.FromImage( bmp );
            // Start at XxY, with size of 1x1.
            grp.CopyFromScreen( new Point( x, y ), Point.Empty, new Size( 1, 1 ) );
            // Save the copy.
            grp.Save();
            // Return the color of the pixel in the 0x0 position of the saved image.
            return bmp.GetPixel( 0, 0 ).ToArgb();
        }
    }

    public class IniFile {
        public string Path;

        [DllImport( "kernel32" )]
        private static extern long WritePrivateProfileString( string section,
            string key, string val, string filePath );
        [DllImport( "kernel32" )]
        private static extern int GetPrivateProfileString( string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath );

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="iniPath">Full path to file.</PARAM>
        /// <exception cref="Path not found">Path not found.</exception>
        public IniFile( string iniPath ) {
            Path = iniPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="section">What section to write in.</PARAM>
        /// <PARAM name="key">What line you want to have changed.</PARAM>
        /// <PARAM name="value">The new value.</PARAM>
        public void Write( string section, string key, dynamic value ) {
            WritePrivateProfileString( section, key, value.ToString(), Path );
        }

        /// <summary>
        /// Read Data value From the Ini File
        /// </summary>
        /// <PARAM name="section">What section to read from.</PARAM>
        /// <PARAM name="key">The line to read from.</PARAM>
        /// <returns>The value of the given line.</returns>
        public string Read( string section, string key ) {
            var temp = new StringBuilder( 255 );
            GetPrivateProfileString( section, key, "", temp, 255, Path );
            return temp.ToString();
        }
    }
}
