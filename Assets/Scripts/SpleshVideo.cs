using UnityEngine;
using System.Collections;
using Prime31;

public class SpleshVideo : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		PlayerPrefs.SetString ("forFirstTime", "yes");
		Handheld.PlayFullScreenMovie("Sample11.mp4",Color.black,FullScreenMovieControlMode.CancelOnInput,FullScreenMovieScalingMode.Fill);
		//EtceteraAndroid.playMovie( "Sample11.mp4", 0xFF0000, false, EtceteraAndroid.ScalingMode.Fill, true );
		Application.LoadLevel("Login");
	}



	

}
