using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GaloreWare.IO;

namespace TGAConv
{
    public class TGA
    {
        ByteWrite _data;
        short _w, _h, _bpp;

        public int Width { get { return _w; } }
        public int Height { get { return _h; } }

        public TGA(string fileName)
        {
            _data = new ByteWrite(fileName);

            _w = _data.ReadInt16(12);
            _h = _data.ReadInt16(14);
            
            _bpp =(short)(_data[16] / 8);
        }

        public TGA(int w, int h)
        {
            _w = (short)w;
            _h = (short)h;

            _data = new ByteWrite(18);

            _data[2] = 2;//Image Type Code (Binary 2, uncompress RGB).
            _data.WriteInt16(12,_w);
            _data.WriteInt16(14,_h);
            _data.WriteInt16(16, 24);//BPP

            _bpp = 3;
        }

        public byte[] GetPixel(int x, int y)
        {
            int offset = 18 + ((((_h - y) * _w) + x) * _bpp);
            return _data[offset,3];
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            int offset = 18 + ((((_h - y) * _w) + x) * _bpp);
            byte[] pixel = { b, g, r };

            _data.Write(offset, pixel, 3);
        }

        public void Save(string fileName)
        {
            _data.Save(fileName);
        }

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
    }
}
