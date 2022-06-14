using SFML.Graphics;
using SFML.System;

namespace SFML_Engine
{
    internal class AABB : PhysicObject
    {
        //Property
        float _width;                                                       //AABB width
        float _height;                                                      //AABB height

        /// <summary>
        /// Get/Set width
        /// </summary>
        public float Width { get => _width; set => _width = value; }

        /// <summary>
        /// Get/Set height
        /// </summary>
        public float Height { get => _height; set => _height = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"> AABB's position </param>
        /// <param name="w"> AABB's width </param>
        /// <param name="h"> AABB's height </param>
        /// <param name="obj"> GameObject related to this AABB </param>
        public AABB(Vector2f pos, float w, float h)
        {
            _vertices = new Vertex[4];
            _width = w;
            _height = h;

            UpdatePosition(pos);
        }

        public override void UpdatePosition(Vector2f pos)
        {
            Vector2f half_W = new Vector2f(_width / 2f, 0);
            Vector2f half_H = new Vector2f(0, _height / 2f);
            _vertices[0].Position = pos - half_W - half_H;
            _vertices[1].Position = pos + half_W - half_H;
            _vertices[2].Position = pos + half_W + half_H;
            _vertices[3].Position = pos - half_W + half_H;
        }
    }
}
