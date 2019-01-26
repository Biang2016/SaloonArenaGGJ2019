using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageMain : MonoBehaviour
{
    public bool Can_Pick;
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
}
