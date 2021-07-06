
namespace CraftsPeople.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using TMPro;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    public class NameDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private string displayName;

        [SerializeField]
        private DisplayStyle displayStyle = DisplayStyle.AbsoluteOffset;
        [SerializeField, Header("Absolute Offset")]
        private Vector3 displayOffset;
        [SerializeField, Header("Relative Offset")]
        private RectTransform.Edge relativeEdgeHorizontal;
        [SerializeField]
        private RectTransform.Edge relativeEdgeVertical;

        [SerializeField, Header("Display")]
        private float displayTimeInSeconds = 2f;
        [SerializeField]
        private int paddingInPixel = 20;
        [SerializeField]
        private GameObject displayPrefab;

        private GameObject displayInstance;
        private float elapsedTime;
        private bool disableDelayed;

        private void Awake()
        {
            displayInstance = Instantiate(displayPrefab, transform);
            displayInstance.name = "NameDisplay-" + displayName;

            UpdateTextDisplay();

            displayInstance.SetActive(false);
        }

        private void UpdateTextDisplay()
        {
            // update position
            if (displayStyle == DisplayStyle.AbsoluteOffset)
                displayInstance.transform.localPosition = displayOffset;

            // Set text
            var lable = displayInstance.GetComponentInChildren<TextMeshProUGUI>();
            lable.text = displayName;

            // Get text size
            var textSize = lable.GetPreferredValues(displayName);
            textSize.x += paddingInPixel;
            textSize.y += paddingInPixel;

            // Update display to text size
            RectTransform rectTransform = displayInstance.transform as RectTransform;
            if (displayStyle == DisplayStyle.RelativeToEdge)
            {
                rectTransform.SetInsetAndSizeFromParentEdge(relativeEdgeHorizontal, 0, textSize.x);
                rectTransform.SetInsetAndSizeFromParentEdge(relativeEdgeVertical, 0, textSize.y);
            }
            else if (displayStyle == DisplayStyle.AbsoluteOffset)
            {
                rectTransform.sizeDelta = textSize;
            }
        }

        private void Update()
        {
            if (disableDelayed)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= displayTimeInSeconds)
                {
                    HideDisplay();
                }
            }
        }

        public void HideDisplay()
        {
            disableDelayed = false;
            displayInstance.SetActive(false);
        }

        public void SetDisplayName(string newName)
        {
            displayName = newName;
            UpdateTextDisplay();
        }

        public void TriggerDisplay()
        {
            if (enabled == false)
                return;

            displayInstance.SetActive(true);
            elapsedTime = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TriggerDisplay();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            disableDelayed = true;
        }

        public enum DisplayStyle
        {
            AbsoluteOffset,
            RelativeToEdge
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(NameDisplay))]
        public class NameDisplayEditor : Editor
        {
            private void OnSceneGUI()
            {
                NameDisplay nameDisplay = (NameDisplay)target;
                Vector3 position = nameDisplay.transform.TransformPoint(nameDisplay.displayOffset);

                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.DoPositionHandle(position, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Move Offset");
                    nameDisplay.displayOffset = nameDisplay.transform.InverseTransformPoint(newPosition);
                }
            }
        }
#endif
    }
}
