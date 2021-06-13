using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GaloreWare.IO;

namespace CDAudioEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CDAudio.wad Extractor";

            Binary file = new Binary("cdaudio.wad");

            //There should be only 130 entries.
            for (int i = 0; i < 130; i++)
            {
                ExtractTR3Audio(file, i);
            }

        }//Main

        public static void ExtractTR3Audio(Binary file, int index)
        {
            string filename = file.ReadASCIIString(index * 0x10C, 260);
            
            int size = file.ReadInt32((index * 0x10C) + 260);
            int offset = file.ReadInt32((index * 0x10C) + 264);

            if (offset == 0 || size == 0)
                return;

            if (string.IsNullOrEmpty(filename))
            {
                filename = string.Format("untitled{0:00}.wav", index);   
            }

            Console.WriteLine("EXTRACTING ENTRY {0} \"{1}\"", index, filename);
            file.SaveOffset(filename, offset, size);
        }//ExtractTR3Audio

        
    }
}
