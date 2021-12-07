using UnityEngine;

namespace API.Game
{
    /// <summary>
    /// Wave Data
    /// </summary>
    [CreateAssetMenu(fileName = "SpawnWave", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 2)]
    public class SpawnWavesScriptableObject : ScriptableObject
    {
        public Wave[] waves;
        public float timeBetweenWaves = 3f;
        public int MaxEnemies = 4;
    }
}
