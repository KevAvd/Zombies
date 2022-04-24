using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using ZombiesGame.PhysicObjects;
using ZombiesGame.Mathematics;
using ZombiesGame.Systems;

namespace ZombiesGame.GameObjects.Characters
{
    internal class Zombie : Character
    {
        public Zombie(float x, float y)
        {
            //Set speed
            _speed = 200;

            //Set AABB
            _physicObject = new AABB(new Vector2f(0, 0), 100, 100);

            //Set vertices
            _vertices = new Vertex[4];
            //Set positions
            _vertices[0].Position = new Vector2f(-50, -50);
            _vertices[1].Position = new Vector2f(50, -50);
            _vertices[2].Position = new Vector2f(50, 50);
            _vertices[3].Position = new Vector2f(-50, 50);
            //Set texCoords
            _vertices[0].TexCoords = new Vector2f(0, 0);
            _vertices[1].TexCoords = new Vector2f(100, 0);
            _vertices[2].TexCoords = new Vector2f(100, 100);
            _vertices[3].TexCoords = new Vector2f(0, 100);
            //Set color
            _vertices[0].Color = new Color(255, 255, 255, 255);
            _vertices[1].Color = new Color(255, 255, 255, 255);
            _vertices[2].Color = new Color(255, 255, 255, 255);
            _vertices[3].Color = new Color(255, 255, 255, 255);

            //Set transformable
            _transformable.Position = new Vector2f(x, y);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(1, 1);
            _transformable.Origin = new Vector2f(0, 0);
        }
    }
}
