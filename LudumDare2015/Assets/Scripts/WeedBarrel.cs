using UnityEngine;
using System.Collections;

public class WeedBarrel : MonoBehaviour, IDamagable {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(int damage) {
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
}
