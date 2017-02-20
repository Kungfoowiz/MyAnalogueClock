using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Drawing.Imaging; 

namespace MyClock
{
    public class Background
    {

        // File name for background image. 
        protected const String FileName = @"..\..\images\Omega.jpg";

        public Background(
            ref Bitmap BitmapObject, 
            out Int32 WidthPx, 
            out Int32 HeightPx){

            //Image ImageFile = Image.FromFile(
            //    @"C:\Users\DaviesE\Documents\Visual Studio 2015\Projects\MyAnalogueClock\MyAnalogueClock\images\Omega.jpg"); 

            Image ImageFile = Image.FromFile(FileName);

            WidthPx = ImageFile.Width;
            HeightPx = ImageFile.Height;

            BitmapObject = (Bitmap)ImageFile; 
                

        }


    }



    


    


}

