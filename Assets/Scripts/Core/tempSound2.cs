using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempSound2 : MonoBehaviour
{

    public void loadMenuFunction()
    {

        StartCoroutine(loadMenu());
    }


    IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("01_MainMenu");
    }
}
