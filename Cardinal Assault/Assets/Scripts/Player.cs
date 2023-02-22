using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public GameObject pressToStartPanel;

    public ParticleSystem hurt;
    public ParticleSystem block;
    public int health = 3;
    public int tutorialPress = 0;

    // audio

    // Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=83127">Pixabay</a>
    public AudioClip blockSound;

    // Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=95847">Pixabay</a>
    public AudioClip hitSound;

    // Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=101008">Pixabay</a>
    public AudioClip moveSound;

    void Start() {  player = this.gameObject.GetComponent<Rigidbody2D>(); levelEditor = GameObject.FindGameObjectWithTag("GameManager");}
    #endregion initialization

    void Update()
    {
        if (9 <= tutorialPress)
        {
            levelEditor.GetComponent<LevelManager>().tutorialDone = true;
            pressToStartPanel.SetActive(false);
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<EnemyDisplay>(out EnemyDisplay e)) return;
        if (e.enemy == null) return;

        LevelManager.Instance.RemoveEnemy(collision.gameObject);

        if (collision.IsTouching(transform.GetComponent<BoxCollider2D>())) {    
            health--;
            Debug.Log($"Player got hit! Health: {health}");
            hurt.Play();
        } else {
            //Debug.Log($"Player blocked an Enemy");
            block.Play();
        }
    }

    #region rotation input
    public void RotateLeft(InputAction.CallbackContext context)
    {
        tutorialPress++;

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
    }

    public void RotateRight(InputAction.CallbackContext context)
    {
        tutorialPress++;

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
    }

    public void RotateUp(InputAction.CallbackContext context)
    {
        tutorialPress++;

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
    }

    public void RotateDown(InputAction.CallbackContext context)
    {
        tutorialPress++;

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
    }
    #endregion rotation input
}