// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FiliaFramework.Graphics.Resources;
using FiliaFramework.Renderer;
using FiliaFramework.Renderer.Common;
using Silk.NET.Assimp;
using Silk.NET.OpenGL;
using Silk.NET.SDL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using AssimpMesh = Silk.NET.Assimp.Mesh;

namespace FiliaFramework.Graphics
{
    public class ModelLoader : IDisposable
    {
        private TextureLoader _textureLoader;
        private Assimp _assimp = Assimp.GetApi();
        private List<Model> _loadedModels = new List<Model>();

        public ModelLoader(TextureLoader textureLoader)
        {
            _textureLoader = textureLoader;
        }
        
        public unsafe Model LoadModel(string path)
        {
            //Directory = path;
            Model model = new Model(path);
            //string path = model.Directory;
            var scene = _assimp.ImportFile(path, (uint)PostProcessSteps.Triangulate);

            if (scene == null || scene->MFlags == Silk.NET.Assimp.Assimp.SceneFlagsIncomplete || scene->MRootNode == null)
            {
                var error = _assimp.GetErrorStringS();
                throw new Exception(error);
            }

            LoadMaterials(model, scene);
            ProcessNode(model, scene->MRootNode, scene);

            _loadedModels.Add(model);
            Console.WriteLine($"Loaded model {path} along with {scene->MNumMeshes} meshes and {scene->MNumMaterials} materials.");

            return model;
        }

        private unsafe void LoadMaterials(Model model, Scene* scene)
        {
            for (int i = 0; i < scene->MNumMaterials; i++)
            {
                Resources.Material newMat = new Resources.Material();
                Silk.NET.Assimp.Material* assimpMat = scene->MMaterials[i];
                
                AssimpString matName;
                _assimp.GetMaterialString(assimpMat, Assimp.MaterialNameBase, 0, 0, &matName);
                Console.WriteLine($"Loading material {matName}... Has {_assimp.GetMaterialTextureCount(assimpMat, TextureType.Emissive)} diffuse textures");

                //Load diffuse texture is available
                AssimpString texPath;
                _assimp.GetMaterialTexture(assimpMat, TextureType.Diffuse, 0, &texPath, null, null, null, null, null, null);
                if (texPath.Length > 0)
                {
                    try
                    {
                        newMat.DiffuseTexture = _textureLoader.LoadTexture(new FileInfo(model.Directory).Directory.FullName + "\\" + texPath);
                        Console.WriteLine("Loaded texture " + texPath.AsString + " for material " + matName);
                    } catch (Exception ex)
                    {
                        Console.WriteLine("!!! Failed to load texture " + texPath.AsString + " for material " + matName);
                    }
                }

                newMat.Name = matName;
                newMat.AmbientColor = GetMatColor(assimpMat, Assimp.MaterialColorAmbientBase);
                newMat.DiffuseColor = GetMatColor(assimpMat, Assimp.MaterialColorDiffuseBase);
                newMat.SpecularColor = GetMatColor(assimpMat, Assimp.MaterialColorSpecularBase);

                model.ModelMaterials.Add(newMat);
            }
        }

        private unsafe Vector4 GetMatColor(Silk.NET.Assimp.Material* mat, string key)
        {
            Vector4 col;
            _assimp.GetMaterialColor(mat, key, 0, 0, &col);
            return col;
        }

        private unsafe void ProcessNode(Model model, Node* node, Scene* scene)
        {
            for (var i = 0; i < node->MNumMeshes; i++)
            {
                var mesh = scene->MMeshes[node->MMeshes[i]];
                model.ModelMeshes.Add(ProcessMesh(model, mesh, scene));

            }

            for (var i = 0; i < node->MNumChildren; i++)
            {
                ProcessNode(model, node->MChildren[i], scene);
            }
        }

