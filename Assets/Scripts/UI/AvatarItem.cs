using System;
using Character;
using PlayerProfileSystem;
using UI.Model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public sealed class AvatarItem: MonoBehaviour
    {
        [SerializeField] private PlayerProfile _playerProfile;
        
        [SerializeField] private UIManager _uiManager;
        
        [SerializeField] private Image _icon;

        [SerializeField] private Button _button;
        
        /*[SerializeField] private CharacterProfile _currentCharacterProfile;*/
        
        /*public PlayerProfile PlayerProfile {
            set => _playerProfile = value;
        }*/
        public CharacterProfile CurrentCharacterProfile { get; set; }
        
        public UIManager UiManage {
            set => _uiManager = value;
        }

        private void Start()
        {
            if (_button)
            {
                _button.onClick.AddListener(() => OpenLevelUpPopup(_uiManager.LevelUpPopup));
            }
        }

        private void OpenLevelUpPopup(UIScreen screen)
        {
            /*_playerProfile.CurrentCharacterProfile = CurrentCharacterProfile;*/
            
            var viewModel = new LevelUpPopupModel(CurrentCharacterProfile);
            _uiManager.LevelUpPopup.Show(viewModel);
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}