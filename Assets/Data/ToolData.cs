
namespace CraftsPeople.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ToolData-", menuName = "ScriptableObjects/ToolData")]
    public class ToolData : ScriptableObject
    {
        public string displayName;
        public Sprite displayImage;
    }
}
