using System.Collections.Generic;
using Character;

namespace UI.Model
{
    public interface IStatsViewModel
    {
        IReadOnlyList<IStatItemModel> StatItemModels { get; }
        CharacterProfile CharacterProfile { get; }
    }
}