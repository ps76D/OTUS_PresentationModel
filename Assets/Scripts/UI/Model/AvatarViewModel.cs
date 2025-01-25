using Character;
using UnityEngine;

namespace UI.Model
{
    public class AvatarViewModel : IAvatarViewModel
    {
        public string Name => _characterProfile.CharacterInfo.Name.Value;
        public string Description => _characterProfile.CharacterInfo.Description.Value;
        public Sprite Icon => _characterProfile.CharacterInfo.Icon.Value;

        private readonly CharacterProfile _characterProfile;

        public AvatarViewModel(CharacterProfile characterProfile)
        {
            _characterProfile = characterProfile;
        }
    }
}