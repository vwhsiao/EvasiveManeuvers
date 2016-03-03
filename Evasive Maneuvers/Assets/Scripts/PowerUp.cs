using UnityEngine;
using System.Collections;

public enum PowerUpType { SnowBall, Icicle, ClearScreen }

public class PowerUp : MonoBehaviour {

    private GameObject player;
    public PowerUpType type = PowerUpType.SnowBall;
    [Range(0.0f, 50.0f)]
    public float timeTilDelete = 5.0f;
    public float explosionDuration = 3.0f;
    
	// Use this for initialization
	void Start () {
        Invoke("deleteSelf", timeTilDelete);
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator GrowExplosion()
    {
        this.gameObject.AddComponent<DestroyField>();
        CancelInvoke("deleteSelf");
        Invoke("deleteSelf", explosionDuration);
        while (true)
        {
            this.transform.localScale += new Vector3(0.1f, 0.1f, 0);
            Color newColor = this.GetComponent<SpriteRenderer>().color;
            newColor.a -= 0.005f;
            this.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        yield break;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            if (type == PowerUpType.ClearScreen)
            {
                StartCoroutine(GrowExplosion());
                return;

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    Destroy(enemies[i].gameObject);
                }
                Destroy(this.gameObject);
            }
            else if (type == PowerUpType.SnowBall)
            {
                player.GetComponent<Player>().setFireSnowball();
                Destroy(this.gameObject);
            }
            else if (type == PowerUpType.Icicle)
            {
                player.GetComponent<Player>().setFireIcicle();
                Destroy(this.gameObject);
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
