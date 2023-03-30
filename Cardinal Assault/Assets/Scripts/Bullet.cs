using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 30;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<EnemyDisplay>(out EnemyDisplay e)) return;
        if (e.enemy == null) return;

        Vector3 pos = new Vector3(int.Parse(collision.gameObject.transform.parent.name), int.Parse(collision.gameObject.name));

        LevelManager.Instance.enemies[(int)pos.x][(int)pos.y] = null;

        pos.y -= 1;
        if (pos.y < 0) pos.y = 0;

        LevelManager.Instance.enemies[(int)pos.x][(int)pos.y] = null;

        LevelManager.Instance.RemoveEnemy(collision.gameObject);

        Destroy(this.gameObject);
    }
}