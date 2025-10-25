using UnityEngine;

namespace TZRpg.Data
{
    public static class DataManager
    {
        private static GameData gameData = new GameData();
        
        public static GameData GetGameData()
        {
            return gameData;
        }
        
        public static void SetSelectedCharacter(int id, string name)
        {
            gameData.SetSelectedCharacter(id, name);
        }
        
        public static void ClearSelection()
        {
            gameData.ClearSelection();
        }
    }
}
