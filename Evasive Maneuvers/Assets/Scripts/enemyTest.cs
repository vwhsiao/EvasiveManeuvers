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
        Invoke("kill", 5.0f);
    }

	// Use this for initialization
	void Start () {
	
	}
		
	void kill()
    {
        Destroy(this.gameObject);
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
    
    public void SetDirection()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
        isFormation = true;
    }
    void move()
    {
        transform.Translate(speed * direction);
        
    }

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "playerProjectile") {
            Destroy(this.gameObject);
			
            if (!coll.gameObject.name.Contains("Icicle"))
            {
                Destroy(coll.gameObject);
            }
			
            
		}
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), coll.GetComponent<Collider2D>());
        }
	}
	
}
