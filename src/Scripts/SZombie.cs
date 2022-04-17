using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombies.GameObjects.Components;
using Zombies.GameObjects.Entities;
using SFML.System;

namespace Zombies.Scripts
{
    internal class SZombie : Script
    {
        float speed = 250;
        Position playerPos;

        public SZombie()
        {
            COMP_Position = new Position();
            COMP_Position.X = 1920f / 2f;
            COMP_Position.Y = 1080f / 2f;
            COMP_Sprite = new GameSprite(100, 100, new Vector2f(0, 0), new Vector2f(100, 0), new Vector2f(100, 100), new Vector2f(0, 100));
        }

        public override void Start()
        {
            if(GAME_State.GetEntity("Player", out Entity[] ent))
            {
                if(ent.Length > 0)
                {
                    if(ent[0].GetComponentOfType(typeof(Position), out Component comp))
                    {
                        playerPos = (Position)comp;
                    }
                }
            }
            else
            {
                playerPos = new Position();
            }
        }

        public override void OnUpdate(float dt)
        {
            Vector2f vecToPlayer = Math.LinearAlgebra.NormalizeVector(new Vector2f(playerPos.X - COMP_Position.X, playerPos.Y - COMP_Position.Y));

            COMP_Position.X += vecToPlayer.X * speed * dt;
            COMP_Position.Y += vecToPlayer.Y * speed * dt;

            //Make zombie aim at player
            Vector2f playerVec = new Vector2f(playerPos.X, playerPos.Y);
            COMP_Position.Heading = MathF.Atan2(playerVec.Y - COMP_Position.Y, playerVec.X - COMP_Position.X) - (99 * 180 / MathF.PI);
        }

    }
}
