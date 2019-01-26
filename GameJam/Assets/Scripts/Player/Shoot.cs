using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shoot_point;   
    public GameObject ammo;
    public float shoot_speed;
    public string Index_name;
    // Start is called before the first frame update
    void Start()
    {
        //shoot_point = GetComponentInChildren<GameObject>();
        //Index_name = GetComponent<Move>().Index_name;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(Input.GetButtonDown(Index_name + "fire"))
        {         
            Fire();
        }
    }

    void Fire()
    {
        Vector3 dir;
        dir = shoot_point.transform.position - transform.position;
        dir.Normalize();
        GameObject am = Instantiate(ammo, shoot_point.transform.position,Quaternion.Euler(dir));
        am.GetComponent<Rigidbody2D>().velocity = dir*shoot_speed;
    }
}
