using UnityEngine;
using TZRpg.Character;
using TZRpg.UI;
using TZRpg.Data;

namespace TZRpg
{
    public class MainInitializer : MonoBehaviour
    {
        [SerializeField] private CharacterDisplay characterDisplay;

        private CharacterManager characterManager;
        private CharacterSelectionController selectionController;

        private void Awake()
        {
            DataManager.LoadFromPlayerPrefs();

            characterManager = new CharacterManager();
            selectionController = new CharacterSelectionController(characterManager);

            if (characterDisplay != null)
                characterDisplay.Bind(characterManager);
            else
                Debug.LogError("CharacterDisplay reference missing in MainInitializer!");
        }

        private void OnDestroy()
        {
            selectionController.Dispose();
        }
    }
}
