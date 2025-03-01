﻿using Character;

namespace UI.Model
{
    public interface IExperienceSliderViewModel
    {
        string LevelCount { get; }
        string ExperienceCount { get; }
        float ExpSliderValue { get; }
        
        CharacterProfile CharacterProfile { get; }
    }
}