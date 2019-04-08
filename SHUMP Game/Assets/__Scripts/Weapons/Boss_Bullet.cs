using UnityEngine;
using System.Collections;

public class Boss_Bullet : MonoBehaviour
{
    float moveSpeed = 10f;//speed of the bullet

    public int angle;//angle at which the bullet comes out
    Rigidbody rigidBody;//rigid body variable
    void Start()
    {
        moveSpeed += ScoreManager.LEVEL;
        Vector3 vel = Vector3.down * moveSpeed;//bullets move down the screen
        rigidBody = GetComponent<Rigidbody>();//get the rigid body component
        rigidBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);//setting the angle of the bullet
        rigidBody.velocity = rigidBody.transform.rotation * vel;//giving the rigid body a velocity
    }
}
