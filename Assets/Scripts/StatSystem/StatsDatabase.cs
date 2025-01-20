﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatsDatabase", menuName = "Stats/StatsDatabase", order = 0)]
    public sealed class StatsDatabase : ScriptableObject
    {
        [SerializeField] private List<StatData> _startStatsDatabase;
        public List<StatData> StartStatsDatabase => _startStatsDatabase;
    }
}