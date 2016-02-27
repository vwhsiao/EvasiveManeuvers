using UnityEngine;
using System.Collections;

public class DestroyField : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        switch (c.gameObject.tag)
        {
        case "playerProjectile":
        case "Enemy":
        case "EnemyGroup":
            Destroy(c.gameObject);
            break;
        }
    }
}
