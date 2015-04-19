using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	private Transform target;					//Allows enemy to move to player
	
	public int atkDmg;							//Amount of damage enemy can do
	public float atkRange;						//Range at which he will stop moving to start attacking
	public float movSpeed = 10f;						//How fast it will move
	public float aggro;
	public bool facingRight = true;
	public bool flying = false;

	private EnemyAttack enemyAttack;
	private Animator animate;					//beats the fuck out of me
	private Rigidbody2D enemyRigidbody;
	private Transform weapon;
	
	private float currPosition;					//used to hold the current position relative to the player
	private float currY;						//used to hold current y position for flying
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		enemyRigidbody = GetComponent<Rigidbody2D>();
		if(flying){
			enemyRigidbody.gravityScale = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!flying)
			Walk();
		else
			Fly();
	}
	
	void FixedUpdate(){


		if(enemyRigidbody.velocity.magnitude > movSpeed)
		{
			enemyRigidbody.velocity = enemyRigidbody.velocity.normalized * movSpeed;
		}
	}	

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
		foreach (Transform child in transform){
			Vector3 childScale = child.localScale;
			childScale.x  *= -1;
			child.localScale = childScale;
		}
	}

	void Walk()  {
		currPosition = target.position.x - transform.position.x;		//calculates the difference in position between target and enemy
		//If enemy is outside of the attack range
		if(Mathf.Abs(currPosition) > atkRange && Mathf.Abs(currPosition) < aggro){
			//Move enemy left
			if((currPosition) < float.Epsilon){
				enemyRigidbody.velocity += (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
				if(facingRight){
					Flip();
				}
				//transform.Translate(-Vector2.right * movSpeed * Time.deltaTime);
			}
			//Move enemy left
			else if(currPosition > float.Epsilon){
				enemyRigidbody.velocity -= (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
				if(!facingRight){
					Flip ();
				}
			}
		}
	}//end of Walk()

	void Fly(){
		//get the current y and x variables
		currPosition = target.position.x - transform.position.x;
		currY = transform.position.y - target.position.y;

		if(Mathf.Abs(currPosition) > atkRange && Mathf.Abs(currPosition) < aggro){
			//Move enemy left
			if (currPosition < float.Epsilon){
				if(currY > float.Epsilon && currY > atkRange){
					enemyRigidbody.velocity += (new Vector2(-transform.right.x, -transform.up.y) * movSpeed * Time.deltaTime);
				}
				else
					enemyRigidbody.velocity += (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
				if(facingRight){
					Flip();
				}
			}
			if (currPosition > float.Epsilon){
				if(currY > float.Epsilon && currY > atkRange){
					enemyRigidbody.velocity -= (new Vector2(-transform.right.x, transform.up.y) * movSpeed * Time.deltaTime);
				}
				else
					enemyRigidbody.velocity -= (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
				if(!facingRight){
					Flip();
				}
			}
		}
	}//end of Fly()
}
