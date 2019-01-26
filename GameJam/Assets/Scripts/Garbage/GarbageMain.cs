using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageMain : MonoBehaviour
{
    public bool Can_Pick;   
    public int num_;
    public int damage;
    CircleCollider2D circle;
    private void Awake()
    {
        circle = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        if(Can_Pick)
        {
            circle.isTrigger = true;
        }
        else
        {
            circle.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerParameter>().Hitted(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerParameter>().Pick_Garbage(num_);
    }
}
