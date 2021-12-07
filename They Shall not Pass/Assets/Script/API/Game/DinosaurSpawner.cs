using API.Dinosaur;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace API.Game
{
    /// <summary>
    /// Resposibility for spawning dinosaur in game
    /// </summary>
    public class DinosaurSpawner : MonoBehaviour
    {
        public Vector3 spawnArea;

        /// <summary>
        /// Scriptable with details of all waves
        /// </summary>
        [SerializeField]
        private SpawnWavesScriptableObject spawnWaves;

        /// <summary>
        /// Called when spawning wave is finished
        /// </summary>
        public event Action<int> WaveUpdated;

        /// <summary>
        /// Called when finish all waves
        /// </summary>
        public event Action SpawningEnded;

        private Wave[] Waves => spawnWaves.waves;
        private bool IsSpawning { get; set; } = true;
        private bool IsSpawnEnded { get; set; }
        private float TimeBetweenWaves { get; set; }

        private readonly List<DinosaurController> _dinosaurSpawnedList = new List<DinosaurController>();
        private float[] _timer;
        private int _indexWaves;

        private void Start()
        {
            spawnWaves = Instantiate(spawnWaves);
            TimeBetweenWaves = spawnWaves.timeBetweenWaves;
            _timer = new float[spawnWaves.MaxEnemies];
        }
        private void Update()
        {
            if (IsSpawnEnded)
                return;

            UpdateSpawning();
        }

        private void OnDestroy()
        {
            _dinosaurSpawnedList?.Clear();
        }

        /// <summary>
        /// Force to finish spawning
        /// </summary>
        public void EndSpawn()
        {
            IsSpawnEnded = true;
        }

        /// <summary>
        /// Get List of dinosaurs in scene
        /// </summary>
        /// <returns></returns>
        public List<DinosaurController> GetAllDinosaurs()
        {
            return _dinosaurSpawnedList;
        }


        private void UpdateSpawning()
        {
            TryToSpawnDinosaur();
            UpdateSpawningTimers();
            CheckIfCanGoToTheNextWave();
        }

        private void UpdateSpawningTimers()
        {
            if (!IsSpawning)
                return;

            for (int i = 0; i < spawnWaves.MaxEnemies; i++)
            {
                _timer[i] += Time.deltaTime;
            }
        }

        private void TryToSpawnDinosaur()
        {
            if (!IsSpawning)
                return;

            for (int i = 0; i < Waves[_indexWaves].dinosaurControllers.Length; i++)
            {
                if (Waves[_indexWaves].maxEnemies[i] > 0 && _timer[i] > Waves[_indexWaves].spawnInterval[i])
                {
                    SpawnDinosaur(Waves[_indexWaves].dinosaurControllers[i]);
                    Waves[_indexWaves].maxEnemies[i]--;
                    _timer[i] = 0.0f;
                }
            }

        }

        private void SpawnDinosaur(DinosaurScriptableObject dinosaurData)
        {
            var spawnAreaY = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            Vector3 spawnPosition = new Vector3(spawnArea.x, spawnAreaY, spawnArea.z);
            var dinosaur = Instantiate(dinosaurData.dinosaurPrefab, spawnPosition, Quaternion.identity);
            dinosaur.Initialize(dinosaurData);
            _dinosaurSpawnedList.Add(dinosaur);
            dinosaur.DinosaurDead += OnDinosaurDead;
        }

        private void CheckIfCanGoToTheNextWave()
        {
            for (int i = 0; i < Waves[_indexWaves].dinosaurControllers.Length; i++)
            {
                if (Waves[_indexWaves].maxEnemies[i] > 0)
                    return;
            }

            IsSpawning = false;
            TimeBetweenWaves -= Time.deltaTime;
            if (TimeBetweenWaves < 0)
            {
                _indexWaves++;
                WaveUpdated?.Invoke(_indexWaves);
                TimeBetweenWaves = spawnWaves.timeBetweenWaves; ;
                IsSpawning = true;
            }

            if (_indexWaves >= Waves.Length)
            {
                SpawningEnded?.Invoke();
                IsSpawnEnded = true;
            }


        }

        private void OnDinosaurDead(DinosaurController dinosaur)
        {
            dinosaur.DinosaurDead -= OnDinosaurDead;
            if (_dinosaurSpawnedList.Contains(dinosaur))
                _dinosaurSpawnedList.Remove(dinosaur);
        }
    }
}

