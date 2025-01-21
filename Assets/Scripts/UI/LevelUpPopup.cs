using System;
using System.Collections.Generic;
using Character;
using PlayerProfileSystem;
using TMPro;
using UI.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public sealed class LevelUpPopup : UIScreen
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _closeFadeBackButton;
        [SerializeField] private Button _levelUpButton;

        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private TMP_Text _levelCount;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _experienceCount;

        [SerializeField] private Slider _expSlider;
        [SerializeField] private AvatarItem _avatar;
        
        [SerializeField] private StatItem _statItemPrefab;
        [SerializeField] private Transform _statsRoot;

        [SerializeField] private List<StatItem> _statItems;
        
        private readonly List<IDisposable> _disposables = new();
        
        private ILevelUpPopupModel _viewModel;
        
        public void Show(ILevelUpPopupModel viewModel)
        {
            _viewModel = viewModel;
            gameObject.SetActive(true);
            
            _closeButton.onClick.AddListener(Hide);
            _closeFadeBackButton.onClick.AddListener(Hide);
            _levelUpButton.onClick.AddListener(LevelUp);
            
            _disposables.Add(viewModel.LevelUpButtonIsInteractable.SubscribeToInteractable(_levelUpButton));
            _disposables.Add(viewModel.CharacterProfile.CharacterLevel.CurrentExperience.Subscribe(UpdateExpDataAndSliderValue));
            
            ReinitializePopUp(viewModel);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            
            _closeButton.onClick.RemoveListener(() => _uiManager.CloseScreen(this));
            _closeFadeBackButton.onClick.RemoveListener(() => _uiManager.CloseScreen(this));
            _levelUpButton.onClick.RemoveListener(LevelUp);
            
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
        
        private void ReinitializePopUp(ILevelUpPopupModel viewModel)
        {
            _avatar.SetIcon(viewModel.Icon);
            _characterName.text = viewModel.Name;
            _description.text = viewModel.Description;
            _levelCount.text = viewModel.LevelCount;
            _experienceCount.text = viewModel.ExperienceCount;
            
            SetExpSliderValue(viewModel.ExpSliderValue.Value);

            ClearStatItems();
            
            SpawnStats(viewModel.CharacterProfile);
        }

        private void UpdateExperience(ILevelUpPopupModel viewModel)
        {
            _levelCount.text = viewModel.LevelCount;
            _experienceCount.text = viewModel.ExperienceCount;
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
        private void UpdateExpDataAndSliderValue(int value)
        {
            UpdateExperience(_viewModel);
            SetExpSliderValue(_viewModel.ExpSliderValue.Value);
        }
        private void SetExpSliderValue(float value)
        {
            _expSlider.value = value;
        }
        
        private void LevelUp()
        {
            _viewModel.LevelUp();
            UpdateExperience(_viewModel);
        }
    }
}