using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public PlayerBody PlayerBody;
    public Transform shoot_point;
    public float shoot_speed;

    private void FixedUpdate()
    {
        if (Input.GetButtonDown(PlayerBody.Index_name + "fire"))
        {
            if (PlayerBody.Trash > 0)
                Fire();
        }
    }

    void Fire()
    {
        PlayerBody.Loss_Garbage(1);
        Vector3 dir = shoot_point.position - transform.position;
        dir.Normalize();
        GarbageMain am = GameObjectPoolManager.Instance.Pool_Garbage.AllocateGameObject<GarbageMain>(GameBoardManager.Instance.GameBoardMovingThingsCanvas.transform);
        am.transform.position = shoot_point.position;
        am.Rigidbody2D.velocity = dir * shoot_speed;
    }
}