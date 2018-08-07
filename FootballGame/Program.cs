using System;

namespace FootballGame
{
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
            using (MainGame.Instance = new MainGame())
                MainGame.Instance.Run();
        }
    }
}
