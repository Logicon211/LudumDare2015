using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private Transform target;					//Allows enemy to move to player
	private int atkDmg;							//Amount of damage enemy can do
	private float atkRange;
	private Animator animate;					//beats the fuck out of me

	private Rigidbody2D enemyRigidbody;

	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		Debug.Log("Target: " + target.tag);
		enemyRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if((target.position.x - transform.position.x) < float.Epsilon){
			enemyRigidbody.velocity += (new Vector2(-transform.right.x, -transform.right.y) * 100f * Time.deltaTime);
		}
		else if((target.position.x - transform.position.x) > float.Epsilon){
			Debug.Log("to my left");
			//enemyRigidbody.AddForce(transform.right * 5000f * Time.deltaTime);
			enemyRigidbody.velocity -= (new Vector2(-transform.right.x, -transform.right.y) * 100f * Time.deltaTime);
		}
	}

}