using System.Collections.Generic;

namespace LightMaze._Scripts.SceneLoader
{
    public class GameScenes
    {
        public readonly string SplashScreen = "SplashScreen";
        public readonly string MainMenu = "MainMenu";
        public readonly string UiBackground = "UiBackground";
        
        public readonly IReadOnlyList<string> Levels = new List<string>()
        {
            "Level_01", "Level_02", "Level_03", "Level_04"
        }.AsReadOnly();
    }
}