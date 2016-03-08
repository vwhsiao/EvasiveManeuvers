using UnityEngine;
using System.Collections;

public class EnemyGroup : MonoBehaviour {
    private GameObject player;
    private Vector3 direction;
    public float speed = 0.1f; // move speed
    private GameManager gameManager;
	void Start()
    {
        gameManager = GameManager.instance;
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

        gameManager.IncrementEnemiesDodgedCount(transform.GetChild(0).childCount);
        
    }

	void Update()
    {
        if (!GameManager.instance.playing)
            return;

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
