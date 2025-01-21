using System;
using UnityEngine;
using Zenject;

namespace Character
{
    [Serializable]
    public class CharacterProfile
    {
        [SerializeField] private CharacterInfoData _characterInfoData;
        [SerializeField] private CharacterInfo _characterInfo;
        [SerializeField] private CharacterLevel _characterLevel;

        [SerializeField] private CharacterStatsInfo _characterStatsInfo;

        public CharacterInfo CharacterInfo => _characterInfo;
        public CharacterLevel CharacterLevel => _characterLevel;
        
        public CharacterStatsInfo CharacterStatsInfo => _characterStatsInfo;

        public CharacterInfoData CharacterInfoData => _characterInfoData; 

        public CharacterProfile(CharacterInfoData characterInfoData)
        {
            _characterInfoData = characterInfoData;
            _characterInfo = CreateCharacterInfo(characterInfoData);
            _characterLevel = new CharacterLevel();
            _characterStatsInfo = CreateCharacterStatsInfo(characterInfoData);
        }

        private CharacterInfo CreateCharacterInfo(CharacterInfoData characterInfoData)
        {
            var characterInfo = new CharacterInfo();

            characterInfo.ChangeName(characterInfoData.CharacterName);
            characterInfo.ChangeDescription(characterInfoData.CharacterDescription);
            characterInfo.ChangeIcon(characterInfoData.CharacterIcon);
            
            return characterInfo;
        }

        private CharacterStatsInfo CreateCharacterStatsInfo(CharacterInfoData characterInfoData)
        {
            var characterStatsInfo = new CharacterStatsInfo();

            foreach (var statData in characterInfoData.StatsDatabase.StartStatsDatabase)
            {
                var stat = new CharacterStat()
                {
                    Name = statData._statInfoData.StatName,
                };
                
                stat.ChangeValue(statData._startStatValue);
                
                characterStatsInfo.AddStat(stat);
            }
            
            return characterStatsInfo;
        }
    }
}