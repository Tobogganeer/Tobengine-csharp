using System;
using System.Collections.Generic;
using System.Text;

namespace TobEngine.Rendering
{
    struct Colour
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Colour(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            a = 1;
        }

        public Colour(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}
