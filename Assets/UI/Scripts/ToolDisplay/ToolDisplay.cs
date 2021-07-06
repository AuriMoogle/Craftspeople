
namespace CraftsPeople.UI
{
    using CraftsPeople.Data;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class ToolDisplay : MonoBehaviour
    {
        public ToolData Tool { get; private set; }

        [SerializeField]
        private Image imageDisplay;

        [SerializeField]
        private NameDisplay nameDisplay;

        Outline outline;

        private void OnEnable()
        {
            outline = GetComponentInChildren<Outline>();
        }

        public void Display(ToolData tool)
        {
            Tool = tool;
            imageDisplay.sprite = tool.displayImage;
            nameDisplay.HideDisplay();
            nameDisplay.SetDisplayName(tool.displayName);
        }

        public void EnableHighlight()
        {
            outline.enabled = true;
        }

        public void DiableHighlight()
        {
            outline.enabled = false;
        }
    }
}
