using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.GameLoop;
using static TobEngine.OpenGL.GL;
using GLFW;
using TobEngine.Rendering.Display;
using TobEngine.Rendering.Shaders;
using System.IO;
using System.Diagnostics;
using TobEngine.EntityComponentPattern.Entities;
using TobEngine.EntityComponentPattern.Components;
using System.Numerics;
using TobEngine.Rendering.Meshing.Meshes.Primitives;
using TobEngine.Rendering.Materials;
using TobEngine.Rendering.Graphics;
using TobEngine.Rendering.Meshing;
using TobEngine.Loading.Parsers;
using TobEngine.Scripts;
using TobEngine.Rendering.Textures;
using TobEngine.Application;
using TobEngine.Rendering.Shaders.SubShaders;
using TobEngine.Rendering.Meshing.OBJ;
using TobEngine.Util;

namespace TobEngine.Testing
{
    class TestGame : Game
    {
        Shader shader;
        GameObject camera;
        GameObject testObject;

        Material material;

        public TestGame(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }

        protected override void Initialize() { }

        protected unsafe override void LoadContent()
        {
            shader = new BasicShader("unlit.vert.glsl", "unlit.frag.glsl");
            //shader = new Shader("Shaders", "VertexShader.hlsl", "FragmentShader.hlsl");
            shader.Load();

            /*

            vao = glGenVertexArray();
            vbo = glGenBuffer();
            
            //binds vertex array to created array (vao) id
            glBindVertexArray(vao);
            
            //binds array buffer (vbo) to created vbo id
            glBindBuffer(GL_ARRAY_BUFFER, vbo);
            
            float[] vertices =
            {
                //x, y, r, g, b
                -0.5f, 0.5f, 1f, 0f, 0f, // top left
                0.5f, 0.5f, 0f, 1f, 0f,// top right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left
            
                0.5f, 0.5f, 0f, 1f, 0f,// top right
                0.5f, -0.5f, 0f, 1f, 1f, // bottom right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left
            };
            
            fixed (float* v = &vertices[0]) // get pointer of first float in array
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
            }
            
            //list 0 in vao, 2 items per vertex (positions), are float, not normalized, bytes between each data set = 5 floats, offset from start to first set of data = 0
            glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);
            
            //list 1 in vao, 3 items per vertex (colour), are float, not normalized, bytes between each data set = 5 floats, offset from start to first set of data = 2 floats
            glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);
            
            //clears current buffers (removes vao and vbo id)
            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            */

            Debug.WriteLine("Loaded content...");
        }

        protected override void Start()
        {
            material = new Material(shader);
            material.mainTexture = Texture2D.LoadTexture("Content/Textures/TR10 Texture.png");
            material.colour = new Rendering.Colour(1, 1, 1, 1);
            
            camera = Instantiate(Transform.Identity, "Camera");
            Camera camComp = new Camera(shader);
            mainCamera = camComp;
            camera.AddComponent(camComp);
            camera.AddComponent(new CameraController());
            camera.transform.position = new Vector3(0, 0, 0.5f);

            testObject = Instantiate(Transform.Identity, "Cube");

            Mesh mesh = OBJParser.ParseOBJFile("TR10").ConvertToMesh();
            //Mesh mesh = OBJParser.ParseOBJFile("Cube").ConvertToMesh();

            Console.WriteLine("Mesh size (bytes): " + mesh.GetByteSize());
            testObject.AddComponent(new MeshFilter(mesh));
            testObject.AddComponent(new MeshRenderer(material));
            testObject.transform.rotation = Transform.FromEuler(new Vector3(10, 10, 10));

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Start();
            }

            Debug.WriteLine("Started...");
        }

        protected override void PreUpdate()
        {
            foreach (GameObject ob in gameObjectsToBeInstantiated)
            {
                ob.Start();
                gameObjects.Add(ob);
            }

            gameObjectsToBeInstantiated.Clear();
        }

        protected override void Update() 
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update();
            }

            //testObject.transform.Translate(new Vector3(MathF.Sin(Time.totalElapsedSeconds) * Time.deltaTime, 0, 0));
            //float rotationAmount = MathF.Sin(Time.totalElapsedSeconds) * Time.deltaTime * 20;
            float rotationAmount = Time.deltaTime * 20;
            //testObject.transform.Rotate(new Vector3(rotationAmount, 0, rotationAmount));

            //if (Input.GetKey(Keys.W)) testObject.transform.Translate(new Vector3(0, 0, Time.deltaTime * 5));
            //if (Input.GetKey(Keys.S)) testObject.transform.Translate(new Vector3(0, 0, -Time.deltaTime * 5));
        }

        protected override void LateUpdate() { }

        protected override void Render()
        {
            Graphics.Prepare(shader);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Render();
            }

            Graphics.Render(mainCamera, shader);

            Graphics.PostRender();
            
            Glfw.SwapBuffers(DisplayManager.Window);
        }

        protected override void OnGameQuit()
        {
            shader.CleanUp();
        }
    }
}
