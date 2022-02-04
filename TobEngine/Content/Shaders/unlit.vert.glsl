#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 normal;
layout (location = 2) in vec2 uv;

out vec2 pass_uv;
out vec4 vertexPosition;
out vec3 pass_normal;

uniform mat4 transformationMatrix;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;

//layout (location = 0) in vec2 position;
//layout (location = 1) in vec3 colour;

//out vec4 vertex_colour;

void main()
{
    vec4 worldPosition = transformationMatrix * vec4(position.xyz, 1.0);
    gl_Position = projectionMatrix * viewMatrix * worldPosition;
    vertexPosition = vec4(position.xyz, 1.0);
    //gl_Position = vec4(position.xyz, 1.0);
    pass_uv = uv;

    //gl_Position = vec4(position.xy, 0, 1.0);
    //vertex_colour = vec4(colour.xyz, 1.0);
    pass_normal = normal;
}