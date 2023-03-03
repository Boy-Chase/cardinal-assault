using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region singleton
    public static EnemyManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    #endregion singleton

    private Vector2[,] lanePositions;
    private Vector2[,] goalPositions;
    private Vector2 playerPosition;
    private Vector2 nullVec = new Vector2(12, 12);
    public float duration;
    public float timeToComplete;

    void Start()
    {
        // Record where the tile positions are so we can animate them moving without distrubting positions
        lanePositions = new Vector2[4,4];
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                lanePositions[i, j] = transform.GetChild(i).GetChild(j).transform.transform.position;
            }
        }
    }

    
    void Update()
    {
        if (goalPositions == null) return;

        duration += Time.deltaTime;
        timeToComplete = 0.1f;
        float stepTime = 0.5f;

        if (duration >= timeToComplete)
        {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 3; j++) {
                    if (goalPositions[i, j] == nullVec) continue;
                    else transform.GetChild(i).GetChild(j).position = goalPositions[i, j];
                }
            }
            goalPositions = null; duration = 0;
        } else {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (goalPositions[i, j] == nullVec) continue;
                    transform.GetChild(i).GetChild(j).position = new Vector2(
                        lanePositions[i, j].x + ((goalPositions[i, j].x - lanePositions[i, j].x) * (duration / timeToComplete)),
                        lanePositions[i, j].y + ((goalPositions[i, j].y - lanePositions[i, j].y) * (duration / timeToComplete)));
                }
            }
        }
    }

    public void SetEnemies()
    {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy = LevelManager.Instance.enemies[i][j];
            }
        }
    }

    public void SetSprites()
    {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                if (transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy == null) {
                    transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sprite = null;
                }
                else transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sprite = transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy.sprite;
            }
        }
    }

    public void SetGoalPositions()
    {
        goalPositions = new Vector2[4, 4];
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                if (transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy == null) goalPositions[i, j] = nullVec;
                else if (j - transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy.movement < 0)
                {
                    //Debug.Log("hit player");
                    goalPositions[i, j] = playerPosition;
                }
                else goalPositions[i, j] = lanePositions[i, j - transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy.movement];
                //else if (j - transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy.movement > 3) Debug.Log("");

            }
        }
    }

    public void SetbackSpace(int i, int j)
    {
        goalPositions[i, j] = lanePositions[i, j];
        transform.GetChild(i).GetChild(j).position = goalPositions[i, j];
    }
}