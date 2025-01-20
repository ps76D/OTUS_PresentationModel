using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterInfoDatabase", menuName = "CharacterProfile/CharacterInfoDatabase", order = 0)]
    public sealed class CharacterDatabase : ScriptableObject
    {
        [SerializeField] private List<CharacterInfoData> _characterInfoDatabase;
        
        public List<CharacterInfoData> CharacterInfoDatabase => _characterInfoDatabase;
    }
}