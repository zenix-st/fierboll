using UnityEngine;

namespace DarkDeepDemo
{
    public class BallController : MonoBehaviour
    {
        public AudioClip shootSound;
        public AudioClip collisionSound;
        private AudioSource audioSource;

        void Start()
        {
            // ≈÷«›… AudioSource
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f; // ’Ê  À·«ÀÌ «·√»⁄«œ

            //  ‘€Ì· ’Ê  «·—„Ì ⁄‰œ «·≈‰‘«¡
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //  ‘€Ì· ’Ê  «·«’ÿœ«„
            if (collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }
}