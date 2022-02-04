using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using static TobEngine.OpenGL.GL;

namespace TobEngine.Rendering.Textures
{
    class Texture2D
    {
        public readonly uint ID;
        public readonly int Width;
        public readonly int Height;

        public Texture2D(uint id, int width, int height)
        {
            this.ID = id;
            this.Width = width;
            this.Height = height;
        }

        public unsafe static Texture2D LoadTexture(string filePath, FilterMode filterMode = FilterMode.Linear)
        {
            using Bitmap map = new Bitmap(filePath);

            uint textureID = glGenTexture();
            BitmapData data = map.LockBits(new Rectangle(0, 0, map.Width, map.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            glBindTexture(GL_TEXTURE_2D, textureID);

            glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, map.Width, map.Height, 0, GL_BGRA, GL_UNSIGNED_BYTE, data.Scan0);

            map.UnlockBits(data);

            int filter = (int)filterMode;
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, filter);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, filter);

            return new Texture2D(textureID, map.Width, map.Height);
        }

        public enum FilterMode
        {
            Nearest = GL_NEAREST,
            Linear = GL_LINEAR
        }
    }
}
