using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerDirection { North, East, South, West }

public class Player : MonoBehaviour
{
    #region initialization
    public PlayerDirection curDirection = PlayerDirection.North;
    private Rigidbody2D player;

    void Start() {  player = this.gameObject.GetComponent<Rigidbody2D>(); }
    #endregion initialization

    void Update()
    {
        switch (curDirection)
        {
            case PlayerDirection.North: player.rotation = 0; break;
            case PlayerDirection.East: player.rotation = 270; break;
            case PlayerDirection.South: player.rotation = 180; break;
            case PlayerDirection.West: player.rotation = 90; break;
        }
    }
    #region rotation input
    public void RotateLeft(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;

        curDirection--;
        if ((int)curDirection < 0) curDirection = PlayerDirection.West;
    }

    public void RotateRight(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;

        curDirection++;
        if ((int)curDirection > 3) curDirection = PlayerDirection.North;
    }
    #endregion rotation input
}