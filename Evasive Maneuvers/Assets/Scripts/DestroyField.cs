using UnityEngine;
using System.Collections;

public class DestroyField : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log ("Destory!");
        switch (c.gameObject.tag)
        {
        case "playerProjectile":
        case "enemy":
        case "Enemy":
            Destroy(c.gameObject);
            break;
        }
    }
}
