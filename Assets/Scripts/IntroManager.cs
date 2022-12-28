using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject dialogueTrigger;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(DelayedDialogue());
    }

    // Update is called once per frame  
    void Update()
    {
            
    }

    IEnumerator DelayedDialogue()
    {
        yield return null;
        dialogueTrigger.GetComponent<DialogueTrigger>().TriggerDialogue();
        Debug.Log("Dialogue Start!");
    }
}
