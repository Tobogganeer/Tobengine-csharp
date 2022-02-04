#version 330 core

out vec4 FragColour;
in vec2 pass_uv;
//in vec4 vertex_colour;
in vec4 vertexPosition;
in vec3 pass_normal;

uniform sampler2D textureSampler;
uniform vec4 materialColour;

void main()
{
	//FragColour = vec4(pass_uv.xy, 0, 1.0);
    //FragColour = vec4(vertexPosition.xyz, 1.0);
    //FragColour = vec4(pass_normal.xyz, 1.0);
    FragColour = materialColour * texture(textureSampler, pass_uv);
}