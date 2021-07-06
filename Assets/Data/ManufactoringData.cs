
namespace CraftsPeople.Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Video;

    [CreateAssetMenu(fileName = "ManufactoringData-", menuName = "ScriptableObjects/ManufactoringData")]
    public class ManufactoringData : ScriptableObject
    {
        public string productName;
        public Sprite productImage;
        public WorkshopData workshop;
        public List<DisplayableText> tips = new List<DisplayableText>();
        public List<InstructionStep> instructions = new List<InstructionStep>();
        public List<ToolData> allTools = new List<ToolData>();
    }
}
