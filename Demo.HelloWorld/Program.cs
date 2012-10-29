using System;
using Micro.GameplayFoundation;

namespace Demo.HelloWorld
{
    class Program
    {
        private class HelloWorldGame : Game
        {
            public HelloWorldGame(int width, int height)
                : base("HelloWorld", width, height)
            {
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            var game = new HelloWorldGame(1024, 768);
            game.Run();
        }
    }
}
