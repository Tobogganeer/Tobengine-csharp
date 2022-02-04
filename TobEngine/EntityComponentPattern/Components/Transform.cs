using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TobEngine.EntityComponentPattern.Components
{
    class Transform : Component
    {
        public Transform()
        {
            position = Vector3.Zero;
            rotation = Quaternion.Identity;
            scale = Vector3.One;
        }

        public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public static Transform Identity
        {
            get
            {
                return new Transform(Vector3.Zero, Quaternion.Identity, Vector3.One);
            }
        }

        public Transform parent { get; set; }
        public Vector3 position { get; set; } = Vector3.Zero;
        public Vector3 localPosition { get; set; } = Vector3.Zero;
        public Quaternion rotation { get; set; } = Quaternion.Identity;
        public Quaternion localRotation { get; set; } = Quaternion.Identity;
        public Vector3 scale { get; set; } = Vector3.One;

        public Vector3 forward { get => Vector3.Transform(Vector3.UnitZ, rotation); }
        public Vector3 right { get => Vector3.Normalize(Vector3.Cross(forward, Vector3.UnitY)); }

        public void Rotate(Vector3 rotation)
        {
            Quaternion rot = Quaternion.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);
            this.rotation += rot;
        }

        public void Translate(Vector3 translation)
        {
            position += translation;
        }


        public static Quaternion FromEuler(Vector3 eulerAngles)
        {
            return Quaternion.CreateFromYawPitchRoll(eulerAngles.Y, eulerAngles.X, eulerAngles.Z);
        }
    }
}
