using ZombiesGame.GameObjects.Items;
using ZombiesGame.Enums;

namespace ZombiesGame.GameObjects
{
    internal class Inventory
    {
        //Constant
        const int MAX_PISTOL_AMMO = 800;
        const int MAX_RIFLE_AMMO = 500;
        const int MAX_SHOTGUN_AMMO = 100;

        //Ammunition
        int _ammo_Pistol;
        int _ammo_Rifle;
        int _ammo_Shotgun;

        //Weapons
        Firearm _firearm1;
        Firearm _firearm2;
        Melee _melee;

        //States
        WeaponState _weaponState;

        //Enums
        enum WeaponState
        {
            FIREARM1,
            FIREARM2,
            Melee,
        }

        /// <summary>
        /// Get/Set firearm 1
        /// </summary>
        internal Firearm Firearm1 { get => _firearm1; set => _firearm1 = value; }

        /// <summary>
        /// Get/Set firearm 2
        /// </summary>
        internal Firearm Firearm2 { get => _firearm2; set => _firearm2 = value; }

        /// <summary>
        /// Get/Set melee weapon
        /// </summary>
        internal Melee Melee { get => _melee; set => _melee = value; }

        /// <summary>
        /// Cons
        /// </summary>
        public Inventory()
        {
            _weaponState = WeaponState.FIREARM1;
        }

        /// <summary>
        /// Get current weapon
        /// </summary>
        /// <returns> Current weapon </returns>
        public Weapon GetCurrentWeapon()
        {
            switch (_weaponState)
            {
                case WeaponState.FIREARM1: return _firearm1;
                case WeaponState.FIREARM2: return _firearm2;
                case WeaponState.Melee: return _melee;
            }

            return _firearm1;
        }

        /// <summary>
        /// Add ammo to inventory
        /// </summary>
        /// <param name="type"> Type of ammo </param>
        /// <param name="amount"> Amount to add </param>
        public void AddAmmo(AmmoType type, int amount)
        {
            switch (type)
            {
                case AmmoType.PISTOL:
                    _ammo_Pistol += amount;
                    if(_ammo_Pistol > MAX_PISTOL_AMMO) { _ammo_Pistol = MAX_PISTOL_AMMO; }
                    break;
                case AmmoType.RIFLE:
                    _ammo_Rifle += amount;
                    if (_ammo_Rifle > MAX_RIFLE_AMMO) { _ammo_Rifle = MAX_RIFLE_AMMO; }
                    break;
                case AmmoType.SHOTGUN:
                    _ammo_Shotgun += amount;
                    if (_ammo_Shotgun > MAX_SHOTGUN_AMMO) { _ammo_Shotgun = MAX_SHOTGUN_AMMO; }
                    break;
            }
        }
    }
}
