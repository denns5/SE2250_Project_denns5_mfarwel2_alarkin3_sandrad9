using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public float radius = 1f;
    public float camWidth;
    public float camHeight;
    public bool keepOnScreen = true;
    public bool isOnScreen = true;

    void Awake()
    {//setting the height and width of the camera to help determine when an object is off or on the screen
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {//creating if statements to check if an object is either off the screen to the left, right, above, or below
        Vector3 pos = transform.position;
        isOnScreen = true;
        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            isOnScreen = false;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            isOnScreen = false;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            isOnScreen = false;
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            isOnScreen = false;
        }
        if (keepOnScreen && !isOnScreen)
        {//this if statement will keep the hero ship on the screen if it is about to leave
            transform.position = pos;
            isOnScreen = true;
        }

    }

    void OnDrawGizmos() {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize); }
    
}
