using FiliaFramework.Graphics.Resources;
using FiliaFramework.Renderer.Common;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.OpenGL.Model
{
    public class GLMesh
    {
        private GL _gl;

        public GLVertexArrayObject<float, uint> VAO { get; private set; }
        public GLBufferObject<float> VBO { get; private set; }
        public GLBufferObject<uint> EBO { get; private set; }
        public float[] Vertices { get; private set; }
        public uint[] Indices { get; private set; }
        public RenderId Material { get; private set; }

        public GLMesh(Mesh mesh, RenderId material)
        {
            _gl = GLRenderer.Gl;

            Material = material;

            Vertices = BuildVertices(mesh.Vertices);
            Indices = BuildIndices(mesh.Indices);
            VBO = new GLBufferObject<float>(_gl, Vertices, BufferTargetARB.ArrayBuffer);
            EBO = new GLBufferObject<uint>(_gl, Indices, BufferTargetARB.ElementArrayBuffer);
            VAO = new GLVertexArrayObject<float, uint>(GLRenderer.Gl, VBO, EBO);
            VAO.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);//pos
            VAO.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);//uv
        }

        public void Dispose()
        {
            VBO.Dispose(); 
            EBO.Dispose();
            VAO.Dispose();
        }

        private float[] BuildVertices(List<Renderer.OpenGL.GLSceneVertex> vertexCollection)
        {
            var vertices = new List<float>();

            foreach (var vertex in vertexCollection)
            {
                vertices.Add(vertex.Position.X);
                vertices.Add(vertex.Position.Y);
                vertices.Add(vertex.Position.Z);
                vertices.Add(vertex.TexCoords.X);
                vertices.Add(vertex.TexCoords.Y);

                //Console.WriteLine($"Created vertex, UV: ({vertex.TexCoords.X}, {vertex.TexCoords.Y})");
            }

            return vertices.ToArray();
        }

        private uint[] BuildIndices(List<uint> indices)
        {
            return indices.ToArray();
        }
    }
}
