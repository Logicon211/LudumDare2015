using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeathTest : MonoBehaviour {

	public Scrollbar HealthBar;
	public float Health = 100;



	public void Damage(float value){
		Health -= value;
		HealthBar.size = Health / 100f;
		Debug.Log (Health);
	}

	// Use this for initialization


	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P))
		{
			Damage(10);
		}
	}
}
