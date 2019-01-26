using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageMain : PoolObject
{
    public bool Can_Pick;
    public int num_;
    public int damage;
    public CircleCollider2D circle;
    public Rigidbody2D Rigidbody2D;

    private void Awake()
    {
    }

    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (Can_Pick)
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
            collision.gameObject.GetComponent<PlayerBody>().Hitted(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerBody>().Pick_Garbage(num_);
    }

    public float time;
    public int Damage;
}