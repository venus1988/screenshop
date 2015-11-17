using UnityEngine;
using System.Collections;

public class MyCam : MonoBehaviour {


		public WebCamTexture mCamera = null;
		public GameObject plane;

		// Use this for initialization
		void Start ()
		{
			Debug.Log ("Script has been started");
			mCamera = new WebCamTexture ();
			plane.GetComponent<MeshRenderer>().material.mainTexture = mCamera;
			mCamera.Play ();
		}

	public Material preview;
	void OnMouseUpAsButton() 
	{
		Resources.UnloadUnusedAssets();
		StartCoroutine(PostScreenshot());
		Debug.Log("Take Screenshots");
		preview.mainTexture=tex;
	}
	
	
	Texture2D tex;
	private IEnumerator PostScreenshot() 
	{
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();
		yield return new WaitForSeconds(1.0f);
		preview.mainTexture=tex;
	}


	/// <summary>
	/// based off 2 lines of Java code found at at http://stackoverflow.com/questions/18416122/open-gallery-app-in-androi
	///      Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse("content://media/internal/images/media"));
	///      startActivity(intent); 
	/// expanded the 1st line to these 3:
	///      Intent intent = new Intent();
	///      intent.setAction(Intent.ACTION_VIEW);
	///      intent.setData(Uri.parse("content://media/internal/images/media"));
	/// </summary>
	/// Intent intent = new Intent();
	/// 
	/// 
	// Show only images, no videos or anything else
	///intent.setType("image/*");
	///intent.setAction(Intent.ACTION_GET_CONTENT);
	// Always show the chooser (if there are multiple options available)
	///startActivityForResult(Intent.createChooser(intent, "Select Picture"), PICK_IMAGE_REQUEST);
	/// 
	/// 
	/// 
	/// 
	/// 
	/// 
	/*public void OpenAndroidGallery()
	{
		#region [ Intent intent = new Intent(); ]
		//instantiate the class Intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		//instantiate the object Intent
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		#endregion [ Intent intent = new Intent(); ]
		#region [ intent.setAction(Intent.ACTION_VIEW); ]
		//call setAction setting ACTION_SEND as parameter
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_VIEW"));
		#endregion [ intent.setAction(Intent.ACTION_VIEW); ]
		#region [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
		//instantiate the class Uri
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		//instantiate the object Uri with the parse of the url's file
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "content://media/internal/images/media");
		//call putExtra with the uri object of the file
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		#endregion [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
		//set the type of file
		intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
		#region [ startActivity(intent); ]
		//instantiate the class UnityPlayer
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//instantiate the object currentActivity
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//call the activity with our Intent
		currentActivity.Call("startActivity", intentObject);
		#endregion [ startActivity(intent); ]
	}*/



	public void OpenAndroidGallery()
	{
		#region [ Intent intent = new Intent(); ]
		//instantiate the class Intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		//instantiate the object Intent
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		#endregion [ Intent intent = new Intent(); ]
		#region [ intent.setAction(Intent.ACTION_GET_CONTENT); ]
		//call setAction setting ACTION_SEND as parameter
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_GET_CONTENT"));
		#endregion [ intent.setAction(Intent.ACTION_GET_CONTENT); ]
		#region [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
		//instantiate the class Uri
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		//instantiate the object Uri with the parse of the url's file
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "content://media/internal/images/media");
		//call putExtra with the uri object of the file
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		#endregion [ intent.setData(Uri.parse("content://media/internal/images/media")); ]
		//set the type of file
		intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
		#region [ startActivity(intent); ]
		//instantiate the class UnityPlayer
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//instantiate the object currentActivity
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//call the activity with our Intent
		currentActivity.Call("startActivity", intentObject);
		#endregion [ startActivity(intent); ]
	}

}
