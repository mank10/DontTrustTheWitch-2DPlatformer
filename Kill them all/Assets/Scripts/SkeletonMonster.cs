using UnityEngine;

public class SkeletonMonster : MonoBehaviour
{
    public GameObject Cage;
    public GameObject helpMeText;
    private Animator cageAnimator;
    private BoxCollider2D skeletonCollider;
    private bool isSaved;
    private AudioManager audioManager;
    private int savedCount;

    void Start()
    {
        cageAnimator = GetComponent<Animator>();
        skeletonCollider = GetComponent<BoxCollider2D>();
        isSaved = false;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if(Cage.activeInHierarchy == false && isSaved == false)
        {
            audioManager.Play("SwordMetal");
            isSaved = !isSaved;
            cageAnimator.SetBool("isDestroyed", true);
            skeletonCollider.enabled = !skeletonCollider.enabled;
            helpMeText.SetActive(false);
            gameObject.GetComponent<AudioSource>().Stop();
            savedCount = PlayerPrefs.GetInt("SkeletonSaved", 0);
            savedCount++;
            PlayerPrefs.SetInt("SkeletonSaved", savedCount);
        }
    }
}
