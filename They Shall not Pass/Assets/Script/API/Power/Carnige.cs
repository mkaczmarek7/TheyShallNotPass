using UnityEngine;

/// <summary>
/// PowerUp which create cannons to shoot dinosaur
/// </summary>
namespace API.PowerUps
{
    public class Carnige : PowerUpButton
    {
        [SerializeField]
        public GameObject cannon;

        [SerializeField]
        public GameObject cannonball;

        [SerializeField]
        private Vector3[] cannonPositions;

        [SerializeField]
        private AudioSource audioSource;

        private const int NumberOfCannons = 3;
        private const float CannonPreaparingOffset = 1.61f;
        private const float CannonballXOffset = 4;
        private const int NumberOfShooting = 5;
        private const float ShootingTimeLimit = 3.0f;

        private GameObject[] _cannonTab;
        private Vector3[] _targetPlace;

        public enum CarnigeState
        {
            Start,
            Shooting,
            StopShooting,
            End
        }

        private CarnigeState _carnige = CarnigeState.End;
        private float _timer = 0.0f;
        private int _shootingCount = 0;

        protected override void Start()
        {
            base.Start();
            CreateCannons();
        }

        private void CreateCannons()
        {
            _cannonTab = new GameObject[NumberOfCannons];
            _targetPlace = new Vector3[NumberOfCannons];
            for (int i = 0; i < NumberOfCannons; i++)
            {
                _cannonTab[i] = Instantiate(cannon, cannonPositions[i], Quaternion.identity);
                _targetPlace[i] = new Vector3(cannonPositions[i].x + CannonPreaparingOffset, cannonPositions[i].y, cannonPositions[i].z);
            }
        }

        private void FixedUpdate()
        {
            switch (_carnige)
            {
                case CarnigeState.Start:
                    PreapareShooting();
                    break;
                case CarnigeState.Shooting:
                    Shoot();
                    break;
                case CarnigeState.StopShooting:
                    StopShooting();
                    break;
            }
        }

        public override void Action()
        {
            base.Action();
            if (_carnige == CarnigeState.End)
                _carnige = CarnigeState.Start;
        }

        private void PreapareShooting()
        {
            for (int i = 0; i < NumberOfCannons; i++)
            {
                MoveCannonToTarget(i, _targetPlace[i]);
            }

            if (_cannonTab[0].transform.position == _targetPlace[0])
            {
                _carnige = CarnigeState.Shooting;
                _timer = 0.0f;
                _shootingCount = 0;
            }
        }

        private void MoveCannonToTarget(int cannonIndex, Vector3 target)
        {
            _cannonTab[cannonIndex].transform.position = Vector3.MoveTowards(_cannonTab[cannonIndex].transform.position, target, Time.fixedDeltaTime);
        }


        private void Shoot()
        {
            _timer += Time.fixedDeltaTime;

            if (_timer <= ShootingTimeLimit)
                return;


            if (_shootingCount % 2 == 1)
            {
                CreateCannonBall(_targetPlace[0]);
                CreateCannonBall(_targetPlace[2]);
            }

            else if (_shootingCount % 2 == 0)
            {
                CreateCannonBall(_targetPlace[1]);
            }

            if (_shootingCount == NumberOfShooting)
            {
                _carnige = CarnigeState.StopShooting;
            }
        }

        private void CreateCannonBall(Vector3 spawnPosition)
        {
            spawnPosition.x -= CannonballXOffset;
            audioSource.Play();
            Instantiate(cannonball, spawnPosition, Quaternion.identity);
            _shootingCount++;
            _timer = 0.0f;

        }

        private void StopShooting()
        {
            for (int i = 0; i < NumberOfCannons; i++)
            {
                MoveCannonToTarget(i, cannonPositions[i]);
            }

            if (_cannonTab[0].transform.position == cannonPositions[0])
            {
                _carnige = CarnigeState.End;
            }
        }
    }
}
