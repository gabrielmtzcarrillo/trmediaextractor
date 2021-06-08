using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using GaloreWare.Drawing.Bitmaps;

namespace TGAConv
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(".", "*.bmp");
            
            BmpImage inImage;
            TgaImage outImage;

            foreach (string file in files)
            {
                Console.WriteLine("Converting {0}",file);
                inImage = new BmpImage(file);
                outImage = TgaImage.FromBMP(inImage);
                outImage.Save(file + ".TGA");
            }

            Console.WriteLine("Done!");
        }
    }
}
