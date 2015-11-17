////////////////////////////////////////////////////////////////////////////////
//  
// @module Mobile Social Plugin 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using System;
using UnityEngine;
using UnionAssets.FLE;
using System.Collections;

public class SPInstagram : SA_Singleton<SPInstagram>  {

	public Action<InstagramPostResult> OnPostingCompleteAction = delegate {};


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {
		Debug.Log("SPInstagram subscribed");
		switch(Application.platform) {

		case RuntimePlatform.IPhonePlayer:
			IOSInstagramManager.instance.addEventListener(InstagramEvents.POST_SUCCEEDED, OnPost);
			IOSInstagramManager.instance.addEventListener(InstagramEvents.POST_FAILED, OnPostFailed);
			break;
		case RuntimePlatform.Android:
			AndroidInstagramManager.instance.addEventListener(InstagramEvents.POST_SUCCEEDED, OnPost);
			AndroidInstagramManager.instance.addEventListener(InstagramEvents.POST_FAILED, OnPostFailed);
			break;
		}
		
		DontDestroyOnLoad(gameObject);
	}


	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	

	public void Share(Texture2D texture) {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			IOSInstagramManager.instance.Share(texture);
			break;
		case RuntimePlatform.Android:
			Debug.Log(AndroidInstagramManager.instance);
			AndroidInstagramManager.instance.Share(texture);
			break;
		}

	}
	
	
	public void Share(Texture2D texture, string message) {
		switch(Application.platform) {
		case RuntimePlatform.IPhonePlayer:
			IOSInstagramManager.instance.Share(texture, message);
			break;
		case RuntimePlatform.Android:
			AndroidInstagramManager.instance.Share(texture, message);
			break;
		}
	}



	
	private void OnPost() {
		Debug.Log("SPInstagram OnPost");
		OnPostingCompleteAction(InstagramPostResult.RESULT_OK);
	}
	
	private void OnPostFailed(CEvent e) {
		Debug.Log("SPInstagram OnPostFailed");
		InstagramPostResult error = (InstagramPostResult) e.data;
		OnPostingCompleteAction(error);
	}

	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
