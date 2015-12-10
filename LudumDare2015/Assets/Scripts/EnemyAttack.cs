using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float atkSpeed = 1f;

	private bool isHugging = false;

	public float currSpeed;
	private Enemy enemy;

	private NuclearMan player;
	private Animator anim;
	// Use this for initialization
	void Start () {
		currSpeed = atkSpeed;
		enemy = transform.parent.GetComponent<Enemy>();
		anim = transform.parent.GetComponent<Animator>();
	}
	void Update(){
		//Debug.Log (currSpeed);
		if (currSpeed > 0){
			currSpeed -= Time.deltaTime;
		}
		if (currSpeed <= 0){
			enemy.attackReady = true;
		}
		if (currSpeed <= 0 && isHugging && player != null){
			currSpeed = atkSpeed;
			player.Damage(enemy.atkDmg);
			enemy.attackReady = false;
			enemy.readyToCharge = false;

		}

		if(isHugging) {
			if(anim != null) {
				transform.parent.GetComponent<Animator>().SetBool("Punching", true);
			}
		} else {
			if(anim != null) {
				transform.parent.GetComponent<Animator>().SetBool("Punching", false);
			}
		}

	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")) {
			player = other.gameObject.GetComponent<NuclearMan>();
			isHugging = true;
		}

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			player = null;
			isHugging = false;
		}
	}
	


	void Attack() {
		transform.Translate(Vector2.right  * 10, Space.Self);
		//transform.Translate(-Vector2.right * 10, Space.Self);
	}
}
