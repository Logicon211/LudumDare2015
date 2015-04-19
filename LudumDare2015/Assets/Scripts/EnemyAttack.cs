using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float atkSpeed = 1f;

	private bool isHugging = false;

	private float currSpeed;
	private Enemy enemy;

	private NuclearMan player;
	// Use this for initialization
	void Start () {
		currSpeed = atkSpeed;
		enemy = transform.parent.GetComponent<Enemy>();
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
			player.Damage(5f);
			enemy.attackReady = false;

		}

		if(isHugging) {
			transform.parent.GetComponent<Animator>().SetBool("Punching", true);
		} else {
			transform.parent.GetComponent<Animator>().SetBool("Punching", false);
		}

	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")) {
			player = other.gameObject.GetComponent<NuclearMan>();
			isHugging = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Player")) {
			player = null;
			isHugging = false;
		}
	}


	void Attack(){
		transform.Translate(Vector2.right  * 10, Space.Self);
		//transform.Translate(-Vector2.right * 10, Space.Self);
	}
}
