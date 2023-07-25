using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(TriggerDetector))]
    public class DamageDealer : MonoBehaviour, IInitializable
    {
        private TriggerDetector detector;
        private DamageDealerData config;

        public void Construct(DamageDealerData config)
        {
            this.config = config;
        }

        public void Initialize()
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
    }

    [Serializable]
    public struct DamageDealerData
    {
        public float Damage;
    }

}