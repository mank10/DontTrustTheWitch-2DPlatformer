using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class WitchScript : MonoBehaviour
{
    public Slider healthBar;
    private int health;
    private int phase;

    public Slider playerBar;
    public GameObject player;
    private PlayerCollision playerScript;
    private int playerHealth;

    public Animator witchAnimator;
    public ParticleSystem witchAttack;
    private float timer;

    private bool hasPhaseStarted;
    private bool isPhaseChanged;
    private GameObject portal;
    public GameObject monster;
    private int attackCount;
    private int isCountChanged;

    public Transform[] witchPositions;

    private bool isFacingLeft = true;

    void Start()
    {
        hasPhaseStarted = false;
        isPhaseChanged = false;
        portal = GameObject.FindGameObjectWithTag("Portal");
        portal.GetComponent<CircleCollider2D>().enabled = false;
        portal.SetActive(false);
        playerHealth = 100;
        playerScript = player.GetComponent<PlayerCollision>();
        playerBar.value = playerBar.maxValue = playerHealth;
        health = 100;
        healthBar.maxValue = health;
        healthBar.value = health;
        phase = 1;
        timer = 0f;
        attackCount = 0;
        isCountChanged = -1;
    }

    void Update()
    {
        if (health <= 0)
        {
            healthBar.gameObject.SetActive(false);
            playerBar.gameObject.SetActive(false);
            portal.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.SetActive(false);
        }

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(player.transform.position.x - transform.position.x > 0 && isFacingLeft)
        {
            Flip();
        }
        else if(player.transform.position.x - transform.position.x < 0 && isFacingLeft == false)
        {
            Flip();
        }

        if(health < 50 && isPhaseChanged == false)
        {
            phase = 2;
            hasPhaseStarted = true;
            isPhaseChanged = true;
        }

        if (playerScript.isPlayerHit == true)
        {
            playerHealth -= 20;
            playerBar.value = playerHealth;
            playerScript.isPlayerHit = false;
        }

        if(attackCount % 2 == 0 && isCountChanged < attackCount)
        {
            int pos = Random.Range(0, witchPositions.Length);
            isCountChanged = attackCount;
            transform.position = witchPositions[pos].position;
            hasPhaseStarted = true;
        }

        if (phase == 1)
        {
            if (timer <= 0)
            {
                witchAnimator.SetBool("isAttacking", true);
                Invoke("StopAttackAnimation", 1f);
                Invoke("StopParticles", 4f);
                timer = 5.5f;
                attackCount++;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else if(phase == 2)
        {
            if(timer <= 0)
            {
                witchAnimator.SetBool("isAttacking", true);
                Invoke("StopAttackAnimation", 1f);
                Invoke("StopParticles", 4f);
                timer = 5.5f;
                attackCount++;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
    }

    public void Attack()
    {
        if(phase == 1)
        {
            witchAttack.gameObject.SetActive(true);
        }
        else if(phase == 2 && hasPhaseStarted)
        {
            hasPhaseStarted = false;
            portal.SetActive(true);
            Instantiate(monster, portal.transform.position,Quaternion.identity);
        }
        else
        {
            witchAttack.gameObject.SetActive(true);
        }
    }

    public void StopParticles()
    {
        witchAttack.gameObject.SetActive(false);
    }

    public void StopAttackAnimation()
    {
        witchAnimator.SetBool("isAttacking", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAttackPoint"))
        {
            health -= Random.Range(5, 8);
            healthBar.value = health;
        }
    }
}
