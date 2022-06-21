using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine
{
    internal class GameSprite : GraphicObject
    {
        //Frame vertex
        Vertex _vertex1;        //Top-Left vertex
        Vertex _vertex2;        //Top-Right vertex
        Vertex _vertex3;        //Bottom-Right vertex
        Vertex _vertex4;        //Bottom-Left vertex

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"> Top-Left X position </param>
        /// <param name="y"> Top-Left Y position </param>
        /// <param name="w"> Width </param>
        /// <param name="h"> Height </param>
        public GameSprite(float x, float y, float w, float h)
        {
            AddSprite(x, y, w, h);
        }

        /// <summary>
        /// Set the actual frame
        /// </summary>
        /// <param name="x"> Top-Left X position </param>
        /// <param name="y"> Top-Left Y position </param>
        /// <param name="w"> Width </param>
        /// <param name="h"> Height </param>
        public override void AddSprite(float x, float y, float w, float h)
        {
            _vertex1 = new Vertex(new Vector2f(-1, -1), new Color(255, 255, 255, 255), new Vector2f(x, y));
            _vertex2 = new Vertex(new Vector2f( 1, -1), new Color(255, 255, 255, 255), new Vector2f(x + w, y));
            _vertex3 = new Vertex(new Vector2f( 1,  1), new Color(255, 255, 255, 255), new Vector2f(x + w, y + h));
            _vertex4 = new Vertex(new Vector2f(-1,  1), new Color(255, 255, 255, 255), new Vector2f(x, y + h));
        }

        /// <summary>
        /// Return all frame vertices
        /// </summary>
        /// <returns> Vertex array </returns>
        public override Vertex[] GetVertices()
        {
            return new Vertex[]
            {
                _vertex1,
                _vertex2,
                _vertex3,
                _vertex4
            };
        }
    }
}
