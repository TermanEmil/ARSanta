using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour {

	// [SerializeField] GameObject blink;
	 [SerializeField] Animator blinkAnimator;

	public void TakeAShot()
	{
		StartCoroutine ("CaptureIt");
		
	}
     
	int captureTry = 0;
	IEnumerator CaptureIt()
	{    
		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		string fileName = "/Screenshot" + timeStamp + ".png";

		if(captureTry < 10000000)
		{
			ScreenCapture.CaptureScreenshot("/Screenshot" + timeStamp + ".jpg");
			Debug.Log("Captured screenshot method 1");
		}
		else
		{
		yield return new WaitForEndOfFrame();
     	int width = Screen.width;
     	int height = Screen.height;
     	Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
     	tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
     	tex.Apply();
		ScreenshotAlternative.SaveImageToGallery(tex, "some title", "Some desc");
			Debug.Log("Captured screenshot method 2");
		}		

		captureTry++;

		blinkAnimator.SetTrigger("Blink");
		Instantiate (blinkAnimator, new Vector2(0f, 0f), Quaternion.identity);
	}
}