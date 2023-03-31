using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGlow : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Update()
    {
        if (Player.Instance.bulletCharge < 10) animator.SetBool("glow", false); 
        else animator.SetBool("glow", true);
    }
}
