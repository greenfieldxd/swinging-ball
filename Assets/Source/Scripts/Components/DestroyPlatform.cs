using UnityEngine;

namespace Source.Scripts.Components
{
    public class DestroyPlatform : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var ball = other.GetComponent<Ball>();
            if (ball) Destroy(ball.gameObject);
        }
    }
}
