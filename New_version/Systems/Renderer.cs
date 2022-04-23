using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using ZombiesGame.Mathematics;
using ZombiesGame.GameObjects;
using ZombiesGame.PhysicObjects;

namespace ZombiesGame.Systems
{
    static class Renderer
    {
        //Vertices
        static List<Vertex> _Vertices_Quads = new List<Vertex>();           //All quads primitives to render
        static List<Vertex> _Vertices_Lines = new List<Vertex>();           //All lines primitives to render
        static List<Vertex> _Vertices_Triangles = new List<Vertex>();       //All triangles primitives to render

        //Shapes
        static List<Shape> _shapes = new List<Shape>();                     //All shapes to render

        //Rendering
        static RenderStates _rndr_State;                                    //Render state
        static RenderTarget _rndr_Target;                                   //Render target

        //Colors
        static Color _Color_Backgroud = new Color(10, 102, 62);             //Background color
        static Color _Color_AABB = Color.Yellow;                            //AABBs color

        //boolean
        static bool RENDER_AABB = false;                                    //Render AABBs if true


        /// <summary>
        /// Get/Set clear color
        /// </summary>
        public static Color ClearColor { get => _Color_Backgroud; set => _Color_Backgroud = value; }

        /// <summary>
        /// Get/Set render state
        /// </summary>
        public static RenderStates State { get => _rndr_State; set => _rndr_State = value; }

        /// <summary>
        /// Get/Set render target
        /// </summary>
        public static RenderTarget Target { get => _rndr_Target; set => _rndr_Target = value; }

        /// <summary>
        /// Render all sprite
        /// </summary>
        public static void Render(GameObject[] gameObjects)
        {
            //Set vertices position to world space
            foreach (GameObject obj in gameObjects)
            {
                ObjectSpaceToWorldSpace(obj.Vertices, obj.Transformable);
                if (RENDER_AABB) { DrawAABB(obj.AABB, _Color_AABB); }
            }

            //Clear last image
            _rndr_Target.Clear(_Color_Backgroud);

            //Render Quads
            if(_Vertices_Quads.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Quads.ToArray(), PrimitiveType.Quads, _rndr_State);
                _Vertices_Quads.Clear();
            }

            //Render triangles
            if (_Vertices_Triangles.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Triangles.ToArray(), PrimitiveType.Triangles, _rndr_State);
                _Vertices_Triangles.Clear();
            }

            //Render lines
            if (_Vertices_Lines.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Lines.ToArray(), PrimitiveType.Lines);
                _Vertices_Lines.Clear();
            }

            //Render shapes
            if(_shapes.Count > 0)
            {
                foreach(Shape shape in _shapes)
                {
                    _rndr_Target.Draw(shape);
                }
                _shapes.Clear();
            }
        }

        /// <summary>
        /// Transform vertices position from object space to world space
        /// </summary>
        /// <param name="vertices"> Vertices to transform </param>
        /// <param name="transformable"></param>
        static void ObjectSpaceToWorldSpace(Vertex[] vertices, Transformable transformable)
        {
            Vertex[] vertices2 = new Vertex[vertices.Length];

            //Set vertices to World space
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices2[i].Position = LinearAlgebra.VectorRotation(vertices[i].Position, transformable.Rotation) + transformable.Position;
                vertices2[i].TexCoords = vertices[i].TexCoords;
                vertices2[i].Color = vertices[i].Color;
            }

            //Add vertices to list
            _Vertices_Quads.AddRange(vertices2);
        }

        /// <summary>
        /// Draw AABB
        /// </summary>
        /// <param name="aabb"> AABB to draw </param>
        /// <param name="color"> Color </param>
        public static void DrawAABB(AABB aabb, Color color)
        {
            for(int i = 0; i < aabb.Vertices.Length; i++) { aabb.Vertices[i].Color = color; }

            //Firts line
            _Vertices_Lines.Add(aabb.Vertices[0]);
            _Vertices_Lines.Add(aabb.Vertices[1]);

            //Second line
            _Vertices_Lines.Add(aabb.Vertices[1]);
            _Vertices_Lines.Add(aabb.Vertices[2]);

            //Third line
            _Vertices_Lines.Add(aabb.Vertices[2]);
            _Vertices_Lines.Add(aabb.Vertices[3]);

            //Fourth line
            _Vertices_Lines.Add(aabb.Vertices[3]);
            _Vertices_Lines.Add(aabb.Vertices[0]);
        }

        /// <summary>
        /// Draw ray
        /// </summary>
        /// <param name="ray"> Ray to draw </param>
        /// <param name="color"> Color </param>
        public static void DrawRay(Ray ray, Color color)
        {
            for (int i = 0; i < ray.Vertices.Length; i++) { ray.Vertices[i].Color = color; }
            _Vertices_Lines.AddRange(ray.Vertices);
        }

        /// <summary>
        /// Draw shape
        /// </summary>
        /// <param name="shape"> Shape to draw </param>
        public static void DrawShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        /// <summary>
        /// Toggle AABB rendering
        /// </summary>
        public static void ToggleAABB()
        {
            RENDER_AABB = !RENDER_AABB;
        }
    }
}
