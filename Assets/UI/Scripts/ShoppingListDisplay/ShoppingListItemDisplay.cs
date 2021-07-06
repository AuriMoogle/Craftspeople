
namespace CraftsPeople.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using CraftsPeople.Data;
    using TMPro;

    public class ShoppingListItemDisplay : MonoBehaviour
    {
        [SerializeField]
        Image image;
        [SerializeField]
        TextMeshProUGUI nameLable;

        ManufactoringData itemData;

        public void HideDisplay()
        {
            image.enabled = false;
            nameLable.enabled = false;
        }

        public void ShowDisplay()
        {
            image.enabled = true;
            nameLable.enabled = true;
        }

        public ManufactoringData GetDisplayedObject()
        {
            return itemData;
        }

        /// <summary>
        /// Calling 'Display' will automatically show the display if it was formerly hidden.
        /// </summary>
        public void Display (ShoppingList shoppingList, int itemIndex)
        {
            ShowDisplay();
            itemData = shoppingList.GetItem(itemIndex);
            image.sprite = itemData.productImage;
            nameLable.text = itemData.productName;
        }
    }
}
