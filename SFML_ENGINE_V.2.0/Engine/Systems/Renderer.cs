using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace SFML_Engine
{
    static class Renderer
    {
        //Vertices
        static List<Vertex> _Vertices_Quads_Background = new List<Vertex>();//All quads primitives to render on background
        static List<Vertex> _Vertices_Quads_Layer1 = new List<Vertex>();    //All quads primitives to render (Layer 1)
        static List<Vertex> _Vertices_Quads_Layer2 = new List<Vertex>();    //All quads primitives to render (Layer 2)
        static List<Vertex> _Vertices_Quads_Layer3 = new List<Vertex>();    //All quads primitives to render (Layer 3)
        static List<Vertex> _Vertices_Quads_UI = new List<Vertex>();        //All quads primitives to render (User Interface)
        static List<Vertex> _Vertices_Lines = new List<Vertex>();           //All lines primitives to render

        //texts
        static List<Text> _texts = new List<Text>();                        //All text to render

        //Rendering
        static RenderStates _rndr_State;                                    //Render state
        static RenderTarget _rndr_Target;                                   //Render target

        //Colors
        static Color _Color_Backgroud = new Color(10, 102, 62);             //Background color
        static Color _Color_AABB = Color.Yellow;                            //AABBs color

        //Fonts
        static Font _mainFont;                                              //Main font

        //boolean
        static bool RENDER_PHYSOBJ = false;                                    //Render AABBs if true


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
        /// Get/Set main font
        /// </summary>
        public static Font MainFont { get => _mainFont; set => _mainFont = value; }

        /// <summary>
        /// Render all sprite
        /// </summary>
        public static void Render(GameObject[] gameObjects)
        {
            //Set vertices position to world space
            foreach (GameObject obj in gameObjects)
            {
                if (RENDER_PHYSOBJ && obj.PhysicObject != null) { RenderPhysicObject(obj.PhysicObject); }
                if(obj.GraphicHandler.CurrentGrphObj == null || obj.GraphicHandler.GraphicState == GraphicState.HIDDEN) { continue; }
                RenderObject(obj);
            }

            //Clear last image
            _rndr_Target.Clear(_Color_Backgroud);

            //Render background quads
            if (_Vertices_Quads_Background.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Quads_Background.ToArray(), PrimitiveType.Quads, _rndr_State);
                _Vertices_Quads_Background.Clear();
            }

            //Render quads
            if (_Vertices_Quads_Layer1.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Quads_Layer1.ToArray(), PrimitiveType.Quads, _rndr_State);
                _Vertices_Quads_Layer1.Clear();
            }

            //Render quads
            if (_Vertices_Quads_Layer2.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Quads_Layer2.ToArray(), PrimitiveType.Quads, _rndr_State);
                _Vertices_Quads_Layer2.Clear();
            }

            //Render quads
            if (_Vertices_Quads_Layer3.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Quads_Layer3.ToArray(), PrimitiveType.Quads, _rndr_State);
                _Vertices_Quads_Layer3.Clear();
            }

            //Render ui quads
            if (_Vertices_Quads_UI.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Quads_UI.ToArray(), PrimitiveType.Quads, _rndr_State);
                _Vertices_Quads_UI.Clear();
            }

            //Render lines
            if (_Vertices_Lines.Count > 0)
            {
                _rndr_Target.Draw(_Vertices_Lines.ToArray(), PrimitiveType.Lines);
                _Vertices_Lines.Clear();
            }

            //Render texts
            if(_texts.Count > 0)
            {
                foreach(Text text in _texts)
                {
                    _rndr_Target.Draw(text);
                }
                _texts.Clear();
            }
        }

        /// <summary>
        /// Transform vertices position from object space to world space
        /// </summary>
        /// <param name="obj"> GameObject to render </param>
        static void RenderObject(GameObject obj)
        {
            Vertex[] vertices = obj.GraphicHandler.CurrentGrphObj.GetVertices();
            Vector2f position;

            for (int i = 0; i < vertices.Length; i++)
            {
                if (obj.IsRelative())
                {
                    position = GameMath.VectorRotation(GameMath.ScaleVector(vertices[i].Position, obj.Scale), obj.Rotation) + obj.RelativePosition;
                }
                else
                {
                    position = GameMath.VectorRotation(GameMath.ScaleVector(vertices[i].Position, obj.Scale), obj.Rotation) + obj.Position;
                }

                switch (obj.GraphicHandler.GraphicState)
                {
                    case GraphicState.BACKGROUND:
                        _Vertices_Quads_Background.Add(new Vertex(position, vertices[i].Color, vertices[i].TexCoords));
                        break;
                    case GraphicState.LAYER_1:
                        _Vertices_Quads_Layer1.Add(new Vertex(position, vertices[i].Color, vertices[i].TexCoords));
                        break;
                    case GraphicState.LAYER_2:
                        _Vertices_Quads_Layer2.Add(new Vertex(position, vertices[i].Color, vertices[i].TexCoords));
                        break;
                    case GraphicState.LAYER_3:
                        _Vertices_Quads_Layer3.Add(new Vertex(position, vertices[i].Color, vertices[i].TexCoords));
                        break;
                    case GraphicState.UI:
                        _Vertices_Quads_UI.Add(new Vertex(position, vertices[i].Color, vertices[i].TexCoords));
                        break;
                }
            }
        }

        static void RenderPhysicObject(PhysicObject obj)
        {
            if(obj.GetType() == typeof(AABB))
            {
                DrawAABB(obj as AABB, Color.Yellow);
            }
            else if (obj.GetType() == typeof(Ray))
            {
                DrawRay(obj as Ray, Color.Yellow);
            }
            else if (obj.GetType() == typeof(Circle))
            {
                DrawCircle(obj as Circle, Color.Yellow, 20);
            }
        }

        /// <summary>
        /// Render text to screen
        /// </summary>
        /// <param name="txt"> Text to render </param>
        /// <param name="charSize"> Character size </param>
        /// <param name="pos"> Text position </param>
        public static void RenderText(string txt, uint charSize, Vector2f pos)
        {
            if(_mainFont == null)
            {
                return;
            }

            _texts.Add(new Text(txt, MainFont, charSize));
            _texts.Last().Position = pos;
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
        /// Draw a circle
        /// </summary>
        /// <param name="circle"> Circle to draw </param>
        /// <param name="nbrOfPoints"> Points of the circle </param>
        public static void DrawCircle(Circle circle, Color color, int nbrOfPoints)
        {
            float toRotate = (float)(Math.PI * 2) / nbrOfPoints;
            Vector2f radius = new Vector2f(circle.Radius, 0);
            Vector2f circlePosition = circle.GetPoints()[0];
            List<Vector2f> points = new List<Vector2f>();

            for(int i = 0; i < nbrOfPoints; i++)
            {
                points.Add(GameMath.VectorRotation(radius, toRotate * i));
                points[points.Count() - 1] += circlePosition;
            }

            for(int i = 0; i < nbrOfPoints; i++)
            {
                _Vertices_Lines.Add(new Vertex(points[i], color));

                if(i+1 >= nbrOfPoints)
                {
                    _Vertices_Lines.Add(new Vertex(points[0], color));
                    continue;
                }

                _Vertices_Lines.Add(new Vertex(points[i+1], color));
            }
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
        /// Toggle AABB rendering
        /// </summary>
        public static void TogglePhysObj()
        {
            RENDER_PHYSOBJ = !RENDER_PHYSOBJ;
        }
    }
}
