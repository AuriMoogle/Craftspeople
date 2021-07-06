using UnityEngine;

namespace CraftsPeople.UI
{
    public class SceneLoader : MonoBehaviour
    {
        public void Load(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    } 
}
