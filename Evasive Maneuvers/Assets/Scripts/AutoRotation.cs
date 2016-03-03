using UnityEngine;
using System.Collections;

public class AutoRotation : MonoBehaviour
{
    public enum Rotation { NONE, CLOCKWISE, COUNTERCLOCKWISE };
    public Rotation rotation = Rotation.NONE;
    public int rotationDegrees = 30;
    private int direction;

	void Start()
    {
	    switch (rotation)
        {
        case Rotation.CLOCKWISE:
            direction = 1;
            break;
        case Rotation.COUNTERCLOCKWISE:
            direction = -1;
            break;
        case Rotation.NONE:
        default:
            direction = 0;
            break;
        }
	}
	
	void Update()
    {
        if (!GameManager.instance.playing)
            return;

        //transform.eulerAngles = Vector3(0f, 0f, (direction)*rotationDegrees);
        transform.Rotate(new Vector3(0f, 0f, (direction)*rotationDegrees));
    }
}
