using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Move : MonoBehaviour
{
    public float hor, ver;
    public int playerIndex;
    public Vector3 ro;
    public float Rotate_Speed;
    public float Move_Speed;
    public float max_speed;
    public float Slow_Down;
    public float AcSpeed;
    public float speed;//当前速度
    string Index_name;
    public float speed_h, speed_v;
    Vector3 lastestSpeed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        Index_name = "P" + playerIndex.ToString() + "_";
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Shoot>().Index_name = Index_name;
        speed_h = 0;
        speed_v = 0;

    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        hor = Input.GetAxisRaw(Index_name + "hor");
        ver = Input.GetAxisRaw(Index_name + "ver");
        if (hor != 0 || ver != 0)
            Rota();
        Trans();
    }

    void Rota()
    {
        ro = this.transform.rotation.eulerAngles;
        float target_z=0;
        if(hor==1)
        {
            if(ver==1)
            {
                target_z = 315;
            }
            else if(ver==0)
            {
                target_z = 270;
            }
            else if(ver ==-1)
            {
                target_z = 225;
            }
        }
        else if(hor==0)
        {
            if (ver == 1)
            {
                target_z = 0;
            }           
            else if (ver == -1)
            {
                target_z = 180;
            }
        }
        else if (hor == -1)
        {
            if (ver == 1)
            {
                target_z = 45;
            }
            else if (ver == 0)
            {
                target_z = 90;
            }
            else if (ver == -1)
            {
                target_z = 135;
            }
        }

        float cha = target_z - this.transform.rotation.eulerAngles.z;
        if(this.transform.rotation.eulerAngles.z>target_z-2&&this.transform.rotation.eulerAngles.z<target_z+2)
        {
            Vector3 temp;
            temp = this.transform.rotation.eulerAngles;
            temp.z = target_z;
            this.transform.rotation = Quaternion.Euler(temp);
        }
        if (this.transform.rotation.eulerAngles.z <target_z)
        {
            Vector3 temp;
            temp = this.transform.rotation.eulerAngles;
            if (cha - 180 > 0)
            {
                temp.z -= Rotate_Speed * Time.deltaTime;
            }
            else
                temp.z += Rotate_Speed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(temp);
        }
        else if (this.transform.rotation.eulerAngles.z > target_z)
        {
            Vector3 temp;
            temp = this.transform.rotation.eulerAngles;
            if (cha + 180 < 0)
            {
                temp.z += Rotate_Speed * Time.deltaTime;
            }
            else
                temp.z -= Rotate_Speed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(temp);
        }

    }
    void Trans()
    {
        if (rb.velocity.x > -max_speed && rb.velocity.x < max_speed)
            rb.AddForce(new Vector2(hor, 0));
        if (rb.velocity.y > -max_speed && rb.velocity.y < max_speed)
            rb.AddForce(new Vector2(0, ver));
        speed = rb.velocity.magnitude;
        /*if (rb.velocity.magnitude < max_speeed)
            rb.AddForce(new Vector2(hor, ver) * Move_Force);
        else if (rb.velocity.magnitude >= max_speeed)
            rb.velocity = new Vector2(hor, ver) * max_speeed;

        if(hor==0&&ver==0&&rb.velocity.magnitude>0)
        {
            rb.velocity -= Slow_Down * rb.velocity.normalized;
        }

        speed = rb.velocity.magnitude;
        */
        /*if (rb.velocity.magnitude < max_speeed)
            rb.velocity += Move_Force * new Vector2(hor, ver);
        else
            rb.velocity = new Vector2(hor, ver) * max_speeed;

        if (hor == 0 && ver == 0 && rb.velocity.magnitude > 0)
        {
            rb.velocity -= Slow_Down * rb.velocity.normalized;
        }

        speed = rb.velocity.magnitude;
        */

        /*if (speed_h > -max_speed && speed_h < max_speed)
        {
            speed_h += hor * AcSpeed * Time.deltaTime;
        }
        else if (speed_h < -max_speed)
            speed_h = -max_speed;
        else if (speed_h > max_speed)
            speed_h = max_speed;

        if (speed_v > -max_speed && speed_v < max_speed)
        {
            speed_v += ver * AcSpeed *Time.deltaTime;
        }
        else if (speed_v < -max_speed)
            speed_h = -max_speed;
        else if (speed_v > max_speed)
            speed_h = max_speed;
/////////////////////////////////////////////////////////////////
        if (hor == 0)
        {
            if(speed_h<0)
            {
                speed_h += Slow_Down*Time.deltaTime;
                if (speed_h > 0)
                    speed_h = 0;
            }
            else if(speed_h>0)
            {
                speed_h -= Slow_Down * Time.deltaTime;
                if (speed_h < 0)
                    speed_h = 0;
            }
        }
        if (ver== 0)
        {
            if (speed_v < 0)
            {
                speed_v += Slow_Down * Time.deltaTime;
                if (speed_v > 0)
                    speed_v = 0;
            }
            else if (speed_v > 0)
            {
                speed_v -= Slow_Down * Time.deltaTime;
                if (speed_v < 0)
                    speed_v = 0;
            }
        }

        /////////////////////////////////////////////
        rb.velocity = new Vector2(speed_h, speed_v);
        speed = rb.velocity.magnitude;
        speed_h = rb.velocity.x;
        speed_v = rb.velocity.y;
        */
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Wall"))
        {
            if(collision.contacts[0].normal.y==1 || collision.contacts[0].normal.y == -1)
            {
                Vector2 temp;
                temp = rb.velocity;
                temp.y = -temp.y;
                rb.velocity = temp;
            }
            if (collision.contacts[0].normal.x == 1 || collision.contacts[0].normal.x == -1)
            {
                Vector2 temp;
                temp = rb.velocity;
                temp.x = -temp.x;
                rb.velocity = temp;
            }
        }
    }
    */
}
