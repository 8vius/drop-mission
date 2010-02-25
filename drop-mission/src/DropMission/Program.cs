using System;

namespace DropMission
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DropMissionGame game = new DropMissionGame())
            {
                game.Run();
            }
        }
    }
}

