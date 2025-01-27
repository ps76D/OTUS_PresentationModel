using System;
using System.Collections.Generic;
using Character;
using TMPro;
using UI.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class AvatarView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;
        
        private CharacterProfile _characterProfile;
        private IAvatarViewModel _avatarViewModel;
        
        private readonly List<IDisposable> _disposables = new();
        
        public void Initialize(IAvatarViewModel viewModel)
        {
            _avatarViewModel = viewModel;
            _characterProfile = _avatarViewModel.CharacterProfile;
            
            SetIcon(viewModel);
            SetTexts(viewModel);
            
            _disposables.Add(_characterProfile.CharacterInfo.Name.Subscribe(UpdateTexts));
            _disposables.Add(_characterProfile.CharacterInfo.Description.Subscribe(UpdateTexts));
            _disposables.Add(_characterProfile.CharacterInfo.Icon.Subscribe(UpdateIcon));
        }

        private void SetIcon(IAvatarViewModel viewModel)
        {
            _icon.sprite = viewModel.Icon;
        }

        private void SetTexts(IAvatarViewModel avatarViewModel)
        {
            _characterName.text = avatarViewModel.Name;
            _description.text = avatarViewModel.Description;
        }
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
        
        private void UpdateIcon(Sprite sprite)
        {
            SetIcon(_avatarViewModel);
        }

        private void UpdateTexts(string value)
        {
            SetTexts(_avatarViewModel);
        }
    }
}