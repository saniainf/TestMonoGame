using System;

namespace Monocle
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Engine(800, 600, 1, "asdf", false))
                game.Run();
        }
    }
#endif
}
