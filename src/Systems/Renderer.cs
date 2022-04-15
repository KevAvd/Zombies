using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using Zombies.GameObjects.Components;
using Zombies.Math;

namespace Zombies.Systems
{
    class Renderer
    {
        //Property
        List<Vertex> _vertices = new List<Vertex>();    //All vertices to render
        RenderStates _rndr_State;                       //Render state
        RenderTarget _rndr_Target;                      //Render target

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rndrState"> Render state </param>
        public Renderer(RenderStates rndrState, RenderTarget renderTarget)
        {
            _rndr_State = rndrState;
            _rndr_Target = renderTarget;
        }

        /// <summary>
        /// Render all sprite
        /// </summary>
        public void Render(GameState state)
        {
            //Variables
            Component[] comps;

            //Get all sprite in current game state
            if(!state.GetAllComponentOfType(typeof(GameSprite), out comps))
            {
                return;
            }

            //Clear current vertices list if it contains some
            if(_vertices.Count > 0)
            {
                _vertices.Clear();
            }

            //Transform sprite to vertices for rendering
            foreach(Component comp in comps)
            {
                CreateVerticesFromSprite((GameSprite)comp);
            }

            //Render game state
            _rndr_Target.Clear(new Color(10,102,62));
            _rndr_Target.Draw(_vertices.ToArray(), PrimitiveType.Quads, _rndr_State);
        }

        private void CreateVerticesFromSprite(GameSprite sprite)
        {
            //Variables
            Vertex[] vertices = new Vertex[4];
            Vector2f position = new Vector2f(0,0);
            Vector2f half_width = new Vector2f(sprite.Width / 2f, 0);
            Vector2f half_height = new Vector2f(0, sprite.Height / 2f);
            Component comp;

            //Get position
            if (!sprite.Entity.GetComponentOfType(typeof(Position), out comp))
            {
                return;
            }

            //Set vertices to objet space
            vertices[0].Position = position - half_width - half_height;
            vertices[1].Position = position + half_width - half_height;
            vertices[2].Position = position + half_width + half_height;
            vertices[3].Position = position - half_width + half_height;

            //Get position
            position = new Vector2f((comp as Position).X, (comp as Position).X);

            //Set vertices to World space
            for(int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Position = LinearAlgebra.VectorRotation(vertices[i].Position, (comp as Position).Heading) + position;
                vertices[i].TexCoords = sprite.TxtCoords[i];
                vertices[i].Color = new Color(255, 255, 255, 255);
            }

            //Add vertices to list
            //_vertices.Add(vertices[3]);
            //_vertices.Add(vertices[0]);
            //_vertices.Add(vertices[1]);
            //_vertices.Add(vertices[3]);
            //_vertices.Add(vertices[1]);
            //_vertices.Add(vertices[2]);
            _vertices.AddRange(vertices);
        }
    }
}
