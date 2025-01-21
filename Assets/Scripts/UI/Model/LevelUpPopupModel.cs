using System;
using System.Collections.Generic;
using Character;
using UniRx;
using UnityEngine;

namespace UI.Model
{
    public class LevelUpPopupModel: ILevelUpPopupModel
    {
        public string Name => CharacterProfile.CharacterInfo.Name.Value;
        public string Description => CharacterProfile.CharacterInfo.Description.Value;
        public string LevelCount => SetLevelCountText(CharacterProfile);
        public string ExperienceCount => SetExperienceCountText(CharacterProfile);
        public float ExpSliderValue => CalculateExpSliderValue(CharacterProfile);

        public Sprite Icon => CharacterProfile.CharacterInfo.Icon.Value;

        public IReadOnlyReactiveProperty<bool> LevelUpButtonIsInteractable => _levelUpButtonIsInteractable;
        private readonly ReactiveProperty<bool> _levelUpButtonIsInteractable = new ();

        public CharacterProfile CharacterProfile { get; }

        private readonly List<IDisposable> _disposables = new();

        public LevelUpPopupModel(CharacterProfile characterProfile)
        {
            CharacterProfile = characterProfile;
            
            var levelUpInteractableSubscription = characterProfile.CharacterLevel.CurrentExperience.
                Subscribe(count => _levelUpButtonIsInteractable.Value = CharacterProfile.CharacterLevel.CanLevelUp());
            _disposables.Add(levelUpInteractableSubscription);
        }

        private string SetLevelCountText(CharacterProfile characterProfile)
        {
            string levelCount = "Level: " + characterProfile.CharacterLevel.CurrentLevel;
            return levelCount;
        }

        private string SetExperienceCountText(CharacterProfile characterProfile)
        {
            string experienceCount = "XP: " + characterProfile.CharacterLevel.CurrentExperience.Value + 
                                     " / " + characterProfile.CharacterLevel.RequiredExperience;
            return experienceCount;
        }

        private float CalculateExpSliderValue(CharacterProfile characterProfile)
        {
            float calcValue = (float)characterProfile.CharacterLevel.CurrentExperience.Value
                              / characterProfile.CharacterLevel.RequiredExperience;
            return calcValue;
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