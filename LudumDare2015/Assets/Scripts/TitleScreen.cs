using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	void Update(){
		if(Input.GetKeyDown("return")){
			Debug.Log ("no");
			Application.LoadLevel("Scene1");

		}
	}

}
