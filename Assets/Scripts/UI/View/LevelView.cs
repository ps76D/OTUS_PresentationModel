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
    public class LevelView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _levelCount;
        [SerializeField] private TMP_Text _experienceCount;
        [SerializeField] private Slider _expSlider;
        
        private CharacterProfile _characterProfile;

        private IExperienceSliderViewModel _viewModel;
        
        private readonly List<IDisposable> _disposables = new();
        
        public void Initialize(IExperienceSliderViewModel viewModel)
        {
            _viewModel = viewModel;
            _characterProfile = _viewModel.CharacterProfile;
            
            UpdateExpDataAndSliderValue(0);
            
            _disposables.Add(_characterProfile.CharacterLevel.CurrentExperience.Subscribe(UpdateExpDataAndSliderValue));
        }
        
        private void SetExpSliderValue()
        {
            _expSlider.value = _viewModel.ExpSliderValue;
        }
        
        private void SetExperienceTexts()
        { 
            _levelCount.text = _viewModel.LevelCount;
            _experienceCount.text = _viewModel.ExperienceCount;
        }

        private void UpdateExpDataAndSliderValue(int value)
        {
            SetExperienceTexts();
            SetExpSliderValue();
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}