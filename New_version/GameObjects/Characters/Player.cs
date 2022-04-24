using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using ZombiesGame.PhysicObjects;
using ZombiesGame.Mathematics;
using ZombiesGame.Systems;
using ZombiesGame.GameObjects.Items;

namespace ZombiesGame.GameObjects.Characters
{
    class Player : Character
    {
        public Player()
        {
            //Set speed
            _speed = 500;

            //Set AABB
            _physicObject = new AABB(new Vector2f(500, 500), 100, 100);

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
            _transformable.Position = new Vector2f(500, 500);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(1, 1);
            _transformable.Origin = new Vector2f(0, 0);
        }
    }
}
