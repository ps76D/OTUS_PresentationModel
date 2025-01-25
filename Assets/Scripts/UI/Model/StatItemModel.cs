using Character;

namespace UI.Model
{
    public class StatItemModel
    {
        public string StatText => SetStatText();

        public CharacterStat CharacterStat { get; }

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
    }
}