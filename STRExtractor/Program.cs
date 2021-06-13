using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GaloreWare.IO;
using GaloreWare.Drawing.Bitmaps;

namespace STRExtractor
{
    class Program
    {
        const int SECTOR_SIZE = 614400;//300 * 2048;

        static void Main(string[] args)
        {
            string[] fnames = System.IO.Directory.GetFiles(".", "*.str");

            foreach (string f in fnames)
            {
                Extract(f);
            }
        }

        static void Extract(string filename)
        {
            string fname = System.IO.Path.GetFileNameWithoutExtension(filename);

            Binary file = new Binary(filename);
            long sectors = file.Length / SECTOR_SIZE;

            Console.WriteLine("{0} BYTES, {1} SECTORS", file.Length, sectors);

            A1R5G5B5Image screen = new A1R5G5B5Image(640, 480);
            BmpImage image = new BmpImage(640, 480);
            Binary sector;

            for (int i = 0; i < sectors; i++)
            {
                Console.WriteLine("Extracting SECTOR {0}", i);
                sector = new Binary(file[SECTOR_SIZE * i, SECTOR_SIZE]);
                screen.Read(sector);
                image.SetPixels(screen.Pixels);
                
                image.Save(string.Format(fname + "_{0:000}.bmp", i));
            }

            Console.WriteLine("Done!");
        }
    }
}
