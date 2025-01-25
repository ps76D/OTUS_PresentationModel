using Character;

namespace UI.Model
{
    public interface IStatItemModel
    {
        string StatText { get; }
        
        CharacterStat CharacterStat { get; }

        void ChangeValue(int value);
    }
}