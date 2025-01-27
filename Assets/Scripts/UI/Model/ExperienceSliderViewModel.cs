using Character;

namespace UI.Model
{
    public class ExperienceSliderViewModel : IExperienceSliderViewModel
    {
        public string LevelCount => SetLevelCountText();
        public string ExperienceCount => SetExperienceCountText();

        public float ExpSliderValue => CalculateExpSliderValue();

        public CharacterProfile CharacterProfile 
        {
            get;
        }
        
        public ExperienceSliderViewModel(CharacterProfile characterProfile)
        {
            CharacterProfile = characterProfile;
        }
        
        private string SetExperienceCountText()
        {
            string experienceCount = "XP: " + CharacterProfile.CharacterLevel.CurrentExperience.Value + 
                                     " / " + CharacterProfile.CharacterLevel.RequiredExperience;
            return experienceCount;
        }
        
        private float CalculateExpSliderValue()
        {
            float calcValue = (float)CharacterProfile.CharacterLevel.CurrentExperience.Value
                              / CharacterProfile.CharacterLevel.RequiredExperience;
            return calcValue;
        }
        
        private string SetLevelCountText()
        {
            string levelCount = "Level: " + CharacterProfile.CharacterLevel.CurrentLevel;
            return levelCount;
        }
    }
}