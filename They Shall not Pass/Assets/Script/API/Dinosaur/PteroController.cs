using UnityEngine;

namespace API.Dinosaur
{
    /// <summary>
    /// Dinosaur with up/down movement
    /// </summary>
    public class PteroController : DinosaurController
    {
        [Header("Ptero")]
        public float maxYLimit;
        public float minYLimit;

        private bool _upDirection = true;

        protected override void Move()
        {
            if (IsDied || IsFrozen)
                return;

            transform.position += -transform.right * _dinosaurData.speed * Time.fixedDeltaTime;
            if (transform.position.y >= maxYLimit)
            {
                _upDirection = false;
            }

            if (transform.position.y <= minYLimit)
            {
                _upDirection = true;
            }

            if (_upDirection)
            {
                transform.position += transform.up * _dinosaurData.speed * Time.fixedDeltaTime;
            }
            else
            {
                transform.position += -transform.up * _dinosaurData.speed * Time.fixedDeltaTime;
            }

        }
    }
}
