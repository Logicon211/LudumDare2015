using UnityEngine;
using System.Collections;

public class GUIBorderBar : MonoBehaviour {

	public Camera mCam; //Set this to your camera in unity
	
	public Transform target; // Also set this to your camera (or player if your healthbar is above them.)
	private float width;
	private float height;
	public RectTransform RT;

	void Start(){
		width = Screen.width;
		height = Screen.height/2;

		RT.sizeDelta = new Vector2(width, height);
		//Debug.Log ("Width" + width + "         height" + height);
		//rectTransform.sizeDelta = new Vector2 (length, height);
		//this.transform.
	
	
	}

	void OnGUI()
	{

		//float width = Screen.width/2;
		//float height = Screen.height/2;
		
		//Debug.Log ("Width" + width + "         height" + height);
		//float halfCameraWidth;
		//halfCameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		
		//Vector3 pos = target.position;
		//pos.y += 4;//offset from target pos
		
		//pos = mCam.WorldToScreenPoint (pos);
		//RT.sizeDelta = new Vector2(width, height);

		//Rect rectGUILabel = new Rect (halfCameraWidth + 10, Screen.height - pos.y - 15, 100, 22);   
		//this.transform.position = new Vector3(halfCameraWidth, Screen.height - pos.y - 15, 22);
		//Debug.Log ("Width" + width + "         height" + height);
		this.transform.position = new Vector3(width/2, 10, 22);

		
	}
}