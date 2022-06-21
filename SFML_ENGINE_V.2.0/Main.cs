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
        byte[,] mapbyte =
        {
            {4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0 },
            {4,1,1,1,1,1,1,1,1,4,0,0,0,0,0,0,0,0,0,0 },
            {4,1,1,1,1,1,1,1,1,4,0,0,0,0,0,0,4,4,4,4 },
            {4,6,1,1,1,1,1,1,1,4,0,0,0,0,0,0,4,1,1,4 },
            {4,5,1,1,1,1,1,1,1,4,0,0,0,0,0,0,4,1,1,4 },
            {4,1,1,1,1,1,1,1,1,4,4,4,4,4,4,4,4,4,3,4 },
            {4,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,4 },
            {4,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,4 },
            {4,1,1,1,1,1,1,1,1,4,1,1,1,1,1,1,1,1,1,4 },
            {4,7,1,8,1,9,1,1,1,4,1,1,1,1,1,1,1,1,1,4 },
            {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4 }
        };
        Map map;
        
        //Constant
        const float MAX_CAMPLAYER_DISTANCE = 200;

        public override void OnStart()
        {
            map = new Map(mapbyte, _player);
            AddGameObj(_player);
            //AddGameObj(new Zombie(new Vector2f(300, 300), _player));
            //AddGameObj(new Zombie(new Vector2f(400, 300), _player));
            //AddGameObj(new Zombie(new Vector2f(500, 300), _player));
            //AddGameObj(new Zombie(new Vector2f(600, 300), _player));
            //AddGameObj(new Zombie(new Vector2f(700, 300), _player));
            //AddGameObj(new Zombie(new Vector2f(300, 400), _player));
            //AddGameObj(new Zombie(new Vector2f(400, 400), _player));
            //AddGameObj(new Zombie(new Vector2f(500, 400), _player));
            //AddGameObj(new Zombie(new Vector2f(600, 400), _player));
            //AddGameObj(new Zombie(new Vector2f(700, 400), _player));
            AddGameObj(new PlayerUI(_player));
            AddGameObj(map);
            map.GenerateMap();
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

                    if(_player.State == Player.PlayerState.HITTED || (_player.NextState == Player.PlayerState.HITTED && _player.IsSwitching))
                    {
                        continue;
                    }

                    if ((col.Obj1.GetType() == typeof(Zombie) && col.Obj2.GetType() == typeof(Player)) ||
                        (col.Obj1.GetType() == typeof(Player) && col.Obj2.GetType() == typeof(Zombie)))
                    {
                        Player p = col.Obj1.GetType() == typeof(Player) ? col.Obj1 as Player : col.Obj2 as Player;
                        Zombie z = col.Obj1.GetType() == typeof(Zombie) ? col.Obj1 as Zombie : col.Obj2 as Zombie;

                        if (CollisionDetection.AABB_RAY((AABB)p.PhysicObject, new Ray(z.Position, p.Position), out Vector2f pNear, out Vector2f pFar, out Vector2f normal))
                        {
                            p.Health--;
                            p.Velocity = GameMath.NormalizeVector(p.Position - pNear) * 1000;
                            p.SwitchState(Player.PlayerState.HITTED);
                        }
                    }
                }

                if ((col.Obj1.GetType() == typeof(Zombie) || col.Obj2.GetType() == typeof(Zombie)) &&
                    (col.Obj1.GetType() == typeof(Bullet) || col.Obj2.GetType() == typeof(Bullet)))
                {
                    Bullet b = col.Obj1.GetType() == typeof(Bullet) ? col.Obj1 as Bullet : col.Obj2 as Bullet;
                    Zombie z = col.Obj1.GetType() == typeof(Zombie) ? col.Obj1 as Zombie : col.Obj2 as Zombie;
                    z.Velocity = -GameMath.NormalizeVector(col.PNear - b.Position) * 500;
                    z.Health -= b.Dammage;
                }

                if ((col.Obj1.GetType() == typeof(Player) && col.Obj2.GetType() == typeof(Door)) ||
                    (col.Obj1.GetType() == typeof(Door) && col.Obj2.GetType() == typeof(Player)))
                {
                    Door d = col.Obj1.GetType() == typeof(Door) ? col.Obj1 as Door : col.Obj2 as Door;
                    d.Open();
                }

                if ((col.Obj1.GetType() == typeof(Player) && col.Obj2.GetType().IsSubclassOf(typeof(Buyable))) ||
                    (col.Obj1.GetType().IsSubclassOf(typeof(Buyable)) && col.Obj2.GetType() == typeof(Player)))
                {
                    Buyable d = col.Obj1.GetType().IsSubclassOf(typeof(Buyable)) ? col.Obj1 as Buyable : col.Obj2 as Buyable;

                    if (Inputs.IsClicked(Keyboard.Key.E))
                    {
                        d.Buy(_player);
                    }
                }

                if (col.Obj1.GetType() == typeof(Bullet) || col.Obj2.GetType() == typeof(Bullet))
                {
                    Bullet b = col.Obj1.GetType() == typeof(Bullet) ? col.Obj1 as Bullet : col.Obj2 as Bullet;
                    b.Destroy();
                }
            }

            foreach(GameObject obj in Objects)
            {
                if(obj.GetType().IsSubclassOf(typeof(Character)))
                {
                    map.ResolveMapCollision(obj);
                }
            }

            Game.SetView(new View(_player.Position, new Vector2f(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height)));
        }
    }
}
