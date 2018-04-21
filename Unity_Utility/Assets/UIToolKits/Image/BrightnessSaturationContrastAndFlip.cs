using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIToolKits.ImageOperation
{
    public static class BrightnessSaturationContrastAndFlip
    {

        public static RenderTexture ProcessTexture(RenderTexture src, Material mat, float brightness = 1f, float contrast = 1f, int isHoriFlip = 0, int isVertiFlip = 0)
        {
            RenderTexture des = new RenderTexture(src.width, src.height, 16, RenderTextureFormat.ARGB32);

            if (mat != null)
            {
                mat.SetFloat("_Brightness", brightness);
                mat.SetFloat("_Saturation", 1f);
                mat.SetFloat("_Contrast", contrast);
                mat.SetInt("_IsVerticalFlip", isVertiFlip);
                mat.SetInt("_IsHorizontalFlip", isHoriFlip);

                Graphics.Blit(src, des, mat);
            }
            return des;
        }

        public static RenderTexture ProcessTexture(Texture2D src, Material mat, float brightness = 1f, float contrast = 1f, int isHoriFlip = 0, int isVertiFlip = 0)
        {
            RenderTexture des = new RenderTexture(src.width, src.height, 16, RenderTextureFormat.ARGB32);

            if (mat != null)
            {
                mat.SetFloat("_Brightness", brightness);
                mat.SetFloat("_Saturation", 1f);
                mat.SetFloat("_Contrast", contrast);
                mat.SetInt("_IsVerticalFlip", isVertiFlip);
                mat.SetInt("_IsHorizontalFlip", isHoriFlip);
                Graphics.Blit(src, des, mat);
            }
            return des;
        }
    }
}
