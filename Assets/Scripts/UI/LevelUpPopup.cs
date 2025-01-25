using System;
using System.Collections.Generic;
using TMPro;
using UI.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] private List<StatItem> _statItems = new();
        
        private readonly List<IDisposable> _disposables = new();
        
        private ILevelUpPopupModel _viewModel;
        public ILevelUpPopupModel ViewModel => _viewModel;
        
        public void Show(ILevelUpPopupModel viewModel)
        {
            _viewModel = viewModel;
            gameObject.SetActive(true);
            
            _closeButton.onClick.AddListener(Hide);
            _closeFadeBackButton.onClick.AddListener(Hide);
            _levelUpButton.onClick.AddListener(LevelUp);
            
            _disposables.Add(viewModel.LevelUpButtonIsInteractable.SubscribeToInteractable(_levelUpButton));
            _disposables.Add(viewModel.CharacterProfile.CharacterLevel.CurrentExperience.
                Subscribe(UpdateExpDataAndSliderValue));
            
            _disposables.Add(viewModel.CharacterProfile.CharacterInfo.Name.Subscribe(InitializeTexts));
            _disposables.Add(viewModel.CharacterProfile.CharacterInfo.Description.Subscribe(InitializeTexts));
            _disposables.Add(viewModel.CharacterProfile.CharacterInfo.Icon.Subscribe(InitializeIcon));
            
            ReinitializePopUp(viewModel);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            
            _closeButton.onClick.RemoveListener(Hide);
            _closeFadeBackButton.onClick.RemoveListener(Hide);
            _levelUpButton.onClick.RemoveListener(LevelUp);
            
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
        
        private void ReinitializePopUp(ILevelUpPopupModel viewModel)
        {
            InitializeIcon(viewModel);
            InitializeTexts(viewModel);
            
            UpdateExperience(viewModel);
            SetExpSliderValue(viewModel.ExpSliderValue);

            ClearStatItems();
            SpawnStats(viewModel);
        }

        private void InitializeIcon(ILevelUpPopupModel viewModel)
        {
            _avatar.SetIcon(viewModel.Icon);
        }
        
        private void InitializeIcon(Sprite value)
        {
            InitializeIcon(_viewModel);
        }
        
        private void InitializeTexts(ILevelUpPopupModel viewModel)
        {
            _characterName.text = viewModel.Name;
            _description.text = viewModel.Description;
        }

        private void InitializeTexts(string value)
        {
            InitializeTexts(_viewModel);
        }
        
        private void UpdateExperience(ILevelUpPopupModel viewModel)
        {
            _levelCount.text = viewModel.LevelCount;
            _experienceCount.text = viewModel.ExperienceCount;
        }

        private void SpawnStats(ILevelUpPopupModel viewModel)
        {
            foreach (var statViewModel in viewModel.StatItemModels)
            {
                var statItem = Instantiate(_statItemPrefab, _statsRoot);

                statItem.Initialize(statViewModel);
                
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
            SetExpSliderValue(_viewModel.ExpSliderValue);
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