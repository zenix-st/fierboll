using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkDeepDemo
{
    public class Player : MonoBehaviour
    {
        public float shootForce = 10f; // Force applied to shoot the ball
        public float moveForce = 5f; // Force applied for movement in opposite direction
        public Rigidbody2D rb;
        bool isShooting = false; // Flag to track if the player is shooting
        public GameObject eyes;
        public Camera mainCamera;
        public SpriteRenderer player, eye1, eye2;
        public ParticleSystem[] tenticles;
        public GameObject splatter;
        public Sprite[] splatters;
        public GameObject blueBall;
        public GameObject blueEx;
        public GameObject blueSpit;
        public static Player Instance { set; get; }
        private void Start()
        {
            Instance = this;
            mainCamera = Camera.main;
        }



        Coroutine co;
        void Update()
        {
            EyesAim();
            // Shooting and movement logic combined
            if (!IsPointerOverUIElement())
            {
                if (!isShooting && Input.GetMouseButtonDown(0)) // Assuming left mouse button is used to shoot
                {
                    // Calculate direction towards the mouse
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0; // Ensure z is zero for 2D
                    Vector2 shootDirection = (mousePosition - transform.position).normalized;

                    // Calculate the angle of the shot
                    float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
                    GameObject ball = blueBall;
                    Instantiate(blueSpit, transform.position, Quaternion.Euler(angle, -90, -90));
                    ball = Instantiate(blueBall, transform.position, Quaternion.identity);

                    // Apply constant velocity to the ball
                    Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
                    ballRb.linearVelocity = shootDirection * shootForce;

                    // Apply constant force to the player in the opposite direction of the shot
                    rb.AddForce(-shootDirection * moveForce);

                    // Set isShooting flag to true to prevent movement while shooting
                    isShooting = true;

                    // Reset isShooting flag after a delay (adjust as needed)
                    Invoke(nameof(ResetShooting), 0.2f);
                }
            }

        }

        void ResetShooting()
        {
            isShooting = false; // Reset isShooting flag to allow shooting and movement again
        }

        public static bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
        ///Returns 'true' if we touched or hovering on Unity UI element.
        public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    return true;
            }
            return false;
        }
        ///Gets all event systen raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }

        void EyesAim()
        {
            var mouseWorldCoord = mainCamera.ScreenPointToRay(Input.mousePosition).origin;
            var originToMouse = mouseWorldCoord - this.transform.position;
            originToMouse += new Vector3(originToMouse.x * 3, originToMouse.y * 3, 0);
            originToMouse = Vector3.ClampMagnitude(originToMouse, 0.2f);
            eyes.transform.position = Vector3.Lerp(eyes.transform.position, this.transform.position + originToMouse, 25 * Time.deltaTime);
        }

        public void DOColorFunc(SpriteRenderer spriteRenderer, Color endColor, float duration)
        {
            StartCoroutine(DoColor(spriteRenderer, endColor, duration));
        }

        public void DOScaleFunc(Transform trans, float endScale, float duration)
        {
            StartCoroutine(DOScale(trans, endScale, duration));
        }

        private IEnumerator DOScale(Transform trans, float endScale, float duration)
        {
            Vector3 startScale = trans.localScale;
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                trans.localScale = Vector3.Lerp(startScale, new Vector3(endScale, endScale, endScale), timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            trans.localScale = new Vector3(endScale, endScale, endScale);
        }

        private IEnumerator DoColor(SpriteRenderer spriteRenderer, Color endColor, float duration)
        {
            Color startColor = spriteRenderer.color;
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                spriteRenderer.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = endColor;
        }

        private IEnumerator DoColorUI(Image image, Color endColor, float duration)
        {
            Color startColor = image.color;
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                image.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            image.color = endColor;
        }
    }
}