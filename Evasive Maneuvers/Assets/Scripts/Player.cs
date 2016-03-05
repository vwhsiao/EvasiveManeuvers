using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// See GameManager for explanations on MonoBehaviour.
public class Player : MonoBehaviour
{
    //Player Member Variables
    public Animator anim;
   
    //Public

    /* The brackets used within MonoBehaviour are for customization of the Unity inspector,
     * which is found at the right of the Unity Editor, in scene when you select a GameObject.
     * You don't have to worry about that and it doesn't affect the actual game code at all!
     * They have to be right above of the variables they affect-- yes, you guessed it right,
     * your own custom variables can show up in Unity's inspector, when declared public,
     * and their types are strings, ints, or other acceptable types (more on that later).
     * For example, [Range(float x, float y)] restricts the possible values that the following
     * (and only) variable can be set in the Unity inspector on the right side. */
    [Range(0.0f, 1.0f)]
    public float moveSpeed;
    public float dir;
    

    //how many shots the player can fire per second. lower number = higher fire rate
    [Range(0.0f, 2.0f)]
    public float fireSpeed;
    public bool canFireSnowball = false;
    public bool canFireIcicle = false;

	

    
    //Private
    //last time since a player has fired. this will be set later in playerAttack function
    private float timeSinceFiring=0.0f;
    private int health = 2;
    private int maxHealth = 2;
    //gets the reference to the GameManager object to handle game related stuff. 
    //for now, it's just projectile management. 
    private GameManager gameManager;
    
    private float snowBallTimeLeft = 15.0f;
    private float icicleTimeLeft = 15.0f;
    // See GameManager for explanations on this method.
    void Awake()
    {    
    }
    
    // See GameManager for explanations on this method.
	void Start()
    {
        gameManager = GameManager.instance;
        anim = GetComponent<Animator>();
    }
    


    // See GameManager for explanations on this method.
    void Update() 
    {
        if (!GameManager.instance.playing)
            return;

        if (canFireIcicle || canFireSnowball)
        {
            if (icicleTimeLeft>0.0f || snowBallTimeLeft >0.0f)
            {
                icicleTimeLeft -= Time.deltaTime;
                snowBallTimeLeft -= Time.deltaTime;
            }
            else
            {
                canFireSnowball = false;
                canFireIcicle = false;
            }
        }
        //check for playerMovement input
        playerMovement();
        
        
        //check for playerAttack input
        playerAttack();

        // update the animator after each frame
        updateAnimator();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(coll.gameObject);
            if (health == 0)
            {
                GameManager.instance.playing = false;
                GameManager.instance.camera.GetComponent<AudioSource>().mute = true;
                anim.SetTrigger("death");
                //Destroytimer(1);
                   
                  


                // Destroy(this.gameObject);
                //gameManager.Death();
            }
            health--;
        }
        //Debug.Log(health);
    }

    // function that destroys the player when it is called
    void Destroyobject()
    {
        gameManager.Death();
        //Destroy(this.gameObject);
    }
    
    /*IEnumerator Destroytimer(float waitTime)
    {
        float timer = Time.time + waitTime;
        while (Time.time > timer){
            yield return null;
        }
    
        
            Destroy(this.gameObject);
        
    }*/

    void updateAnimator()
    {
        // sets the movement parameter from animations as the moveSpeed 
        anim.SetFloat("movement", dir);
        

    }
    //handles player attacks. title yo.
    void playerAttack()
    {
        //timeSinceFiring is a float that basically says how often a player can fire stuff
        //subtracting deltaTime lets us see how long it's been since the last firing of stuff.
        timeSinceFiring -= Time.deltaTime;

        //if timeSinceFiring <= 0 and if a left mouse click is detected
        // player can fire something. otherwise, NOPE. 
        if ((timeSinceFiring <=0) && Input.GetMouseButton(0))
        {

            //change timeSinceFiring to the fireSpeed (which is how often the player can attack) 
            //this basically resets the attack timer
            timeSinceFiring = fireSpeed;
            
            //call the GameManager class to fire a projectile. 
            //GameManager class handles this
            gameManager.FireProjectile();
        }
        else if ((timeSinceFiring <=0) && Input.GetMouseButton(1))
        {
            timeSinceFiring = fireSpeed;
            
            gameManager.FireSnowbomb();
        }
    }

    //handles playerMovement. again. Title yo
    void playerMovement()
    {
        //checks if any input that handles left/right or up/down is detected
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        
        //get the direction from the inputX and inputY
        //works for diagonal movement as well
        Vector3 direction = new Vector3(inputX, inputY, 0);
        if (direction == Vector3.zero)
        {
            dir = 0.0f;
        }
        if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX= true;
        }
        else if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //normalize (make it 1's), although i suspect this is already normalized since inputX and inputY return values of 1, -1, or 0.
        //still, better safe than sorry lol
        direction.Normalize();
        dir += Mathf.Abs(direction.x) + Mathf.Abs(direction.y);
        Mathf.Clamp(dir, 0, 1);
        

        //increase the  of movement by moveSpeed so we move. 
        //moveSpeed is a float that's locked between 0 and 1. just...trust me on this one. 
        direction *= moveSpeed;
        
        //change the position of the Player object by calling the transform position of this game object and adding the direction
        //Vector3 testPos = transform.position + dir;
        //if ((testPos.x<-26)||(testPos.x>26) || (testPos.y>15) || (testPos.y<-15))
        //{
        //    return;
        //}
        transform.position += (direction);
        
        //keep player in camera view
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.02f, 0.98f);
        pos.y = Mathf.Clamp(pos.y, 0.05f, 0.95f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        
    }

    public float returnHealthPercent()
    {
        return health / maxHealth;
    }

    public void setFireSnowball()
    {
        canFireSnowball = true;
        snowBallTimeLeft = 15.0f;
    }

    public void setFireIcicle()
    {
        canFireIcicle = true;
        icicleTimeLeft = 15.0f;

    }
}
