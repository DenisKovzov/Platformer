using System;
using UnityEngine;

namespace Platformer
{
    public class PlayerRichTargetCondition : ICondition
    {
        private TriggerDetector detector;
        private bool hasRich = false;
        public event Action OnConditionMet;

        public PlayerRichTargetCondition(TriggerDetector detector)
        {
            this.detector = detector;

            detector.OnTriggerEnter += TriggerDetector_OnTriggerEnter;
        }


        private void TriggerDetector_OnTriggerEnter(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                hasRich = true;
                OnConditionMet?.Invoke();
            }
        }

        public bool IsConditionMet()
        {
            return hasRich;
        }

        public void Reset()
        {
            hasRich = false;
        }

        
        ~PlayerRichTargetCondition()
        {
            detector.OnTriggerEnter -= TriggerDetector_OnTriggerEnter;
        }
    }

}