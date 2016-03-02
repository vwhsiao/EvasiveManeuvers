using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed = 5.0f; // move speed
    private Vector3 direction;
    private GameManager gameManager;
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetDirection();
        //Invoke("Kill", 5.0f);
    }
		
	void Kill()
    {
        Destroy(this.gameObject);
    }

    public void SetDirection()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }

	void Update()
    {
        Move();
	}

    void Move()
    {
        transform.Translate(speed * direction);
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
		if (coll.gameObject.tag == "playerProjectile")
        {
            gameManager.IncrementEnemiesKilledCount(1);
            Destroy(this.gameObject);
			
            /*if (!coll.gameObject.name.Contains("Icicle"))
            {
                Destroy(coll.gameObject);
            }*/
		}
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), coll.GetComponent<Collider2D>());
        }
	}
	
}
