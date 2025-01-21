using System;
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
        [SerializeField] private AvatarItem _avatarItemPrefab;
        [SerializeField] private List<AvatarItem> _avatarItems;

        private void Start()
        {
            _avatarItems = new List<AvatarItem>();
            
            SpawnCharacterAvatars();
        }

        private void SpawnCharacterAvatars()
        {
            foreach (var character in _playerProfile.Characters)
            {
                var avatar = Instantiate(_avatarItemPrefab, _avatarsSpawnRoot);

                avatar.SetIcon(character.CharacterInfoData.CharacterIcon);
                avatar.UiManage = _uiManager;
                /*avatar.PlayerProfile = _playerProfile;*/
                avatar.CurrentCharacterProfile = character;
                _avatarItems.Add(avatar);
            }
        }
    }
}