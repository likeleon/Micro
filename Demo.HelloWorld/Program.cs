using System;
using Micro.GameplayFoundation;
using Micro.Graphic;

namespace Demo.HelloWorld
{
    class Program
    {
        private class HelloWorldGame : Game
        {
            private readonly TrueTypeFont font;

            public HelloWorldGame(int width, int height)
                : base("HelloWorld", width, height)
            {
                this.font = new TrueTypeFont(Device, "Consolas", 12);
            }

            protected override void Draw(float elapsed)
            {
                base.Draw(elapsed);
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
