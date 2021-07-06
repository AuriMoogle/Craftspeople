
namespace CraftsPeople.UI
{
    using CraftsPeople.Data;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class TextDisplayer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI authorDisplay;
        [SerializeField]
        private TextMeshProUGUI textDisplay;
        [SerializeField]
        private Image arrowImage;

        [SerializeField]
        private float delayBetweenCharsInSeconds = 0.001f;

        private DisplayableText currentTextToDisplay;
        private int currentCharIndex = 0;
        private bool displayDelayed;
        private float elapsedTime;

        private UnityAction onButtonClick;

		private void Awake()
		{
            ResetButton();
        }

		public void Display(DisplayableText displayableText, UnityAction onButtonClick, int buttonDirection)
        {
            Display(displayableText);

            this.onButtonClick = onButtonClick;

            arrowImage.gameObject.SetActive(true);
            arrowImage.gameObject.transform.localScale = 
                new Vector3(buttonDirection, buttonDirection, buttonDirection);
        }

        public void Display(DisplayableText displayableText)
        {
            authorDisplay.gameObject.SetActive(displayableText.HasAuthor);

            if (displayableText.HasAuthor)
                authorDisplay.text = displayableText.Author.AuthorName;

            currentTextToDisplay = displayableText;
            currentCharIndex = 0;
            elapsedTime = 0;
            displayDelayed = true;
            textDisplay.text = string.Empty;
        }

        public void ResetButton()
        {
            arrowImage.gameObject.SetActive(false);
            this.onButtonClick = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnTextDisplayClicked();
        }

        private void Update()
        {
            if (displayDelayed)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= delayBetweenCharsInSeconds)
                {
                    if (currentCharIndex >= currentTextToDisplay.DisplayText.Length)
                    {
                        displayDelayed = false;
                        return;
                    }

                    textDisplay.text += currentTextToDisplay.DisplayText[currentCharIndex];
                    currentCharIndex++;
                    elapsedTime = 0;
                }
            }
        }

        private void OnTextDisplayClicked()
        {
            if (displayDelayed)
                SkipTextTyping();
            else if (onButtonClick != null)
                onButtonClick.Invoke();
        }

        private void SkipTextTyping()
        {
            displayDelayed = false;
            textDisplay.text = currentTextToDisplay.DisplayText;
        }
    }
}