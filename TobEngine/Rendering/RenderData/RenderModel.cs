using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.Loading.Loaders;

namespace TobEngine.Rendering.RenderData
{
    class RenderModel
    {
        public uint vaoId { get; private set; }
        public int numVertices { get; private set; }
        public uint[] tris { get; private set; }

        public RenderModel(uint vaoId, uint[] tris)
        {
            this.vaoId = vaoId;
            this.numVertices = tris.Length;
            this.tris = tris;
        }
    }
}
