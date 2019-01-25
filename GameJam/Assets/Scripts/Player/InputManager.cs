using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class InputButton
{
    public int PlayerIndex;

    public KeyCode Up, Down, Left, Right;
}


public class InputManager : MonoBehaviour
{
    InputButton[] p = new InputButton[4];
    private void Start()
    {
        
    }



}
