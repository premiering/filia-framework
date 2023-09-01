using FiliaFramework.Graphics;
using FiliaFramework.Renderer.Common;
using FiliaFramework.Renderer.OpenGL.Model;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.OpenGL
{
    public class GLSceneRenderer : SceneRenderer
    {
        private GL _gl = GLRenderer.Gl;
        private GLShader _sceneShader;

        private GLMaterialHandler _materialHandler;
        private GLMeshHandler _meshHandler;
        private GLTextureHandler _textureHandler;

        public GLSceneRenderer(GLMaterialHandler matHandler, GLMeshHandler meshHandler, GLTextureHandler textureHandler)
        {
            _sceneShader = new GLShader("assets/shaders/scene.vert", "assets/shaders/scene.frag");
            _materialHandler = matHandler;
            _meshHandler = meshHandler;
            _textureHandler = textureHandler;
        }

        public override void BeginSceneRender()
        {
            _sceneShader.Use();
        }

        public override void EndSceneRender()
        {

        }

        public override void SetActiveCamera(Camera cam)
        {
            _sceneShader.SetUniform("uView", Matrix4x4.CreateLookAt(cam.CameraPosition, cam.CameraPosition + cam.CameraFront, Camera.CameraUp));
            _sceneShader.SetUniform("uProjection", Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(cam.CameraFov), 16f / 9f, cam.NearPlaneDist, cam.FarPlaneDist));
        }

        public override unsafe void RenderMesh(RenderId renderId)
        {
            var mesh = _meshHandler.GetGLMesh(renderId);

            mesh.VAO.Bind();
            mesh.EBO.Bind();

            var modelUniform = Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(0)) * Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(0));
            _sceneShader.SetUniform("uModel", modelUniform);

            UseMaterial(mesh.Material);

            _gl.DrawElements(GLEnum.Triangles, (uint)mesh.Indices.Length, DrawElementsType.UnsignedInt, (void*)0);
        }

        public override void UseMaterial(RenderId renderId)
        {
            var glMat = _materialHandler.GetMaterial(renderId);
            //_sceneShader.SetUniform("material.ambientColor", glMat.Material.AmbientColor);
            _sceneShader.SetUniform("material.diffuseColor", glMat.Material.DiffuseColor);
            var diffTex = _textureHandler.GetTexture(glMat.DiffuseTexture);
            _sceneShader.SetUniform("material.diffuseTexture", diffTex.Handle);
            //_sceneShader.SetUniform("material.specularColor", glMat.Material.SpecularColor);
            //shader.SetUniform("material.ambientColor", mat.AmbientColor);
            //shader.SetUniform("material.diffuseColor", mat.DiffuseColor);
            //shader.SetUniform("material.specularColor", mat.SpecularColor);
            //ApplyDiffuseTexture(shader, mat.DiffuseTexture);
        }
    }
}
