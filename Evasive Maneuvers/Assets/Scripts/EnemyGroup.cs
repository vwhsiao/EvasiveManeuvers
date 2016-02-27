using UnityEngine;
using System.Collections;

public class EnemyGroup : MonoBehaviour {
    private GameObject player;
    private Vector3 direction;
    public float speed = 0.08f; // move speed

	void Start()
    {
        player = GameObject.Find("Player");
        SetDirection();
        //Invoke("Kill", 5.0f);
	}
	
	// Update is called once per frame
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
