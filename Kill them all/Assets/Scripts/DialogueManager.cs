using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public string[] dialogue;
    public Text dialogueBox;
    private int index;
    public GameObject continueButton;
    public GameObject skipTutorial;
    public GameObject TouchInput;
    public Transform player;
    public GameObject DialogueCanvas;
    private Vector3 initialCameraPosition;
    private 

    void Start()
    {
        initialCameraPosition = Camera.main.transform.position;
        Camera.main.gameObject.GetComponent<CameraFollow>().enabled = false;
        if(PlayerPrefs.GetInt("isSkipped",0) == 0)
        {
            StartCoroutine(Type());
        }
        else if(PlayerPrefs.GetInt("isSkipped",0) == 1)
        {
            StopAllCoroutines();
            dialogueBox.text = "";
            DialogueCanvas.SetActive(false);
            Camera.main.transform.position = initialCameraPosition;
            Camera.main.gameObject.GetComponent<CameraFollow>().enabled = true;
            TouchInput.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if(dialogueBox.text == dialogue[index])
        {
            continueButton.SetActive(true);
            skipTutorial.SetActive(true);
        }

        if(index == 10)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(18.65f, 5.18f,initialCameraPosition.z),1f);
        }
        if (index == 11)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(10f, 18f, initialCameraPosition.z), 1f);
        }
        if (index == 14)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(35f, 24f, initialCameraPosition.z), 1f);
        }
        if (index == 15)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(25.7f, 32.4f, initialCameraPosition.z), 1f);
        }
    }

    IEnumerator Type()
    {
        dialogueBox.text = "";
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueBox.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void NextDialogue()
    {
        continueButton.SetActive(false);
        skipTutorial.SetActive(false);

        if(index < dialogue.Length - 1)
        {
            index++;
            StartCoroutine(Type());
        }
        else
        {
            StopAllCoroutines();
            dialogueBox.text = "";
            DialogueCanvas.SetActive(false);
            Camera.main.transform.position = initialCameraPosition;
            Camera.main.gameObject.GetComponent<CameraFollow>().enabled = true;
            TouchInput.SetActive(true);
        }
    }

    public void SkipTutorial()
    {
        index = 15;
        PlayerPrefs.SetInt("isSkipped", 1);
    }
}
