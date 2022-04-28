using SFML.System;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Systems;
using SFML_Engine.Mathematics;

namespace ZombiesGame
{
    internal class Zombie : Character
    {
        public Zombie(float x, float y)
        {
            //Set speed
            _speed = 200;
            _health = 100;

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

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            Player player = PlayerSGLTN.GetInstance().Player;

            //Move zombie to player
            _movement = player.Position - Position;

            //Make zombie aim at player
            Rotation = MathF.Atan2(player.Position.Y - Position.Y, player.Position.X - Position.X);

            //Execute base methode
            base.OnUpdate();
        }
    }
}