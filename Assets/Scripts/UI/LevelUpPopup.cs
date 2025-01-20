using System;
using System.Collections.Generic;
using Character;
using PlayerProfileSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public sealed class LevelUpPopup : UIScreen
    {
        [Inject]
        private PlayerProfile _playerProfile;
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _closeFadeBackButton;
        [SerializeField] private Button _levelUpButton;

        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private TMP_Text _levelCount;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _experienceCount;

        [SerializeField] private AvatarItem _avatar;
        
        [SerializeField] private StatItem _statItemPrefab;
        [SerializeField] private Transform _statsRoot;

        [SerializeField] private List<StatItem> _statItems;
        
        private void Start()
        {
            _closeButton.onClick.AddListener(() => _uiManager.CloseScreen(this));
            _closeFadeBackButton.onClick.AddListener(() => _uiManager.CloseScreen(this));
            _levelUpButton.onClick.AddListener(LevelUp);
        }

        private void OnEnable()
        {
            ReinitializePopUp();
        }

        private void ReinitializePopUp()
        {
            var currentCharacter = _playerProfile.CurrentCharacterProfile;
            
            _avatar.SetIcon(currentCharacter.CharacterInfoData.CharacterIcon);
            _characterName.text = currentCharacter.CharacterInfoData.CharacterName;
            _description.text = currentCharacter.CharacterInfoData.CharacterDescription;
            _levelCount.text = "Level: " + currentCharacter.CharacterLevel.CurrentLevel;
            _experienceCount.text = "XP: " + currentCharacter.CharacterLevel.CurrentExperience + 
                                    " / " + currentCharacter.CharacterLevel.RequiredExperience;

            ClearStatItems();
            SpawnStats(currentCharacter);
        }

        private void SpawnStats(CharacterProfile characterProfile)
        {
            _statItems = new List<StatItem>();

            foreach (var stat in characterProfile.CharacterStatsInfo.GetStats())
            {
                var statItem = Instantiate(_statItemPrefab, _statsRoot);
                
                statItem.SetStatData(stat);
                
                _statItems.Add(statItem);
            }
        }

        private void ClearStatItems()
        {
            if (_statItems is not { Count: > 0 }) return;
            
            foreach (var statItem in _statItems)
            {
                DestroyImmediate(statItem.gameObject);
            }
                
            _statItems.Clear();
        }
        
        private void LevelUp()
        {
            
        }
    }
}