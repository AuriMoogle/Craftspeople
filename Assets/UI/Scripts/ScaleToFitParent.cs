

namespace CraftsPeople.UI
{
    using UnityEngine;

    public class ScaleToFitParent : MonoBehaviour
    {
        RectTransform rectTransform;

        void OnEnable()
        {
            rectTransform = transform as RectTransform;

            float scale;
            Vector2 size = rectTransform.sizeDelta;

            Vector2 parentSize = GetParentSize();
            scale = parentSize.y / size.y;
            transform.localScale += Vector3.one * scale;
        }

        private Vector2 GetParentSize()
        {
            RectTransform parent = rectTransform.parent as RectTransform;
            if (!parent)
                return Vector2.zero;
            return parent.rect.size;
        }
    }
}
