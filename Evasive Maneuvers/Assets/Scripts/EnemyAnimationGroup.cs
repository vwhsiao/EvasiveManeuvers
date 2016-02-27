using UnityEngine;
using System.Collections;

public class EnemyAnimationGroup : MonoBehaviour
{
    public enum Rotation { NONE, CLOCKWISE, COUNTERCLOCKWISE };
    public Rotation rotation = Rotation.NONE;
    public int rotationDegrees = 30;
    private float deltaRotationZ;

	void Start()
    {
	    switch (rotation)
        {
        case Rotation.CLOCKWISE:
            deltaRotationZ = rotationDegrees;
            break;
        case Rotation.COUNTERCLOCKWISE:
            deltaRotationZ = -rotationDegrees;
            break;
        case Rotation.NONE:
        default:
            deltaRotationZ = 0f;
            break;
        }
	}
	
	void Update()
    {
        Quaternion newRotation = transform.rotation;
        newRotation.z += deltaRotationZ;
        Debug.Log (newRotation.z);
        transform.rotation = newRotation;
    }
}
