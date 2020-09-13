using System;
using UnityEngine;

namespace NewtsWrath
{
    public static class Utils
    {
        public static Texture2D LoadTexture2D(Byte[] resourceBytes)
        {
            //Check to make sure that the byte array supplied is not null, and throw an appropriate exception if they are.
            if (resourceBytes == null) throw new ArgumentNullException(nameof(resourceBytes));

            //Create a temporary texture, then load the texture onto it.
            var tempTex = new Texture2D(128, 128, TextureFormat.RGBAFloat, false);
            ImageConversion.LoadImage(tempTex, resourceBytes, false);
            return tempTex;
        }
    }

}
