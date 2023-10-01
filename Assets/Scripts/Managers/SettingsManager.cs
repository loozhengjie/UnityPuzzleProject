using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private GameManager gameManager;
    public TMP_InputField inputField;
    public TextMeshProUGUI nameInput;
    public TMP_Dropdown themeSelection;
    public GameObject warningText;
     
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        warningText.SetActive(false);
    }


   public void SaveSettings()
   {
        if (string.IsNullOrWhiteSpace(inputField.text)) 
        { 
            warningText.SetActive(true);
            StartCoroutine(warningdelay());
        
        }
        else
        {
           gameManager.gameData.playerName= nameInput.text;
           StartCoroutine(loadScene());
        }

       

    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuDelay());
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("02_MainLevel");
    }

    IEnumerator warningdelay()
    {
        yield return new WaitForSeconds(2f);
        warningText.SetActive(false);
    }

    IEnumerator MainMenuDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("01_MainMenu");
    }
}
