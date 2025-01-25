using System;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace PlayerProfileSystem
{
    [Serializable]
    public class PlayerProfile
    {
        [SerializeField] private List<CharacterProfile> _characters;
        public IReadOnlyList<CharacterProfile> Characters => _characters;
        
        public PlayerProfile(CharacterDatabase characterDatabase)
        {
            _characters = CreateAllCharacters(characterDatabase);
        }
        
        private List<CharacterProfile> CreateAllCharacters(CharacterDatabase characterDatabase)
        {
            var characters = new List<CharacterProfile>(); 
            
            foreach (var character in characterDatabase.CharacterInfoDatabase)
            {
                var characterProfile = new CharacterProfile(character);

                characters.Add(characterProfile);
            }

            return characters;
        }
    }
}