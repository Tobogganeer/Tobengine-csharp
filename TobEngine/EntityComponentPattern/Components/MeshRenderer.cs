using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.GameLoop;
using TobEngine.Rendering.Graphics;
using TobEngine.Rendering.Materials;
using TobEngine.Rendering.RenderData;
using TobEngine.Util;
using static TobEngine.OpenGL.GL;

namespace TobEngine.EntityComponentPattern.Components
{
    class MeshRenderer : Component
    {
        public Material material;

        public MeshRenderer(Material material)
        {
            this.material = material;
        }

        public unsafe override void Render()
        {
            if (TryGetComponent(out MeshFilter filter))
            {
                Graphics.DrawMeshInstanced(filter.mesh, material, transform);
                //RenderModel model = meshFilter.model;
                //glBindVertexArray(model.vaoId);
                //glEnableVertexAttribArray(0);
                //glEnableVertexAttribArray(1);
                //glEnableVertexAttribArray(2);
                //
                //material.shader.LoadTransformationMatrix(transform);
                //
                ////glDrawArrays(GL_TRIANGLES, 0, model.numVertices);
                //fixed (uint* v = &meshFilter.mesh.tris[0]) // get pointer of first float in array
                //{
                //    glDrawElements(GL_TRIANGLES, model.numVertices, GL_UNSIGNED_INT, v);
                //}
                ////glDrawElements(GL_TRIANGLES, meshFilter.mesh.tris);
                //
                //glBindVertexArray(0);
            }
        }
    }
}
