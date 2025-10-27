using UnityEngine;
using UnityEngine.UI;
using TZRpg.Data;
using TZRpg.Game;

namespace TZRpg.Game
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Text infoText;
        [SerializeField] private Transform spawnArea;

        private void Start()
        {
            if (backButton != null)
                backButton.onClick.AddListener(OnBackClicked);
            else
                Debug.LogError("Back button not assigned!");

            var data = DataManager.GetGameData();

            if (data == null || !data.HasSelectedCharacter)
            {
                SceneLoader.LoadCharacterSelector();
                return;
            }

            var prefab = Resources.Load<GameObject>($"Prefabs/Chara_{data.selectedCharacterId:D2}");
            if (prefab == null)
            {
                Debug.LogError($"Could not load prefab for Character {data.selectedCharacterId}");
                SceneLoader.LoadCharacterSelector();
                return;
            }

            var instance = Instantiate(prefab, spawnArea);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = Vector3.one;
            infoText.text = $"Playing as: {data.selectedCharacterName}";
        }

        private void OnBackClicked()
        {
            SceneLoader.LoadCharacterSelector();
        }
    }
}
