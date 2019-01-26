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
    public float Move_Force;
    string Index_name;  
    
    
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        Index_name = "P" + playerIndex.ToString() + "_";
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Shoot>().Index_name = Index_name;
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
        rb.AddForce(new Vector2(hor, ver) *Move_Force );
    }

}
