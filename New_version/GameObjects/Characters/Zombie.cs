using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using ZombiesGame.Components;
using ZombiesGame.Mathematics;
using ZombiesGame.Systems;

namespace ZombiesGame.GameObjects.Characters
{
    internal class Zombie : Character
    {
        float _speed;
        Player _player;

        public Zombie(Player player, float x, float y)
        {
            _player = player;

            //Set speed
            _speed = 200;

            //Set AABB
            _aabb = new AABB(100, 100);

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
            _transformable = new Transformable();
            _transformable.Position = new Vector2f(x, y);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(0, 0);
        }

        public override void Start()
        {

        }

        public override void Update()
        {
            FollowPlayer(GameTime.UpdateDeltaTime);
        }

        protected void FollowPlayer(float dt)
        {
            Vector2f vecToPlayer = LinearAlgebra.NormalizeVector(new Vector2f(_player.Transformable.Position.X - _transformable.Position.X, _player.Transformable.Position.Y - _transformable.Position.Y));

            _transformable.Position += vecToPlayer * _speed * dt;

            //Make zombie aim at player
            Vector2f playerVec = new Vector2f(_player.Transformable.Position.X, _player.Transformable.Position.Y);
            _transformable.Rotation = MathF.Atan2(playerVec.Y - _transformable.Position.Y, playerVec.X - _transformable.Position.X) - (99 * 180 / MathF.PI);
        }
    }
}
