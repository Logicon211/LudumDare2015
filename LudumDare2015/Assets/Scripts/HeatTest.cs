using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeatTest : MonoBehaviour {
	
	public Scrollbar HeatBar;
	public float Heat = 100;
	
	
	
	public void Damage(float value){
		Heat -= value;
		HeatBar.size = Heat / 100f;
		Debug.Log (Heat);
	}
	
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.H))
		{
			Damage(10);
		}
	}
}
