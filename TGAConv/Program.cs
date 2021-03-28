using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGAConv
{
    class Program
    {
        static void Main(string[] args)
        {
            BMP image = new BMP("image.bmp");
            image.DrawDiagonal();
            image.Save("TEST.bmp");

            TGA tgaIm = new TGA("level1.tga");
            tgaIm.DrawDiagonal();
            tgaIm.Save("TEST.tga");
        }
    }
}
