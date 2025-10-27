using UnityEngine.SceneManagement;

namespace TZRpg.Game
{
    public static class SceneLoader
    {
        public static void LoadGameScene()
        {
            SceneManager.LoadScene("Game");
        }

        public static void LoadCharacterSelector()
        {
            SceneManager.LoadScene("CharacterSelector");
        }
    }
}
