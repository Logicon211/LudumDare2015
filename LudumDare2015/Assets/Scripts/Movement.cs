using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private Rigidbody2D playerRigidbody;

	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)) 
		{
			playerRigidbody.velocity += (new Vector2(-transform.right.x, -transform.right.y) * 100f * Time.deltaTime);
			//rigidbody.AddForce(-transform.right * 5000f * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			playerRigidbody.AddForce(transform.right * 5000f * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			playerRigidbody.AddForce(transform.up * 5000f * Time.deltaTime);
		}

		if(Input.GetMouseButton(0)){
			Vector2 mousePos = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			playerRigidbody.AddForce(new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized * 2500f * Time.deltaTime);
		
			Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}
