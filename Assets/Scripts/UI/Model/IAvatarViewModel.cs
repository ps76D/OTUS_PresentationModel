using UnityEngine;

namespace UI.Model
{
    public interface IAvatarViewModel
    {
        string Name { get; }
        string Description { get; }

        Sprite Icon { get; }
    }
}