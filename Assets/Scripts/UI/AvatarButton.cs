﻿using Character;
using UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class AvatarButton: MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        
        [SerializeField] private Image _icon;

        [SerializeField] private Button _button;
        
        public CharacterProfile CurrentCharacterProfile { get; set; }
        
        public UIManager UiManage 
        {
            set => _uiManager = value;
        }

        private void Start()
        {
            if (_button)
            {
                _button.onClick.AddListener(OpenLevelUpPopup);
            }
        }

        private void OnDestroy()
        {
            if (_button)
            {
                _button.onClick.RemoveListener(OpenLevelUpPopup);
            }
        }
    
        private void OpenLevelUpPopup()
        {
            var viewModel = new LevelUpPopupModel(CurrentCharacterProfile);
            _uiManager.LevelUpPopup.Show(viewModel);
        }

        public void SetSpriteIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}