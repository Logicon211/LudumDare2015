using UnityEngine;
using System.Collections;

public class GUIPlacement : MonoBehaviour {
	
public Camera mCam; //Set this to your camera in unity

public Transform target; // Also set this to your camera (or player if your healthbar is above them.)

void OnGUI()
{
	//float halfCameraWidth;
	//halfCameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
	
	Vector3 pos = target.position;
	pos.y += 4;//offset from target pos
	
	pos = mCam.WorldToScreenPoint (pos);

	//Rect rectGUILabel = new Rect (halfCameraWidth + 10, Screen.height - pos.y - 15, 100, 22);   
		//this.transform.position = new Vector3(halfCameraWidth, Screen.height - pos.y - 15, 22);
		this.transform.position = new Vector3(300, Screen.height-30, 22);
		Debug.Log(Screen.height / 2);
		Debug.Log(Screen.width / 2);



}
}