using System;
using System.Collections.Generic;
using Character;
using UniRx;
using UnityEngine;

namespace UI.Model
{
    public class LevelUpPopupModel: ILevelUpPopupModel
    {
        public string Name => _avatarViewModel.Name;
        public string Description => _avatarViewModel.Description;
        public Sprite Icon => _avatarViewModel.Icon;

        public string LevelCount => SetLevelCountText(CharacterProfile);

        public string ExperienceCount => _experienceSliderViewModel.SetExperienceCountText();
        public float ExpSliderValue => _experienceSliderViewModel.CalculateExpSliderValue();
        
        public IReadOnlyList<IStatItemModel> StatItemModels => SetStatItemModels(CharacterProfile);

        public IReadOnlyReactiveProperty<bool> LevelUpButtonIsInteractable => _levelUpButtonIsInteractable;
        private readonly ReactiveProperty<bool> _levelUpButtonIsInteractable = new ();

        public CharacterProfile CharacterProfile { get; }
        
        private readonly AvatarViewModel _avatarViewModel;
        private readonly ExperienceSliderViewModel _experienceSliderViewModel;

        private readonly List<IDisposable> _disposables = new();

        public LevelUpPopupModel(CharacterProfile characterProfile)
        {
            CharacterProfile = characterProfile;
            
            _avatarViewModel = new AvatarViewModel(characterProfile);
            _experienceSliderViewModel = new ExperienceSliderViewModel(characterProfile);
            
            var levelUpInteractableSubscription = characterProfile.CharacterLevel.CurrentExperience.
                Subscribe(count => _levelUpButtonIsInteractable.Value = CharacterProfile.CharacterLevel.CanLevelUp());
            _disposables.Add(levelUpInteractableSubscription);
        }

        private string SetLevelCountText(CharacterProfile characterProfile)
        {
            string levelCount = "Level: " + characterProfile.CharacterLevel.CurrentLevel;
            return levelCount;
        }
        
        public void LevelUp()
        {
            CharacterProfile.CharacterLevel.LevelUp();
        }

        private List<StatItemModel> SetStatItemModels(CharacterProfile characterProfile)
        {
            List<StatItemModel> stats = new();
            
            foreach (var stat in characterProfile.CharacterStatsInfo.GetStats())
            { 
                var statViewModel = new StatItemModel(stat);
                
                stats.Add(statViewModel);
            }

            return stats;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}