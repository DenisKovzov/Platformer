using System;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(TriggerDetector))]
    public class DamageDealer : MonoBehaviour
    {
        private TriggerDetector detector;
        private DamageDealerData config;

        public void Construct(DamageDealerData config)
        {
            this.config = config;
        }

        private void Awake()
        {
            detector = GetComponent<TriggerDetector>();

            detector.OnTriggerEnter += TriggerDetector_OnTriggerEnter;
        }

        private void TriggerDetector_OnTriggerEnter(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(config.Damage);
            }
        }

        private void OnDestroy()
        {
            detector.OnTriggerEnter -= TriggerDetector_OnTriggerEnter;
        }
    }

    [Serializable]
    public struct DamageDealerData
    {
        public float Damage;
    }

}