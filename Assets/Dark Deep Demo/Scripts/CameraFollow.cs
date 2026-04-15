using UnityEngine;
namespace DarkDeepDemo
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        [Range(1, 10)]
        public float smoothFactor;
        public Vector3 minValues, maxValue;

        private void Start()
        {
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;

            }
        }

        private void FixedUpdate()
        {
            if (target != null)
            {
                Follow();
            }
        }

        void Follow()
        {
            Vector3 targetPosition = target.position + offset;

            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);

            transform.position = smoothPosition;

        }
    }
}