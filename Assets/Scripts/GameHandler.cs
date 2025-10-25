using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TZRpg.Character;

public class GameHandler : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button backButton;
    [SerializeField] private UnityEngine.UI.Text characterInfoText;
    [SerializeField] private Transform characterSpawnArea;

    private CharacterManager characterManager;

    private void Start()
    {
        characterManager = new CharacterManager();

        if (backButton != null)
            backButton.onClick.AddListener(OnBackClicked);
        else
            Debug.LogError("Back button not assigned!");

        GameObject spawnedCharacter = characterManager.LoadSelectedCharacter(characterSpawnArea, characterInfoText);

        if (spawnedCharacter == null)
            SceneManager.LoadScene("CharacterSelector");
    }

    private void OnBackClicked()
    {
        SceneManager.LoadScene("CharacterSelector");
    }
}
