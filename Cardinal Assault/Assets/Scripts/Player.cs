using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public int streak = 0;
    public int highestStreak = 0;
    public int tutorialPress = 0;

    public HealthBar healthBar;
    public TextMeshProUGUI streakNum;
    public TextMeshProUGUI gradingGrade;

    // audio

    // Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=83127">Pixabay</a>
    public AudioClip blockSound;

    // Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=95847">Pixabay</a>
    public AudioClip hitSound;

    // Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=101008">Pixabay</a>
    public AudioClip moveSound;

    void Start() {  
        player = this.gameObject.GetComponent<Rigidbody2D>(); 
        levelEditor = GameObject.FindGameObjectWithTag("GameManager");
        healthBar.setMaxHealth(health);
    }
    #endregion initialization

    void Update()
    {
        if (health <= 0) SceneManager.LoadScene("GameOver");

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

        if (highestStreak < 6 && streak < 4)
        {
            gradingGrade.SetText("F");
            gradingGrade.color = new Color(1, 1, 1);
        }
        else if (highestStreak < 12 && streak < 7)
        {
            gradingGrade.SetText("D");
            gradingGrade.color = new Color(1, 0.8f, 0.8f);
        }
        else if (highestStreak < 18 && streak < 10)
        {
            gradingGrade.SetText("C");
            gradingGrade.color = new Color(1, 0.6f, 0.6f);
        }
        else if (highestStreak < 24 && streak < 13)
        {
            gradingGrade.SetText("B");
            gradingGrade.color = new Color(1, 0.4f, 0.4f);
        }
        else if (highestStreak < 30 && streak < 20)
        {
            gradingGrade.SetText("A");
            gradingGrade.color = new Color(1, 0.2f, 0.2f);
        }
        else if (highestStreak < 40)
        {
            gradingGrade.SetText("S!");
            gradingGrade.color = new Color(1, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<EnemyDisplay>(out EnemyDisplay e)) return;
        if (e.enemy == null) return;

        LevelManager.Instance.RemoveEnemy(collision.gameObject);

        if (collision.IsTouching(transform.GetComponent<BoxCollider2D>())) {    
            health--;
            healthBar.setHealth(health);
            Debug.Log($"Player got hit! Health: {health}");
            AudioSource.PlayClipAtPoint(hitSound, gameObject.transform.position);
            CameraEffects.Instance.Hurt();
            hurt.Play();
            streak = 0;
            streakNum.SetText(streak.ToString());
            streakNum.color = new Color(1, 1, 1);
        } else {
            AudioSource.PlayClipAtPoint(blockSound, gameObject.transform.position);
            block.Play();
            streak++;
            if (highestStreak < streak) 
            {
                highestStreak = streak;
            }
            streakNum.SetText(streak.ToString());
            streakNum.color = new Color(1, 1 - (streak/10), 1 - (streak/10));
        }
    }

    // used in level manager
    public void StepSound()
    {
        AudioSource.PlayClipAtPoint(moveSound, gameObject.transform.position);
    }

    public void Restart(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;
        SceneManager.LoadScene("StartScene");
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