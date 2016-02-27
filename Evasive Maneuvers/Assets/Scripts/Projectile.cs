using UnityEngine;
using System.Collections;


//Projectile class is made so projectiles handle their own logic for collisions, etc. 
//Movement is handled by the physics engine as GameManager class creates projectiles and lets physics handle the rest
public class Projectile : MonoBehaviour
{
   
	// Use this for initialization
	void Start()
    {
        //Invoke calls a function after a predetermined time
        Invoke("kill", 3);
	}
	
	// Update is called once per frame
	void Update()
    {
	   
	}
    
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("PowerUp"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
        }
    }

    //Destroys this object after a set time so we do'nt use up a ton of processing power
    //Unity still renders and calculates collisions for things that aren't on screen, 
    // so we destroy this before we bluescreen somebody's computer with an obsene amount of objects
    void kill()
    {
        Destroy(this.gameObject);
    }
}
