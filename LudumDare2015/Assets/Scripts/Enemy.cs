using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	private Transform target;					//Allows enemy to move to player
	
	public int atkDmg;							//Amount of damage enemy can do
	public float atkRange;						//Range at which he will stop moving to start attacking
	public float movSpeed = 10f;						//How fast it will move
	public float aggro;
	
	private Animator animate;					//beats the fuck out of me
	private Rigidbody2D enemyRigidbody;
	
	private float currPosition;					//used to hold the current position relative to the player
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		Debug.Log("Target: " + target.tag);
		enemyRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		currPosition = target.position.x - transform.position.x;		//calculates the difference in position between target and enemy
		//If enemy is outside of the attack range
		if(Mathf.Abs(currPosition) > atkRange && Mathf.Abs(currPosition) < aggro){
			//Move enemy left
			if((currPosition) < float.Epsilon){
				enemyRigidbody.velocity += (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
				//transform.Translate(-Vector2.right * movSpeed * Time.deltaTime);
			}
			//Move enemy left
			else if(currPosition > float.Epsilon){
				enemyRigidbody.velocity -= (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
			}
		}
		
		if(Mathf.Abs(currPosition) < atkRange){
			
		}
	}
	void FixedUpdate(){
		if(enemyRigidbody.velocity.magnitude > movSpeed)
		{
			enemyRigidbody.velocity = enemyRigidbody.velocity.normalized * movSpeed;
		}
	}	
	
	void attackPlayer(){
	}
}

