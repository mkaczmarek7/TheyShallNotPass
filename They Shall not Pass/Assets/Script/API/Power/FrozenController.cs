using API.Game;
using UnityEngine;

namespace API.PowerUps
{
    /// <summary>
    /// PowerUps which block dinosaur movements
    /// </summary>
    public class FrozenController : PowerUpButton
    {
        [SerializeField]
        private DinosaurSpawner dinosaurSpawner;

        [SerializeField]
        private AudioSource audioFrozen;

        public override void Action()
        {
            base.Action();
            audioFrozen.Play();
            foreach (var dinosaur in dinosaurSpawner.GetAllDinosaurs())
            {
                dinosaur.ChangeFreezeMode(true);
            }
        }
    }
}
