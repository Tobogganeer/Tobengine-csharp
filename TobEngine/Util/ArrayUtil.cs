using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TobEngine.Util
{
    static class ArrayUtil
    {
        public static float[] AsFloatArray(this Vector3[] vectors)
        {
            float[] allFloats = new float[vectors.Length * 3];

            for (int i = 0; i < vectors.Length; i++)
            {
                allFloats[i * 3] = vectors[i].X;
                allFloats[i * 3 + 1] = vectors[i].Y;
                allFloats[i * 3 + 2] = vectors[i].Z;
            }

            return allFloats;
        }

        public static float[] AsFloatArray(this List<Vector3> vectors)
        {
            return vectors.ToArray().AsFloatArray();
        }

        public static float[] AsFloatArray(this Vector2[] vectors)
        {
            float[] allFloats = new float[vectors.Length * 2];

            for (int i = 0; i < vectors.Length; i++)
            {
                allFloats[i * 2] = vectors[i].X;
                allFloats[i * 2 + 1] = vectors[i].Y;
            }

            return allFloats;
        }

        public static float[] AsFloatArray(this List<Vector2> vectors)
        {
            return vectors.ToArray().AsFloatArray();
        }
    }
}
