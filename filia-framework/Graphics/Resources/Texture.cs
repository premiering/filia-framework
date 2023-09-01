using Silk.NET.Assimp;
using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace FiliaFramework.Graphics.Resources
{
    public class Texture
    {
        public uint Width { get; set; }
        public uint Height { get; set; }
        public byte[] Data { get; set; }

        public Texture(uint width, uint height, byte[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }
    }
}