        private unsafe Graphics.Resources.Mesh ProcessMesh(Model model, AssimpMesh* mesh, Scene* scene)
        {
            // data to fill
            List<Renderer.OpenGL.GLSceneVertex> vertices = new List<Renderer.OpenGL.GLSceneVertex>();
            List<uint> indices = new List<uint>();
            //List<Texture> textures = new List<Texture>();

            // walk through each of the mesh's vertices
            for (uint i = 0; i < mesh->MNumVertices; i++)
            {
                Renderer.OpenGL.GLSceneVertex vertex = new Renderer.OpenGL.GLSceneVertex();
                vertex.BoneIds = new int[Renderer.OpenGL.GLSceneVertex.MAX_BONE_INFLUENCE];
                vertex.Weights = new float[Renderer.OpenGL.GLSceneVertex.MAX_BONE_INFLUENCE];

                vertex.Position = mesh->MVertices[i];

                // normals
                if (mesh->MNormals != null)
                    vertex.Normal = mesh->MNormals[i];
                // tangent
                if (mesh->MTangents != null)
                    vertex.Tangent = mesh->MTangents[i];
                // bitangent
                if (mesh->MBitangents != null)
                    vertex.Bitangent = mesh->MBitangents[i];

                // texture coordinatesd
                if (mesh->MTextureCoords[0] != null) // does the mesh contain texture coordinates?
                {
                    // a vertex can contain up to 8 different texture coordinates. We thus make the assumption that we won't 
                    // use models where a vertex can have multiple texture coordinates so we always take the first set (0).
                    Vector3 texcoord3 = mesh->MTextureCoords[0][i];
                    vertex.TexCoords = new Vector2(texcoord3.X, texcoord3.Y);
                }

                vertices.Add(vertex);
            }

            // now wak through each of the mesh's faces (a face is a mesh its triangle) and retrieve the corresponding vertex indices.
            for (uint i = 0; i < mesh->MNumFaces; i++)
            {
                Face face = mesh->MFaces[i];

                // retrieve all indices of the face and store them in the indices vector
                for (uint j = 0; j < face.MNumIndices; j++)
                    indices.Add(face.MIndices[j]);
            }

            // return a mesh object created from the extracted mesh data
            //var result = new Graphics.Resources.Mesh(mesh->MName, BuildVertices(vertices), BuildIndices(indices));//, textures);
            var result = new Graphics.Resources.Mesh(mesh->MName, vertices, indices);
            Resources.Material mat;
            if (mesh->MMaterialIndex >= 0)
                result.Material = model.ModelMaterials[(int) mesh->MMaterialIndex];
            else
                result.Material = new Graphics.Resources.Material();

            Console.WriteLine($"Loaded mesh {result.Name} for model {model.Directory}");

            return result;
        }

        /*private float[] BuildVertices(List<Renderer.OpenGL.GLSceneVertex> vertexCollection)
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
        }*/

        /*public unsafe void RenderModel(Renderer.OpenGL.GLShader shader, Texture defaultTexture)
        {
            foreach (var mesh in Meshes)
            {
                mesh.Bind();
                shader.Use();
                //DefaultTexture.Bind();
                //shader.SetUniform("uTexture0", DefaultTexture.Handle);
                var model = Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(0)) * Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(0));

                shader.SetUniform("uModel", model);
                mesh.Material.Use(shader, defaultTexture);
                
                
                _gl.PolygonMode(GLEnum.FrontAndBack, GLEnum.Fill);
                mesh.EBO.Bind();
                _gl.DrawElements(GLEnum.Triangles, (uint)mesh.Indices.Length, DrawElementsType.UnsignedInt, (void*)0);
                //mesh.VBO.Bind();
                //_gl.DrawArrays(GLEnum.Triangles, 0, (uint)mesh.Vertices.Length);
            }
        }*/

        public void Dispose()
        {
            /*foreach (var mesh in Meshes)
            {
                mesh.Dispose();
            }

            _texturesLoaded = null;*/
        }
    }
}
