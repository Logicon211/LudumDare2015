using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour {

	NuclearMan player;
	int damageAmount = 10;

	// Use this for initialization
	void Start () {
		player = transform.parent.GetComponent<NuclearMan>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		//if(player.isPunching) {
			IDamagable damagable = (IDamagable)col.gameObject.GetComponent(typeof(IDamagable));
			if(damagable != null) {
				GetComponent<AudioSource>().Play();
				damagable.Damage(damageAmount);
				player.ChangeHeat(-10);
			}
		//}
	}
}
