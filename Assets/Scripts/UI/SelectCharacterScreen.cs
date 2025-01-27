using System.Collections.Generic;
using PlayerProfileSystem;
using UnityEngine;
using Zenject;

namespace UI
{
    public sealed class SelectCharacterScreen : UIScreen
    {
        [Inject]
        private PlayerProfile _playerProfile;
        
        [SerializeField] private Transform _avatarsSpawnRoot;
        [SerializeField] private AvatarButton _avatarButtonPrefab;
        [SerializeField] private List<AvatarButton> _avatarItems = new ();

        private void Start()
        {
            SpawnCharacterAvatars();
        }

        private void SpawnCharacterAvatars()
        {
            _avatarItems.Clear();
            
            foreach (var character in _playerProfile.Characters)
            {
                var avatar = Instantiate(_avatarButtonPrefab, _avatarsSpawnRoot);

                avatar.SetSpriteIcon(character.CharacterInfoData.CharacterIcon);
                avatar.UiManage = _uiManager;
                avatar.CurrentCharacterProfile = character;
                _avatarItems.Add(avatar);
            }
        }
    }
}