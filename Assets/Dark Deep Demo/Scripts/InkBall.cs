using UnityEngine;

namespace DarkDeepDemo
{
    public class InkBall : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D o)
        {
            GameObject splat = Instantiate(Player.Instance.splatter, this.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Instantiate(Player.Instance.blueEx, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }


    }

}
