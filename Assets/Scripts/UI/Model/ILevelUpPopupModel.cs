using System;
using System.Collections.Generic;
using Character;
using UniRx;
using UnityEngine;

namespace UI.Model
{
    public interface ILevelUpPopupModel: IDisposable
    {
        string Name { get; }
        string Description { get; }
        string LevelCount { get; }
        string ExperienceCount { get; }
        float ExpSliderValue { get; }
        Sprite Icon { get; }
        
        IReadOnlyList<IStatItemModel> StatItemModels { get; }
        
        IReadOnlyReactiveProperty<bool> LevelUpButtonIsInteractable { get; }
        
        
        CharacterProfile CharacterProfile { get; }
        
        void LevelUp();
    }
}