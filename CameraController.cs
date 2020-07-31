using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;            // Add player as our camera target in the unity transform

    public Vector3 offset;              // Stores how far from player we should be & move cam within world based on where player moved

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;          // Let player choose max rotation anglE
    public float minViewAngle;          // Let player choose min rotation angle

    public bool invertY;                // Let player choose if they want to invert cam

    void Start () {
        if(!useOffsetValues)            // If usv is false, then set offset based on how far camera is currently away from player, otherwise use default value
        {
            offset = target.position - transform.position;      // The player target position - where the camera is (took targets positon & took away our position)
        }
        pivot.transform.position = target.transform.position;   // will go wherever target is in world
        //pivot.transform.parent = target.transform;            // Make pivot a child of the player (target)
        pivot.transform.parent = null;                          // Make camera not move with pivot
        Cursor.lockState = CursorLockMode.Locked;               // Lock cursor to center of game window
	}

    void LateUpdate () {                                        // LateUpdate happens after Update, used to make player run smoother

        pivot.transform.position = target.transform.position;   // Pivot point will move with player

        // Get X position of mouse & rotate target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;  // Where mouse is moving on x-axis (left/right)
        pivot.Rotate(0, horizontal, 0);                             // Look from left to right on y-axis (changed to pivot from target)

        // Get the Y position of mouse & rotate pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;        // Mouse moving on y-axis (up/down)
        //pivot.Rotate(-vertical, 0, 0);                                // Only rotate pivot point, camera can take rotation from pivot point and play it to camera itself
        if(invertY)                                                     // Let player choose if they want to invert camera
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }


        // Limit up/down camra rotation so that caera doesn't flip around
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)         // If x angle is greater than 45 & less than 180 for top value rotation
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)   // If x angle is greater than 180 & less than 315 (360-45) for bottom value rotation
        {
            pivot.rotation = Quaternion.Euler(minViewAngle, 0, 0);
        }

        // Move camera based on current rotation of target & the original offset
        float desiredYAngle = pivot.eulerAngles.y;                                 // Euler angles - transform 4 axis quaternion (rotation) back into vector3 for simplicity, changed from target to pivot
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);    // New rotation y value
        transform.position = target.position - (rotation * offset);                 // Take offset and change position in world to desired position

        if(transform.position.y < target.position.y)            // If camera position < target position (if camera goes below height of where player is in world)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);      // Camera zooms in to player the further you go down, -.5f to go slightly below player to see sky
        }

        transform.LookAt(target);       // Camera takes whatever current transform is and makes object rotate aroud world depending on target
	}
}
