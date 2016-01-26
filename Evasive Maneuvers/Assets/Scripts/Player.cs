using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// See GameManager for explanations on MonoBehaviour.
public class Player : MonoBehaviour
{
    //Player Member Variables
    
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


    //how many shots the player can fire per second. lower number = higher fire rate
    [Range(0.0f, 2.0f)]
    public float fireSpeed;
    
	public int health = 2;
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "enemy") {
			Destroy (coll.gameObject);
			if (health == 0) {
				Destroy (this.gameObject);
			}
			health--;
		}
	}
    
    //Private
    //last time since a player has fired. this will be set later in playerAttack function
    private float timeSinceFiring=0.0f;
    
    //gets the reference to the GameManager object to handle game related stuff. 
    //for now, it's just projectile management. 
    private GameManager gameManager;


    // See GameManager for explanations on this method.
    void Awake()
    {
        /*Finds the Game Manager object in the scene and saves the GameManager component to the private variable.
         * Avoid using GameObject.Find() too much, because it loops through every object in the scene,
         * searching for the object by exact name. If you misspelled something or changed a name,
         * it will give you trouble. Also if you have a lot of objects... */

        /* Currently as of 1/23/2016, GameManager is what handles firing projectiles. 
          Will probably change for other things as time goes on*/
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    // See GameManager for explanations on this method.
	void Start()
    {

	}


    // See GameManager for explanations on this method.
	void Update() 
    {
        //check for playerMovement input
        playerMovement();

        //check for playerAttack input
        playerAttack();
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
    }

    //handles playerMovement. again. Title yo
    void playerMovement()
    {
        //checks if any input that handles left/right or up/down is detected
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        
        //get the direction from the inputX and inputY
        //works for diagonal movement as well
        Vector3 dir = new Vector3(inputX, inputY, 0);

        //normalize (make it 1's), although i suspect this is already normalized since inputX and inputY return values of 1, -1, or 0.
        //still, better safe than sorry lol
        dir.Normalize();

        //increase the amount of movement by moveSpeed so we move. 
        //moveSpeed is a float that's locked between 0 and 1. just...trust me on this one. 
        dir *= moveSpeed;

        //change the position of the Player object by calling the transform position of this game object and adding the direction
        transform.position += (dir);
    }
}
