// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Numerics;

namespace FiliaFramework.Renderer.OpenGL
{
    public struct GLSceneVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 Tangent;
        public Vector2 TexCoords;
        public Vector3 Bitangent;

        public const int MAX_BONE_INFLUENCE = 128;
        public int[] BoneIds;
        public float[] Weights;
    }
}
