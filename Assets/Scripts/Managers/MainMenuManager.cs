using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject intermediateLock;
    public GameObject intermediateText;
    public GameObject advancedLock;
    public GameObject advancedText;

    private void Start()
    {

        for (int i =0; i < GameManager.Instance.gameData.isLevelLock.Length; i++) 
        {
            switch (i)
            {
                case 0:
                    if (GameManager.Instance.gameData.isLevelLock[i] == false)
                    {
                        intermediateLock.SetActive(true);
                        intermediateText.SetActive(false);
                    }
                    else
                    {
                        intermediateLock.SetActive(false);
                        intermediateText.SetActive(false);
                    }
                    break;

                case 1:
                    if (GameManager.Instance.gameData.isLevelLock[i] == false)
                    {
                        advancedLock.SetActive(true);
                        advancedText.SetActive(false);
                    }
                    else
                    {
                        advancedText.SetActive(false);
                        advancedLock.SetActive(false);
                    }
                    break;
            }
        }

    }
    
    public void loadTutorial()
    {
        StartCoroutine(loadTutorialDelay());
    }

    public void StartSettingsMenu()
    {
        StartCoroutine(loadSettings());
    }

    public void LoadLeaderboard()
    {
        StartCoroutine(loadLeader());
    }

     
    public void SetBeginerLevel()
    {
        GameManager.Instance.setDiffuclty(LevelDifficulty.Beginner);
        //SaveSystem.saveGame(GameManager.Instance.gameData);
    }

    public void SetIntermediateLevel()
    {
        GameManager.Instance.setDiffuclty(LevelDifficulty.Intermediate);
        //SaveSystem.saveGame(GameManager.Instance.gameData);
    }

    public void SetAdvancedLevel()
    {
        GameManager.Instance.setDiffuclty(LevelDifficulty.Advanced);
    }

    IEnumerator loadSettings()
    {
        switch (GameManager.Instance.gameData.levelDifficulty)
        {
            case LevelDifficulty.Beginner:
                yield return new WaitForSeconds(0.5f);
                SceneManager.LoadScene("02_LevelSettingsMenu");
                break;

            case LevelDifficulty.Intermediate:
                if (GameManager.Instance.gameData.isLevelLock[0] == true)
                {
                    yield return new WaitForSeconds(0.5f);
                    intermediateLock.SetActive(false);
                    SceneManager.LoadScene("02_LevelSettingsMenu");
                }
                else
                {
                    //Debug.Log("Please complete beginner first");
                    advancedText.SetActive(false);
                    intermediateLock.SetActive(true);
                    intermediateText.SetActive(true);
                    StartCoroutine(textFlashDelay(intermediateText));
                }
                break;

            case LevelDifficulty.Advanced:
                if (GameManager.Instance.gameData.isLevelLock[1] == true)
                {
                    yield return new WaitForSeconds(0.5f);
                    advancedLock.SetActive(false);
                    SceneManager.LoadScene("02_LevelSettingsMenu");
                }
                else
                {
                    //Debug.Log("Please complete Intermediate first");
                    intermediateText.SetActive(false);
                    advancedLock.SetActive(true);
                    advancedText.SetActive(true);
                    StartCoroutine(textFlashDelay(advancedText));
                }
                break;


        }
        
    }

    IEnumerator loadLeader()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("03_LeaderboardMenu");
    }

    IEnumerator loadTutorialDelay()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("02_TutorialScreen");
    }

    IEnumerator textFlashDelay(GameObject text)
    {
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
