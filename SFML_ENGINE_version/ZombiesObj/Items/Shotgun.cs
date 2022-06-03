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

        //Sheesh
        Ray[] rays = new Ray[5];

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"> Position </param>
        /// <param name="p"> Ref to player </param>
        public Shotgun(Vector2f pos, Player p)
        {
            //Set player
            _player = p;

            //Set weapon's property
            _dammage = 25;
            _fireRate = 0.5f;
            _maxammo = 6;
            _ammo = 6;
            _fireMode = FireMode.SEMI_AUTO;
            _weaponState = WeaponState.ONGROUND;
            _ammoType = AmmoType.SHOTGUN;
            _shotDistance = 2000;
            _shotsRay = new Ray[5];
            _shotSound = new Sound(new SoundBuffer(@"C:\Users\pq34bsi\Desktop\Zombies\Assets\Sounds\GunShot.wav"));
            _shotOffset = GameMath.ToRadian(10);

            //Set physic object
            _physicObject = new AABB(pos, 50, 50);

            //Set muzzle flash
            _muzzleFlashPosition = new Vector2f(100, -25);

            //Set sprites
            _sprite_idle = new GameSprite(48, 0, 16, 16);

            //Set graphic object
            _graphicObject = _sprite_idle;
            _graphicState = GraphicState.BACKGROUND;

            //Set transformable
            _transformable.Position = pos;
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }
    }
}
