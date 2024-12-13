using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DitzelGames.FastIK;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public GameObject painting; 
    public GameObject model; 
    public GameObject handIK;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        GetComponent<AudioSource>().Play();

        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        GetComponent<AudioSource>().Stop();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            GetComponent<AudioSource>().Play();
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);

            painting.SetActive(true);
            model.SetActive(false);
            handIK.GetComponent<FastIKFabric>().enabled = false;
        }
    }
}
