using System;
using System.Collections.Generic;
using System.Text;

namespace TobEngine.Rendering.Shaders.SubShaders
{
    class BasicShader : Shader
    {
        public BasicShader(string vertexShaderName, string fragmentShaderName) : base(vertexShaderName, fragmentShaderName) { }

        public BasicShader(string combinedShaderName) : base(combinedShaderName) { }

        protected override void BindAttributes() { }
    }
}
