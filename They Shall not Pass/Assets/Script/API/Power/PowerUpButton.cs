using UnityEngine;
using UnityEngine.UI;

namespace API.PowerUps
{
    /// <summary>
    /// Button prepared to use powerUp functionality
    /// </summary>
    public class PowerUpButton : MonoBehaviour, IPowerUps
    {
        /// <summary>
        /// PowerUp loading time
        /// </summary>
        public float recoveryTime;

        [SerializeField]
        private Image iconImage;

        private Button _button;

        private bool IsEnabled { get; set; } = true;
        private float timer = 0f;

        protected virtual void Start()
        {
            _button = GetComponent<Button>();
        }

        protected virtual void Update()
        {
            if (IsEnabled)
                return;

            timer += Time.deltaTime;

            if (timer > recoveryTime)
            {
                SetButtonInteractable(true);
                timer = 0;
            }
        }

        public virtual void Action()
        {
            SetButtonInteractable(false);
        }

        private void SetButtonInteractable(bool enabled)
        {
            _button.interactable = enabled;
            IsEnabled = enabled;
        }

        protected void ChangeButtonColor(Color color)
        {
            iconImage.color = color;
        }

    }  
}
