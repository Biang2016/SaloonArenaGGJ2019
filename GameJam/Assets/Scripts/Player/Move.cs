using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Move : MonoBehaviour
{
    public PlayerBody PlayerBody;
    public float hor, ver;
    public float Rotate_Speed;
    public float max_speed;

    public float speed; //当前速度

    Vector3 lastestSpeed;
    public Rigidbody2D Rigidbody2D;

    private void Awake()
    {
    }

    void Start()
    {
        Rotate_Speed = GameManager.Instance.Rotate_Speed;
        max_speed = GameManager.Instance.Max_Speed;
        Rigidbody2D.mass = GameManager.Instance.RobotMass;
        Rigidbody2D.drag = GameManager.Instance.RobotDrag;
        Rigidbody2D.angularDrag = GameManager.Instance.RobotRotateDrag;
    }

    private void FixedUpdate()
    {
        if (!PlayerBody.Lying)
        {
            hor = Input.GetAxisRaw(PlayerBody.Index_name + "hor");
            ver = Input.GetAxisRaw(PlayerBody.Index_name + "ver");
            if (hor != 0 || ver != 0)
                Rota();
            Trans();
        }
    }

    void Rota()
    {
        float target_z = 0;
        if (hor == 1)
        {
            if (ver == 1)
            {
                target_z = 315;
            }
            else if (ver == 0)
            {
                target_z = 270;
            }
            else if (ver == -1)
            {
                target_z = 225;
            }
        }
        else if (hor == 0)
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
        if (this.transform.rotation.eulerAngles.z > target_z - 2 && this.transform.rotation.eulerAngles.z < target_z + 2)
        {
            Vector3 temp;
            temp = this.transform.rotation.eulerAngles;
            temp.z = target_z;
            this.transform.rotation = Quaternion.Euler(temp);
        }

        if (this.transform.rotation.eulerAngles.z < target_z)
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
        if (Rigidbody2D.velocity.x > -max_speed && Rigidbody2D.velocity.x < max_speed)
            Rigidbody2D.AddForce(new Vector2(hor, 0));
        if (Rigidbody2D.velocity.y > -max_speed && Rigidbody2D.velocity.y < max_speed)
            Rigidbody2D.AddForce(new Vector2(0, ver));
        speed = Rigidbody2D.velocity.magnitude;
    }
}