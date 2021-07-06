
namespace CraftsPeople
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using CraftsPeople.Data;

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField]
        private string workshopSceneName = "Workshop";
        [SerializeField]
        private ShoppingList shoppingList;

        private static WorkshopData requestedWorkshop;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
                DestroyImmediate(this);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public static void RequestLoadWorkshop(WorkshopData workshopData)
        {
            requestedWorkshop = workshopData;
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (instance == null)
                throw new System.Exception("Game manager instance is missing. You must start in the main menu scene!");
            SceneManager.LoadScene(instance.workshopSceneName);
        }

        private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (requestedWorkshop != null)
            {
                WorkshopManager workshopManager = FindObjectOfType<WorkshopManager>();

                if (workshopManager == null)
                    throw new System.Exception("No workshop manager found, is the wrong scene loaded?");

                workshopManager.Initialize(requestedWorkshop);
                requestedWorkshop = null;
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }
    }
}
