using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIToolKits.ImageOperation
{
    public static class ImageFilp
    {

        public static void HorizontalFlipImage(Image image)
        {
            Texture2D newTexture = HorizontalFlipPic(image.sprite.texture);
            Sprite newSprite = Sprite.Create(newTexture, new Rect(Vector2.zero, new Vector2(newTexture.width, newTexture.height)), Vector2.zero);

            image.sprite = newSprite;
        }

        public static void VerticalFlipImage(Image image)
        {
            Texture2D newTexture = VerticalFlipPic(image.sprite.texture);
            Sprite newSprite = Sprite.Create(newTexture, new Rect(Vector2.zero, new Vector2(newTexture.width, newTexture.height)), Vector2.zero);

            image.sprite = newSprite;
        }

        //水平翻转texture2d
        public static Texture2D HorizontalFlipPic(Texture2D texture2d)
        {
            int width = texture2d.width;//得到图片的宽度.   
            int height = texture2d.height;//得到图片的高度 

            Texture2D NewTexture2d = new Texture2D(width, height);//创建一张同等大小的空白图片 

            int i = 0;

            while (i < width)
            {
                NewTexture2d.SetPixels(i, 0, 1, height, texture2d.GetPixels(width - i - 1, 0, 1, height));
                i++;
            }
            NewTexture2d.Apply();

            return NewTexture2d;
        }

        //垂直翻转texture2d
        public static Texture2D VerticalFlipPic(Texture2D texture2d)
        {
            int width = texture2d.width;//得到图片的宽度.   
            int height = texture2d.height;//得到图片的高度 

            Texture2D NewTexture2d = new Texture2D(width, height);//创建一张同等大小的空白图片 

            int i = 0;

            while (i < height)
            {
                NewTexture2d.SetPixels(0, i, width, 1, texture2d.GetPixels(0, height - i - 1, width, 1));
                i++;
            }
            NewTexture2d.Apply();

            return NewTexture2d;
        }

        public static Texture2D toTexture2D(this RenderTexture rTex)
        {
            Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            return tex;
        }
    }

}
