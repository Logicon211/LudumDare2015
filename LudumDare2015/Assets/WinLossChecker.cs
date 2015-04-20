using UnityEngine;
using System.Collections;

public class WinLossChecker : MonoBehaviour {

	public GameObject player;
	public GameObject gameEndingBarrel;

	private NuclearMan playerScript;
	private GameEndingBarrel gameEndingBarrelScript;
	// Use this for initialization
	void Start () {
		playerScript = player.GetComponent<NuclearMan>();
		gameEndingBarrelScript = gameEndingBarrel.GetComponent<GameEndingBarrel>();
	}
	
	// Update is called once per frame
	void Update () {

		if(player == null || playerScript.Health <= 0) {
			StartCoroutine("LoseGame");
		}

		if(gameEndingBarrel == null || gameEndingBarrelScript.health <= 0) {
			StartCoroutine("WinGame");
		}
	}

	IEnumerator LoseGame() {			
		yield return new  WaitForSeconds(3);  // or however long you want it to wait
		Application.LoadLevel("Game_Over");
	}

	IEnumerator WinGame() {			
		yield return new WaitForSeconds(3);  // or however long you want it to wait
		Application.LoadLevel("VictoryScreen");
	}
}
