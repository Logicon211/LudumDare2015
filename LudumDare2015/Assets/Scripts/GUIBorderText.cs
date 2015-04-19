using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIBorderText : MonoBehaviour {
	
	public Camera mCam; //Set this to your camera in unity
	
	public Transform target; // Also set this to your camera (or player if your healthbar is above them.)

	float oldWidth;
	float oldHeight;
	int fontSize = 16;
	public float Ratio = 20; // public
	Text txt;
	RectTransform RT2;

	void Start(){
		txt = gameObject.GetComponent<Text>(); 
		//oldWidth = Screen.width;
		//oldHeight = Screen.height/3;
		//RT2 = gameObject.GetComponent<RectTransform>(); 
		//RT2.sizeDelta = new Vector2(oldWidth, oldHeight);
	}

	void OnGUI()
	{
		GUI.skin.button.wordWrap = true;
		//float halfCameraWidth;
		//halfCameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		
		Vector3 pos = target.position;
		pos.y += 4;//offset from target pos
		
		pos = mCam.WorldToScreenPoint (pos);
		
		//Rect rectGUILabel = new Rect (halfCameraWidth + 10, Screen.height - pos.y - 15, 100, 22);   
		//this.transform.position = new Vector3(halfCameraWidth, Screen.height - pos.y - 15, 22);
		this.transform.position = new Vector3(oldWidth/2, oldHeight/8, 22);

	}

	void Update() {
		if (oldWidth != Screen.width || oldHeight != Screen.height) {
			oldWidth = Screen.width;
			oldHeight = Screen.height;
			//fontSize = (int) (Mathf.Min(Screen.width, Screen.height) / Ratio);
		}
			if(Input.GetKeyUp(KeyCode.K))
			{
				textEdit("BUTTTTTTTTTTTTTTS");
			//txt.text.wordWrap = true;
			}


	}
	void textEdit(string textIn){
		txt.text = textIn;
	}

}