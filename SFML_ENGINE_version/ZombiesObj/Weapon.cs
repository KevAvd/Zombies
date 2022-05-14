using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects;
using SFML_Engine.Systems;
using SFML_Engine.Enums;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML.System;
using SFML.Window;

namespace ZombiesGame
{
    abstract class Weapon : ScriptObject
    {
        //Instance of player
        protected Player _player;

        //Weapon's property
        protected float _dammage;
        protected float _fireRate;
        float _fireTimeAcc = 0;
        protected FireMode _fireMode;
        protected WeaponState _weaponState;
        protected int _rounds;
        
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

                    switch (_fireMode)
                    {
                        case FireMode.AUTO:
                            if(Inputs.IsPressed(Mouse.Button.Left) && _fireTimeAcc >= _fireRate)
                            {
                                Shoot();
                                _fireTimeAcc = 0;
                            }
                            break;
                        case FireMode.SEMI_AUTO:
                            if (Inputs.IsClicked(Mouse.Button.Left) && _fireTimeAcc >= _fireRate)
                            {
                                Shoot();
                                _fireTimeAcc = 0;
                            }
                            break;
                        case FireMode.BURST:
                            //TO DO: implement a way of handling busrt shot
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Make the weapon shoot
        /// </summary>
        protected abstract void Shoot();
    }
}
