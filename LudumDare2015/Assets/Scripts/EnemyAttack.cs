using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float atkSpeed = 1f;

	private bool isHugging = false;

	private float currSpeed;
	private Enemy enemy;
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
		if (currSpeed < 0 && isHugging){
			currSpeed = atkSpeed;
			Debug.Log ("This man has accosted you");
			enemy.attackReady = false;

		}

	}
	void OnTriggerEnter2D(Collider2D other){
		isHugging = true;
		Debug.Log ("hugging");
	}

	void OnTriggerExit2D(Collider2D other){
		isHugging = false;
		Debug.Log ("Not hugging");
	}


	void Attack(){
		transform.Translate(Vector2.right  * 10, Space.Self);
		//transform.Translate(-Vector2.right * 10, Space.Self);
	}
}
