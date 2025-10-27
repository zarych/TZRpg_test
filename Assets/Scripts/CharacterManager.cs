using System.Collections.Generic;
using UnityEngine;
using TZRpg.Data;
using System.Linq;

namespace TZRpg.Character
{
    public class CharacterManager
    {
        private static readonly Dictionary<int, GameObject> cachedPrefabs = new();
        
        private CharacterData selectedCharacter;
        private GameObject currentCharacterInstance;
        private int lastCharacterId = -1;

        public CharacterData SelectedCharacter => selectedCharacter;
        public bool HasSelectedCharacter => selectedCharacter != null;

        public CharacterManager() { }
        
        public void ShowCharacterIfInstantiated(Transform displayArea)
        {
            if (currentCharacterInstance != null && currentCharacterInstance.activeSelf == false)
            {
                currentCharacterInstance.transform.SetParent(displayArea);
                currentCharacterInstance.transform.localPosition = Vector3.zero;
                currentCharacterInstance.transform.localScale = Vector3.one;
                currentCharacterInstance.SetActive(true);
            }
        }

        public GameObject GenerateRandomCharacter(Transform displayArea, UnityEngine.UI.Text displayText)
        {
            selectedCharacter = GetRandomCharacter();
            if (selectedCharacter == null || selectedCharacter.Prefab == null)
                return null;

            bool isDifferentCharacter = selectedCharacter.Id != lastCharacterId;
            
            if (currentCharacterInstance != null && isDifferentCharacter)
            {
                DestroyCurrentInstance();
                currentCharacterInstance = InstantiateCharacter(selectedCharacter.Prefab, displayArea);
            }
            else if (currentCharacterInstance != null)
            {
                currentCharacterInstance.transform.SetParent(displayArea);
                currentCharacterInstance.transform.localPosition = Vector3.zero;
                currentCharacterInstance.transform.localScale = Vector3.one;
            }
            else
            {
                currentCharacterInstance = InstantiateCharacter(selectedCharacter.Prefab, displayArea);
            }
            
            lastCharacterId = selectedCharacter.Id;
            UpdateUIText(displayText, $"Selected: {selectedCharacter.Name}");
            return currentCharacterInstance;
        }

        public GameObject LoadFromSave(Transform displayArea, UnityEngine.UI.Text displayText)
        {
            var data = DataManager.GetGameData();
            if (data == null || !data.HasSelectedCharacter)
                return null;

            selectedCharacter = LoadCharacterById(data.selectedCharacterId);
            if (selectedCharacter == null)
                return null;

            if (currentCharacterInstance != null)
            {
                currentCharacterInstance.transform.SetParent(displayArea);
                currentCharacterInstance.transform.localPosition = Vector3.zero;
                currentCharacterInstance.transform.localScale = Vector3.one;
            }
            else
            {
                currentCharacterInstance = InstantiateCharacter(selectedCharacter.Prefab, displayArea);
            }
            
            UpdateUIText(displayText, $"Selected: {data.selectedCharacterName}");
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

            var characterData = LoadCharacterById(data.selectedCharacterId);
            if (characterData == null)
                return null;

            var character = InstantiateCharacter(characterData.Prefab, spawnArea);
            UpdateUIText(infoText, $"Playing as: {data.selectedCharacterName}");
            return character;
        }

        private CharacterData GetRandomCharacter()
        {
            int randomId = Random.Range(0, 16);
            return LoadCharacterById(randomId);
        }
        
        private CharacterData LoadCharacterById(int id)
        {
            if (!cachedPrefabs.TryGetValue(id, out GameObject prefab))
            {
                prefab = Resources.Load<GameObject>($"Prefabs/Chara_{id:D2}");
                if (prefab == null)
                {
                    Debug.LogError($"Could not load character with ID: {id}");
                    return null;
                }
                cachedPrefabs[id] = prefab;
            }
            
            return new CharacterData($"Character {id:D2}", id, prefab);
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

        public void ClearSelection() => selectedCharacter = null;
    }
}
