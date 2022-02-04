using TobEngine.Rendering.Display;
using GLFW;
using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.EntityComponentPattern.Entities;
using TobEngine.Loading.Loaders;
using TobEngine.EntityComponentPattern.Components;
using System.Numerics;
using TobEngine.EntityComponentPattern;
using TobEngine.Application;

namespace TobEngine.GameLoop
{
    abstract class Game
    {
        protected List<GameObject> gameObjects = new List<GameObject>();
        protected List<GameObject> gameObjectsToBeInstantiated = new List<GameObject>();
        protected static Camera mainCamera;
        protected int InitialWindowWidth { get; set; } = 1280;
        protected int InitialWindowHeight { get; set; } = 720;
        protected string InitialWindowTitle { get; set; } = "Tobo Game Engine";

        protected Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTitle = initialWindowTitle;
        }

        public void Run()
        {
            Initialize();

            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);
            Input.Initialize();

            LoadContent();

            Start();

            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                Time.deltaTime = (float)Glfw.Time - Time.totalElapsedSeconds;
                Time.totalElapsedSeconds = (float)Glfw.Time;

                Glfw.PollEvents();

                Input.Update();

                PreUpdate();
                Update();
                LateUpdate();

                Render();
            }

            OnGameQuit();

            ModelLoader.CleanUp();
            DisplayManager.CloseWindow();
        }

        protected abstract void Initialize();
        protected abstract void LoadContent();

        protected abstract void Start();
        protected abstract void PreUpdate();
        protected abstract void Update();
        protected abstract void LateUpdate();
        protected abstract void Render();

        protected abstract void OnGameQuit();

        public static Camera GetMainCamera()
        {
            return mainCamera;
        }

        public GameObject Instantiate(Transform transform, string name = "New GameObject")
        {
            GameObject newOb = new GameObject(transform, name);
            gameObjectsToBeInstantiated.Add(newOb);
            return newOb;
        }
    }
}
