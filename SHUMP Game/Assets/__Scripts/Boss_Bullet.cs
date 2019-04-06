using UnityEngine;
using System.Collections;

public class Boss_Bullet : MonoBehaviour
{
    float moveSpeed = 10f;

    public int angle;
    Rigidbody rb;
    void Start()
    {
        Vector3 vel = Vector3.down * moveSpeed;
        rb = GetComponent<Rigidbody>();
        rb.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        rb.velocity = rb.transform.rotation * vel;
    }
}
