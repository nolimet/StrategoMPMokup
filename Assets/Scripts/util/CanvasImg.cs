using UnityEngine;
using System.Collections;

namespace util
{
    [System.Serializable]
    public class CanvasImg
    {
        public RectTransform RectTransform;
        public Vector2 orignalSize;
        public Vector3 orignalPos;

        public void SetOrignals()
        {
            orignalPos = new Vector3(RectTransform.rect.x, RectTransform.rect.y);
            orignalSize = new Vector2(RectTransform.rect.width, RectTransform.rect.height);
        }

        public void ResizeScale(float x, float y = 1f)
        {
            RectTransform.sizeDelta = new Vector2(orignalSize.x * x, orignalSize.y * y);
        }
    }
}