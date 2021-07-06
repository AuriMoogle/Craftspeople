
namespace CraftsPeople
{
    using CraftsPeople.Data;
    using CraftsPeople.UI;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "InstructionStep-Drag", menuName = "ScriptableObjects/InstructionStep-Drag")]
    public class InstructionStepDrag : InstructionStep
    {
        [SerializeField]
        ToolData toolToDrop;

        ToolDisplay toolDisplayInScene;

        public override void Enter(TextDisplayer textDisplayer, List<InteractableObject> interactables, ToolsDisplay toolsDisplay, Action onFinished)
        {
            base.Enter(textDisplayer, interactables, toolsDisplay, onFinished);

            for (int i = 0; i < interactables.Count; i++)
                interactables[i].DroppedOn += OnInteractableObjectDropped;

            var displays = toolsDisplay.Displays;
            for (int i = 0; i < displays.Count; i++)
            {
                if (displays[i].Tool == toolToDrop)
                {
                    toolDisplayInScene = displays[i];
                    break;
                }
            }
        }

        public override void Exit()
        {
            base.Exit();

            for (int i = 0; i < interactables.Count; i++)
                interactables[i].DroppedOn -= OnInteractableObjectDropped;

            toolDisplayInScene.DiableHighlight();
        }

        private void OnInteractableObjectDropped(InteractableObjectDroppedOnArgs obj)
        {
            if (obj.sender.ID == InteractionTarget)
            {
                ToolDisplay toolDisplay = obj.droppedObject.GetComponent<ToolDisplay>();
                if (toolDisplay != null && toolDisplay == toolDisplayInScene)
                        onStepFinished?.Invoke();
            }
        }

        public override void HighlightInteractionTargets()
        {
            base.HighlightInteractionTargets();
            toolDisplayInScene.EnableHighlight();
        }

        public override ToolData GetTool()
        {
            return toolToDrop;
        }
    }
}