using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col) 
	{
		if(col.gameObject.GetComponent<HealthController>() != null) {
			Debug.Log(col.gameObject.GetComponent<HealthController>().health);
			col.gameObject.GetComponent<HealthController>().takeDamage(1);
		}
	}
}
