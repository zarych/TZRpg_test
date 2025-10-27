using System.Collections.Generic;
using UnityEngine;
using TZRpg.Data;

namespace TZRpg.Character
{
    public class CharacterManager
    {
        private readonly List<CharacterData> availableCharacters = new();
        private CharacterData selectedCharacter;
        private GameObject currentCharacterInstance;

        public CharacterData SelectedCharacter => selectedCharacter;
        public bool HasSelectedCharacter => selectedCharacter != null;

        public CharacterManager() => LoadCharacterPrefabs();

        private void LoadCharacterPrefabs()
        {
            foreach (var prefab in Resources.LoadAll<GameObject>("Prefabs"))
            {
                if (!prefab.name.StartsWith("Chara_")) continue;

                if (int.TryParse(prefab.name.Substring(6), out int id))
                    availableCharacters.Add(new CharacterData($"Character {id:D2}", id, prefab));
            }
        }

        public GameObject GenerateRandomCharacter(Transform displayArea, UnityEngine.UI.Text displayText)
        {
            DestroyCurrentInstance();

            selectedCharacter = GetRandomCharacter();
            if (selectedCharacter == null || selectedCharacter.Prefab == null)
                return null;

            currentCharacterInstance = InstantiateCharacter(selectedCharacter.Prefab, displayArea);
            UpdateUIText(displayText, $"Selected: {selectedCharacter.Name}");
            return currentCharacterInstance;
        }

        public GameObject LoadFromSave(Transform displayArea, UnityEngine.UI.Text displayText)
        {
            var data = DataManager.GetGameData();
            if (data == null || !data.HasSelectedCharacter)
                return null;

            var prefab = Resources.Load<GameObject>($"Prefabs/Chara_{data.selectedCharacterId:D2}");
            if (prefab == null)
                return null;

            DestroyCurrentInstance();
            currentCharacterInstance = InstantiateCharacter(prefab, displayArea);
            UpdateUIText(displayText, $"Selected: {data.selectedCharacterName}");
            
            foreach (var charData in availableCharacters)
            {
                if (charData.Id == data.selectedCharacterId)
                {
                    selectedCharacter = charData;
                    break;
                }
            }
            
            return currentCharacterInstance;
        }

        public GameObject LoadSelectedCharacter(Transform spawnArea, UnityEngine.UI.Text infoText)
        {
            var data = DataManager.GetGameData();
            if (data == null || !data.HasSelectedCharacter)
            {
                Debug.LogWarning("No selected character data found!");
                return null;
            }

            var prefab = Resources.Load<GameObject>($"Prefabs/Chara_{data.selectedCharacterId:D2}");
            if (prefab == null)
            {
                Debug.LogError($"Could not load character from path: Prefabs/Chara_{data.selectedCharacterId:D2}");
                return null;
            }

            var character = InstantiateCharacter(prefab, spawnArea);
            UpdateUIText(infoText, $"Playing as: {data.selectedCharacterName}");
            return character;
        }

        private CharacterData GetRandomCharacter()
        {
            if (availableCharacters.Count == 0)
            {
                Debug.LogError("No characters available for selection!");
                return null;
            }

            return availableCharacters[Random.Range(0, availableCharacters.Count)];
        }

        private static GameObject InstantiateCharacter(GameObject prefab, Transform parent)
        {
            var instance = Object.Instantiate(prefab, parent);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = Vector3.one;
            return instance;
        }

        private static void UpdateUIText(UnityEngine.UI.Text text, string content)
        {
            if (text != null)
                text.text = content;
        }

        private void DestroyCurrentInstance()
        {
            if (currentCharacterInstance != null)
                Object.DestroyImmediate(currentCharacterInstance);
        }

        public List<CharacterData> GetAvailableCharacters() => new(availableCharacters);
        public void ClearSelection() => selectedCharacter = null;
    }
}
