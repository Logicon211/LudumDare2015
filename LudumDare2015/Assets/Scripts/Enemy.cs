using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamagable {
	
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
	private EnemyAttack eAtk;
	
	private float currPosition;					//used to hold the current position relative to the player
	private float currY;						//used to hold current y position for flying

	public float initCharge;
	public bool attackReady = false;			//used by EnemyAttack to tell Enemy when to move to attack
	public bool readyToCharge = false;

	private bool aggroCheck = false;

	public GameObject explosion;

	private int touchingPlatforms = 0;
	private float g = 0;

	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		eAtk = transform.GetComponentInChildren<EnemyAttack>();
		enemyRigidbody = GetComponent<Rigidbody2D>();
		g = enemyRigidbody.gravityScale;
		if(flying){
			enemyRigidbody.gravityScale = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null ) {
			if(Mathf.Abs(transform.position.x - target.position.x) < aggro) {
				aggroCheck = true;
				if (!flying)
					Walk();
				else if (attackReady)
					FlyAttack();
				else
					Fly();
			}
		}
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
		if(target != null) {
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
		}
	}//end of Walk()

	void Fly(){

		if(target != null) {
			//get the current y and x variables
			currPosition = target.position.x - transform.position.x;
			currY = transform.position.y - target.position.y;
		
			//Changing his direction
			if(Mathf.Abs(currPosition) > atkRange && Mathf.Abs(currPosition) < aggro){
				//Move enemy left
				if (currPosition < float.Epsilon){
					if(currY > float.Epsilon && currY > atkRange){
						enemyRigidbody.velocity += (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
					}
					else
						enemyRigidbody.velocity += (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
					if(facingRight){
						Flip();
					}
				}
				//Move enemy right
				if (currPosition > float.Epsilon){
					if(currY > float.Epsilon && currY > atkRange){
						enemyRigidbody.velocity -= (new Vector2(-transform.right.x, transform.right.y) * movSpeed * Time.deltaTime);
					}
					else
						enemyRigidbody.velocity -= (new Vector2(-transform.right.x, -transform.right.y) * movSpeed * Time.deltaTime);
					if(!facingRight){
						Flip();
					}
				}
			}
			if((currY) < 5){
				enemyRigidbody.velocity +=(new Vector2(transform.up.x, transform.up.y) * movSpeed * Time.deltaTime);
				if(aggroCheck) {
					//Debug.Log("fly up");
				}

			}
			if((currY) > 5){
				enemyRigidbody.velocity -=(new Vector2(transform.up.x, transform.up.y) * movSpeed * Time.deltaTime);

			}
		}
	}//end of Fly()

	//If attackReady is true, this will cause the enemy to fly towards the player
	void FlyAttack(){
		if(aggroCheck){
			//Debug.Log("flying attack");
		}
		if(target != null) {
			//get the current y and x variables
			currPosition = target.position.x - transform.position.x;
			currY = transform.position.y - target.position.y;

			if(Mathf.Abs(currY) > 1f){
				enemyRigidbody.velocity -=(new Vector2(transform.up.x, transform.up.y) * movSpeed * Time.deltaTime);
			}
			if(Mathf.Abs(currY) <= 2f && !readyToCharge)
				readyToCharge = true;
			if((currPosition) < 0 && readyToCharge){
				enemyRigidbody.velocity -=(new Vector2(transform.right.x, transform.right.y) * movSpeed * Time.deltaTime);
				if(facingRight){
					Flip();
					eAtk.currSpeed = eAtk.atkSpeed;
					attackReady = false;
					Debug.Log("flip");
				}
			}
			else if((currPosition) > 0 && readyToCharge){
				enemyRigidbody.velocity +=(new Vector2(transform.right.x, transform.right.y) * movSpeed * Time.deltaTime);
				if(!facingRight){
					Flip();
					eAtk.currSpeed = eAtk.atkSpeed;
					attackReady = false;
				}
			}
			
		}
	}

	public void Damage(int damage) {
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.CompareTag("OneWayPlatform")) {
			touchingPlatforms++;
			
			//float maxDistance = (col.transform.localScale.y + transform.localScale.y) / 2;
			
			//if(RB.velocity.y < 0 && transform.position.y - col.transform.position.y - RB.velocity.y * Time.fixedDeltaTime > maxDistance - deadZone) {
			
			if(enemyRigidbody.velocity.y < 0 && !(Input.GetAxis("Vertical") < 0)) {// && transform.position.y - col.transform.position.y > maxDistance) {
				enemyRigidbody.gravityScale = 0;
				enemyRigidbody.velocity = new Vector2(enemyRigidbody.velocity.x, 0);
				//transform.position = new Vector3(transform.position.x, col.transform.position.y + maxDistance, 0);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.CompareTag("OneWayPlatform") && touchingPlatforms-- == 1 && !flying) {
			enemyRigidbody.gravityScale = g;
		}
	}
}
