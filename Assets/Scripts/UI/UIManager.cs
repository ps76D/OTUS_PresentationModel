using System;
using UI.Debug;
using UnityEngine;

namespace UI
{
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField] private SelectCharacterScreen _selectCharacterScreen;
        [SerializeField] private LevelUpPopup _levelUpPopup;
        
        [SerializeField] private DebugPanel _debugPanel;

        public LevelUpPopup LevelUpPopup => _levelUpPopup;

        public void ShowScreen(Component screen)
        {
            screen.gameObject.SetActive(true);
        }

        public void CloseScreen(Component screen)
        {
            screen.gameObject.SetActive(false);
        }
    }
}