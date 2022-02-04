using System;
using System.Collections.Generic;
using System.Text;

namespace TobEngine.Rendering.Meshing
{
    abstract class MeshSource
    {
        public abstract Mesh mesh { get; }
    }
}
