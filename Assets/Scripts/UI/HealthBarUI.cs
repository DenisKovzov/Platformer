using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private IDamageable target;

        public void Construct(IDamageable target)
        {
            this.target = target;

            target.OnHealthChanged += UpdateUI;
            UpdateUI();
        }

        private void OnDestroy()
        {
            target.OnHealthChanged -= UpdateUI;
        }

        private void UpdateUI()
        {
            slider.maxValue = target.MaxHealth;
            slider.value = target.CurrentHealth;
        }

    }

}