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
using ZombiesGame.GraphicObjects;

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

            //Set graphic object
            Animation animation = new Animation(1000);
            animation.AddFrame(
                new Vector2f(0, 16),
                new Vector2f(16, 16),
                new Vector2f(16, 32),
                new Vector2f(0, 32)
            );
            animation.AddFrame(
                new Vector2f(0, 32),
                new Vector2f(16, 32),
                new Vector2f(16, 48),
                new Vector2f(0, 48)
            );
            _graphicObject = new GraphicObject("Switching", animation);

            //Set transformable
            _transformable.Position = new Vector2f(500, 500);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }
    }
}
