using SFML_Engine;

namespace ZombiesGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game("Zombies");
            game.Start();
        }
    }
}
