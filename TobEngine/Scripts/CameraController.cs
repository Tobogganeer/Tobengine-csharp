using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.EntityComponentPattern;
using GLFW;
using TobEngine.Application;
using System.Numerics;
using TobEngine.GameLoop;

namespace TobEngine.Scripts
{
    class CameraController : Component
    {
        float speed = 1;

        public override void Start()
        {
            //Input.InputCallback += GetInput;
            //Input.MousePositionCallback += Input_MousePositionCallback;
            Input.SetCursorMode(Application.CursorMode.LOCKED);
        }

        private void Input_MousePositionCallback(double arg1, double arg2)
        {
            transform.Rotate(new Vector3((float)(1 - arg2) / 500f, (float)(1 - arg1) / 500f, 0));
            //transform.rotation = new Vector3(transform.rotation.X, transform.rotation.Y, transform.rotation.Z);
        }

        private void GetInput(Keys key, InputState state)
        {
            if (key == Keys.Escape && state == InputState.Press)
            {

            }

            Vector3 direction = new Vector3();

            if (key == Keys.W)
                direction.Z -= 1;
            if (key == Keys.S)
                direction.Z += 1;

            if (key == Keys.A)
                direction.X -= 1;
            if (key == Keys.D)
                direction.X += 1;

            if (key == Keys.Space)
                direction.Y += 1;
            if (key == Keys.LeftShift)
                direction.Y -= 1;

            transform.Translate(direction * speed * Time.deltaTime);
        }

        public override void Update()
        {
            if (Input.GetKeyDown(Keys.Escape))
            {
                if (Input.currentCursorMode == Application.CursorMode.LOCKED)
                    Input.SetCursorMode(Application.CursorMode.NORMAL);
                else
                    Input.SetCursorMode(Application.CursorMode.LOCKED);
            }

            Vector3 direction = new Vector3();

            if (Input.GetKey(Keys.W))
                direction.Z -= 1;
            if (Input.GetKey(Keys.S))
                direction.Z += 1;

            if (Input.GetKey(Keys.A))
                direction.X -= 1;
            if (Input.GetKey(Keys.D))
                direction.X += 1;

            if (Input.GetKey(Keys.Space))
                direction.Y += 1;
            if (Input.GetKey(Keys.LeftShift))
                direction.Y -= 1;

            transform.Translate(direction * speed * Time.deltaTime);

            transform.Rotate(new Vector3(Input.mouseAxisY / 20f, Input.mouseAxisX / 20f, 0));
            //transform.rotation = new Vector3(transform.rotation.X, transform.rotation.Y, transform.rotation.Z);
            //Console.WriteLine($"X: {Input.mouseAxisX} - Y: {Input.mouseAxisY}");
        }
    }
}
