using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.Rendering.Meshing;
using TobEngine.Rendering.RenderData;
using static TobEngine.OpenGL.GL;

namespace TobEngine.Loading.Loaders
{
    class ModelLoader
    {
        private static List<uint> vbos = new List<uint>();
        private static List<uint> vaos = new List<uint>();

        public static RenderModel LoadToVAO(Mesh mesh)
        {
            uint vaoId = CreateVAO();
            BindIndicesBuffer(mesh.tris);

            StoreDataInAttributeList(0, mesh.vertices, 3);
            StoreDataInAttributeList(1, mesh.normals, 3);
            StoreDataInAttributeList(2, mesh.uvs, 2);
            UnbindVAO();
            Console.WriteLine("Loaded model to VAO.");
            return new RenderModel(vaoId, mesh.tris);
        }

        private static uint CreateVAO()
        {
            uint vaoId = glGenVertexArray();
            vaos.Add(vaoId);
            glBindVertexArray(vaoId);
            return vaoId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribList">The list to store the data in.</param>
        /// <param name="data">The data to store.</param>
        /// <param name="dataSetSize">Number of floats in each data set (Position = 3 floats, UV = 2 floats, etc)</param>
        private static unsafe void StoreDataInAttributeList(uint attribList, float[] data, int dataSetSize)
        {
            uint vboId = glGenBuffer();
            vbos.Add(vboId);
            glBindBuffer(GL_ARRAY_BUFFER, vboId);

            //Console.WriteLine($"Writing data {DebugGetStringFromArray(data)} to list {attribList}, each set having {dataSetSize} floats.");

            fixed (float* v = &data[0]) // get pointer of first float in array
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * data.Length, v, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(attribList, dataSetSize, GL_FLOAT, false, 0, (void*)0);
            glBindBuffer(GL_ARRAY_BUFFER, 0);
        }

        private static void UnbindVAO()
        {
            glBindVertexArray(0);
        }

        private unsafe static void BindIndicesBuffer(uint[] indices)
        {
            uint vboId = glGenBuffer();
            vbos.Add(vboId);
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, vboId);

            //Console.WriteLine($"Bound indices buffer {DebugGetStringFromArray(indices)}.");

            fixed (uint* v = &indices[0]) // get pointer of first index in array
            {
                glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(uint) * indices.Length, v, GL_STATIC_DRAW);
            }

            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        public static void CleanUp()
        {
            foreach (uint vao in vaos)
            {
                glDeleteVertexArray(vao);
            }
            foreach (uint vbo in vaos)
            {
                glDeleteBuffer(vbo);
            }
        }

        private static string DebugGetStringFromArray(float[] data)
        {
            string dataString = "DATA: ";

            foreach (float ob in data)
            {
                dataString = $"{dataString} {ob}, ";
            }

            return dataString;
        }

        private static string DebugGetStringFromArray(uint[] data)
        {
            string dataString = "DATA: ";

            foreach (uint ob in data)
            {
                dataString = $"{dataString} {ob}, ";
            }

            return dataString;
        }
    }
}
