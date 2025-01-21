using System;
using System.Collections.Generic;
using Character;
using UniRx;
using UnityEngine;

namespace UI.Model
{
    public class LevelUpPopupModel: ILevelUpPopupModel
    {
        public string Name => CharacterProfile.CharacterInfo.Name;
        public string Description => CharacterProfile.CharacterInfo.Description;
        public string LevelCount => SetLevelCount(CharacterProfile);
        public string ExperienceCount => SetExperienceCount(CharacterProfile);
        public IReactiveProperty<float> ExpSliderValue {
            get => _expSliderValue;
            private set => _expSliderValue = (ReactiveProperty<float>) value;
        }
        private ReactiveProperty<float> _expSliderValue = new ReactiveProperty<float>();
        
        public Sprite Icon => CharacterProfile.CharacterInfo.Icon;

        public IReadOnlyReactiveProperty<bool> LevelUpButtonIsInteractable => _levelUpButtonIsInteractable;
        private readonly ReactiveProperty<bool> _levelUpButtonIsInteractable = new ReactiveProperty<bool>();

        public CharacterProfile CharacterProfile { get; }

        private readonly List<IDisposable> _disposables = new();

        public LevelUpPopupModel(CharacterProfile characterProfile)
        {
            CharacterProfile = characterProfile;
            
            var subscription = characterProfile.CharacterLevel.CurrentExperience.
                Subscribe(count => _levelUpButtonIsInteractable.Value = CharacterProfile.CharacterLevel.CanLevelUp());
            _disposables.Add(subscription);

            var sliderValueSubscription = characterProfile.CharacterLevel.CurrentExperience.
                Subscribe(SetExpSliderValue);
            _disposables.Add(sliderValueSubscription);
        }

        private string SetLevelCount(CharacterProfile characterProfile)
        {
            string levelCount = "Level: " + characterProfile.CharacterLevel.CurrentLevel;
            return levelCount;
        }

        private string SetExperienceCount(CharacterProfile characterProfile)
        {
            string experienceCount = "XP: " + characterProfile.CharacterLevel.CurrentExperience.Value + 
                                     " / " + characterProfile.CharacterLevel.RequiredExperience;
            return experienceCount;
        }

        private void SetExpSliderValue(int value)
        {
            ExpSliderValue = CalculateExpSliderValue(CharacterProfile);
        }
        
        private IReactiveProperty<float> CalculateExpSliderValue(CharacterProfile characterProfile)
        {
            float calcValue = (float)characterProfile.CharacterLevel.CurrentExperience.Value
                              / characterProfile.CharacterLevel.RequiredExperience;

            IReactiveProperty<float> value = new ReactiveProperty<float>();
            value.Value = calcValue;
            
            return value;
        }

        public void LevelUp()
        {
            CharacterProfile.CharacterLevel.LevelUp();
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}