using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public float speed;
    public PlayerBody p;
    public int whichPlayer;
    bool Charging;

    private void FixedUpdate()
    {
        if (Charging)
            p.Add_Energy(speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((int)collision.GetComponent<PlayerBody>().WhichPlayer+1) == whichPlayer)
        {
            Charging = true;
            if (p == null)
                p = collision.gameObject.GetComponent<PlayerBody>();
        }
           
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((int)collision.GetComponent<PlayerBody>().WhichPlayer + 1) == whichPlayer)
            Charging = false;
    }

}
