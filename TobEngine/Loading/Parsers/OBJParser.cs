using System;
using System.IO;
using System.Collections.Generic;
using TobEngine.Rendering.Meshing;
using TobEngine.Rendering.Meshing.OBJ;
using System.Numerics;

namespace TobEngine.Loading.Parsers
{
    static class OBJParser
    {
		//private static uint currentIndex;
		//private static Dictionary<VertexKey, uint> currentVertices;

		/// <summary>
		/// NOTE: The .obj file must be located in the Content/Models directory
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static OBJMesh ParseOBJFile(string fileName)
        {
            try
            {
				//filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
				if (!fileName.EndsWith(".obj")) fileName += ".obj";
                return ParseOBJFileContents(File.ReadAllText(Path.Combine("Content", "Models", fileName)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading OBJ file! " + ex);
            }

            return null;
        }

		private static OBJMesh ParseOBJFileContents(string fileContents)
		{
			using StringReader reader = new StringReader(fileContents);
			string line;

			List<Vector3> vertices = new List<Vector3>();
			List<Vector2> uvs = new List<Vector2>();
			List<Vector3> normals = new List<Vector3>();
			List<OBJFace> faces = new List<OBJFace>();
			//List<uint> indices = new List<uint>();

			//currentIndex = 0;
			//currentVertices = new Dictionary<VertexKey, uint>();

			try
			{
				while (true)
				{
					line = reader.ReadLine();
					string[] currentLine = line.Split(" ");
					if (line.StartsWith("v "))
					{
						Vector3 vertex = new Vector3(float.Parse(currentLine[1]),
								float.Parse(currentLine[2]), float.Parse(currentLine[3]));
						vertices.Add(vertex);

					}
					else if (line.StartsWith("vt "))
					{
						Vector2 uv = new Vector2(float.Parse(currentLine[1]),
								float.Parse(currentLine[2]));
						uvs.Add(uv);

					}
					else if (line.StartsWith("vn "))
					{
						Vector3 normal = new Vector3(float.Parse(currentLine[1]),
								float.Parse(currentLine[2]), float.Parse(currentLine[3]));
						normals.Add(normal);

					}
					else if (line.StartsWith("f "))
					{
						break;
					}
				}

				while (line != null)
				{
					if (!line.StartsWith("f "))
					{
						line = reader.ReadLine();
						continue;
					}
					string[] currentLine = line.Split(" ");
					string[] vertex1 = currentLine[1].Split("/");
					string[] vertex2 = currentLine[2].Split("/");
					string[] vertex3 = currentLine[3].Split("/");

					faces.Add(new OBJFace(ParseVertex(vertex1), ParseVertex(vertex2), ParseVertex(vertex3)));



					//indices.Add(ProcessVertex(vertex1, vertices, normals, uvs));
					//indices.Add(ProcessVertex(vertex2, vertices, normals, uvs));
					//indices.Add(ProcessVertex(vertex3, vertices, normals, uvs));
					line = reader.ReadLine();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Error parsing OBJ file! " + e);
			}

			Console.WriteLine($"Parsed OBJ: Vertices: {vertices.Count} UVs: {uvs.Count} Normals: {normals} Faces: {faces.Count}");
			return new OBJMesh(vertices.ToArray(), uvs.ToArray(), normals.ToArray(), faces.ToArray());

			//List<float> verticesList = new List<float>();
			//List<float> uvsList = new List<float>();
			//List<float> normalsList = new List<float>();
			//
			//Dictionary<uint, VertexKey> vertexKeys = new Dictionary<uint, VertexKey>();
			//
             //foreach (VertexKey key in currentVertices.Keys)
             //{
			 //	if (!vertexKeys.ContainsKey(currentVertices[key]))
             //    {
			 //		vertexKeys.Add(currentVertices[key], key);
             //    }
             //    else
             //    {
			 //		Console.WriteLine($"Vertex Key {key} points to more than one index!");
             //    }
             //}
			//
			//List<VertexKey> alreadyProcessed = new List<VertexKey>();
            //foreach (uint index in indices)
            //{
            //	ProcessVertexKey(vertexKeys[index], verticesList, uvsList, normalsList, alreadyProcessed);
			//	//Console.WriteLine($"INDEX: {index} - {vertexKeys[index]}");
            //}
			//
			////Console.WriteLine($"VERT: {verticesList.Count / 3}");
			//
			//return new Mesh(verticesList.ToArray(), normalsList.ToArray(), uvsList.ToArray(), indices.ToArray());
		}

		private static OBJVertex ParseVertex(string[] vertexString)
        {
			return new OBJVertex(uint.Parse(vertexString[0]), uint.Parse(vertexString[1]), uint.Parse(vertexString[2]));
		}

		//private static VertexKey GetVertexKey(string[] vertex, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uvs)
        //{
		//	Vector3 currentVertex = vertices[int.Parse(vertex[0]) - 1];
		//	Vector3 currentNormal = normals[int.Parse(vertex[2]) - 1];
		//	Vector2 currentUV = uvs[int.Parse(vertex[1]) - 1];
		//
		//	return new VertexKey(currentVertex, currentNormal, currentUV);
		//}

		//private static uint ProcessVertex(string[] vertex, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uvs)
		//{
		//	VertexKey key = GetVertexKey(vertex, vertices, normals, uvs);
		//	uint index;
		//
		//	if (!currentVertices.ContainsKey(key))
        //    {
		//		currentVertices.Add(key, currentIndex);
		//		index = currentIndex;
		//		currentIndex++;
        //    }
        //    else
        //    {
		//		index = currentVertices[key];
        //    }
		//
		//	return index;
		//}

		//private static void ProcessVertexKey(VertexKey key, List<float> verticesList, List<float> uvsList, List<float> normalsList, List<VertexKey> alreadyProcessed)
        //{
		//	if (alreadyProcessed.Contains(key))
        //    {
		//		return;
        //    }
		//
		//	alreadyProcessed.Add(key);
		//
		//	verticesList.Add(key.vertex.X);
		//	verticesList.Add(key.vertex.Y);
		//	verticesList.Add(key.vertex.Z);
		//
		//	normalsList.Add(key.normal.X);
		//	normalsList.Add(key.normal.Y);
		//	normalsList.Add(key.normal.Z);
		//
		//	uvsList.Add(key.uv.X);
		//	uvsList.Add(key.uv.Y);
		//}

		//private struct VertexKey
        //{
		//	public Vector3 vertex;
		//	public Vector3 normal;
		//	public Vector2 uv;
		//
        //    public VertexKey(Vector3 vertex, Vector3 normal, Vector2 uv)
        //    {
        //        this.vertex = vertex;
        //        this.normal = normal;
        //        this.uv = uv;
        //    }
		//
        //    public override string ToString()
        //    {
		//		return $"POS: {vertex} - NORMAL: {normal} - UV: {uv}";
        //    }
        //}

	}
}
