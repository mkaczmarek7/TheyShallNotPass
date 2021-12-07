using API.Dinosaur;
using System;
using UnityEngine;

namespace App.Game
{
    /// <summary>
    /// Ark's collider which catch all dinosaur
    /// </summary>
    public class TSNPCollider : MonoBehaviour
    {
        /// <summary>
        /// Called when dinosaur enter to the collider
        /// </summary>
        public static event Action LifeLost;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                LifeLost?.Invoke();
                other.GetComponent<DinosaurController>().Kill(true);
            }
        }
    }
}
