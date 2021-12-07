using API.Dinosaur;
using UnityEngine;

namespace API.PowerUps
{
    /// <summary>
    /// Object with attack range - created from powerUp
    /// </summary>
    public class AsteroidController : MonoBehaviour
    {
        public float speed = 5f; 

        [SerializeField]
        private GameObject hole;

        private Vector3 _targetPosition;

        private const float OverlapRadius = 3.16f;


        public void Initialize(Vector3 target)
        {
            _targetPosition = target;
        }

        private void FixedUpdate()
        {
            if (_targetPosition == default)
                return;

            Move();

            if (transform.position == _targetPosition)
            {
                CreateHole();
                KillAllDinosaurs();
                Destroy(gameObject);
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.fixedDeltaTime * speed);
        }

        private void CreateHole()
        {
            Instantiate(hole, _targetPosition, Quaternion.identity);
        }

        private void KillAllDinosaurs()
        {
            var colliders = Physics2D.OverlapCircleAll(_targetPosition, OverlapRadius);

            foreach (var collider in colliders)
            {
                if (!collider.CompareTag("Enemy"))
                    continue;

                var dinosaur = collider.GetComponent<DinosaurController>();
                if (dinosaur.IsDied)
                    continue;
                dinosaur.Kill();
            }
        }
    }
}

