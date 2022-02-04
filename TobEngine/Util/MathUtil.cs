using System;
using System.Collections.Generic;
using System.Text;

namespace TobEngine.Util
{
    static class MathUtil
    {
        public static float ToRadians(this float angle)
        {
            return (MathF.PI / 180f) * angle;
        }

        public static float GetRadians(float angle)
        {
            return MathF.PI / 180f * angle;
        }
    }
}
