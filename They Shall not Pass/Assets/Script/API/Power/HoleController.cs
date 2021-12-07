using UnityEngine;

namespace API.PowerUps
{
    /// <summary>
    /// Visual object created when asteroid touch the ground
    /// </summary>
    public class HoleController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioAsteroid;

        /// <summary>
        /// Set color alpha texture
        /// </summary>
        public float fade = 2.5f;

        private Material _material;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            audioAsteroid?.Play();
        }

        private void FixedUpdate()
        {
            Color color = _material.color;
            _material.color = new Color(color.r, color.g, color.b, color.a - (fade * Time.fixedDeltaTime));
            if (_material.color.a == 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
