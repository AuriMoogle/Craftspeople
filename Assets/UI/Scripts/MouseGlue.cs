
namespace CraftsPeople
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System.Collections.Generic;

    public class MouseGlue : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        GraphicRaycaster raycaster;
        PointerEventData eventData;
        List<RaycastResult> results = new List<RaycastResult>();


        Vector3 originalObjectPosition;
        Vector3 fingerOffset;

        bool glued = false;

        InteractableObject lastInteractable;

        void Start()
        {
            raycaster = FindObjectOfType<GraphicRaycaster>();
            originalObjectPosition = transform.localPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            glued = true;
            fingerOffset =  transform.position - Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Reset object
            glued = false;
            transform.localPosition = originalObjectPosition;

            // Raycast
            eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            results.Clear();

            InteractableObject interactable = GetInteractableUnderMouse();
            if (interactable !=null)
            {
                // This is only the visual, send the parent as dropped object.
                interactable.DropedOn(this.transform.parent.gameObject);
                interactable.OnHoverEnd();
            }
        }

        private void Update()
        {
            if (glued)
            {
                transform.position = Input.mousePosition + fingerOffset;

                InteractableObject interactable = GetInteractableUnderMouse();
                if (interactable != lastInteractable && lastInteractable != null)
                    lastInteractable.OnHoverEnd();

                if (interactable != null)
                    interactable.OnHoverStart();

                lastInteractable = interactable;
            }
        }

        private InteractableObject GetInteractableUnderMouse()
        {
            // Raycast
            eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            results.Clear();

            // Search for interactable objects
            raycaster.Raycast(eventData, results);
            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                var interactable = result.gameObject.GetComponent<InteractableObject>();
                if (interactable != null && interactable.IsInteractable == true)
                    return interactable;
            }
            return null;
        }
    } 
}
