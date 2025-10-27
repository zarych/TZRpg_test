using UnityEngine;

namespace TZRpg.Data
{
    [System.Serializable]
    public class GameData
    {
        public int selectedCharacterId = -1;
        public string selectedCharacterName = "";
        
        public bool HasSelectedCharacter => selectedCharacterId >= 0;
        
        public void SetSelectedCharacter(int id, string name)
        {
            selectedCharacterId = id;
            selectedCharacterName = name;
        }
        
        public void ClearSelection()
        {
            selectedCharacterId = -1;
            selectedCharacterName = "";
        }
    }
}
