using FiliaFramework.Graphics.Resources;
using FiliaFramework.Nodes;
using FiliaFramework.Renderer.Common;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.OpenGL.Model
{
    public class GLMeshHandler : MeshHandler
    {
        private MaterialHandler _materialHandler;
        private Dictionary<RenderId, GLMesh> _uploadedMeshes = new Dictionary<RenderId, GLMesh>();

        public GLMeshHandler(MaterialHandler materialHandler)
        {
            _materialHandler = materialHandler;
        }

        public override RenderId UploadMesh(Mesh mesh)
        {
            var rid = RenderId.CreateRID();

            var mat = _materialHandler.UploadMaterial(mesh.Material);

            _uploadedMeshes.Add(rid, new GLMesh(mesh, mat));

            return rid;
        }

        public override void DisposeMesh(RenderId rid)
        {
            var mesh = _uploadedMeshes[rid];
            mesh.Dispose();

            _uploadedMeshes.Remove(rid);
            RenderId.DisposeRID(rid);
        }

        public override void UpdateMaterial(RenderId rid, Material material)
        {
            var mesh = _uploadedMeshes[rid];
            _materialHandler.UpdateMaterial(mesh.Material, material);
        }

        public GLMesh GetGLMesh(RenderId rid)
        {
            var mesh = _uploadedMeshes[rid];
            return mesh;
        }
    }

    /*private GL _glCtx;
    private GLShader _sceneShader;

    private GLMaterialManager _materialManager;

    public GLMeshHandler(GL gl, GLShader sceneShader)
    {
        _glCtx = gl;
        _sceneShader = sceneShader;

        _materialManager = new GLMaterialManager();
    }

    public void LoadMesh3D(Mesh3D mesh3d)
    {
        if (mesh3d.Mesh == null) return;

        var mesh = mesh3d.Mesh;
        var ebo = new GLBufferObject<uint>(_glCtx, mesh.Indices, BufferTargetARB.ElementArrayBuffer);
        var vbo = new GLBufferObject<float>(_glCtx, mesh.Vertices, BufferTargetARB.ArrayBuffer);
        var vao = new GLVertexArrayObject<float, uint>(_glCtx, vbo, ebo);
        vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

        SetVao(mesh3d, vao);
        SetVbo(mesh3d, vbo);
        SetEbo(mesh3d, ebo);
    }

    public unsafe void RenderMesh3D(Mesh3D mesh3d, Graphics.Texture defaultTexture)
    {
        var mesh = mesh3d.Mesh;
        var vao = GetVao(mesh3d);
        var vbo = GetVao(mesh3d);
        var ebo = GetEbo(mesh3d);

        //mesh.Bind();
        _sceneShader.Use();
        //DefaultTexture.Bind();
        //shader.SetUniform("uTexture0", DefaultTexture.Handle);
        var modelUniform = Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(0)) * Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(0));

        _sceneShader.SetUniform("uModel", modelUniform);
        _materialManager.UseMaterial(mesh.Material, _sceneShader, defaultTexture);
        //Console.WriteLine("Drawing mesh " + mesh.Name + " using material " + mesh.Material);

        vao.Bind();
        ebo.Bind();
        _glCtx.DrawElements(GLEnum.Triangles, (uint)mesh.Indices.Length, DrawElementsType.UnsignedInt, (void*)0);
        //mesh.VBO.Bind();
        //_gl.DrawArrays(GLEnum.Triangles, 0, (uint)mesh.Vertices.Length);
    }

    protected void SetVao(Mesh3D mesh3d, GLVertexArrayObject<float, uint> vao)
    {
        mesh3d.RendererData["vao"] = vao;
    }

    protected void SetVbo(Mesh3D mesh3d, GLBufferObject<float> vbo)
    {
        mesh3d.RendererData["vbo"] = vbo;
    }

    protected void SetEbo(Mesh3D mesh3d, GLBufferObject<uint> ebo)
    {
        mesh3d.RendererData["ebo"] = ebo;
    }

    protected GLVertexArrayObject<float, uint> GetVao(Mesh3D mesh3d)
    {
        return (GLVertexArrayObject<float, uint>) mesh3d.RendererData["vao"];
    }

    protected GLBufferObject<float> GetVbo(Mesh3D mesh3d)
    {
        return (GLBufferObject<float>) mesh3d.RendererData["vbo"];
    }

    protected GLBufferObject<uint> GetEbo(Mesh3D mesh3d)
    {
        return (GLBufferObject<uint>)mesh3d.RendererData["ebo"];
    }
}
internal class GLMaterialManager
{
    public void UseMaterial(Material mat, GLShader shader, Graphics.Texture defaultTexture)
    {
        //shader.SetUniform("material.ambientColor", mat.AmbientColor);
        //shader.SetUniform("material.diffuseColor", mat.DiffuseColor);
        //shader.SetUniform("material.specularColor", mat.SpecularColor);
        ApplyDiffuseTexture(shader, mat.DiffuseTexture);
    }

    protected void ApplyDiffuseTexture(GLShader shader, Graphics.Texture texture)
    {
        if (texture == null) return;

        Console.WriteLine("Applying diffuse texture: " + texture.Path);
        texture.Bind();
        shader.SetUniform("material.diffuseTexture", texture.Handle);
    }
}*/
}
