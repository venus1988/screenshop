﻿using UnityEngine;
using UnionAssets.FLE;
using System;
using System.Collections;

public class InstagramPostScreenshotTask : EventDispatcher {

	public Action<InstagramPostResult> OnPostScreenshotCompleteAction = delegate {};

	private string text;

	public static InstagramPostScreenshotTask  Create() {
		return new GameObject("InstagramPostScreenshotTask").AddComponent<InstagramPostScreenshotTask>();
	}


	public void Post(string _text) {
		text = _text;
		StartCoroutine(PostScreenshot());
	}


	private IEnumerator PostScreenshot() {

		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();

		SPInstagram.instance.OnPostingCompleteAction += OnPostingCompleteAction;
		SPInstagram.instance.Share(tex, text);

		Destroy(tex);
	}
	
	void OnPostingCompleteAction (InstagramPostResult res) {
		SPInstagram.instance.OnPostingCompleteAction -= OnPostingCompleteAction;
		OnPostScreenshotCompleteAction (res);

		dispatch(BaseEvent.COMPLETE);
	}
}
