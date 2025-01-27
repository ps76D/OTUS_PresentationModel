using Character;
using UnityEngine;

namespace UI.Model
{
    public class AvatarViewModel : IAvatarViewModel
    {
        public string Name => CharacterProfile.CharacterInfo.Name.Value;
        public string Description => CharacterProfile.CharacterInfo.Description.Value;
        public Sprite Icon => CharacterProfile.CharacterInfo.Icon.Value;

        public CharacterProfile CharacterProfile 
        {
            get;
        }

        public AvatarViewModel(CharacterProfile characterProfile)
        {
            CharacterProfile = characterProfile;
        }
    }
}