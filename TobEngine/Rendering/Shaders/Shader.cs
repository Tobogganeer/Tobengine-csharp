using System;
using System.IO;
using System.Diagnostics;
using static TobEngine.OpenGL.GL;
using System.Numerics;
using TobEngine.Util;
using TobEngine.EntityComponentPattern.Components;

namespace TobEngine.Rendering.Shaders
{
    abstract class Shader
    {
        string vertexShader;
        string fragmentShader;

        public uint programID { get; private set; }

        private static float[] matrixBuffer = new float[16];

        protected int location_transformationMatrix;
        protected int location_projectionMatrix;
        protected int location_viewMatrix;
        protected int location_materialColour;

        //public Shader(string vertexShaderContents, string fragmentShaderContents)
        //{
        //    vertexShader = vertexShaderContents;
        //    fragmentShader = fragmentShaderContents;
        //}

        /// <summary>
        /// NOTE: The shaders must be located in the Content/Shaders directory
        /// </summary>
        /// <param name="vertexShaderName"></param>
        /// <param name="fragmentShaderName"></param>
        public Shader(string vertexShaderName, string fragmentShaderName)
        {
            try
            {
                vertexShader = File.ReadAllText(Path.Combine("Content", "Shaders", vertexShaderName));
                fragmentShader = File.ReadAllText(Path.Combine("Content", "Shaders", fragmentShaderName));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error reading shaders! " + ex);
            }
        }

        /// <summary>
        /// Fills both the vertex and fragment shaders using a combined shader.
        /// To make a combined shader, simply include both shaders in one file.
        /// Mark the start of the vertex shader with #SHADER VERTEX
        /// Mark the start of the vertex shader with #SHADER FRAGMENT
        /// NOTE: The shader must be located in the Content/Shaders directory
        /// </summary>
        /// <param name="combinedShaderPath"></param>
        public Shader(string combinedShaderName)
        {
            using StringReader reader = new StringReader(File.ReadAllText(Path.Combine("Content", "Shaders", combinedShaderName)));
            StringWriter[] shaders = new StringWriter[2];
            shaders[0] = new StringWriter();
            shaders[1] = new StringWriter();
            ShaderType type = ShaderType.NONE;
            string line = reader.ReadLine();

            while (line != null)
            {
                if (line.Contains("#SHADER"))
                {
                    if (line.Contains("VERTEX"))
                        type = ShaderType.VERTEX;
                    else if (line.Contains("FRAGMENT"))
                        type = ShaderType.FRAGMENT;
                }
                else
                {
                    if (type != ShaderType.NONE)
                    {
                        shaders[(int)type].WriteLine(line);
                    }
                }

                line = reader.ReadLine();
            }

            vertexShader = shaders[0].ToString();
            fragmentShader = shaders[1].ToString();

            shaders[0].Dispose();
            shaders[1].Dispose();
        }

        public void Load()
        {
            uint vShader = glCreateShader(GL_VERTEX_SHADER); // create vertex shader
            glShaderSource(vShader, vertexShader); // set source code
            glCompileShader(vShader); // compile

            int[] status = glGetShaderiv(vShader, GL_COMPILE_STATUS, 1);

            if (status[0] == 0)
            {
                string error = glGetShaderInfoLog(vShader);
                Debug.WriteLine("Error compiling vertex shader! " + error);
            }

            uint fShader = glCreateShader(GL_FRAGMENT_SHADER); // create fragment shader
            glShaderSource(fShader, fragmentShader); // set source code
            glCompileShader(fShader); // compile

            status = glGetShaderiv(fShader, GL_COMPILE_STATUS, 1);

            if (status[0] == 0)
            {
                string error = glGetShaderInfoLog(fShader);
                Debug.WriteLine("Error compiling fragment shader! " + error);
            }

            programID = glCreateProgram(); // create actual shader
            glAttachShader(programID, vShader); // attach vertex shader
            glAttachShader(programID, fShader); // attach fragment shader

            BindAttributes();

            glLinkProgram(programID); // link and finalize shader
            glValidateProgram(programID);

            glDetachShader(programID, vShader);
            glDetachShader(programID, fShader);
            glDeleteShader(vShader);
            glDeleteShader(fShader);

            GetAllUniformLocations();

            Debug.WriteLine("Loaded shader.");
        }

        public void Start()
        {
            glUseProgram(programID);
        }

        public void Stop()
        {
            glUseProgram(0);
        }

        public void CleanUp()
        {
            Stop();
            glDeleteShader(programID);
        }


        protected virtual void GetAllUniformLocations()
        {
            location_transformationMatrix = GetUniformLocation("transformationMatrix");
            location_projectionMatrix = GetUniformLocation("projectionMatrix");
            location_viewMatrix = GetUniformLocation("viewMatrix");
            location_materialColour = GetUniformLocation("materialColour");
        }

        protected int GetUniformLocation(string uniformName)
        {
            return glGetUniformLocation(programID, uniformName);
        }

        protected abstract void BindAttributes();

        protected void BindAttribute(uint attribute, string variableName)
        {
            glBindAttribLocation(programID, attribute, variableName);
        }

        protected void LoadFloat(int location, float value)
        {
            glUniform1f(location, value);
        }

        protected void LoadVector3(int location, Vector3 value)
        {
            glUniform3f(location, value.X, value.Y, value.Z);
        }

        protected void LoadVector4(int location, Vector4 value)
        {
            glUniform4f(location, value.X, value.Y, value.Z, value.W);
        }

        protected void LoadBoolean(int location, bool value)
        {
            float toLoad = value ? 1 : 0;
            glUniform1f(location, toLoad);
        }

        protected void LoadMatrix(int location, Matrix4x4 matrix)
        {
            matrixBuffer = MatrixUtil.GetMatrix4x4Values(matrix);
            glUniformMatrix4fv(location, 1, false, matrixBuffer);
        }


        public void LoadTransformationMatrix(Matrix4x4 matrix)
        {
            LoadMatrix(location_transformationMatrix, matrix);
        }

        public void LoadProjectionMatrix(Matrix4x4 projection)
        {
            LoadMatrix(location_projectionMatrix, projection);
        }

        public void LoadViewMatrix(Matrix4x4 viewMatrix)
        {
            LoadMatrix(location_viewMatrix, viewMatrix);
        }

        public void LoadMaterialColour(Colour colour)
        {
            LoadVector4(location_materialColour, new System.Numerics.Vector4(colour.r, colour.g, colour.b, colour.a));
        }


        public void LoadTransformationMatrix(Transform transform)
        {
            Matrix4x4 transformationMatrix = MatrixUtil.CreateTransformationMatrix(transform);
            LoadMatrix(location_transformationMatrix, transformationMatrix);
        }

        public void LoadViewMatrix(Transform cameraTransform)
        {
            Matrix4x4 matrix = MatrixUtil.CreateViewMatrix(cameraTransform.position, cameraTransform.rotation);
            LoadMatrix(location_viewMatrix, matrix);
        }
    }

    public enum ShaderType
    {
        NONE = -1,
        VERTEX = 0,
        FRAGMENT = 1
    }
}
