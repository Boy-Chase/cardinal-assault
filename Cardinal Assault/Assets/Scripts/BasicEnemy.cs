using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // left = 0
    // up = 1
    // right = 2
    // down = 3
    public int lane;
    public int tilePosition;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().change)
        {
            tilePosition++;
        }

        if (lane == 0 && tilePosition == 0)
        {
            gameObject.transform.position = new Vector2(-9, 0);
        }
        else if (lane == 0 && tilePosition == 1)
        {
            gameObject.transform.position = new Vector2(-6, 0);
        }
        else if (lane == 0 && tilePosition == 2)
        {
            gameObject.transform.position = new Vector2(-3, 0);
        }
        else if (lane == 0 && tilePosition == 3)
        {
            gameObject.transform.position = new Vector2(-2, 0);
        }
        else if (lane == 1 && tilePosition == 0)
        {
            gameObject.transform.position = new Vector2(0, 4);
        }
        else if (lane == 1 && tilePosition == 1)
        {
            gameObject.transform.position = new Vector2(0, 3);
        }
        else if (lane == 1 && tilePosition == 2)
        {
            gameObject.transform.position = new Vector2(0, 2);
        }
        else if (lane == 1 && tilePosition == 3)
        {
            gameObject.transform.position = new Vector2(0, 1);
        }
        else if (lane == 2 && tilePosition == 0)
        {
            gameObject.transform.position = new Vector2(9, 0);
        }
        else if (lane == 2 && tilePosition == 1)
        {
            gameObject.transform.position = new Vector2(6, 0);
        }
        else if (lane == 2 && tilePosition == 2)
        {
            gameObject.transform.position = new Vector2(3, 0);
        }
        else if (lane == 2 && tilePosition == 3)
        {
            gameObject.transform.position = new Vector2(2, 0);
        }
        else if (lane == 3 && tilePosition == 0)
        {
            gameObject.transform.position = new Vector2(0, -4);
        }
        else if (lane == 3 && tilePosition == 1)
        {
            gameObject.transform.position = new Vector2(0, -3);
        }
        else if (lane == 3 && tilePosition == 2)
        {
            gameObject.transform.position = new Vector2(0, -2);
        }
        else if (lane == 3 && tilePosition == 3)
        {
            gameObject.transform.position = new Vector2(0, -1);
        }
    }
}
