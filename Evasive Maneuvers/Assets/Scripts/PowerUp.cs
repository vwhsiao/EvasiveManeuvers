using UnityEngine;
using System.Collections;

public enum PowerUpType { SpeedUp, ClearScreen }

public class PowerUp : MonoBehaviour {


    public PowerUpType type = PowerUpType.SpeedUp;
    [Range(0.0f, 50.0f)]
    public float timeTilDelete = 5.0f;
    
	// Use this for initialization
	void Start () {
        Invoke("deleteSelf", timeTilDelete);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            if (type == PowerUpType.ClearScreen)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    Destroy(enemies[i].gameObject);
                }
                Destroy(this.gameObject);
            }
            else if (type == PowerUpType.SpeedUp)
            {
                StartCoroutine(SpeedUpBoost());
                
            }
        }
    }

    public IEnumerator SpeedUpBoost()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Player>().moveSpeed *= 2;
        
        yield return new WaitForSeconds(5f);
        player.GetComponent<Player>().moveSpeed /= 2;
    }

    void deleteSelf()
    {
        Destroy(this.gameObject);
    }
}
