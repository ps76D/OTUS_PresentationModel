using System;
using Character;
using UniRx;

namespace UI.Model
{
    public interface ILevelUpPopupModel: IDisposable
    {
        IAvatarViewModel AvatarViewModel { get; }
        IExperienceSliderViewModel ExperienceSliderViewModel { get; }
        IStatsViewModel StatsViewModel { get; }
        
        IReadOnlyReactiveProperty<bool> LevelUpButtonIsInteractable { get; }
        
        CharacterProfile CharacterProfile { get; }
        
        void LevelUp();
    }
}