using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiWaitEnemy : MonoBehaviour
{
    // left = 0
    // up = 1
    // right = 2
    // down = 3
    public int lane;

    // increment the closer to the player the enemy is (spawning in at tilePosition 0)
    public int tilePosition;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // get reference to our player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().change)
        {
            tilePosition++;
        }

        // set position
        if (lane == 0 && tilePosition == 0)
        {
            gameObject.transform.position = new Vector2(-4.6f, 0);
        }
        else if (lane == 0 && tilePosition == 1)
        {
            gameObject.transform.position = new Vector2(-3, 0);
        }
        else if (lane == 0 && tilePosition == 2)
        {
            gameObject.transform.position = new Vector2(-1.6f, 0);
        }
        else if (lane == 0 && tilePosition == 3)
        {
            gameObject.transform.position = new Vector2(-1.3f, 0);
        }
        else if (lane == 1 && tilePosition == 0)
        {
            gameObject.transform.position = new Vector2(0, 4.3f);
        }
        else if (lane == 1 && tilePosition == 1)
        {
            gameObject.transform.position = new Vector2(0, 3);
        }
        else if (lane == 1 && tilePosition == 2)
        {
            gameObject.transform.position = new Vector2(0, 1.7f);
        }
        else if (lane == 1 && tilePosition == 3)
        {
            gameObject.transform.position = new Vector2(0, 1.4f);
        }
        else if (lane == 2 && tilePosition == 0)
        {
            gameObject.transform.position = new Vector2(4.6f, 0);
        }
        else if (lane == 2 && tilePosition == 1)
        {
            gameObject.transform.position = new Vector2(3, 0);
        }
        else if (lane == 2 && tilePosition == 2)
        {
            gameObject.transform.position = new Vector2(1.6f, 0);
        }
        else if (lane == 2 && tilePosition == 3)
        {
            gameObject.transform.position = new Vector2(1.3f, 0);
        }
        else if (lane == 3 && tilePosition == 0)
        {
            gameObject.transform.position = new Vector2(0, -4.3f);
        }
        else if (lane == 3 && tilePosition == 1)
        {
            gameObject.transform.position = new Vector2(0, -3);
        }
        else if (lane == 3 && tilePosition == 2)
        {
            gameObject.transform.position = new Vector2(0, -1.7f);
        }
        else if (lane == 3 && tilePosition == 3)
        {
            gameObject.transform.position = new Vector2(0, -1.4f);
        }

        // if enemy has reached the player
        if (tilePosition == 3)
        {
            // if player is blocking correctly
            if ((lane == 0 && player.GetComponent<Player>().curDirection == PlayerDirection.West) || (lane == 1 && player.GetComponent<Player>().curDirection == PlayerDirection.North) || (lane == 2 && player.GetComponent<Player>().curDirection == PlayerDirection.East) || (lane == 3 && player.GetComponent<Player>().curDirection == PlayerDirection.South))
            {
                Destroy(gameObject);
            }
            else
            {
                // subtract health here
            }
        }
    }
}
