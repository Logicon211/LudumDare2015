using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NuclearMan : MonoBehaviour {

	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public bool facingRight = true;
	public float speed = 10f;
	public float jumpSpeed = 20f;
	public float climbSpeed = 12f;

	public float deadZone = 0.1f;

	public GameObject laser;
	private GameObject laserInstance;

	private Rigidbody2D RB;
	private Animator anim;


	private Transform groundCheck;
	private float groundCheckRadius = 1f;
	private bool grounded = false;			// Whether or not the player is grounded.

	private float g = 0;
	private int touchingPlatforms = 0;
	private BoxCollider2D heroCollider;

	public Scrollbar HeatBar;
	public float Heat = 100;

	//public bool isPunching = false;
	public bool isLasering = false;
	private Transform laserPoint;

	public Scrollbar HealthBar;
	public float Health = 100;

	// Use this for initialization
	void Start () {
		RB = GetComponent<Rigidbody2D>();
		groundCheck = transform.FindChild ("groundCheck");
		g = RB.gravityScale;
		heroCollider = GetComponent<BoxCollider2D>();
		laserPoint = transform.FindChild("LaserPoint");
	}

	void Update() {
	
		// The player is grounded if a ground check overlaps with anything on the ground layer.
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, 1 << LayerMask.NameToLayer("OneWayPlatform"));  
	

		// If the jump button is pressed and the player is grounded then the player should jump.
		bool touchingVines = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Vines"));
		if(Input.GetAxis("Vertical") > 0 && grounded && !touchingVines) {
			jump = true;
		}

		if(Input.GetAxis("Vertical") < 0 && heroCollider.IsTouchingLayers(1 << LayerMask.NameToLayer("OneWayPlatform"))) {
			RB.gravityScale = g;
		}

		//Punching
		if(Input.GetButtonDown("Punch")) {
			anim.SetBool("Punching", true);
			//isPunching = true;
			speed = 4f;
		} else if(Input.GetButtonUp("Punch")) {
			anim.SetBool("Punching", false);
			//isPunching = false;
			speed = 10f;
		}

		//Firing Laser
		if (Input.GetButtonDown ("Laser")) {
			if(Heat >= 5)	{
				anim.SetBool("Lasering", true);
				isLasering = true;
				laserInstance = Instantiate (laser, laserPoint.position, transform.rotation) as GameObject;
				ChangeHeat (5);
				if (!facingRight) {
					Vector3 laserScale = laserInstance.transform.localScale;
					laserScale.x *= -1;
					laserInstance.transform.localScale = laserScale;
				}
			}
			else{
				
				//Rect rectGUILabel = new Rect (0,0,Screen.width,Screen.height),"Not Enough PowerHeat Kill More");
				//GUI.Label(Rect(0,0,Screen.width,Screen.height),"Not Enough PowerHeat Kill More");
			}

		} else if (Input.GetButton ("Laser")) {
			
			if(Heat >= 5){
				ChangeHeat (5);
				laserInstance.transform.position = laserPoint.position;
			}
			else{
				anim.SetBool("Lasering", false);
				isLasering = false;
				Destroy (laserInstance);
			}
		} else if (Input.GetButtonUp ("Laser")) {
			anim.SetBool("Lasering", false);
			isLasering = false;
			Destroy (laserInstance);
		} else {
			if(Heat < 100){
				ChangeHeat (-2);

			}
		}


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		anim = GetComponent<Animator>();

		bool touchingVines = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Vines"));
		if (jump) {
			//RB.AddForce(new Vector2(0f, jumpForce));
			RB.velocity = new Vector2(RB.velocity.x, jumpSpeed);
			anim.SetBool("Jumping", true);
			jump = false; //reset the jump flag so it doesn't happen again immediately
		} else if(Input.GetAxis("Vertical") > 0 && touchingVines) {
			//Vine Climbing
			RB.velocity = new Vector2(0, climbSpeed);
			anim.SetBool("Climbing", true);
		} else {
			anim.SetBool("Climbing", false);
		}

		float move = Input.GetAxis("Horizontal");

		if(move != 0/*Input.GetKey(KeyCode.RightArrow*/) {
			if(move > 0) {
				if(!facingRight && !isLasering) {
					Flip();
				}
			} else {
				if(facingRight && !isLasering) {
					Flip();
				}
			}
			RB.velocity = new Vector2(speed * move, RB.velocity.y);
			anim.SetBool("Moving", true);
		} else {
			anim.SetBool("Moving", false);
		}
	}

	void OnCollisionEnter2D (Collision2D col) 
	{
		if(col.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			anim.SetBool("Jumping", false);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.CompareTag("OneWayPlatform")) {
			touchingPlatforms++;

			//float maxDistance = (col.transform.localScale.y + transform.localScale.y) / 2;

			//if(RB.velocity.y < 0 && transform.position.y - col.transform.position.y - RB.velocity.y * Time.fixedDeltaTime > maxDistance - deadZone) {

			if(RB.velocity.y < 0 && !(Input.GetAxis("Vertical") < 0)) {// && transform.position.y - col.transform.position.y > maxDistance) {
				RB.gravityScale = 0;
				RB.velocity = new Vector2(RB.velocity.x, 0);
				anim.SetBool("Jumping", false);
				//transform.position = new Vector3(transform.position.x, col.transform.position.y + maxDistance, 0);
			}
		}
	}

	public void ChangeHeat(float value){
		Heat -= value;
		HeatBar.size = Heat / 100f;
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.CompareTag("OneWayPlatform") && touchingPlatforms-- == 1) {
			RB.gravityScale = g;
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

	public void Damage(float value){
		Health -= value;
		HealthBar.size = Health / 100f;
		GetComponent<AudioSource>().Play();
		if (Health <= 0) {
			Application.LoadLevel("Game_Over");
		}
		//Debug.Log (Health);
	}
}
