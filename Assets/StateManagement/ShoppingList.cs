
namespace CraftsPeople
{
    using CraftsPeople.Data;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "ShoppingList", menuName = "ScriptableObjects/ShoppingList")]
    public class ShoppingList : ScriptableObject
    {
        [SerializeField]
        private List<ManufactoringData> items = new List<ManufactoringData>();
        [SerializeField]
        private List<ItemState> itemStates = new List<ItemState>();

        public int ItemCount
        {
            get { return items.Count; }
        }

        public ManufactoringData GetItem(int index)
        {
            return items[index];
        }

        private ItemState GetItemState(ManufactoringData item)
        {
            int index = items.IndexOf(item);
            return GetItemState(index);
        }

        private ItemState GetItemState(int index)
        {
            return itemStates[index];
        }

        private void OnValidate()
        {
            // Make sure that there are always as many items as item states.
            if (items.Count != itemStates.Count)
            {
                int difference = Mathf.Abs(items.Count - itemStates.Count);

                // Add misssing item states.
                if (items.Count > itemStates.Count)
                {
                    for (int i = 0; i < difference; i++)
                        itemStates.Add(ItemState.Default);
                }
                else // Remove spare item states.
                {
                    int startIndex = itemStates.Count - difference;
                    itemStates.RemoveRange(startIndex, difference);
                }
            }
        }

        public enum ItemState
        {
            Default,
            WorkingOn,
            Finished
        }
    }
}