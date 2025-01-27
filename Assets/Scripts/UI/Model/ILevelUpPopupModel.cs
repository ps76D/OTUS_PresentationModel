using System;
using System.Collections.Generic;
using Character;
using UniRx;

namespace UI.Model
{
    public interface ILevelUpPopupModel: IDisposable
    {
        IAvatarViewModel AvatarViewModel { get; }
        IExperienceSliderViewModel ExperienceSliderViewModel { get; }
        
        IReadOnlyList<IStatItemModel> StatItemModels { get; }
        
        IReadOnlyReactiveProperty<bool> LevelUpButtonIsInteractable { get; }
        
        
        CharacterProfile CharacterProfile { get; }
        
        void LevelUp();
    }
}