using System;
using System.Collections.Generic;
using System.Text;
using GLFW;
using TobEngine.Rendering.Display;

namespace TobEngine.Application
{
    static class Input
    {
        public static event Action<Keys, InputState> InputCallback;
        public static event Action<double, double> MousePositionCallback;
        public static CursorMode currentCursorMode { get; private set; }
        private static List<Keys> currentKeysToCheck = new List<Keys>();

        public static float mouseAxisX { get; private set; }
        public static float mouseAxisY { get; private set; }

        private static float lastMouseX = 0;
        private static float lastMouseY = 0;

        private static Dictionary<Keys, KeyState> currentKeys = new Dictionary<Keys, KeyState>();

        public static void Initialize()
        {
            //Glfw.SetKeyCallback(DisplayManager.Window, new KeyCallback(InputKeyCallback));
            //Glfw.SetCursorPositionCallback(DisplayManager.Window, new MouseCallback(CursorPositionCallback));
        }

        private static void InputKeyCallback(Window window, Keys keys, int scancode, InputState state, ModifierKeys mods)
        {
            InputCallback?.Invoke(keys, state);
        }

        private static void CursorPositionCallback(Window window, double xPos, double yPos)
        {
            MousePositionCallback?.Invoke(xPos, yPos);
        }

        public static void SetCursorMode(CursorMode mode)
        {
            currentCursorMode = mode;
            if (mode == CursorMode.LOCKED)
            {
                Glfw.SetInputMode(DisplayManager.Window, InputMode.Cursor, (int)GLFW.CursorMode.Disabled);
                if (Glfw.RawMouseMotionSupported())
                    Glfw.SetInputMode(DisplayManager.Window, InputMode.RawMouseMotion, (int)GLFW.CursorMode.Disabled);
                else
                {
                    currentCursorMode = CursorMode.NORMAL;
                    Glfw.SetInputMode(DisplayManager.Window, InputMode.Cursor, (int)GLFW.CursorMode.Normal);
                }
            }
            else
                Glfw.SetInputMode(DisplayManager.Window, InputMode.Cursor, (int)GLFW.CursorMode.Normal);
        }

        public static void Update()
        {
            //FetchKeys();
            //Glfw.GetCursorPosition(DisplayManager.Window, out double x, out double y);
            //mouseAxisX = (float)x - lastMouseX;
            //mouseAxisY = (float)y - lastMouseY;
            //
            //lastMouseX = (float)x;
            //lastMouseY = (float)y;
        }

        public static void FetchKeys()
        {
            foreach (Keys key in currentKeysToCheck)
            {
                FetchKey(key);
            }
        }

        private static void FetchKey(Keys key)
        {
            Window window = DisplayManager.Window;

            InputState state = Glfw.GetKey(window, key);
            if (currentKeys.ContainsKey(key))
            {
                KeyState keyState = currentKeys[key];
                switch (state)
                {
                    case InputState.Release:
                        if (keyState.up)
                        {
                            keyState.up = false;
                            keyState.held = false;
                            keyState.down = false;
                            //currentKeys.Remove(key); // last known state was released and key was up
                        }
                        else
                        {
                            keyState.up = true; // last known state was released and key was not up
                            keyState.held = true;
                            keyState.down = false;
                        }
                        break;
                    case InputState.Press:
                        if (keyState.down) // last known state was pressed and key was down
                        {
                            keyState.down = false;
                            keyState.held = true;
                        }
                        else if (!keyState.held) // last known state was pressed and key was not down
                        {
                            keyState.down = true;
                            keyState.held = true;
                        }

                        keyState.up = false;
                        break;
                }

                currentKeys[key] = keyState;
            }
            else
            {
                //Console.WriteLine($"Adding key {key}, current state {Glfw.GetKey(window, key)}");
                //if (Glfw.GetKey(window, key) != InputState.Release)
                    currentKeys.Add(key, new KeyState(false, false, false));
            }
        }

        public static bool GetKeyDown(Keys key)
        {
            if (currentKeysToCheck.Contains(key))
            {
                if (currentKeys.TryGetValue(key, out KeyState state))
                {
                    return state.down;
                }
            }
            else
            {
                currentKeysToCheck.Add(key);
                //FetchKey(key);
                //
                //if (currentKeys.TryGetValue(key, out KeyState state))
                //{
                //    return state.down;
                //}
            }

            return false;
        }

        public static bool GetKey(Keys key)
        {
            //if (currentKeysToCheck.Contains(key))
            //{
            //    if (currentKeys.TryGetValue(key, out KeyState state))
            //    {
            //        return state.held;
            //    }
            //}
            //else
            //{
            //    currentKeysToCheck.Add(key);
            //    //FetchKey(key);
            //    //
            //    //if (currentKeys.TryGetValue(key, out KeyState state))
            //    //{
            //    //    return state.held;
            //    //}
            //}

            if (!currentKeysToCheck.Contains(key)) currentKeysToCheck.Add(key);

            return Glfw.GetKey(DisplayManager.Window, key) == InputState.Press;
        }


        public static bool GetKeyUp(Keys key)
        {
            if (currentKeysToCheck.Contains(key))
            {
                if (currentKeys.TryGetValue(key, out KeyState state))
                {
                    return state.up;
                }
            }
            else
            {
                currentKeysToCheck.Add(key);
            }

            return false;
        }
    }

    public enum CursorMode
    {
        NORMAL,
        LOCKED
    }

    public struct KeyState
    {
        public bool down;
        public bool held;
        public bool up;

        public KeyState(bool down, bool held, bool up)
        {
            this.down = down;
            this.held = held;
            this.up = up;
        }
    }
}
