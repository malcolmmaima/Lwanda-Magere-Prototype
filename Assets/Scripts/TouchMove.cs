using UnityEngine;
using System.Collections;

public class TouchMove : MonoBehaviour {

public float speed = 1.0f;

void Update () {
//Drag our game object across the Y Axis
        foreach(Touch touch in Input.touches)
        {
        Vector3 newPosition = transform.position;

        newPosition.y += touch.deltaPosition.y * speed * Time.deltaTime;

        transform.position = newPosition;
        }

		//Drag our game object across the X Axis
		foreach(Touch touch in Input.touches)
		{
			Vector4 newPosition2 = transform.position;
	
			newPosition2.x += touch.deltaPosition.x * speed * Time.deltaTime;
			
			transform.position = newPosition2;
		}

}
}