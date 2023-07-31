using UnityEngine;

namespace Platformer
{
    public class CameraController : MonoBehaviour
    {
        private Transform target;

        public void Construct(Transform target)
        {
            this.target = target;
        }

        private void LateUpdate()
        {
            Vector3 newPosition = transform.position;
            newPosition.x = target.transform.position.x;

            transform.position = newPosition;
        }
    }
}
