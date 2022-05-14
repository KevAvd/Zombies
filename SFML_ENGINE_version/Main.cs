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
        int nbrOfZombToSpawn = 10;
        Player player = new Player();

        public override void OnStart()
        {
            AddGameObj(player);
            AddGameObj(new Pistol(200,200,player));
            AddGameObj(new Rifle(1900,1000,player));
            AddGameObj(new Shotgun(1800,200,player));
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
                for (int i = 0; i < nbrOfZombToSpawn; i++)
                {
                    Zombie z = new Zombie(i * 51, 50, player);
                    AddGameObj(z);
                }

                nbrOfZombToSpawn += 10;
            }
        }
    }
}
