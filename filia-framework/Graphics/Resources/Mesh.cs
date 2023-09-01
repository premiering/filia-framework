// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using FiliaFramework.Renderer.OpenGL;
using Silk.NET.OpenGL;

namespace FiliaFramework.Graphics.Resources
{
    public class Mesh
    {
        /*public Mesh(GL gl, String name, float[] vertices, uint[] indices, List<Texture> textures)
        {
            GL = gl;
            Name = name;
            Vertices = vertices;
            Indices = indices;
            Textures = textures;
            SetupMesh();
        }*/

        public string Name { get; set; }
        public Material Material { get; set; } = new Material();//TODO: some way to use pointers? idk
                                                                //public float[] Vertices { get; private set; }
                                                                //public uint[] Indices { get; private set; }
        public List<GLSceneVertex> Vertices { get; set; } = new List<GLSceneVertex>();
        public List<uint> Indices { get; set; } = new List<uint>();

        public Mesh(string name, List<GLSceneVertex> vertices, List<uint> indices)
        {
            Name = name;
            Vertices = vertices;
            Indices = indices;
        }
        //public IReadOnlyList<Texture> Textures { get; private set; }
        /*public GLVertexArrayObject<float, uint> VAO { get; set; }
        public GLBufferObject<float> VBO { get; set; }
        public GLBufferObject<uint> EBO { get; set; }
        public GL GL { get; }

        public unsafe void SetupMesh()
        {
            EBO = new GLBufferObject<uint>(GL, Indices, BufferTargetARB.ElementArrayBuffer);
            VBO = new GLBufferObject<float>(GL, Vertices, BufferTargetARB.ArrayBuffer);
            VAO = new GLVertexArrayObject<float, uint>(GL, VBO, EBO);
            VAO.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            VAO.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);
        }

        public void Bind()
        {
            VAO.Bind();
        }

        public void Dispose()
        {
            Textures = null;
            VAO.Dispose();
            VBO.Dispose();
            EBO.Dispose();
        }*/

        public override string ToString()
        {
            return $"Mesh{{{Name}, {Material.ToString()}}}";
        }
    }
}
