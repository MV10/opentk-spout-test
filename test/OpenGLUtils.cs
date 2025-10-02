using eyecandy;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;

namespace test;

internal static class OpenGLUtils
{
    // just a quad from two triangles that cover the whole display area
    internal static readonly float[] Vertices =
    {
        // position          texture coords
         1.0f,  1.0f, 0.0f,   1.0f, 1.0f,     // top right
         1.0f, -1.0f, 0.0f,   1.0f, 0.0f,     // bottom right
        -1.0f, -1.0f, 0.0f,   0.0f, 0.0f,     // bottom left
        -1.0f,  1.0f, 0.0f,   0.0f, 1.0f      // top left
    };

    internal static readonly uint[] Indices =
    {
        0, 1, 3,
        1, 2, 3
    };

    internal static int ElementBufferObject;
    internal static int VertexBufferObject;
    internal static int VertexArrayObject;

    internal static Action SetTextureUniformCallback;

    internal static Stopwatch time = new();

    internal static void Initialize(Shader shader)
    {
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, OpenGLUtils.Vertices.Length * sizeof(float), OpenGLUtils.Vertices, BufferUsageHint.StaticDraw);

        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, OpenGLUtils.Indices.Length * sizeof(uint), OpenGLUtils.Indices, BufferUsageHint.StaticDraw);

        shader.Use();

        var locationVertices = shader.GetAttribLocation("vertices");
        GL.EnableVertexAttribArray(locationVertices);
        GL.VertexAttribPointer(locationVertices, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        //                                       ^ 3 vertex is 3 floats                   ^ 5 per row        ^ 0 offset per row

        var locationTexCoords = shader.GetAttribLocation("vertexTexCoords");
        GL.EnableVertexAttribArray(locationTexCoords);
        GL.VertexAttribPointer(locationTexCoords, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        //                                        ^ tex coords is 2 floats                 ^ 5 per row        ^ 4th and 5th float in each row

        time.Start();
    }

    internal static void SetUniforms(Shader shader)
    {
        shader.Use();
        shader.SetUniform("time", (float)time.Elapsed.TotalSeconds);
        shader.SetUniform("resolution", new Vector2(Program.Window.Size.X, Program.Window.Size.Y));
        SetTextureUniformCallback?.Invoke();
    }

    internal static void Draw()
    {
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}
