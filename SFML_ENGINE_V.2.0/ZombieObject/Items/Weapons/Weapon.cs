using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;
using SFML.Window;

namespace ZombiesGame
{
    class Weapon : GameObject
    {
        //Properties
        protected int _maxammo;               //Weapon max ammo
        protected float _range;               //Weapon's range
        protected int _dammage;               //Weapon's dammage
        protected int _ammo;                  //Amount of ammo in current magazine
        protected FireMode _fireMode;         //Weapon's fire mode
        protected AmmoType _ammoType;         //Weapon's ammo type
        protected float _fireRate;            //Weapon's cooldown
        protected float _burstCooldown;       //Burst shot cooldown
        int _burstCount;                      //Count number of burst shot
        bool _burst;                          //Indicate if weapon is actually shooting in burst mode

        //Time accumulators
        float _timeAcc = 0;
        float _burstAcc = 0;

        //enum
        protected enum FireMode
        {
            SEMI_AUTO,
            AUTO,
            BURST
        }

        protected enum AmmoType
        {
            PISTOL,
            RIFLE,
            SHOTGUN
        }

        /// <summary>
        /// Get max ammo
        /// </summary>
        public int MaxAmmo { get => _maxammo; }

        /// <summary>
        /// Make the weapon shoot
        /// </summary>
        /// <param name="player"> Player using this weapon </param>
        public void Shoot(Player player)
        {
            if(_timeAcc < _fireRate)
            {
                _timeAcc += GameTime.DeltaTimeU;
            }

            if (_ammo > 0)
            {
                switch (_fireMode)
                {
                    case FireMode.SEMI_AUTO: SEMI_AUTO_SHOT(player); break;
                    case FireMode.AUTO: AUTO_SHOT(player); break;
                    case FireMode.BURST: BURST_SHOT(player); break;
                }
            }
        }

        void SEMI_AUTO_SHOT(Player p)
        {
            if (Inputs.IsClicked(Mouse.Button.Left) && _timeAcc >= _fireRate)
            {
                GameState.AddGameObj(new Bullet(p.Position, Inputs.GetMousePosition(true) - p.Position, _range, _dammage));
                AddMuzzleFlash(p);
                _timeAcc = 0;
                _ammo--;
            }
        }

        void AUTO_SHOT(Player p)
        {
            if (Inputs.IsPressed(Mouse.Button.Left) && _timeAcc >= _fireRate)
            {
                GameState.AddGameObj(new Bullet(p.Position, Inputs.GetMousePosition(true) - p.Position, _range, _dammage));
                AddMuzzleFlash(p);
                _timeAcc = 0;
                _ammo--;
            }
        }

        void BURST_SHOT(Player p)
        {
            if (!_burst && Inputs.IsClicked(Mouse.Button.Left) && _timeAcc >= _fireRate)
            {
                _burst = true;
                _burstCount = 0;
            }

            if (_burst)
            {
                if(_burstCount < 3)
                {
                    _burstAcc += GameTime.DeltaTimeU;

                    if (_burstAcc >= _burstCooldown)
                    {
                        _burstAcc = 0;
                        GameState.AddGameObj(new Bullet(p.Position, Inputs.GetMousePosition(true) - p.Position, _range, _dammage));
                        AddMuzzleFlash(p);
                        _burstCount++;
                        _ammo--;
                    }
                }
                else
                {
                    _burst = false;
                    _timeAcc = 0;
                }
            }
        }

        /// <summary>
        /// Reload weapon
        /// </summary>
        /// <param name="amount"> Amount to reload </param>
        /// <param name="excess"> Excess ammo </param>
        /// <returns> True if reload </returns>
        public bool Reload(Player p)
        {
            int toReload = _maxammo - _ammo;
            int totalReload = 0;

            if (toReload == 0)
            {
                return false;
            }

            switch (_ammoType)
            {
                case AmmoType.PISTOL:
                    if (p.PistolBullet >= toReload)
                    {
                        p.PistolBullet -= toReload;
                        _ammo += toReload;
                        totalReload = toReload;
                    }
                    else
                    {
                        _ammo += p.PistolBullet;
                        totalReload = p.PistolBullet;
                        p.PistolBullet -= p.PistolBullet;
                    }
                    break;
                case AmmoType.SHOTGUN:
                    if (p.Shell >= toReload)
                    {
                        p.Shell -= toReload;
                        _ammo += toReload;
                        totalReload = toReload;
                    }
                    else
                    {
                        _ammo += p.Shell;
                        totalReload = p.Shell;
                        p.Shell -= p.Shell;
                    }
                    break;
                case AmmoType.RIFLE:
                    if (p.RifleBullet >= toReload)
                    {
                        p.RifleBullet -= toReload;
                        _ammo += toReload;
                        totalReload = toReload;
                    }
                    else
                    {
                        _ammo += p.RifleBullet;
                        totalReload = p.RifleBullet;
                        p.RifleBullet -= p.RifleBullet;
                    }
                    break;
            }

            if (totalReload == 0)
            {
                return false;
            }

            return true;
        }

        void AddMuzzleFlash(Player p)
        {
            Vector2f _muzzleFlashPosition;
            if (_ammoType == AmmoType.PISTOL)
            {
                _muzzleFlashPosition = new Vector2f(100, 0);
            }
            else
            {
                _muzzleFlashPosition = new Vector2f(100, -25);
            }
            GameState.AddGameObj(new MuzzleFlash(p, _muzzleFlashPosition.X, _muzzleFlashPosition.Y));
        }
    }
}
