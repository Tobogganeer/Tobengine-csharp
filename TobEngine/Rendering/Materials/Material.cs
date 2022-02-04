using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TobEngine.Rendering.Shaders;
using TobEngine.Rendering.Textures;

namespace TobEngine.Rendering.Materials
{
    class Material
    {
        public Shader shader;
        public Colour colour;
        public Texture2D mainTexture;

        public Vector2 textureScale;
        public Vector2 textureOffset;

        public Material(Shader shader)
        {
            this.shader = shader;
        }
    }
}
