﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;
using SFML_Engine;
using SFML_Engine.GameObjects;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Mathematics;
using SFML_Engine.Systems;

namespace ZombiesGame
{
    class Main : GameState
    {
        Player player = new Player();

        public override void OnStart()
        {
            Renderer.MainFont = new Font(@"C:\Users\drimi\OneDrive\Bureau\Asset\Fonts\prstartk.ttf");
            AddGameObj(player);
            AddGameObj(new Pistol(new Vector2f(200,200) ,player));
            AddGameObj(new Rifle(new Vector2f(1800,1000) ,player));
            AddGameObj(new Shotgun(new Vector2f(1800,200) ,player));
        }

        public override void OnUpdate()
        {
            int zombieCount = 0;

            if (Inputs.IsClicked(Keyboard.Key.K))
            {
                Renderer.ToggleAABB();
            }

            foreach (GameObject obj in Objects)
            {
                //Handles collision
                if (obj.GetType().IsSubclassOf(typeof(Character)))
                {
                    if ((obj as Character).Health <= 0)
                    {
                        continue;
                    }

                    if (obj.GetType().Name == "Zombie")
                    {
                        zombieCount++;
                    }

                    foreach (GameObject c in Objects)
                    {
                        if (obj.Equals(c) || !c.GetType().IsSubclassOf(typeof(Character)))
                        {
                            continue;
                        }

                        if ((c as Character).Health <= 0)
                        {
                            continue;
                        }

                        if (CollisionDetection.AABB_AABB((AABB)obj.PhysicObject, (AABB)c.PhysicObject))
                        {
                            Ray ray = new Ray(obj.Transformable.Position, c.Transformable.Position);
                            CollisionDetection.AABB_RAY((AABB)obj.PhysicObject, ray, out Vector2f pNear1, out Vector2f pFar1, out Vector2f normal1);
                            CollisionDetection.AABB_RAY((AABB)c.PhysicObject, ray, out Vector2f pNear2, out Vector2f pFar2, out Vector2f normal2);
                            Vector2f toMove = pNear2 - pFar1;
                            toMove /= 2f;
                            obj.Position += toMove;
                            c.Position -= toMove;
                        }
                    }
                }
            }

            if (zombieCount == 0)
            {
                Zombie[] zombies = new Zombie[10];
                zombies[0] = new Zombie(50, 50, player);
                zombies[1] = new Zombie(100, 50, player);
                zombies[2] = new Zombie(1870, 50, player);
                zombies[3] = new Zombie(1820, 50, player);
                zombies[4] = new Zombie(50, 1030, player);
                zombies[5] = new Zombie(100, 1030, player);
                zombies[6] = new Zombie(1870, 1030, player);
                zombies[7] = new Zombie(1820, 1030, player);
                zombies[8] = new Zombie(910, 50, player);
                zombies[9] = new Zombie(910, 1030, player);

                foreach(Zombie z in zombies) AddGameObj(z);
            }
        }
    }
}
