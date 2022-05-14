using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.Systems;
using SFML_Engine.Mathematics;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Enums;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace ZombiesGame
{
    class Shotgun : Weapon
    {
        //Sprites
        GameSprite _sprite_idle;

        //Sounds
        Sound _gunShot = new Sound(new SoundBuffer(@"C:\Users\drimi\OneDrive\Bureau\Asset\Sounds\GunShot.wav"));

        //Sheesh
        Ray[] rays = new Ray[5];

        public Shotgun(float x, float y, Player p)
        {
            //Set player
            _player = p;

            //Set weapon
            _fireMode = FireMode.SEMI_AUTO;
            _weaponState = WeaponState.ONGROUND;
            _dammage = 25;
            _fireRate = 0.5f;

            //Set physic object
            _physicObject = new AABB(new Vector2f(x, y), 50, 50);

            //Set sprites
            _sprite_idle = new GameSprite(48, 0, 16, 16);

            //Set graphic object
            _graphicObject = _sprite_idle;
            _graphicObject.State = GraphicState.BACKGROUND;

            //Set transformable
            _transformable.Position = new Vector2f(x, y);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }

        protected override void Shoot()
        {
            //Play gun shot sound
            _gunShot.Play();

            //Add muzzle flash
            GetGameState().AddGameObj(new MuzzleFlash(_player, 100, -25));

            //Set rays
            Vector2f playerMouseVec = Inputs.GetMousePosition(true) - Position;
            float rotation = (float)Math.Atan2(playerMouseVec.Y, playerMouseVec.X);
            float radianOffset = 10 * (float)Math.PI / 180.0f;
            rays[0] = new Ray(Position, rotation + radianOffset * 2, 1000);
            rays[1] = new Ray(Position, rotation + radianOffset, 1000);
            rays[2] = new Ray(Position, rotation, 1000);
            rays[3] = new Ray(Position, rotation - radianOffset, 1000);
            rays[4] = new Ray(Position, rotation - radianOffset * 2, 1000);

            //Detect shot collision
            foreach (GameObject obj in GetGameState().Objects)
            {
                if (obj.GetType() != typeof(Zombie))
                {
                    continue;
                }

                for(int i = 0; i < rays.Length; i++)
                {
                    if (CollisionDetection.AABB_RAY(obj.PhysicObject as AABB, rays[i], out Vector2f pNear, out Vector2f pFar, out Vector2f normal))
                    {
                        (obj as Zombie).Velocity = LinearAlgebra.NormalizeVector(pNear - Position) * 500;
                        (obj as Zombie).Health -= _dammage;
                    }
                }
            }
        }
    }
}
