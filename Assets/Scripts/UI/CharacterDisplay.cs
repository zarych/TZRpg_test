using UnityEngine;
using UnityEngine.UI;
using TZRpg.Character;

namespace TZRpg.UI
{
    public class CharacterDisplay : MonoBehaviour
    {
        [SerializeField] private Transform displayArea;
        [SerializeField] private Text characterInfoText;

        private GameObject currentInstance;
        private CharacterManager manager;

        public void Bind(CharacterManager manager)
        {
            this.manager = manager;
            manager.OnCharacterSelected += UpdateInfoText;
            manager.OnCharacterInstantiated += DisplayCharacter;
        }
        
        private void OnDestroy()
        {
            if (manager != null)
            {
                manager.OnCharacterSelected -= UpdateInfoText;
                manager.OnCharacterInstantiated -= DisplayCharacter;
            }
        }

        private void UpdateInfoText(string info)
        {
            if (characterInfoText != null)
                characterInfoText.text = info;
        }

        private void DisplayCharacter(GameObject prefab)
        {
            if (prefab == null) return;

            if (currentInstance != null)
                Destroy(currentInstance);

            currentInstance = Instantiate(prefab, displayArea);
            currentInstance.transform.localPosition = Vector3.zero;
            currentInstance.transform.localScale = Vector3.one;
        }
    }
}
