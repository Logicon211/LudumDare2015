using UnityEngine;
using System.Collections;

public class GameEndingBarrel : MonoBehaviour, IDamagable {

	public GameObject explosion;
	public int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(int damage) {
		int healthBefore = health;
		health -= damage;

		if(healthBefore >= 100 && health <= 66) {
			GameObject exp = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
			exp.transform.localScale = transform.localScale;
		}
		if(healthBefore >= 66 && health <= 33) {
			GameObject exp = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
			exp.transform.localScale = transform.localScale;
		}

		if(health <= 0) {
			GameObject exp = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
			exp.transform.localScale = transform.localScale;

			/*
			 * PUT GAME ENDING SCENE CHANGING CODE HERE
			 */

			Destroy(this.gameObject);
		}
	}
}
