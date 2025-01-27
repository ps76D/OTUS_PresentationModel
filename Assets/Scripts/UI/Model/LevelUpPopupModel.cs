using System;
using System.Collections.Generic;
using Character;
using UniRx;

namespace UI.Model
{
    public class LevelUpPopupModel: ILevelUpPopupModel
    {
        public IReadOnlyList<IStatItemModel> StatItemModels => SetStatItemModels(CharacterProfile);

        public IReadOnlyReactiveProperty<bool> LevelUpButtonIsInteractable => _levelUpButtonIsInteractable;
        private readonly ReactiveProperty<bool> _levelUpButtonIsInteractable = new ();

        public CharacterProfile CharacterProfile { get; }
        
        private readonly AvatarViewModel _avatarViewModel;
        public IAvatarViewModel AvatarViewModel => _avatarViewModel; 

        private readonly ExperienceSliderViewModel _experienceSliderViewModel;
        public IExperienceSliderViewModel ExperienceSliderViewModel => _experienceSliderViewModel;

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