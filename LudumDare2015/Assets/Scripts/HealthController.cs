using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public int health { get; set; }
	// Use this for initialization
	void Start () {
		health = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Inheritance example. WallHealth controller is a subclass of HealthController, it's definition of takeDamage happens instead of this one
	//even if I call GetComponent<HealthController>() (As long as I pass in WallHealthController) of course
	public virtual void takeDamage(int dmg) {
		health -= dmg;
		if(health <= 0) {
			Destroy(this.gameObject);
		}
	}
}
