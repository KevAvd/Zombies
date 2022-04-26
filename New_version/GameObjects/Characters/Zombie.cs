using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using ZombiesGame.PhysicObjects;
using ZombiesGame.GraphicObjects;
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
            Animation animation = new Animation();
            animation.AddFrame(
                new Vector2f(0, 0),
                new Vector2f(16, 0),
                new Vector2f(16, 16),
                new Vector2f(0, 16)
            );
            _graphicObject = new GraphicObject("Idle", animation);

            //Set transformable
            _transformable.Position = new Vector2f(x, y);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }
    }
}
