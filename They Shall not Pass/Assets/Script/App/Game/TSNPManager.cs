using API.Dinosaur;
using API.Game;
using App.UI;
using UnityEngine;

namespace App.Game
{
    /// <summary>
    /// Class which control game state
    /// </summary>
    public class TSNPManager : MonoBehaviour
    {
        [SerializeField]
        private DinosaurSpawner dinosaurSpawner;

        [SerializeField]
        private InfoPanel infoPanel;

        public int life = 10;

        /// <summary>
        /// Player Health
        /// </summary>
        public int Life
        {
            get
            {
                return life;
            }

            private set
            {
                if (value >= 0)
                    life = value;
                infoPanel.UpdateLifeText(life.ToString());
                if (life <= 0)
                {
                    dinosaurSpawner.EndSpawn();
                    infoPanel.UpdateFinalText("GameOver");
                }
            }
        }

        private void Start()
        {
            InitializeInfoPanel();
        }

        private void InitializeInfoPanel()
        {

            infoPanel.UpdateWaveText("0");
            infoPanel.UpdateLifeText(life.ToString());
            infoPanel.UpdateScoreText(0);
        }

        private void OnEnable()
        {
            TSNPCollider.LifeLost += OnLifeLost;
            dinosaurSpawner.SpawningEnded += OnSpawningEnded;
            dinosaurSpawner.WaveUpdated += OnWaveUpdated;
            DinosaurController.PointsScored += OnPointsScored;
        }

        private void OnDisable()
        {
            TSNPCollider.LifeLost -= OnLifeLost;
            dinosaurSpawner.SpawningEnded -= OnSpawningEnded;
            dinosaurSpawner.WaveUpdated -= OnWaveUpdated;
            DinosaurController.PointsScored -= OnPointsScored;
        }

        private void OnWaveUpdated(int indexWave)
        {
            infoPanel.UpdateWaveText(indexWave.ToString());
        }

        private void OnPointsScored(float score)
        {
            infoPanel.UpdateScoreText(score);
        }

        private void OnLifeLost()
        {
            Life--;
        }

        private void OnSpawningEnded()
        {
            infoPanel.UpdateFinalText("YOU WIN!");
        }
    }
}
