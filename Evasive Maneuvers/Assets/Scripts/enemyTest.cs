using UnityEngine;
using System.Collections;

public class enemyTest : MonoBehaviour {

	private GameManager gameManager;
	// Use this for initialization
	void Start () {
	
	}
		
	public float speed = 5.0f; // move speed
	// Update is called once per frame


	public Transform leader;
	public Transform follower;


	void Update () {
		follower.LookAt(leader);
		follower.Translate(speed*Vector3.forward*Time.deltaTime);
		//transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "playerProjectile") {
			Destroy (coll.gameObject);
			Destroy (this.gameObject);
		}
	}
	void Awake()
	{
		/*Finds the Game Manager object in the scene and saves the GameManager component to the private variable.
         * Avoid using GameObject.Find() too much, because it loops through every object in the scene,
         * searching for the object by exact name. If you misspelled something or changed a name,
         * it will give you trouble. Also if you have a lot of objects... */

		/* Currently as of 1/23/2016, GameManager is what handles firing projectiles. 
		Will probably change for other things as time goes on*/
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
}
