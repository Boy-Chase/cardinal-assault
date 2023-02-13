using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerDirection { North, East, South, West }

public class Player : MonoBehaviour
{
    #region singleton
    public static Player Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    #endregion singleton

    #region initialization
    public PlayerDirection curDirection = PlayerDirection.North;
    private Rigidbody2D player;

    public PlayerDirection lastDirection = PlayerDirection.North;
    public bool waiting = false;
    public bool change = false;
    public int movesMade;

    // time used for each movement + overall time saved with moves
    public float moveTimer = 5.0f;
    public float scoreTime = 0.0f;
    public GameObject levelEditor;

    public ParticleSystem hurt;
    public ParticleSystem block;

    public int health = 3;

    void Start() {  player = this.gameObject.GetComponent<Rigidbody2D>(); levelEditor = GameObject.FindGameObjectWithTag("GameManager");}
    #endregion initialization

    void Update()
    {
        moveTimer -= Time.deltaTime;

        waiting = false;
        change = false;

        switch (curDirection)
        {
            case PlayerDirection.North: player.rotation = 0; break;
            case PlayerDirection.East: player.rotation = 270; break;
            case PlayerDirection.South: player.rotation = 180; break;
            case PlayerDirection.West: player.rotation = 90; break;
        }

        if (curDirection != lastDirection)
        {
            change = true;
        }

        lastDirection = curDirection;
    }

    #region rotation input
    public void RotateLeft(InputAction.CallbackContext context)
    {
        levelEditor.GetComponent<LevelManager>().tutorialDone = true;

        // add saved time to time score
        if (0.0f < moveTimer)
        {
            scoreTime += moveTimer;
        }

        // increment moves made + return and reset move timer
        movesMade++;
        moveTimer = 5.0f;

        if (!context.action.triggered) return;

        curDirection = PlayerDirection.West;
        // if ((int)curDirection < 0) curDirection = PlayerDirection.West;
    }

    public void RotateRight(InputAction.CallbackContext context)
    {
        levelEditor.GetComponent<LevelManager>().tutorialDone = true;

        // add saved time to time score
        if (0.0f < moveTimer)
        {
            scoreTime += moveTimer;
        }

        // increment moves made + return and reset move timer
        movesMade++;
        moveTimer = 5.0f;

        if (!context.action.triggered) return;

        curDirection = PlayerDirection.East;
        // if ((int)curDirection > 3) curDirection = PlayerDirection.North;
    }

    public void RotateUp(InputAction.CallbackContext context)
    {
        levelEditor.GetComponent<LevelManager>().tutorialDone = true;

        // add saved time to time score
        if (0.0f < moveTimer)
        {
            scoreTime += moveTimer;
        }

        // increment moves made + return and reset move timer
        movesMade++;
        moveTimer = 5.0f;

        if (!context.action.triggered) return;

        curDirection = PlayerDirection.North;
        // if ((int)curDirection < 0) curDirection = PlayerDirection.North;
    }

    public void RotateDown(InputAction.CallbackContext context)
    {
        levelEditor.GetComponent<LevelManager>().tutorialDone = true;

        // add saved time to time score
        if (0.0f < moveTimer)
        {
            scoreTime += moveTimer;
        }

        // increment moves made + return and reset move timer
        movesMade++;
        moveTimer = 5.0f;

        if (!context.action.triggered) return;

        curDirection = PlayerDirection.South;
        // if ((int)curDirection < 0) curDirection = PlayerDirection.South;
    }
    #endregion rotation input
}