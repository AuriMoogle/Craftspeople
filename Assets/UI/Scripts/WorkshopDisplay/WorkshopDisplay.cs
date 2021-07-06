
namespace CraftsPeople.UI
{
    using CraftsPeople.Data;
    using UnityEngine;
    using UnityEngine.UI;

    public class WorkshopDisplay : MonoBehaviour
    {
        private WorkshopData data;
        private Image previewImage;

        private void Awake()
        {
            previewImage = GetComponent<Image>();
        }

        public void SetData(WorkshopData data)
        {
            this.data = data;
            previewImage.sprite = data.previewImage;
        }

        public void RequestLoadWorkshop()
        {
            GameManager.RequestLoadWorkshop(data);
        }
    }
}
