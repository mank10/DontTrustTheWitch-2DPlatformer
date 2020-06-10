using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject playerAttackPoint;
    private Animator player;
    private bool attackButton;
    private GameObject audioObject;
    private AudioManager audioManager;
    
    void Start()
    {
        playerAttackPoint.SetActive(false);
        player = GetComponent<Animator>();
        audioObject = GameObject.Find("AudioManager");
        audioManager = audioObject.GetComponent<AudioManager>();
    }

    void Update()
    {
        if(attackButton && player.GetBool("isPoisoned") == false)
        {
            player.SetBool("isAttacking", true);
            player.SetBool("isJumpAttack", true);
            Invoke("StopJumpAttack", 0.5f);
            Invoke("StopAttackCollider", 0.6f);
        }
        else
        {
            player.SetBool("isAttacking", false);
        }
    }

    void StartAttackCollider()
    {
        playerAttackPoint.SetActive(true);
    }

    void StopAttackCollider()
    {
        playerAttackPoint.SetActive(false);
    }

    void StopJumpAttack()
    {
        player.SetBool("isJumpAttack",false);
    }
    
    public void AttackButtonPressed()
    {
        attackButton = true;
        Invoke("StopAttacking", 0.01f);
    }

    public void PlayAttackSound()
    {
        audioManager.Play("SwordAir");
    }

    void StopAttacking()
    {
        attackButton = false;
    }
}
