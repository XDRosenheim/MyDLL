using System.Drawing;
using System.Drawing.Imaging;

namespace MyDLL
{
    public class MyDLL
    {
        /// <summary>
        /// Get Argb color for chosen pixel
        /// </summary>
        /// <param name="x">Left point</param>
        /// <param name="y">Top point</param>
        /// <returns></returns>
        public int GetPixel( int x, int y ) {
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
}
