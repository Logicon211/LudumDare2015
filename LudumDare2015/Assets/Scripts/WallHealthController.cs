using UnityEngine;
using System.Collections;

public class WallHealthController : HealthController {

	public override void takeDamage (int dmg)
	{
		health -= dmg*2;
		if(health <= 0) {
			Destroy(this.gameObject);
		}
	}
}
