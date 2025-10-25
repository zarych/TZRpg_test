using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TZRpg.Data;
using TZRpg.Character;

public class ButtonHandler : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private Button generateButton;
    [SerializeField] private Button playButton;

    [Header("Character Display")]
    [SerializeField] private Transform characterDisplayArea;
    [SerializeField] private UnityEngine.UI.Text characterDisplayText;

    private CharacterManager characterManager;
    private GameObject currentCharacter;

    private void Start()
    {
        characterManager = new CharacterManager();

        if (generateButton != null)
            generateButton.onClick.AddListener(OnGenerateClicked);
        else
            Debug.LogError("Generate button not assigned!");

        if (playButton != null)
            playButton.onClick.AddListener(OnPlayClicked);
        else
            Debug.LogError("Play button not assigned!");
    }

    private void OnGenerateClicked()
    {
        currentCharacter = characterManager.GenerateRandomCharacter(characterDisplayArea, characterDisplayText);
    }

    private void OnPlayClicked()
    {
        if (characterManager.HasSelectedCharacter)
        {
            var data = characterManager.SelectedCharacter;
            DataManager.SetSelectedCharacter(data.Id, data.Name);
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.LogWarning("No character selected before play!");
        }
    }
}
