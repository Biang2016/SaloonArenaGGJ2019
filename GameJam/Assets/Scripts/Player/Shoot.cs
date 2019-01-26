using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public PlayerBody PlayerBody;
    public Transform shoot_point;
    public float shoot_speed;
    public GameObject ammo;
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
        int sfx_index = Random.Range(0, 2);
        PlayerBody.SoundPlay("sfx/HitFromFar_" + sfx_index, 0.5f);
        Ammo am = GameObjectPoolManager.Instance.Pool_GarbageLitter.AllocateGameObject<Ammo>(GameBoardManager.Instance.GameBoardGarbagesCanvas.transform);
        
        
        am.player = (int)PlayerBody.WhichPlayer;
        //am.CanPick = false;
        am.transform.position = shoot_point.transform.position;
        am.transform.rotation = transform.rotation;
        am.Rigidbody2D.velocity = dir * shoot_speed;
    }
}