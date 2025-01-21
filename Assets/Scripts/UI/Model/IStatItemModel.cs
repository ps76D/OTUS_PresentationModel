using System;
using Character;

namespace UI.Model
{
    public interface IStatItemModel: IDisposable
    {
        string StatText { get; }
        
        CharacterStat CharacterStat { get; }

        void ChangeValue(int value);
    }
}