using System;
using UnityEngine.Serialization;

namespace StatSystem
{
    [Serializable]
    public sealed class StatData
    {
        [FormerlySerializedAs("_statInfo")]
        public StatInfoData _statInfoData;
        public int _startStatValue;
    }
}