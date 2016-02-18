using UnityEngine;
using System.Collections;

public class enemyTest : MonoBehaviour {

	//private GameManager gameManager;
    private GameObject player;
    public float speed = 5.0f; // move speed
    private Vector3 direction;
    private bool isFormation = false; 
    void Awake()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }

	// Use this for initialization
	void Start () {
	
	}
		
	
	// Update is called once per frame


    //public Transform leader;
    //public Transform follower;


	void Update () {
        //follower.LookAt(leader);
        //follower.Translate(speed*Vector3.forward*Time.deltaTime);
		//transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);


       // Quaternion rotation = Quaternion.LookRotation(Vector3.forward, transform.position - player.transform.position);



        if (isFormation)
        {
            move();
        }
        else
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(speed * direction);
        }
	
	}
    
    public void SetDirection(Transform destination)
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
        isFormation = true;
    }
    void move()
    {
        transform.Translate(speed * direction);
        
    }

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "playerProjectile") {
			Destroy (coll.gameObject);
			Destroy (this.gameObject);
		}
	}
	
}
