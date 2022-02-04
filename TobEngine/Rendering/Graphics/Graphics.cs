using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TobEngine.EntityComponentPattern.Components;
using TobEngine.Rendering.Materials;
using TobEngine.Rendering.Meshing;
using TobEngine.Rendering.Meshing.Meshes.Primitives;
using TobEngine.Rendering.RenderData;
using static TobEngine.OpenGL.GL;

namespace TobEngine.Rendering.Graphics
{
    static class Graphics
    {
        private static Dictionary<MeshKey, RenderModelSet> currentRenderSet = new Dictionary<MeshKey, RenderModelSet>();

        public static void Prepare(Shaders.Shader shader)
        {
            Colour bgCol = Camera.BACKGROUND_COLOUR;
            glEnable(GL_DEPTH_TEST);
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            glClearColor(bgCol.r, bgCol.g, bgCol.b, 0);

            shader.Start();
        }

        public static void DrawMeshInstanced(Mesh mesh, Material material, Transform transform)
        {
            MeshKey key = mesh.GetKey();
            
            if (!currentRenderSet.ContainsKey(key))
                currentRenderSet.Add(key, new RenderModelSet(mesh.LoadToVAO(), key, new List<Transform>(), new List<Material>()));

            currentRenderSet[key].transforms.Add(transform);
            currentRenderSet[key].materials.Add(material);
        }

        public unsafe static void Render(Camera camera, Shaders.Shader shader) 
        {
            shader.LoadViewMatrix(camera.transform);

            foreach (RenderModelSet set in currentRenderSet.Values)
            {
                glBindVertexArray(set.renderModel.vaoId);
                glEnableVertexAttribArray(0);
                glEnableVertexAttribArray(1);
                glEnableVertexAttribArray(2);

                //set.renderModel.BindIndicesBuffer();
            
                if (set.transforms.Count != set.materials.Count)
                {
                    Console.WriteLine("Tried to render models but number of materials does not match number of transforms!");
                    break;
                }

                if (set.transforms.Count > 0)
                {
                    for (int i = 0; i < set.transforms.Count; i++)
                    {
                        shader.LoadTransformationMatrix(set.transforms[i]);
                        shader.LoadMaterialColour(set.materials[i].colour);

                        glActiveTexture(GL_TEXTURE0);
                        glBindTexture(GL_TEXTURE_2D, set.materials[i].mainTexture.ID);

                        //glDrawElements(GL_TRIANGLES, set.renderModel.numVertices, GL_UNSIGNED_INT, (void*)0);
                        glDrawElements(GL_TRIANGLES, set.renderModel.tris);
                    }
                }
            }
            
            glBindVertexArray(0);

            glDisableVertexAttribArray(0);
            glDisableVertexAttribArray(1);
            glDisableVertexAttribArray(2);

            shader.Stop();
        }

        public static void PostRender()
        {
            foreach (RenderModelSet set in currentRenderSet.Values)
            {
                set.transforms.Clear();
                set.materials.Clear();
            }
        }
    }

    class RenderModelSet
    {
        public RenderModel renderModel;
        public MeshKey key;
        public List<Transform> transforms;
        public List<Material> materials;

        public RenderModelSet(RenderModel renderModel, MeshKey key, List<Transform> transforms, List<Material> materials)
        {
            this.renderModel = renderModel;
            this.key = key;
            this.transforms = transforms;
            this.materials = materials;
        }
    }
}
