using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public int damageAmount = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		IDamagable damagable = (IDamagable)col.gameObject.GetComponent(typeof(IDamagable));
		if(damagable != null) {
			damagable.Damage(damageAmount);
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		
		IDamagable damagable = (IDamagable)col.gameObject.GetComponent(typeof(IDamagable));
		if(damagable != null) {
			damagable.Damage(damageAmount);
		}
	}
}
