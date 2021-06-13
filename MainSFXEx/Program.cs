using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GaloreWare.IO;

namespace MainSFXEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Main.sfx Extractor";

            Binary sfx = new Binary("main.sfx");

            int offset = 0;
            int index = 1;

            do
            {
                offset = ExtractSFX(sfx, offset, string.Format("SFX{0:000}.wav", index));
                index++;
            } while (offset > 0);

        }//MAIN

        public static int ExtractSFX(Binary file, int offset, string filename)
        {
            if (offset >= file.Length)
                return -1;

            int size = file.ReadInt32(offset + 40);

            Console.WriteLine("EXTRACTING ENTRY {0}",offset);

            file.SaveOffset(filename, offset, 44 + size);

            return offset + 44 + size;
        }//ExctractSFX

    }
}
