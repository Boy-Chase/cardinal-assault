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

    void Start()
    {
        // wipe all the sprites
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 3; j++) {
                transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    public void SetEnemies()
    {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 3; j++) {
                transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy = LevelManager.Instance.enemies[i][j];
            }
        }
    }

    public void SetSprites()
    {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 3; j++) {
                if (transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy == null) {
                    transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sprite = null;
                }
                else transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().sprite = transform.GetChild(i).GetChild(j).GetComponent<EnemyDisplay>().enemy.sprite;
            }
        }
    }
}