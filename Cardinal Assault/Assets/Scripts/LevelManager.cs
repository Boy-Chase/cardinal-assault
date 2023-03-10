using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region singleton
    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    #endregion singleton

    [Serializable]
    public class Rows
    {
        public string[] North;
        public string[] East;
        public string[] South;
        public string[] West;
    }
    [Serializable]
    public class LevelData
    {
        public Rows Rows;
        public int Length;
        public float BeatSpeed;
    }

    [SerializeField] public TextAsset level;
    private LevelData data;
    public List<Enemy[]> enemies;
    public Enemy[] enemyTypes;

    public float beatSpeed = 0.5f;
    public float timePassed;
    public bool tutorialDone;

    void Start()
    {
        data = JsonUtility.FromJson<LevelData>(level.text);
        enemies = new List<Enemy[]>();
        for (int i = 0; i < 4; i++) enemies.Add(new Enemy[data.Length]);

        beatSpeed = data.BeatSpeed;

        ConvertToObjects();

        EnemyManager.Instance.SetEnemies();
        EnemyManager.Instance.SetSprites();
    }

    private void Update()
    {
        if (!tutorialDone) return;
        timePassed += Time.deltaTime;

        if (beatSpeed <= timePassed)
        {
            Step();
            timePassed = 0;
        }
    }

    public void ConvertToObjects()
    {
        string[] row = data.Rows.North;
        for (int i = 0; i < 4; i++)
        {
            if (i == 1) row = data.Rows.East;
            if (i == 2) row = data.Rows.South;
            if (i == 3) row = data.Rows.West;

            for (int j = 0; j < data.Length; j++) {
                for (int k = 0; k < enemyTypes.Length; k++) {
                    if (enemyTypes[k].name == row[j]) enemies[i][j] = enemyTypes[k];
                }
            }
        }
    }

    
    public void Step()
    {
        // sound effect
        Player.Instance.StepSound();

        CameraEffects.Instance.BeginZoom();

        EnemyManager.Instance.SetEnemies();
        EnemyManager.Instance.SetSprites();
        EnemyManager.Instance.SetGoalPositions();
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < data.Length; j++) {
                if (j == 0) {
                    enemies[i][0] = null;
                }
                else if (enemies[i][j-1] == null) {
                    enemies[i][j - 1] = enemies[i][j];
                    enemies[i][j] = null;
                }
            }
        }
    }  

    public void RemoveEnemy(GameObject g)
    {
        //Debug.Log($"{g.transform.parent.GetSiblingIndex()}, {int.Parse(g.name) - 1}");
        //EnemyManager.Instance.SetbackSpace(g.transform.parent.GetSiblingIndex(), int.Parse(g.name) - 1);
        //enemies[g.transform.parent.GetSiblingIndex()][int.Parse(g.name)-1] = null;
        g.GetComponent<EnemyDisplay>().enemy = null;
        EnemyManager.Instance.SetSprites();
    }
}