using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    Button playButton;
    Button quitButton;

    [SerializeField] private Animator transition; 

    // Start is called before the first frame update
    void Start()
    {
        playButton = GameObject.Find("Play Button").GetComponent<Button>();
        quitButton = GameObject.Find("Quit Button").GetComponent<Button>();
        playButton.onClick.AddListener(OnPlayButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    void OnPlayButtonClick()
    {
        StartCoroutine(loadLevel());
    }

    void OnQuitButtonClick()
    {
        Application.Quit();
    } 

    private IEnumerator loadLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("TestScene");
    }
}
