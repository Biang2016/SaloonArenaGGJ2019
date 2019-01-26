using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doarea : MonoBehaviour
{

    public PlayerBody playerBody;
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerBody pb = collision.gameObject.GetComponent<PlayerBody>();
        if (collision.CompareTag("Player") && pb.Lying && pb.WhichPlayer != playerBody.WhichPlayer)
        {
            if (playerBody.do_time > 1)
            {
                if(pb.Trash>0)
                {
                    int temp = Mathf.CeilToInt(pb.Trash * (float)playerBody.Do_num / 100);
                    pb.Loss_Garbage(temp);
                    playerBody.Pick_Garbage(temp);
                    playerBody.do_time = 0;
                }               
            }
            else
            {
                playerBody.do_time += Time.deltaTime;
            }

        }
    }
}
