using UnityEngine;

namespace API.PowerUps
{
    /// <summary>
    /// Power which create object with range attack
    /// </summary>
    public class AsteroidPowerUp : PowerUpButton
    {
        [SerializeField]
        private AsteroidController asteroid;

        [SerializeField]
        private Transform spawnPoint;

        private float timer = 0.0f;
        private bool IsChoosingDestination { get; set; }

        protected override void Update()
        {
            base.Update();
            if (IsChoosingDestination)
            {
                ChooseDestination();
            }
        }

        private void ChooseDestination()
        {
            if (timer > 0)
                timer -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && timer <= 0f)
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var asteroidController = Instantiate(asteroid, spawnPoint.position, Quaternion.identity);
                asteroidController.Initialize(new Vector3(wp.x, wp.y, -2));
                IsChoosingDestination = !IsChoosingDestination;
                ChangeButtonColor(Color.white);
            }
        }

        public override void Action()
        {
            base.Action();
            timer = 0.5f;
            IsChoosingDestination = true;
            ChangeButtonColor(Color.green);
        }
    }
}
