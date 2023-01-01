namespace ZombiesGame
{
    class Shotgun : Weapon
    {
        public Shotgun()
        {
            //Set weapon properties
            _maxammo = 6;
            _ammo = 6;
            _fireMode = FireMode.SEMI_AUTO;
            _fireRate = 0.5f;
            _burstCooldown = 0.0f;
            _dammage = 50;
            _range = 1000;
            _ammoType = AmmoType.SHOTGUN;
        }
    }
}
