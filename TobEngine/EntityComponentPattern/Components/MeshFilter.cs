using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.Loading.Loaders;
using TobEngine.Rendering.Meshing;
using TobEngine.Rendering.RenderData;

namespace TobEngine.EntityComponentPattern.Components
{
    class MeshFilter : Component
    {
        public Mesh mesh;

        public MeshFilter(Mesh mesh)
        {
            this.mesh = mesh;
        }
    }
}
