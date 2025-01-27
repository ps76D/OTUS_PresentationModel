using TMPro;
using UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelCount;
        [SerializeField] private TMP_Text _experienceCount;
        [SerializeField] private Slider _expSlider;
        
        public void Initialize(IExperienceSliderViewModel viewModel)
        {
            SetExpSliderValue(viewModel);
            SetExperienceTexts(viewModel);
        }
        
        private void SetExpSliderValue(IExperienceSliderViewModel viewModel)
        {
            _expSlider.value = viewModel.ExpSliderValue;
        }
        
        private void SetExperienceTexts(IExperienceSliderViewModel viewModel)
        { 
            _levelCount.text = viewModel.LevelCount;
            _experienceCount.text = viewModel.ExperienceCount;
        }
    }
}