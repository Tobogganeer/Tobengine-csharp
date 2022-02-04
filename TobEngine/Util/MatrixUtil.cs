using System;
using System.Numerics;
using TobEngine.EntityComponentPattern.Components;

namespace TobEngine.Util
{
    static class MatrixUtil
    {
        public static float[] GetMatrix4x4Values(Matrix4x4 m)
        {
            return new float[]
            {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            };
        }

		public static Matrix4x4 CreateTransformationMatrix(Vector3 translation, Quaternion rotation, Vector3 scale)
		{
			Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(translation);
			Matrix4x4 rotationMatrix = Matrix4x4.CreateFromQuaternion(rotation);
			//Matrix4x4 rotationMatrix = Matrix4x4.CreateFromYawPitchRoll(rotation.Y.ToRadians(), rotation.X.ToRadians(), rotation.Z.ToRadians());
			Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(scale);

			return translationMatrix * rotationMatrix * scaleMatrix;
		}

		public static Matrix4x4 CreateTransformationMatrix(Transform transform)
		{
			return CreateTransformationMatrix(transform.position, transform.rotation, transform.scale);
		}

		public static Matrix4x4 CreateViewMatrix(Vector3 position, Quaternion rotation)
		{
			//Matrix4x4 rotationMatrix = Matrix4x4.CreateFromYawPitchRoll(rotation.Y.ToRadians(), rotation.X.ToRadians(), 0);
			Matrix4x4 rotationMatrix = Matrix4x4.CreateFromQuaternion(rotation);

			Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(-position);

			return translationMatrix * rotationMatrix;
		}
	}
}
