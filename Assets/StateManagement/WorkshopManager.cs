
namespace CraftsPeople
{
    using CraftsPeople.Data;
    using CraftsPeople.UI;
    using System.Collections.Generic;
    using UnityEngine;

    public class WorkshopManager : MonoBehaviour
    {
        [SerializeField]
        TextDisplayer spookenTextDisplayer;

        private readonly string animatedSceneParentTag = "AnimatedSceneParent";
        private readonly string InitialInteractableTag = "InitialInteractable";

        private State workshopState = State.WaitingForManufactoringData;
        private WorkshopData currentWorkshopData;
        private ManufactoringData currentManufactoringData;
        private InstructionStep currentInstruction;
        private int instructionIndex = 0;

        [SerializeField]
        private ShoppingListDisplay shoppingListDisplay;
        [SerializeField]
        private ToolsDisplay toolsDisplay;

        private List<InteractableObject> interactableObjects = new List<InteractableObject>();

        private static WorkshopManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                toolsDisplay.HideDisplays();
            }
            else
                Destroy(this);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < interactableObjects.Count; i++)
                interactableObjects[i].DroppedOn -= instance.OnInteractableObjectDropped;
        }

        private void Update()
        {
            if (workshopState == State.StartInstructionState)
            {
                currentInstruction = currentManufactoringData.instructions[instructionIndex];
                DisplayTools(currentInstruction);
                currentInstruction.Enter(spookenTextDisplayer, interactableObjects, toolsDisplay, OnStateFinished);
                workshopState = State.WaitingForStateToFinish;
            }
        }

        private void DisplayTools(InstructionStep instruction)
        {
            int displaySpace = toolsDisplay.Displays.Count;

            List<ToolData> allTools = new List<ToolData>(currentManufactoringData.allTools);
            allTools.Shuffle();
            List<ToolData> toolsToDisplay = new List<ToolData>(displaySpace);

            ToolData neededTool = instruction.GetTool();
            if (neededTool != null)
                toolsToDisplay.Add(neededTool);

            for (int i = 0; i < allTools.Count; i++)
            {
                if (toolsToDisplay.Count == displaySpace)
                    break;

                if (allTools[i] != neededTool)
                    toolsToDisplay.Add(allTools[i]);
            }

            toolsToDisplay.Shuffle();
            toolsDisplay.Display(toolsToDisplay);
        }

        private void OnStateFinished()
        {
            currentInstruction.Exit();
            instructionIndex++;

            if (instructionIndex >= currentManufactoringData.instructions.Count)
                workshopState = State.Finished;
            else
                workshopState = State.StartInstructionState;
        }

        public void Initialize(WorkshopData data)
        {
            currentWorkshopData = data;
            currentManufactoringData = null;

            CreateWorkshop();

            shoppingListDisplay.Display();
            DisplayGreeting();
        }

        private void DisplayGreeting()
        {
            spookenTextDisplayer.Display(currentWorkshopData.greeting, DisplayTask, 1);
        }

        private void DisplayTask()
        {
            spookenTextDisplayer.Display(currentWorkshopData.task, DisplayGreeting, -1);
        }

        public static void RegisterInteractableObject(InteractableObject interactableObject)
        {
            instance.interactableObjects.Add(interactableObject);

            if (interactableObject.gameObject.CompareTag(instance.InitialInteractableTag))
                interactableObject.DroppedOn += instance.OnInteractableObjectDropped;
            else
                interactableObject.IsInteractable = false;
        }

        public static void DeregisterInteractableObject(InteractableObject interactableObject)
        {
            instance.interactableObjects.Remove(interactableObject);
            interactableObject.DroppedOn -= instance.OnInteractableObjectDropped;
        }

        public void DisplayHelp()
        {
            if (currentInstruction != null)
                currentInstruction.HighlightInteractionTargets();
        }

        private void OnInteractableObjectDropped(InteractableObjectDroppedOnArgs obj)
        {
            if (workshopState == State.WaitingForManufactoringData)
            {
                if (GetManufactoringData(obj))
                {
                    workshopState = State.StartInstructionState;
                    instructionIndex = 0;

                    // Enable all interactables in the scene -> make it playable.
                    for (int i = 0; i < interactableObjects.Count; i++)
                        interactableObjects[i].IsInteractable = true;

                    // Disable the initianl interactable. (The background of the image)
                    // We only need it to detect the drop of an product on the craft image.
                    obj.sender.DroppedOn -= instance.OnInteractableObjectDropped;
                    obj.sender.IsInteractable = false;

                    // Disable product display. We got a manufactoring data. It is not longer needed.
                    shoppingListDisplay.HideDisplay();
                }
            }
        }

        private bool GetManufactoringData(InteractableObjectDroppedOnArgs obj)
        {
            ShoppingListItemDisplay shoppingListDisplay = obj.droppedObject.GetComponent<ShoppingListItemDisplay>();
            if (shoppingListDisplay != null)
            {
                ManufactoringData manufactoringData = shoppingListDisplay.GetDisplayedObject();

                // Object can be created.
                if (manufactoringData.workshop == instance.currentWorkshopData)
                {
                    instance.currentManufactoringData = manufactoringData;
                    return true;
                }
                else // Object can not be created
                {
                    if (manufactoringData.tips.Count > 0)
                    {
                        int randomIndex = Random.Range(0, manufactoringData.tips.Count);
                        DisplayableText tipForCorrectWorkshop = manufactoringData.tips[randomIndex];
                        instance.spookenTextDisplayer.Display(tipForCorrectWorkshop);
                    }
                    return false;
                }
            }
            return false;
        }

        private void CreateWorkshop()
        {
            GameObject parent = GameObject.FindGameObjectWithTag(animatedSceneParentTag);

            if (parent == null)
                throw new System.Exception("No animated scene parent found, is the tag '" + animatedSceneParentTag + "' wrong?");

            Instantiate(currentWorkshopData.workshopPrefab, parent.transform);
        }

        private enum State
        {
            WaitingForManufactoringData,
            StartInstructionState,
            WaitingForStateToFinish,
            Finished
        }
    }
}

