using System.Collections.Generic;
using Character;

namespace UI.Model
{
    public class StatsViewModel : IStatsViewModel
    {
        public IReadOnlyList<IStatItemModel> StatItemModels => SetStatItemModels();
        
        public CharacterProfile CharacterProfile 
        {
            get;
        }
        
        public StatsViewModel(CharacterProfile characterProfile)
        {
            CharacterProfile = characterProfile;
        }
        
        private List<StatItemModel> SetStatItemModels()
        {
            List<StatItemModel> stats = new();
            
            foreach (var stat in CharacterProfile.CharacterStatsInfo.GetStats())
            { 
                var statViewModel = new StatItemModel(stat);
                
                stats.Add(statViewModel);
            }

            return stats;
        }
    }
}