using TMPro;
using UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class AvatarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;
        
        public void Initialize(IAvatarViewModel viewModel)
        {
            SetIcon(viewModel);
            SetTexts(viewModel);
        }

        private void SetIcon(IAvatarViewModel viewModel)
        {
            _icon.sprite = viewModel.Icon;
        }

        private void SetTexts(IAvatarViewModel avatarViewModel)
        {
            _characterName.text = avatarViewModel.Name;
            _description.text = avatarViewModel.Description;
        }
    }
}