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

        [SerializeField] private StatsView _statsView;

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
            InitializeStatsView(viewModel.StatsViewModel);
        }

        private void InitializeAvatarView(IAvatarViewModel avatarViewModel)
        {
            _avatarView.Initialize(avatarViewModel);
        }

        private void InitializeLevelView(IExperienceSliderViewModel viewModel)
        {
            _levelView.Initialize(viewModel);
        }
        
        private void InitializeStatsView(IStatsViewModel viewModel)
        {
            _statsView.Initialize(viewModel);
        }
        
        private void LevelUp()
        {
            _viewModel.LevelUp();
            _levelView.Initialize(_viewModel.ExperienceSliderViewModel);
        }
    }
}