using UnityEngine;
using System.Collections;

public class Pause_Menu : MonoBehaviour {

	public GUITexture textureOverlay;
	public int overlayDepth = 1;
	private bool menuEnabled = false;

	// Use this for initialization
	void Start () {
		//on start, make texture same size as the screen.
		textureOverlay.transform.position = new Vector3(0, 0, overlayDepth);
		textureOverlay.pixelInset = new Rect(100, 100, Screen.width-200, Screen.height-200);
		textureOverlay.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp(KeyCode.Escape))
		{
			menuEnabled = !menuEnabled;
		}
		//show or hide texture
		switch (menuEnabled)
		{ 
		case true:
			if(!textureOverlay.enabled)
				textureOverlay.enabled = true;
			Time.timeScale =0;
			break;
		case false:
			if(textureOverlay.enabled)
				textureOverlay.enabled = false;
				Time.timeScale =1;
			break;
		}
	}
}