using System;
using Micro.Core;
using Micro.GameplayFoundation;
using Micro.Graphic;
using Micro.GUI;

namespace Demo.HelloWorld
{
    class Program
    {
        private class HelloWorldGame : Game
        {
            public HelloWorldGame(int width, int height)
                : base("HelloWorld", width, height)
            {
                var msgTextBlock = new TextBlock()
                {
                    Foreground = Color.White,
                    Font = new TrueTypeFont(Device, "Consolas", 14),
                    Text = "Hello, World"
                };
                SceneGraph.AddSprite(msgTextBlock);
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            var game = new HelloWorldGame(800, 600);
            game.Run();
        }
    }
}
