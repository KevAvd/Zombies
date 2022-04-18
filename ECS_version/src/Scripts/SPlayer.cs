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
            COMP_Position = new Position();
            COMP_Sprite = new GameSprite(100, 100, new Vector2f(0, 0), new Vector2f(100, 0), new Vector2f(100, 100), new Vector2f(0, 100));
            ENTITY_Name = "Player";
        }

        public override void Start()
        {

        }

        public override void OnUpdate(float dt)
        {
            //Move player
            if (Inputs.IsPressed(Keyboard.Key.W))
            {
                COMP_Position.Y -= _speed * dt;
            }
            if (Inputs.IsPressed(Keyboard.Key.A))
            {
                COMP_Position.X -= _speed * dt;
            }
            if (Inputs.IsPressed(Keyboard.Key.S))
            {
                COMP_Position.Y += _speed * dt;
            }
            if (Inputs.IsPressed(Keyboard.Key.D))
            {
                COMP_Position.X += _speed * dt;
            }

            //Make player aim at mouse cursor
            Vector2i mousePos = Inputs.GetMousePosition(true);
            COMP_Position.Heading = MathF.Atan2(mousePos.Y - COMP_Position.Y, mousePos.X - COMP_Position.X) - (99 * 180 / MathF.PI);
        }
    }
}
