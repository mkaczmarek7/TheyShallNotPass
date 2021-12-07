using UnityEngine;

namespace API.Dinosaur
{
    /// <summary>
    /// Dinosaur Data
    /// </summary>
    [CreateAssetMenu(fileName = "DinosaurData", menuName = "ScriptableObjects/DinosaurObject", order = 2)]
    public class DinosaurScriptableObject : ScriptableObject
    {
        public DinosaurController dinosaurPrefab;

        public float speed;
        public float score = 0.0f;
        public float health = 100.0f;
        public float lifetime = 3;
        public float frozenTime = 3;

    }
}
