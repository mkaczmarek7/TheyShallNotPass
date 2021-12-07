using System;
using UnityEngine;

namespace API.Dinosaur
{
    /// <summary>
    /// Class which control enemy character
    /// </summary>
    public class DinosaurController : MonoBehaviour
    {
        [Header("Dinosaur")]

        [SerializeField]
        private AudioSource audio;

        [SerializeField]
        private Collider2D collider2d;

        [SerializeField]
        private Animator animator;

        public bool IsDied { get; set; }

        protected DinosaurScriptableObject _dinosaurData;

        /// <summary>
        /// Called when Dinosaur is killed
        /// </summary>
        public event Action<DinosaurController> DinosaurDead;

        /// <summary>
        /// Called when user kill dinosaur (tap or using powerups)
        /// </summary>
        public static event Action<float> PointsScored;


        private const float ReadyToTouchTime = 0.2f;

        private bool _isFrozen;
        protected bool IsFrozen
        {
            get
            {
                return _isFrozen;
            }
            set
            {
                _isFrozen = value;
                animator.SetBool("frozen", value);
            }
        }
        private bool IsReadyToTouch { get; set; }

        private float _touchTimer = 0.0f;
        private float _freezeTimer = 0.0f;

        /// <summary>
        /// Prepare controller with specific data
        /// </summary>
        /// <param name="data"></param>
        public void Initialize(DinosaurScriptableObject data)
        {
            _dinosaurData = Instantiate(data);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            UpdateFrozenTimer();
            UpdateTouchTimer();
            CheckInput();
        }

        protected virtual void Move()
        {
            if (!IsDied && !IsFrozen)
            {
                transform.position += -transform.right * _dinosaurData.speed * Time.fixedDeltaTime;
            }
        }

        /// <summary>
        /// Block dinosaur movement using powerUp
        /// </summary>
        /// <param name="enabled"></param>
        public void ChangeFreezeMode(bool enabled)
        {
            IsFrozen = enabled;
        }

        /// <summary>
        /// Destroy dinosaur object
        /// </summary>
        /// <param name="forceDelete"></param>
        public void Kill(bool forceDelete = false)
        {
            if (!forceDelete)
            {
                audio.Play();
                animator.SetBool("death", true);
                IsDied = true;
                PointsScored?.Invoke(_dinosaurData.score);
            }

            DinosaurDead?.Invoke(this);
            Destroy(gameObject, _dinosaurData.lifetime);
        }


        private void CheckInput()
        {
            if (Input.GetMouseButtonDown(0) && IsReadyToTouch && !IsDied)
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (collider2d != Physics2D.OverlapPoint(wp))
                    return;

                _dinosaurData.health -= 10;
                if (_dinosaurData.health <= 0)
                {
                    Kill();
                }

                IsReadyToTouch = false;
                _touchTimer = 0.0f;
            }
        }

        private void UpdateFrozenTimer()
        {
            if (!IsFrozen)
                return;

            _freezeTimer += Time.deltaTime;
            if (_freezeTimer >= _dinosaurData.frozenTime)
            {
                ChangeFreezeMode(false);
                _freezeTimer = 0.0f;
            }
        }

        private void UpdateTouchTimer()
        {
            if (IsReadyToTouch)
                return;

            _touchTimer += Time.deltaTime;
            if (_touchTimer >= ReadyToTouchTime)
            {
                IsReadyToTouch = true;
            }
        }
    }
}
