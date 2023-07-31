using UnityEngine;
using System;

namespace Platformer
{

    public class TriggerDetector : MonoBehaviour
    {
        public event Action<Collider2D> OnTriggerEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter?.Invoke(other);
        }
    }

}