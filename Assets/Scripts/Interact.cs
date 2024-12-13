using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject painting;
    [SerializeField] private GameObject handIK;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject dialogueBox;

    private bool inRange = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    } 

    public void ActivatePainting()
    {
        if (inRange)
        {
            painting.SetActive(false);
            model.SetActive(true);
            dialogueBox.SetActive(true);

            handIK.GetComponent<FastIKFabric>().enabled = true;
        }
    }
}
