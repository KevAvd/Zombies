using SFML.Graphics;
using SFML.System;
using SFML_Engine.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SFML_Engine.GameObjects.GraphicObjects
{
    class Frame : GraphicObject
    {
        //Property
        List<Vertex> _vertices = new List<Vertex>();        //All vertices (4 by sprite)

        public override void AddSprite(float x, float y, float w, float h)
        {

        }

        /// <summary>
        /// Add a sprite to the frame
        /// </summary>
        /// <param name="position"> Sprite position in frame </param>
        /// <param name="texCoords"> Sprite texture coordinate </param>
        /// <param name="size"> Sprite size in frame </param>
        /// <param name="texSize"> Sprite texture size </param>
        public void AddSprite(Vector2f position, Vector2f texCoords, Vector2f size, Vector2f texSize)
        {
            _vertices.Add(new Vertex(position, Color.White, texCoords));
            _vertices.Add(new Vertex(GameMath.AddOnlyOneAxis(position, size, true), Color.White, GameMath.AddOnlyOneAxis(texCoords, texSize, true)));
            _vertices.Add(new Vertex(position + size, Color.White, texCoords + texSize));
            _vertices.Add(new Vertex(GameMath.AddOnlyOneAxis(position, size, false), Color.White, GameMath.AddOnlyOneAxis(texCoords, texSize, false)));
        }

        public override Vertex[] GetVertices()
        {
            return _vertices.ToArray();
        }
    }
}
