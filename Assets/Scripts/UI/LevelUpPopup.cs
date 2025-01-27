using System;
using System.Collections.Generic;
using UI.Model;
using UI.View;
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
        
        [SerializeField] private AvatarView _avatarView;
        [SerializeField] private LevelView _levelView;

        [SerializeField] private StatItemView _statItemViewPrefab;
        [SerializeField] private Transform _statsRoot;

        [SerializeField] private List<StatItemView> _statItems = new();

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
            
            _disposables.Add(viewModel.CharacterProfile.CharacterInfo.Name.Subscribe(UpdateTexts));
            _disposables.Add(viewModel.CharacterProfile.CharacterInfo.Description.Subscribe(UpdateTexts));
            _disposables.Add(viewModel.CharacterProfile.CharacterInfo.Icon.Subscribe(UpdateIcon));
            
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
            InitializeAvatarView(viewModel.AvatarViewModel);
            InitializeLevelView(viewModel.ExperienceSliderViewModel);

            ClearStatItems();
            SpawnStats(viewModel);
        }

        private void InitializeAvatarView(IAvatarViewModel avatarViewModel)
        {
            _avatarView.Initialize(avatarViewModel);
        }

        private void InitializeLevelView(IExperienceSliderViewModel viewModel)
        {
            _levelView.Initialize(viewModel);
        }

        private void UpdateIcon(Sprite sprite)
        {
            _avatarView.Initialize(_viewModel.AvatarViewModel);
        }

        private void UpdateTexts(string value)
        {
            _avatarView.Initialize(_viewModel.AvatarViewModel);
        }

        private void UpdateExpDataAndSliderValue(int value)
        {
            _levelView.Initialize(_viewModel.ExperienceSliderViewModel);
        }

        private void SpawnStats(ILevelUpPopupModel viewModel)
        {
            int index;
            for (index = 0; index < viewModel.StatItemModels.Count; index++)
            {
                IStatItemModel statViewModel = viewModel.StatItemModels[index];
                var statItem = Instantiate(_statItemViewPrefab, _statsRoot);

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
        
        private void LevelUp()
        {
            _viewModel.LevelUp();
            _levelView.Initialize(_viewModel.ExperienceSliderViewModel);
        }
    }
}