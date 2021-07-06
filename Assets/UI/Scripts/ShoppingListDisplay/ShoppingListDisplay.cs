
namespace CraftsPeople.UI
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ShoppingListDisplay : MonoBehaviour
    {
        [SerializeField]
        bool autoDisplay = true;

        [SerializeField]
        ShoppingList shoppingList;

        [SerializeField]
        List<ShoppingListItemDisplay> itemDisplays = new List<ShoppingListItemDisplay>();

        private void Start()
        {
            if (autoDisplay)
                Display();
        }

        public void HideDisplay()
        {
            for (int i = 0; i < itemDisplays.Count; i++)
                itemDisplays[i].gameObject.SetActive(false);
        }

        public void Display()
        {
            if (shoppingList.ItemCount > itemDisplays.Count)
            {
                throw new System.Exception(
                    "There are more items on the shopping list than item displays!");
            }

            for (int i = 0; i < itemDisplays.Count; i++)
            {
                if (i < shoppingList.ItemCount)
                {
                    itemDisplays[i].gameObject.SetActive(true);
                    itemDisplays[i].Display(shoppingList, i);
                }
                else
                    itemDisplays[i].gameObject.SetActive(false);
            }
        }
    }
}