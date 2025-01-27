using System;
using System.Collections.Generic;
using Character;
using UniRx;

namespace UI.Model
{
    public class LevelUpPopupModel: ILevelUpPopupModel
    {
        public IReadOnlyReactiveProperty<bool> LevelUpButtonIsInteractable => _levelUpButtonIsInteractable;
        private readonly ReactiveProperty<bool> _levelUpButtonIsInteractable = new ();

        public CharacterProfile CharacterProfile { get; }

        private readonly AvatarViewModel _avatarViewModel;
        public IAvatarViewModel AvatarViewModel => _avatarViewModel;

        private readonly ExperienceSliderViewModel _experienceSliderViewModel;
        public IExperienceSliderViewModel ExperienceSliderViewModel => _experienceSliderViewModel;
        
        private readonly StatsViewModel _statsViewModel;
        public IStatsViewModel StatsViewModel => _statsViewModel;

        private readonly List<IDisposable> _disposables = new();

        public LevelUpPopupModel(CharacterProfile characterProfile)
        {
            CharacterProfile = characterProfile;
            
            _avatarViewModel = new AvatarViewModel(characterProfile);
            _experienceSliderViewModel = new ExperienceSliderViewModel(characterProfile);
            _statsViewModel = new StatsViewModel(characterProfile);
            
            var levelUpInteractableSubscription = characterProfile.CharacterLevel.CurrentExperience.
                Subscribe(count => _levelUpButtonIsInteractable.Value = CharacterProfile.CharacterLevel.CanLevelUp());
            _disposables.Add(levelUpInteractableSubscription);
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