using UnityEngine;

public class ScreenshotAlternative
{
    public static AndroidJavaObject _activity;
	public static AndroidJavaObject Activity
         {
             get
             {
                 if (_activity == null)
                 {
                     var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                     _activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                 }
                 return _activity;
             }
         }
         
    private const string MediaStoreImagesMediaClass = "android.provider.MediaStore$Images$Media";
          public static string SaveImageToGallery(Texture2D texture2D, string title, string description)
          {
              using (var mediaClass = new AndroidJavaClass(MediaStoreImagesMediaClass))
              {
                  using (var cr = Activity.Call<AndroidJavaObject>("getContentResolver"))
                  {
                      var image = Texture2DToAndroidBitmap(texture2D);
                      var imageUrl = mediaClass.CallStatic<string>("insertImage", cr, image, title, description);
                      return imageUrl;
                  }
              }
          }
  
          public static AndroidJavaObject Texture2DToAndroidBitmap(Texture2D texture2D)
          {
              byte[] encoded = texture2D.EncodeToPNG();
              using (var bf = new AndroidJavaClass("android.graphics.BitmapFactory"))
              {
                  return bf.CallStatic<AndroidJavaObject>("decodeByteArray", encoded, 0, encoded.Length);
              }
          }
}

