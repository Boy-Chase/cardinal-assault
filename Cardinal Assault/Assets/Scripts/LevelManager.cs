using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        public int EnemyCount;
    }

    [SerializeField] public TextAsset level;
    [SerializeField] public Image img;
    private LevelData data;
    public Player player;
    public List<Enemy[]> enemies;
    public Enemy[] enemyTypes;

    public float beatSpeed = 0.5f;
    public float timePassed;
    public float tutorialTimer;
    public bool tutorialDone;
    public int steps = 0;
    public GameObject endPanel;
    public TextMeshProUGUI gradeText;

    private int enemyCount;

    private Color tempColor;
    private float imgAlpha = .5f;

    public Image tutorialScreen;
    public TextMeshProUGUI countDownText;

    public int getEnemyCount()
    {
        return enemyCount;
    }

    void Start()
    {
        data = JsonUtility.FromJson<LevelData>(level.text);
        enemies = new List<Enemy[]>();
        for (int i = 0; i < 4; i++) enemies.Add(new Enemy[data.Length]);

        beatSpeed = data.BeatSpeed;

        enemyCount = data.EnemyCount;

        tempColor = img.color;

        ResetImageAlpha(false);

        ConvertToObjects();

        tutorialTimer = 0;

        tutorialDone = false;

        EnemyManager.Instance.SetEnemies();
        EnemyManager.Instance.SetSprites();
    }

    private void Update()
    {
        if (tutorialScreen.isActiveAndEnabled) return;
        if (tutorialTimer > 4)
        {
            tutorialDone = true;
            tutorialTimer = 0;
            timePassed = 0;
            player.inTutorial = false;
        }
        if (!tutorialDone && !tutorialScreen.isActiveAndEnabled)
        {
            timePassed += Time.deltaTime;
            if (beatSpeed <= timePassed)
            {
                tutorialTimer++;
                if (tutorialTimer == 4)
                {
                    countDownText.SetText("Go!");
                }
                else
                {
                    countDownText.SetText((4 - tutorialTimer).ToString());
                }
                
                timePassed = 0;
            }
            return;
        }
        
        
        timePassed += Time.deltaTime;

        tempColor.a = imgAlpha - (timePassed / beatSpeed);
        img.color = tempColor;

        if (beatSpeed <= timePassed)
        {
            Step();
            timePassed = 0;
            ResetImageAlpha(true);
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
        Player.Instance.StepSound();
        Player.Instance.bulletCharge += 1;

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
        steps++;
        if (data.Length + 5 < steps)
        {
            endPanel.SetActive(true);
            gradeText.SetText(player.GetComponent<Player>().grade);
        }
    }  

    public void ResetImageAlpha(bool indicator)
    {
        if (!indicator)
        {
            tempColor.a = 0;
            img.color = tempColor;
        }
        else
        {
            tempColor.a = imgAlpha;
            img.color = tempColor;
        }
    }

    public void RemoveEnemy(GameObject g)
    {
        //Debug.Log($"{g.transform.parent.GetSiblingIndex()}, {int.Parse(g.name) - 1}");
        //EnemyManager.Instance.SetbackSpace(g.transform.parent.GetSiblingIndex(), int.Parse(g.name) - 1);
        //enemies[g.transform.parent.GetSiblingIndex()][int.Parse(g.name)-1] = null;
        //enemies[(int)g.GetComponent<EnemyDisplay>().dataPosition.x -1][(int)g.GetComponent<EnemyDisplay>().dataPosition.y -1] = null;
        g.GetComponent<EnemyDisplay>().enemy = null;
        EnemyManager.Instance.SetSprites();
    }
}