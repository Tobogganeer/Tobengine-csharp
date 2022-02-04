using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TobEngine.Rendering;
using TobEngine.Rendering.Display;
using TobEngine.Rendering.Shaders;
using TobEngine.Util;
using static TobEngine.OpenGL.GL;

namespace TobEngine.EntityComponentPattern.Components
{
    class Camera : Component
    {
		private static float VERTICAL_FOV = 75;
		private static float NEAR_PLANE = 0.1f;
		private static float FAR_PLANE = 1000f;

		private Matrix4x4 projectionMatrix;
		public static Colour BACKGROUND_COLOUR = new Colour(109f / 255f, 195f / 255f, 227f / 255f);
		//public static Colour BACKGROUND_COLOUR = new Colour(0, 0, 0);


		public Camera(Shader shader)
		{
			glEnable(GL_CULL_FACE);
			glCullFace(GL_BACK);
			CreateProjectionMatrix();
			shader.Start();
			shader.LoadProjectionMatrix(projectionMatrix);
			shader.Stop();
		}

		private void CreateProjectionMatrix()
		{
			float aspectRatio = DisplayManager.WindowSize.X / DisplayManager.WindowSize.Y;
			//float VERTICAL_FOV = 2 * MathF.Atan(MathF.Tan(FOV / 2) * aspectRatio);

			projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(MathUtil.GetRadians(VERTICAL_FOV), aspectRatio, NEAR_PLANE, FAR_PLANE);
		}
	}
}
