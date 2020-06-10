using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement playerMoveScript;
    private DoubleJump playerJumpScript;
    public bool isPlayerHit = false;

    private Animator player;

    public GameObject monsterDeath;
    private Animator monsterDeathAnimation;

    private AudioManager audioManager;
    private int levelsCompleted;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerMoveScript = gameObject.GetComponent<PlayerMovement>();
        playerJumpScript = gameObject.GetComponent<DoubleJump>();
        player = GetComponent<Animator>();
        monsterDeathAnimation = monsterDeath.GetComponent<Animator>();
        monsterDeath.SetActive(false);
    }

    private void OnParticleCollision()
    {
        player.SetBool("isPoisoned", true);
        isPlayerHit = true;
        Invoke("StopHurtAnimation", 0.4f);
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Portal"))
        {
            audioManager.Play("MagicalDisappearance");
            levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 1);
            levelsCompleted++;
            PlayerPrefs.SetInt("LevelsCompleted", levelsCompleted);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MonsterAttack")
        {
            playerMoveScript.isPoisoned = true;
            playerMoveScript.poisonStartTime = 5f;
            player.SetBool("isPoisoned", true);
            Invoke("StopHurtAnimation", 0.5f);
        }
        else if (collision.gameObject.tag == "LevelBoundary")
        {
            Death();
        }
        if (collision.gameObject.tag == "Cage" && collision.IsTouching(this.transform.Find("PlayerAttackPoint").GetComponent<PolygonCollider2D>()))
        { 
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.SetActive(false);
            monsterDeath.SetActive(true);
            monsterDeath.transform.position = collision.transform.position;
            monsterDeathAnimation.Play("Monster_Death");
            Invoke("StopMonsterDeathAnimation", 1f);
        }
        
    }

    void StopMonsterDeathAnimation()
    {
        monsterDeath.SetActive(false);
    }

    void StopHurtAnimation()
    {
        player.SetBool("isPoisoned", false);
    }

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
