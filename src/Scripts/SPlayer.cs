using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using Zombies.Enum;
using Zombies.GameObjects.Components;
using Zombies.GameObjects.Entities;
using Zombies.Systems;

namespace Zombies.Scripts
{
    internal class SPlayer : Script
    {
        float _speed = 500;

        /// <summary>
        /// Init component
        /// </summary>
        public SPlayer()
        {
            //Init component
            _position = new Position();
            _sprite = new GameSprite(100, 100, new Vector2f(0, 0), new Vector2f(100, 0), new Vector2f(100, 100), new Vector2f(0, 100));
        }

        public override void Start()
        {

        }

        public override void OnUpdate(float dt)
        {
            //Move player
            if (Inputs.IsPressed(Keyboard.Key.W))
            {
                _position.Y -= _speed * dt;
            }
            if (Inputs.IsPressed(Keyboard.Key.A))
            {
                _position.X -= _speed * dt;
            }
            if (Inputs.IsPressed(Keyboard.Key.S))
            {
                _position.Y += _speed * dt;
            }
            if (Inputs.IsPressed(Keyboard.Key.D))
            {
                _position.X += _speed * dt;
            }

            //Make player aim at mouse cursor
            Vector2i mousePos = Inputs.GetMousePosition(true);
            _position.Heading = MathF.Atan2(mousePos.Y - _position.Y, mousePos.X - _position.X) - (99 * 180 / MathF.PI);
        }
    }
}
