using OpenGl3d.Infrastructure.Shapes;

namespace OpenGl3d
{
    public static class Program
    {
        public static void Main()
        {
            using var game = new Game(800, 600, "Test", new CubeWrapper(), new Camera());
            game.Run();
        }
    }
} 