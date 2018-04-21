using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIToolKits.ImageOperation
{
    public class TextureAlign : MonoBehaviour
    {

        public enum MatchMode
        {
            FixedByImage = 0,
            AlignByImageWidth = 1,
        }

        public static void SetTextureToImage(Image image, Texture2D texture2D, MatchMode matMod = MatchMode.FixedByImage)
        {
            Sprite tempSprite = Sprite.Create(texture2D, new Rect(Vector2.zero, new Vector2(texture2D.width, texture2D.height)), Vector2.zero);

            switch (matMod)
            {
                case MatchMode.FixedByImage:
                    image.sprite = tempSprite;
                    break;
                case MatchMode.AlignByImageWidth:
                    image.sprite = tempSprite;
                    Vector2 wh = new Vector2(image.rectTransform.rect.width,
                                             image.rectTransform.rect.width * (texture2D.height * 1.0f / texture2D.width * 1.0f));
                    SetWidthAndHeightOfRectTransform(image.rectTransform, wh);
                    break;
            }
        }

        public static void SetTextureToImage(RawImage rawImage, Texture texture2D, MatchMode matMod = MatchMode.FixedByImage)
        {

            switch (matMod)
            {
                case MatchMode.FixedByImage:
                    rawImage.texture = texture2D;
                    break;
                case MatchMode.AlignByImageWidth:
                    rawImage.texture = texture2D;
                    Vector2 wh = new Vector2(rawImage.rectTransform.rect.width,
                                             rawImage.rectTransform.rect.width * (texture2D.height * 1.0f / texture2D.width * 1.0f));
                    SetWidthAndHeightOfRectTransform(rawImage.rectTransform, wh);
                    break;
            }
        }

        public static void SetWidthAndHeightOfRectTransform(RectTransform recTra, Vector2 wh)
        {
            float anchorRectWidth = recTra.parent.GetComponent<RectTransform>().rect.width * (recTra.anchorMax.x - recTra.anchorMin.x);
            float anchorRectHeight = recTra.parent.GetComponent<RectTransform>().rect.height * (recTra.anchorMax.y - recTra.anchorMin.y);
            //原理：sizeDeltaX = recTra.rect.width - anchorRectWidth;
            //     sizeDeltaY = recTra.rect.height - anchorRectHeight;
            float sizeDeltaX = wh.x - anchorRectWidth;
            float sizeDeltaY = wh.y - anchorRectHeight;

            recTra.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY);
            Debug.Log("sizeDeltaY:" + sizeDeltaY);
        }
    }
}

