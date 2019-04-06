using UnityEngine;
using System.Collections;

public class Boss_Bullet : MonoBehaviour
{
    float moveSpeed = 10f;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.down * moveSpeed;
    }
}
