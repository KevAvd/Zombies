using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using ZombiesGame.Mathematics;
using ZombiesGame.GameObjects;

namespace ZombiesGame.Systems
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
        public void Render(GameObject[] gameObjects)
        {
            //Clear current vertices list if it contains some
            if (_vertices.Count > 0)
            {
                _vertices.Clear();
            }

            //Set vertices position to world space
            foreach (GameObject obj in gameObjects)
            {
                ObjectSpaceToWorldSpace(obj.Vertices, obj.Transformable);
            }

            //Render game state
            _rndr_Target.Clear(new Color(10, 102, 62));
            _rndr_Target.Draw(_vertices.ToArray(), PrimitiveType.Quads, _rndr_State);
        }

        private void ObjectSpaceToWorldSpace(Vertex[] vertices, Transformable transformable)
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
            _vertices.AddRange(vertices2);
        }
    }
}
