using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GaloreWare.IO;

namespace TGAConv
{
    public class BMP
    {
        ByteWrite _data;
        int _w, _h, _row_size;

        public int Width { get { return _w; } }
        public int Height { get { return _h; } }

        public BMP(string fileName)
        {
            _data = new ByteWrite(fileName);

            _w = _data.ReadInt32(0x12);
            _h = _data.ReadInt32(0x16);

            _row_size = (int)Math.Ceiling(((float)_w * 3.0f) / 8.0) * 8;
        }

        public BMP(int w, int h)
        {
            _w = w;
            _h = h;
            _row_size = (int)Math.Ceiling(((float)w * 3.0f) / 8.0) * 8;

            int dataSize = _row_size * h;

            _data = new ByteWrite(56);

            //BMP HEADER (DOESN'T CHANGE AT ALL)
            _data.WriteASCII(0x0, "BM", 2);//BITMAP TYPE - BM = WINDOWS BITMAP
            _data.WriteInt32(0xA, 54);//DATA OFFSET

            //DIB HEADER (DOESN'T CHANGE AT ALL)
            _data.WriteInt32(0xE, 40);//HEADER SIZE
            _data.WriteInt16(0x1A, 1);//PLANES, ALWAYS 1
            _data.WriteInt16(0x1C, 24);//BITS PER PIXEL

            //VARIABLE VALUES
            _data.WriteInt32(0x12, w);//SET WIDTH
            _data.WriteInt32(0x16, -h);//SET HEIGHT

            _data.WriteInt32(0x2, 54 + dataSize);//DATA OFFSET
            _data.WriteInt32(0x22, dataSize);//DATA SIZE

            _data.Fill(54, dataSize);//SET DEFULT VALUES (BLACK)
        }//NEW

        public void DrawDiagonal()
        {
            for (int y = 0; y < _h; y++)
            {
                for (int x = 0; x < _w; x++)
                {
                    if (x == y)
                        SetPixel(x, y, 255, 0, 0);
                }
            }
        }

        public void Save(string fileName)
        {
            _data.Save(fileName);
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            int offset = 54 + 3 * x + _row_size * (_h - y);
            byte[] pixel = { g, b, r };
            _data.Write(offset, pixel, 3);
        }//SETPIXEL

    }
}
