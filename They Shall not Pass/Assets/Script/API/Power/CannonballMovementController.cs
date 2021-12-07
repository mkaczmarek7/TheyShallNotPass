using API.Dinosaur;
using UnityEngine;

namespace API.PowerUps
{
    /// <summary>
    /// Object  created from powerUp cannon. Kill all encountered enemies.
    /// </summary>
    public class CannonballMovementController : MonoBehaviour
    {
        public float speed = 10f;
        private const float BoundX = 18f;

        private void Start()
        {
            gameObject.name = "Cannonball";
        }

        private void FixedUpdate()
        {
            if (transform.position.x > BoundX)
            {
                Destroy(gameObject);
            }
            transform.Translate(Vector3.right * Time.fixedDeltaTime * speed);

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                var dinosaur = other.gameObject.GetComponent<DinosaurController>();
                if (!dinosaur.IsDied)
                    dinosaur.Kill();
            }
        }
    }
}
