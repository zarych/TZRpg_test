using System;
using System.Collections.Generic;
using UnityEngine;

namespace TZRpg.Character
{
    public class CharacterManager
    {
        private static readonly Dictionary<int, GameObject> cachedPrefabs = new();
        private CharacterData selectedCharacter;

        public CharacterData SelectedCharacter => selectedCharacter;
        public bool HasSelectedCharacter => selectedCharacter != null;

        public event Action<string> OnCharacterSelected;
        public event Action<GameObject> OnCharacterInstantiated;

        public void GenerateRandomCharacter()
        {
            selectedCharacter = GetRandomCharacter();
            if (selectedCharacter == null)
            {
                Debug.LogWarning("No valid character found!");
                return;
            }

            OnCharacterSelected?.Invoke($"Selected: {selectedCharacter.Name}");
            OnCharacterInstantiated?.Invoke(selectedCharacter.Prefab);
        }

        private CharacterData GetRandomCharacter()
        {
            int randomId = UnityEngine.Random.Range(0, 16);
            return LoadCharacterById(randomId);
        }

        private CharacterData LoadCharacterById(int id)
        {
            if (!cachedPrefabs.TryGetValue(id, out GameObject prefab))
            {
                prefab = Resources.Load<GameObject>($"Prefabs/Chara_{id:D2}");
                if (prefab == null)
                {
                    Debug.LogError($"Could not load character prefab with ID {id}");
                    return null;
                }
                cachedPrefabs[id] = prefab;
            }
            return new CharacterData($"Character {id:D2}", id, prefab);
        }
    }
}
