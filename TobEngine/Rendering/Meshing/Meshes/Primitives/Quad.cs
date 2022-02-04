using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TobEngine.Rendering.Meshing.Meshes.Primitives
{
    class Quad : MeshSource
    {
        public override Mesh mesh => new Mesh(
            new float[]
            {
                -0.5f, 0.5f, 0f, // top left
                -0.5f, -0.5f, 0f, // bottom left
                0.5f, -0.5f, 0f, // bottom right
                0.5f, 0.5f, 0f // top right
            },
            new float[]
            {
                0, 0, 0,
                0, 0, 0,
                0, 0, 0,
                0, 0, 0
            },
            new float[]
            {
                0,1,
                0,0,
                1,0,
                1,1
            },
            new uint[]
            {
                0,1,3,
                3,1,2,
            }
        );
    }
}
