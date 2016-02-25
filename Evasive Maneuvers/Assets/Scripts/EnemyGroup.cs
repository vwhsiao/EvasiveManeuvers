using UnityEngine;
using System.Collections;

public class EnemyGroup : MonoBehaviour {
    private GameObject player;
    private Vector3 direction;
    public float speed = 5.0f; // move speed

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        SetDirection();
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}

    public void SetDirection()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();

    }
    void move()
    {
        transform.Translate(speed * direction);

    }
}
