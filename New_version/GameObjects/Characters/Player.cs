﻿using System;
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

namespace ZombiesGame.GameObjects.Characters
{
    internal class Player : Character
    {
        public Player()
        {
            //Set speed
            _speed = 500;

            //Set AABB
            _aabb = new AABB(new Vector2f(500, 500), 100, 100, this);

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
            _transformable.Position = new Vector2f(500, 500);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(0,0);
        }

        public override void Start()
        {

        }

        public override void Update()
        {
            //Move player
            _velocity = new Vector2f(0, 0);
            if (Inputs.IsPressed(Keyboard.Key.W))
            {
                _velocity.Y -= 1;
            }
            if (Inputs.IsPressed(Keyboard.Key.A))
            {
                _velocity.X -= 1;
            }
            if (Inputs.IsPressed(Keyboard.Key.S))
            {
                _velocity.Y += 1;
            }
            if (Inputs.IsPressed(Keyboard.Key.D))
            {
                _velocity.X += 1;
            }

            //Make player aim at mouse cursor
            Vector2f mousePos = Inputs.GetMousePosition(true);
            _transformable.Rotation = MathF.Atan2(mousePos.Y - _transformable.Position.Y, mousePos.X - _transformable.Position.X) - (99 * 180 / MathF.PI);

            //Call base method
            base.Update();
        }
    }
}
