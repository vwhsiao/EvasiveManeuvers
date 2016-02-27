using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/* MonoBehaviour basically means it inherits from Unity's special class (GameObject)
 * with it you can access functions and variables within Unity's scenes, and all GameObjects inside it.
 * Nobody really knows what it is, and we don't have to know :) */
public class GameManager : MonoBehaviour
{
    //public variables
    public GameObject snowBall;
    public GameObject icicle;
    public GameObject SnowBallPowerUp;
    public GameObject IciclePowerUp;

    public float icicleSpeed;
    public float snowballSpeed;

    //private variables
    private GameObject player;
    private Vector2 direction;
    //private RectTransform canvas;
    //private Image healthBar, backBar;
    //float xbar;


    // Awake is called when this script is first activated, kind of like the Init()
    void Awake()
    {
        player = GameObject.Find("Player");

        //canvas = GameObject.Find("UI").GetComponent<RectTransform>();

        //GameObject healthFront = new GameObject("healthBar");
        //healthBar = healthFront.AddComponent<Image>();
        //healthBar.rectTransform.SetParent(canvas.transform, false);
        //healthBar.color = Color.red;

        //GameObject healthBack = new GameObject("healthBarBack");
        //backBar = healthBack.AddComponent<Image>();
        //backBar.rectTransform.SetParent(canvas.transform, false);
        //backBar.color = new Color(0.0f, 0.0f, 0.0f);

    }

    // Use this for initialization, it happens after Awake()
	void Start()
    {
	
	}
	
    /* Update is called once per frame,
     * there are different kinds of Update functions (within the MonoBehaviour class),
     * such as FixedUpdate() for physics calculations, and LateUpdate() after everything's done.
     * For now, we can just stick with Update(), and change our needs as we go.
     * Be careful, Update() is called often, so it slows down the game with too much in there. */
	void Update()
    {
        //updateHealth();
	}

    public void FireProjectile()
    {
        //find mouse position and translate that into a Vector with 3 floats (x,y,z)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Quaternion is just a fancy thing to say a Vector with a direction. 
        //Gets the rotation for the object (projectile) to point at
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, mousePos - player.transform.position);

        //Create the object as a GameObject so we can still change values of it
        if (player.GetComponent<Player>().canFireIcicle)
        {
            GameObject firedProjectile = Instantiate(icicle, player.transform.position, rotation) as GameObject;
            moveProjectile(firedProjectile, mousePos);
        }

    }

    public void FireSnowbomb()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, mousePos - player.transform.position);
        if (player.GetComponent<Player>().canFireSnowball)
        {
            GameObject firedProjectile = Instantiate(snowBall, player.transform.position, rotation) as GameObject;
            firedProjectile.GetComponent<Projectile>().destination = mousePos;
            firedProjectile.GetComponent<Projectile>().speed = snowballSpeed;
            firedProjectile.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void createPowerUp()
    {

    }

    void moveProjectile(GameObject projectile, Vector3 mousePos)
    {

        
        //Get the vector of the direction projectil is going to go in
        direction = mousePos - player.transform.position;

        //Normalize it (make it into units of 1's)
        direction.Normalize();

        //Move the projectile in front of the player object so it doesn't just create it on top of the player
        //trust me, it looks wonky as hell. if you do'nt believe me, get rid of the *2's in the next line and then try it
        projectile.transform.position += new Vector3(direction.x * 2, direction.y * 2, 0);

        //increase the projectile velocity so it actually, you know, projectiles
        projectile.GetComponent<Rigidbody2D>().velocity = direction * icicleSpeed;

        //logistical thing, set the parent of the object to the GameManager so we do'nt flood the hierarchy screen
        projectile.transform.parent = transform;
    }

    //public void setHealthBar(bool visible)
    //{
    //    healthBar.gameObject.SetActive(visible);
    //    backBar.gameObject.SetActive(visible);
    //}


    //private void updateHealth()
    //{

    //    float targetX = player.GetComponent<Player>().returnHealthPercent();
    //    xbar = Mathf.Lerp(xbar, targetX, Time.deltaTime * 2.0f);

    //    healthBar.rectTransform.offsetMin = Vector2.zero;
    //    healthBar.rectTransform.offsetMax = Vector2.zero;
    //    healthBar.rectTransform.anchorMin = new Vector2(0.0f, 0.0f);
    //    healthBar.rectTransform.anchorMax = new Vector2(xbar, 0.05f);

    //    backBar.rectTransform.offsetMin = Vector2.zero;
    //    backBar.rectTransform.offsetMax = Vector2.zero;
    //    backBar.rectTransform.anchorMin = new Vector2(xbar, 0.0f);
    //    backBar.rectTransform.anchorMax = new Vector2(1.0f, 0.05f);
    //}

    public void Death()
    {
        SceneManager.LoadScene("stats_screen");
    }
}
