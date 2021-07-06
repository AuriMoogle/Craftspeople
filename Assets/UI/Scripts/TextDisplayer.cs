
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

        public void EnableButton(UnityAction onButtonClick)
        {
            this.onButtonClick = onButtonClick;
        }

        public void ResetButton()
        {
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
            {
                onButtonClick.Invoke();
                arrowImage.gameObject.transform.localScale *= -1f;
            }
        }

        private void SkipTextTyping()
        {
            displayDelayed = false;
            textDisplay.text = currentTextToDisplay.DisplayText;
        }
    }
}