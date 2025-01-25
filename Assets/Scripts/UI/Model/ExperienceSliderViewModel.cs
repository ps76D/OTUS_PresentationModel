using Character;

namespace UI.Model
{
    public class ExperienceSliderViewModel : IExperienceSliderViewModel
    {

        public string ExperienceCount => SetExperienceCountText();

        public float ExpSliderValue => CalculateExpSliderValue();
        
        private readonly CharacterProfile _characterProfile;
        
        public ExperienceSliderViewModel(CharacterProfile characterProfile)
        {
            _characterProfile = characterProfile;
        }
        
        public string SetExperienceCountText()
        {
            string experienceCount = "XP: " + _characterProfile.CharacterLevel.CurrentExperience.Value + 
                                     " / " + _characterProfile.CharacterLevel.RequiredExperience;
            return experienceCount;
        }
        
        public float CalculateExpSliderValue()
        {
            float calcValue = (float)_characterProfile.CharacterLevel.CurrentExperience.Value
                              / _characterProfile.CharacterLevel.RequiredExperience;
            return calcValue;
        }
    }
}