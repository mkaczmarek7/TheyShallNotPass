using UnityEngine;

namespace App.UI
{
    public class ApplicationQuitButton : MonoBehaviour
    {
        public void OnAppQuit()
        {
            Application.Quit();
        }
    }
}
