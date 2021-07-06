
namespace CraftsPeople.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "WorkshopData-", menuName = "ScriptableObjects/WorkshopData")]
    public class WorkshopData : ScriptableObject
    {
        public string workshopName;
        public Sprite previewImage;
        public GameObject workshopPrefab;

        [Header("Spooken Text")]
        public DisplayableText greeting;
        public DisplayableText task;
    }
}
