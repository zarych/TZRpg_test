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
            PlayerPrefs.SetInt("SelectedCharacterId", id);
            PlayerPrefs.SetString("SelectedCharacterName", name);
            PlayerPrefs.Save();
        }
        
        public static void ClearSelection()
        {
            gameData.ClearSelection();
            PlayerPrefs.DeleteKey("SelectedCharacterId");
            PlayerPrefs.DeleteKey("SelectedCharacterName");
            PlayerPrefs.Save();
        }
        
        public static void LoadFromPlayerPrefs()
        {
            int id = PlayerPrefs.GetInt("SelectedCharacterId", -1);
            string name = PlayerPrefs.GetString("SelectedCharacterName", "");
            
            if (id >= 0 && !string.IsNullOrEmpty(name))
            {
                gameData.SetSelectedCharacter(id, name);
            }
        }
    }
}
