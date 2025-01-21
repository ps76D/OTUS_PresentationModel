using System;
using System.Collections.Generic;
using Character;

namespace UI.Model
{
    public class StatItemModel: IStatItemModel
    {
        public string StatText => SetStatText();

        public CharacterStat CharacterStat { get; }
        
        private readonly List<IDisposable> _disposables = new();

        public StatItemModel(CharacterStat characterStat)
        {
            CharacterStat = characterStat;
        }

        public void ChangeValue(int value)
        {
            CharacterStat.ChangeValue(value);
        }
        
        private string SetStatText()
        {
            string statText = CharacterStat.Name + ": " + CharacterStat.Value;

            return statText;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}