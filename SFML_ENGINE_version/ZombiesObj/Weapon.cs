using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects;
using SFML_Engine.Systems;
using SFML_Engine.Enums;
using SFML_Engine.Mathematics;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace ZombiesGame
{
    abstract class Weapon : ScriptObject
    {
        //Instance of player
        protected Player _player;

        //Weapon's property
        protected float _dammage;
        protected float _fireRate;
        protected int _maxammo;
        protected int _ammo;
        protected FireMode _fireMode;
        protected WeaponState _weaponState;
        protected AmmoType _ammoType;
        protected Ray[] _shotsRay;
        protected float _shotOffset;
        protected float _shotDistance;
        protected Sound _shotSound;
        protected Vector2f _muzzleFlashPosition;

        //Time accumulator
        float _fireTimeAcc = 0;

        /// <summary>
        /// Get number of ammo
        /// </summary>
        public int Ammo { get => _ammo; }

        public enum AmmoType
        {
            PISTOL,
            SHOTGUN,
            RIFLE
        }

        public enum FireMode
        {
            BURST,
            SEMI_AUTO,
            AUTO
        }

        public enum WeaponState
        {
            ONGROUND,
            ONPLAYER
        }

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            //Update time accumulator
            _fireTimeAcc += GameTime.DeltaTimeU;

            switch (_weaponState)
            {
                case WeaponState.ONGROUND:
                    //Check collision with player
                    if (CollisionDetection.AABB_AABB(_physicObject as AABB, _player.PhysicObject as AABB) && Inputs.IsClicked(Keyboard.Key.E))
                    {
                        if(_player.Weapon != null)
                        {
                            _player.Weapon.Destroy();
                        }
                        _player.Weapon = this;
                        _weaponState = WeaponState.ONPLAYER;
                        _graphicObject.State = GraphicState.HIDDEN;
                        _physicObject.State = PhysicState.NOCLIP;
                    }
                    break;
                case WeaponState.ONPLAYER:
                    //Update position
                    Position = _player.Position;
                    break;
            }
        }

        /// <summary>
        /// Make the weapon shoot
        /// </summary>
        /// <param name="button"> Shoot button </param>
        public void Shoot(Mouse.Button button)
        {
            //Prevent auto shooting if firemode is semi-auto
            if(_fireMode == FireMode.SEMI_AUTO && Inputs.IsPressed(button) && Inputs.IsSameState(button))
            {
                return;
            }

            //Check if weapon can shoot
            if (_fireTimeAcc < _fireRate || _ammo <= 0)
            {
                return;
            }

            //Reset time accumulator
            _fireTimeAcc = 0;

            //Remove one ammo
            _ammo--;

            //Play gun shot sound
            _shotSound.Play();

            //Set rays
            Vector2f playerMouseVec = Inputs.GetMousePosition(true) - Position;
            float rotation = (float)Math.Atan2(playerMouseVec.Y, playerMouseVec.X);
            if (_shotsRay.Length == 1)
            {
                _shotsRay[0] = new Ray(Position, rotation, _shotDistance);
            }
            else
            {
                float halfAngle = (_shotOffset * _shotsRay.Length) / 2.0f;
                for (int i = 0; i < _shotsRay.Length; i++)
                {
                    if (i == 0)
                    {
                        _shotsRay[i] = new Ray(Position, GameMath.Stay2PI(rotation - halfAngle), _shotDistance);
                        continue;
                    }

                    _shotsRay[i] = new Ray(Position, GameMath.Stay2PI(_shotsRay[i - 1].Rotation + _shotOffset), _shotDistance);
                }
            }

            //Add muzzle flash
            GetGameState().AddGameObj(new MuzzleFlash(_player, _muzzleFlashPosition.X, _muzzleFlashPosition.Y));

            //Detect shot collision
            foreach (GameObject obj in GetGameState().Objects)
            {
                if (obj.GetType() != typeof(Zombie))
                {
                    continue;
                }

                for (int i = 0; i < _shotsRay.Length; i++)
                {
                    if (CollisionDetection.AABB_RAY(obj.PhysicObject as AABB, _shotsRay[i], out Vector2f pNear, out Vector2f pFar, out Vector2f normal))
                    {
                        (obj as Zombie).Velocity = GameMath.NormalizeVector(pNear - Position) * 500;
                        (obj as Zombie).Health -= _dammage;
                    }
                }
            }
        }

        public bool Reload()
        {
            int toReload = _maxammo - _ammo;
            int totalReload = 0;

            if(toReload == 0)
            {
                return false;
            }

            switch (_ammoType)
            {
                case AmmoType.PISTOL:
                    if (_player.PistolAmmo >= toReload)
                    {
                        _player.PistolAmmo -= toReload;
                        _ammo += toReload;
                        totalReload = toReload;
                    }
                    else
                    {
                        _ammo += _player.PistolAmmo;
                        totalReload = _player.PistolAmmo;
                        _player.PistolAmmo -= _player.PistolAmmo;
                    }
                    break;
                case AmmoType.SHOTGUN:
                    if (_player.ShotgunAmmo >= toReload)
                    {
                        _player.ShotgunAmmo -= toReload;
                        _ammo += toReload;
                        totalReload = toReload;
                    }
                    else
                    {
                        _ammo += _player.ShotgunAmmo;
                        totalReload = _player.ShotgunAmmo;
                        _player.ShotgunAmmo -= _player.ShotgunAmmo;
                    }
                    break;
                case AmmoType.RIFLE:
                    if (_player.RifleAmmo >= toReload)
                    {
                        _player.RifleAmmo -= toReload;
                        _ammo += toReload;
                        totalReload = toReload;
                    }
                    else
                    {
                        _ammo += _player.RifleAmmo;
                        totalReload = _player.RifleAmmo;
                        _player.RifleAmmo -= _player.RifleAmmo;
                    }
                    break;
            }

            if(totalReload == 0)
            {
                return false;
            }

            return true;
        }
    }
}
