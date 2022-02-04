using System;
using TobEngine.GameLoop;
using TobEngine.Testing;

namespace TobEngine
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game game = new TestGame(800, 600, "Test");
            game.Run();
        }
    }
}
