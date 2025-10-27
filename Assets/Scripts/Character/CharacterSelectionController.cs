using TZRpg.Core.Events;
using TZRpg.Data;
using TZRpg.Game;
using UnityEngine;

namespace TZRpg.Character
{
    public class CharacterSelectionController
    {
        private readonly CharacterManager manager;

        public CharacterSelectionController(CharacterManager characterManager)
        {
            manager = characterManager;
            UIEvents.GenerateCharacterRequested += OnGenerateCharacter;
            UIEvents.PlayButtonClicked += OnPlayClicked;
        }

        private void OnGenerateCharacter() => manager.GenerateRandomCharacter();

        private void OnPlayClicked()
        {
            if (!manager.HasSelectedCharacter)
            {
                Debug.LogWarning("No character selected before play!");
                return;
            }

            var data = manager.SelectedCharacter;
            DataManager.SetSelectedCharacter(data.Id, data.Name);

            SceneLoader.LoadGameScene();
        }

        public void Dispose()
        {
            UIEvents.GenerateCharacterRequested -= OnGenerateCharacter;
            UIEvents.PlayButtonClicked -= OnPlayClicked;
        }
    }
}
