using Character;

namespace UI.Model
{
    public class ExperienceSliderViewModel : IExperienceSliderViewModel
    {
        public string LevelCount => SetLevelCountText();
        public string ExperienceCount => SetExperienceCountText();

        public float ExpSliderValue => CalculateExpSliderValue();
        
        private readonly CharacterProfile _characterProfile;
        
        public ExperienceSliderViewModel(CharacterProfile characterProfile)
        {
            _characterProfile = characterProfile;
        }
        
        private string SetExperienceCountText()
        {
            string experienceCount = "XP: " + _characterProfile.CharacterLevel.CurrentExperience.Value + 
                                     " / " + _characterProfile.CharacterLevel.RequiredExperience;
            return experienceCount;
        }
        
        private float CalculateExpSliderValue()
        {
            float calcValue = (float)_characterProfile.CharacterLevel.CurrentExperience.Value
                              / _characterProfile.CharacterLevel.RequiredExperience;
            return calcValue;
        }
        
        private string SetLevelCountText()
        {
            string levelCount = "Level: " + _characterProfile.CharacterLevel.CurrentLevel;
            return levelCount;
        }
    }
}