using Character;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class StatItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statName;
        
        public void SetStatData(CharacterStat stat)
        {
            _statName.text = stat.Name + ": " + stat.Value;
        }
    }
}