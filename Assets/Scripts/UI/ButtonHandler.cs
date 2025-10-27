using UnityEngine;
using UnityEngine.UI;
using TZRpg.Core.Events;

namespace TZRpg.UI
{
    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button generateButton;
        [SerializeField] private Button playButton;

        private void Start()
        {
            if (generateButton != null)
                generateButton.onClick.AddListener(UIEvents.RaiseGenerateCharacterRequested);
            else
                Debug.LogError("Generate button not assigned!");

            if (playButton != null)
                playButton.onClick.AddListener(UIEvents.RaisePlayButtonClicked);
            else
                Debug.LogError("Play button not assigned!");
        }
        
        private void OnDestroy()
        {
            if (generateButton != null)
                generateButton.onClick.RemoveListener(UIEvents.RaiseGenerateCharacterRequested);
            
            if (playButton != null)
                playButton.onClick.RemoveListener(UIEvents.RaisePlayButtonClicked);
        }
    }
}
