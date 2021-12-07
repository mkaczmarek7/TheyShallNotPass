using App.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.UI
{
    /// <summary>
    /// Class which load chosen scene
    /// </summary>
    public class SceneSwitcher : MonoBehaviour
    {
        public TSNPScene scene;

        [SerializeField]
        private GameObject loadingPanel;

        public void OnSwitchScene()
        {
            SceneManager.LoadSceneAsync((int)scene);
            loadingPanel.SetActive(true);
        }
    }
}
