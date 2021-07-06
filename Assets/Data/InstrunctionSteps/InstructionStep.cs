
namespace CraftsPeople
{
    using CraftsPeople.Data;
    using CraftsPeople.UI;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Serialization;

    public abstract class InstructionStep : ScriptableObject
    {
        [SerializeField]
        protected Sprite InteractionTarget;

        [SerializeField, FormerlySerializedAs("displayOnEnter")]
        protected DisplayableText instructionText;
        [SerializeField]
        protected DisplayableText taskText;

        protected List<InteractableObject> interactables = new List<InteractableObject>();
        protected ToolsDisplay toolsDisplay;
        protected Action onStepFinished;
        protected TextDisplayer textDisplayer;

        protected InteractableObject interactionTargetInScene;

        public virtual void Enter(TextDisplayer textDisplayer, List<InteractableObject> interactables, ToolsDisplay toolsDisplay, Action onFinished)
        {
            this.textDisplayer = textDisplayer;
            onStepFinished = onFinished;
            this.interactables = interactables;
            this.toolsDisplay = toolsDisplay;

            for (int i = 0; i < interactables.Count; i++)
            {
                if (interactables[i].ID == InteractionTarget)
                {
                    interactionTargetInScene = interactables[i];
                    break;
                }
            }

            DisplayFirstText();
        }

        public virtual void Exit()
        {
            if (interactionTargetInScene != null)
                interactionTargetInScene.DisableHelpHighlight();

            textDisplayer.ResetButton();
        }

        public virtual ToolData GetTool() { return null; }

        public virtual void HighlightInteractionTargets()
        {
            if (interactionTargetInScene != null)
                interactionTargetInScene.EnableHelpHighlight();
        }

        private void DisplayFirstText()
        {
            UnityAction onClick = () => DisplaySecondText();
            textDisplayer.Display(instructionText, onClick, 1);
        }

        private void DisplaySecondText()
        {
            UnityAction onClick = () => DisplayFirstText();
            textDisplayer.Display(taskText, onClick, -1);
        }
    }
}
