using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TobEngine.Loading.Loaders;
using TobEngine.Rendering.RenderData;

namespace TobEngine.Rendering.Meshing
{
    class Mesh
    {
        public readonly float[] vertices;
        public readonly float[] normals;
        public readonly float[] uvs;
        public readonly uint[] tris;

        public Mesh(float[] vertices, float[] normals, float[] uvs, uint[] tris)
        {
            this.vertices = vertices;
            this.normals = normals;
            this.uvs = uvs;
            this.tris = tris;
        }

        public MeshKey GetKey()
        {
            return new MeshKey(vertices, normals, uvs, tris);
        }

        public RenderModel LoadToVAO()
        {
            return ModelLoader.LoadToVAO(this);
        }

        public uint GetByteSize()
        {
            return (uint)(sizeof(float) * vertices.Length + sizeof(float) * normals.Length + sizeof(float) * uvs.Length + sizeof(uint) * tris.Length);
        }
    }

    struct MeshKey
    {
        public float[] vertices;
        public float[] normals;
        public float[] uvs;
        public uint[] tris;

        public MeshKey(float[] vertices, float[] normals, float[] uvs, uint[] tris)
        {
            this.vertices = vertices;
            this.normals = normals;
            this.uvs = uvs;
            this.tris = tris;
        }
    }
}

