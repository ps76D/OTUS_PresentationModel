using System.Collections.Generic;
using UI.Model;
using UnityEngine;

namespace UI.View
{
    public class StatsView : MonoBehaviour
    {
        [SerializeField] private StatItemView _statItemViewPrefab;
        [SerializeField] private Transform _statsRoot;
        
        private IStatsViewModel _viewModel;
        
        [SerializeField] private List<StatItemView> _statItems = new();
        
        public void Initialize(IStatsViewModel viewModel)
        {
            _viewModel = viewModel;
            
            ClearStatItems();
            SpawnStats();
        }
        
        private void SpawnStats()
        {
            int index;
            for (index = 0; index < _viewModel.StatItemModels.Count; index++)
            {
                IStatItemModel statViewModel = _viewModel.StatItemModels[index];
                var statItem = Instantiate(_statItemViewPrefab, _statsRoot);

                statItem.Initialize(statViewModel);

                _statItems.Add(statItem);
            }
        }

        private void ClearStatItems()
        {
            if (_statItems is not { Count: > 0 }) return;
            
            foreach (var statItem in _statItems)
            {
                DestroyImmediate(statItem.gameObject);
            }
                
            _statItems.Clear();
        }
    }
}