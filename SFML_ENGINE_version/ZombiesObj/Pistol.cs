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
    class Pistol : Weapon
    {
        //Sprites
        GameSprite _sprite_idle;

        //Sounds
        Sound _gunShot = new Sound(new SoundBuffer(@"C:\Users\drimi\OneDrive\Bureau\Asset\Sounds\GunShot.wav"));

        public Pistol(float x, float y, Player p)
        {
            //Set player
            _player = p;

            //Set weapon
            _fireMode = FireMode.SEMI_AUTO;
            _weaponState = WeaponState.ONGROUND;
            _dammage = 25;
            _fireRate = 0.3f;

            //Set physic object
            _physicObject = new AABB(new Vector2f(x, y), 50, 50);

            //Set sprites
            _sprite_idle = new GameSprite(32, 0, 16, 16);

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
            GetGameState().AddGameObj(new MuzzleFlash(_player, 100, 0));

            //Detect shot collision
            Ray shot = new Ray(Position, Inputs.GetMousePosition(true) - Position, 4000);
            foreach (GameObject obj in GetGameState().Objects)
            {
                if (obj.GetType() != typeof(Zombie))
                {
                    continue;
                }

                if (CollisionDetection.AABB_RAY(obj.PhysicObject as AABB, shot, out Vector2f pNear, out Vector2f pFar, out Vector2f normal))
                {
                    (obj as Zombie).Velocity = LinearAlgebra.NormalizeVector(pNear - Position) * 500;
                    (obj as Zombie).Health -= _dammage;
                }
            }
        }
    }
}
