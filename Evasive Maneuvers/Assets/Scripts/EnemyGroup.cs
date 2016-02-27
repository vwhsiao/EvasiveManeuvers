using UnityEngine;
using System.Collections;

public class EnemyGroup : MonoBehaviour {
    private GameObject player;
    private Vector3 direction;
    public float speed = 0.1f; // move speed
    private GameManager gameManager;
	void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        SetDirection();
        //Invoke("Kill", 20.0f);
	}
	
    void Kill()
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        
        gameManager.IncrementEnemiesDodgedCount(transform.childCount);
    }

	void Update()
    {
        Move();
	}

    public void SetDirection()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }

    void Move()
    {
        transform.Translate(speed * direction);
    }
}
