using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace ZombiesGame
{
    class Main : GameState
    {
        Player _player = new Player(new Vector2f(1920 / 2, 1080 / 2));

        /// <summary>
        /// Get player
        /// </summary>
        public Player Player { get => _player; }

        public override void OnStart()
        {
            AddGameObj(_player);
            AddGameObj(new Zombie(new Vector2f(0,0), _player));
            
        }

        public override void OnUpdate()
        {
            if (Inputs.IsClicked(Keyboard.Key.K))
            {
                Renderer.TogglePhysObj();
            }

            CheckCollision();

            //Handle collision
            foreach (Collision col in Collisions)
            {
                if(col.Obj1.GetType().IsSubclassOf(typeof(Character)) &&
                   col.Obj2.GetType().IsSubclassOf(typeof(Character)))
                {
                    Ray ray = new Ray(col.Obj1.Transformable.Position, col.Obj2.Transformable.Position);
                    CollisionDetection.AABB_RAY((AABB)col.Obj1.PhysicObject, ray, out Vector2f pNear1, out Vector2f pFar1, out Vector2f normal1);
                    CollisionDetection.AABB_RAY((AABB)col.Obj2.PhysicObject, ray, out Vector2f pNear2, out Vector2f pFar2, out Vector2f normal2);
                    Vector2f toMove = pNear2 - pFar1;
                    toMove /= 2f;
                    col.Obj1.Position += toMove;
                    col.Obj2.Position -= toMove;
                }

                if ((col.Obj1.GetType() == typeof(Zombie) || col.Obj2.GetType() == typeof(Zombie)) &&
                    (col.Obj1.GetType() == typeof(Bullet) || col.Obj2.GetType() == typeof(Bullet)))
                {
                    Bullet b = col.Obj1.GetType() == typeof(Bullet) ? col.Obj1 as Bullet : col.Obj2 as Bullet;
                    Zombie z = col.Obj1.GetType() == typeof(Zombie) ? col.Obj1 as Zombie : col.Obj2 as Zombie;
                    z.Velocity = -GameMath.NormalizeVector(col.PNear - b.Position) * 500;
                    z.Health -= b.Dammage;
                }

                if(col.Obj1.GetType() == typeof(Bullet) || col.Obj2.GetType() == typeof(Bullet))
                {
                    Bullet b = col.Obj1.GetType() == typeof(Bullet) ? col.Obj1 as Bullet : col.Obj2 as Bullet;
                    b.Destroy();
                }
            }
        }
    }
}
