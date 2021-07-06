namespace CraftsPeople.UI
{
    using CraftsPeople.Data;
    using System.Collections.Generic;
    using UnityEngine;

    public class ToolsDisplay : MonoBehaviour
    {
        [SerializeField]
        List<ToolDisplay> displays = new List<ToolDisplay>();

        public List<ToolDisplay> Displays { get { return displays; } }

        public void HideDisplays()
        {
            for (int i = 0; i < displays.Count; i++)
                displays[i].gameObject.SetActive(false);
        }

        public void Display(List<ToolData> toolDatas)
        {
            if (toolDatas.Count > displays.Count)
            {
                throw new System.Exception(
                    "There are more items on the shopping list than item displays!");
            }

            for (int i = 0; i < displays.Count; i++)
            {
                if (i < toolDatas.Count)
                {
                    displays[i].gameObject.SetActive(true);
                    displays[i].Display(toolDatas[i]);
                }
                else
                    displays[i].gameObject.SetActive(false);
            }
        }
    }
}