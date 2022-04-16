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
        InputHandler inptHdlr;
        Position _pos;
        float _speed;

        public override void Start()
        {
            inptHdlr = InputHandler.GetInstance();

            if (_entity.GetComponentOfType(typeof(Position), out Component comp))
            {
                _pos = (Position)comp;
            }

            _speed = 500;
        }

        public override void OnUpdate(float dt)
        {
            if (inptHdlr.IsPressed(Keyboard.Key.W))
            {
                _pos.Y -= _speed * dt;
            }
            if (inptHdlr.IsPressed(Keyboard.Key.A))
            {
                _pos.X -= _speed * dt;
            }
            if (inptHdlr.IsPressed(Keyboard.Key.S))
            {
                _pos.Y += _speed * dt;
            }
            if (inptHdlr.IsPressed(Keyboard.Key.D))
            {
                _pos.X += _speed * dt;
            }

            Vector2i mousePos = inptHdlr.GetMousePosition(true);
            _pos.Heading = MathF.Atan2(mousePos.Y - _pos.Y, mousePos.X - _pos.X) - (99 * 180 / MathF.PI);
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Player;
        }

    }
}
