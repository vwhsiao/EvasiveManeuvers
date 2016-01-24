using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //public variables
    public GameObject projectile;
    public int bulletBillMoveSpeed;

    //private variables
    private GameObject player;
    private Vector2 direction; 
    void Awake()
    {
        player = GameObject.Find("Player");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FireProjectile()
    {
        //find mouse position and translate that into a Vector with 3 floats (x,y,z)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Quaternion is just a fancy thing to say a Vector with a direction. 
        //Gets the rotation for the object (projectile) to point at
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, mousePos - player.transform.position);
        
        //Create the object as a GameObject so we can still change values of it
        GameObject firedProjectile = Instantiate(projectile, player.transform.position, rotation ) as GameObject;
        
        //Get the vector of the direction projectil is going to go in
		direction = mousePos - player.transform.position;

        //Normalize it (make it into units of 1's)
		direction.Normalize();

        //Move the projectile in front of the player object so it doesn't just create it on top of the player
        //trust me, it looks wonky as hell. if you do'nt believe me, get rid of the *2's in the next line and then try it
        firedProjectile.transform.position += new Vector3 (direction.x*2, direction.y*2, 0);

        //increase the projectile velocity so it actually, you know, projectiles
        firedProjectile.GetComponent<Rigidbody2D>().velocity = direction * bulletBillMoveSpeed;

        //logistical thing, set the parent of the object to the GameManager so we do'nt flood the hierarchy screen
        firedProjectile.transform.parent = transform;
        
    }
}
