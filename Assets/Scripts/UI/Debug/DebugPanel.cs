using UnityEngine;
using UnityEngine.UI;

namespace UI.Debug
{
    public sealed class DebugPanel : UIScreen
    {
        [SerializeField] private LevelUpPopup _levelUpPopup;
        
        [SerializeField] private Button _showDebugButton;
        [SerializeField] private Button _hideDebugButton;
        [SerializeField] private Button _addExpButton;

        [SerializeField] private Transform _debugPanel;
        
        private void Start()
        {
            _showDebugButton.onClick.AddListener(() => _uiManager.ShowScreen(_debugPanel));
            _hideDebugButton.onClick.AddListener(() => _uiManager.CloseScreen(_debugPanel));
            _addExpButton.onClick.AddListener(AddExperience);
        }

        private void AddExperience()
        {
            _levelUpPopup.ViewModel.CharacterProfile.CharacterLevel.AddExperience(50);
        }
        
        
    }
}