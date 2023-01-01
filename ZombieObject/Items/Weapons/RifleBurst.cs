namespace ZombiesGame
{
    class RifleBurst : Weapon
    {
        public RifleBurst()
        {
            //Set weapon properties
            _maxammo = 30;
            _ammo = 30;
            _fireMode = FireMode.BURST;
            _fireRate = 1f;
            _burstCooldown = 0.2f;
            _dammage = 25;
            _range = 2000;
            _ammoType = AmmoType.RIFLE;
        }
    }
}
