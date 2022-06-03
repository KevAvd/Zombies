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
    class Rifle : Weapon
    {
        //Sprites
        GameSprite _sprite_idle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"> Position </param>
        /// <param name="p"> Ref to player </param>
        public Rifle(Vector2f pos, Player p)
        {
            //Set player 
            _player = p;

            //Set physic object
            _physicObject = new AABB(pos, 50, 50);

            //Set weapons property
            _dammage = 20;
            _fireRate = 0.1f;
            _maxammo = 30;
            _ammo = 30;
            _fireMode = FireMode.AUTO;
            _weaponState = WeaponState.ONGROUND;
            _ammoType = AmmoType.RIFLE;
            _shotDistance = 4000;
            _shotsRay = new Ray[1];
            _shotSound = new Sound(new SoundBuffer(@"C:\Users\pq34bsi\Desktop\Zombies\Assets\Sounds\GunShot.wav"));
            _shotOffset = 0;

            //Set sprites
            _sprite_idle = new GameSprite(64, 0, 16, 16);

            //Set muzzle flash
            _muzzleFlashPosition = new Vector2f(100, -25);

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
