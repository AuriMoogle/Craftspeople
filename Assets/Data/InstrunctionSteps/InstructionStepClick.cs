
namespace CraftsPeople
{
    using CraftsPeople.UI;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "InstructionStep-Click", menuName = "ScriptableObjects/InstructionStep-Click")]
    public class InstructionStepClick : InstructionStep
    {
        public override void Enter(TextDisplayer textDisplayer, List<InteractableObject> interactables, ToolsDisplay toolsDisplay, Action onFinished)
        {
            base.Enter(textDisplayer, interactables, toolsDisplay, onFinished);
            for (int i = 0; i < interactables.Count; i++)
            {
                interactables[i].Clicked += OnInteractableObjectClicked;
            }
        }

        public override void Exit()
        {
            base.Exit();
            for (int i = 0; i < interactables.Count; i++)
            {
                interactables[i].Clicked -= OnInteractableObjectClicked;
            }
        }

        private void OnInteractableObjectClicked(InteractableObject obj)
        {
            if (obj.ID == InteractionTarget)
                onStepFinished?.Invoke();
        }
    }
}
