using UnityEngine;
using System.Collections;

public class NoseGun : MonoBehaviour {

	public Rigidbody2D bullet;
	[Range(0, 200)]
	public float speed = 20f;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = transform.root.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;
			bulletInstance.velocity = transform.right * speed;

			player.GetComponent<Rigidbody2D>().AddForce(-player.transform.right * 50f);
		}
	}
}
