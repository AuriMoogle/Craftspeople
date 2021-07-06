
namespace CraftsPeople
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class InteractableObject : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
    {
        public bool IsInteractable = true;

        public event System.Action<InteractableObject> Clicked;
        public event System.Action<InteractableObjectDroppedOnArgs> DroppedOn;

        [HideInInspector]
        public Sprite ID;

        [SerializeField]
        private Color clickFeedbackColor = Color.cyan;
        [SerializeField, FormerlySerializedAs("interactionFeedbackTime")]
        private float clickFeedbackDuration = 0.25f;

        private float elapsedClickFeedbackTime;
        private bool clickFeedbackIsDisplayed;
        private bool removeClickFeedbackDelayed;

        [SerializeField]
        private Color helpHighlightColor = Color.red;
        private bool helpHighlightEnabled;
        private bool removeHelpHighlightDelayed;

        private Outline outline;
        private Color hoverHighlightColor;

        private Image image;
        private Color originalImageColor;

        private void Start()
        {
            image = GetComponent<Image>();
            ID = image.sprite;
            image.alphaHitTestMinimumThreshold = 0.8f;
            originalImageColor = image.color;

            outline = GetComponent<Outline>();
            hoverHighlightColor = outline.effectColor;

            WorkshopManager.RegisterInteractableObject(this);
        }

        private void OnDestroy()
        {
            WorkshopManager.DeregisterInteractableObject(this);
        }

        private void Update()
        {
            if (removeClickFeedbackDelayed)
            {
                elapsedClickFeedbackTime += Time.deltaTime;
                if (elapsedClickFeedbackTime >= clickFeedbackDuration)
                {
                    RemoveClickFeedback();

                    if (removeHelpHighlightDelayed)
                        outline.enabled = false;
                }
            }
        }

        public void OnHoverStart()
        {
            if (IsInteractable)
            {
                outline.enabled = true;
                outline.effectColor = hoverHighlightColor;
            }
        }

        public void OnHoverEnd()
        {
            if (helpHighlightEnabled)
                outline.effectColor = helpHighlightColor;
            else
                outline.enabled = false;
        }

        public void DropedOn(GameObject droppedObject)
        {
            if (IsInteractable == false)
                return;

            var args = new InteractableObjectDroppedOnArgs(this, droppedObject);
            DroppedOn?.Invoke(args);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsInteractable == false)
                return;

            RemoveClickFeedbackDelayed();
            Clicked?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsInteractable == false)
                return;

            EnableClickFeedback();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsInteractable == false)
                return;

            RemoveClickFeedbackDelayed();
        }

        private void EnableClickFeedback()
        {
            clickFeedbackIsDisplayed = true;
            removeClickFeedbackDelayed = false;

            image.color = clickFeedbackColor;
        }

        private void RemoveClickFeedbackDelayed()
        {
            if (clickFeedbackIsDisplayed)
            {
                removeClickFeedbackDelayed = true;
                elapsedClickFeedbackTime = 0;
            }
        }

        private void RemoveClickFeedback()
        {
            elapsedClickFeedbackTime = 0;
            removeClickFeedbackDelayed = false;
            image.color = originalImageColor;
        }

        public void EnableHelpHighlight()
        {
            helpHighlightEnabled = true;
            outline.enabled = true;
            outline.effectColor = helpHighlightColor;
        }

        public void DisableHelpHighlight()
        {
            helpHighlightEnabled = false;
            removeHelpHighlightDelayed = true;
        }
    }

    public struct InteractableObjectDroppedOnArgs
    {
        public readonly InteractableObject sender;
        public readonly GameObject droppedObject;

        public InteractableObjectDroppedOnArgs(InteractableObject sender, GameObject droppedObject)
        {
            this.sender = sender;
            this.droppedObject = droppedObject;
        }
    }
}
