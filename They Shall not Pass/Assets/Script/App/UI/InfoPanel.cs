using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    /// <summary>
    /// UI Board with game status
    /// </summary>
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField]
        private Text scoreText;

        [SerializeField]
        private Text waveText;

        [SerializeField]
        private Text lifeText;

        [SerializeField]
        private Text finalText;

        private float _score;

        public void UpdateScoreText(float score)
        {
            _score += score;
            scoreText.text = _score.ToString();
        }

        public void UpdateWaveText(string text)
        {
            waveText.text = text;
        }

        public void UpdateLifeText(string text)
        {
            lifeText.text = text;
        }

        public void UpdateFinalText(string text)
        {
            finalText.text = text;
        }
    }
}
