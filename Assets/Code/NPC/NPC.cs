using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    void Update()
    {
        // When closed to the NPC, press E to trigger dialogue
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < 3f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueTrigger.TriggerDialogue();
            }
        }
    }


}
