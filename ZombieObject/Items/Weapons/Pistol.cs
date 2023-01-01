namespace ZombiesGame
{
    class Pistol : Weapon
    {
        public Pistol()
        {
            //Set weapon properties
            _maxammo = 7;
            _ammo = 7;
            _fireMode = FireMode.SEMI_AUTO;
            _fireRate = 0.3f;
            _burstCooldown = 0.0f;
            _dammage = 20;
            _range = 2000;
            _ammoType = AmmoType.PISTOL;
        }
    }
}
