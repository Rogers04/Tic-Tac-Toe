using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public void LoadScene(string cena)
    {
        //SceneManager.LoadScene(cena);
        AsyncOperation operation = SceneManager.LoadSceneAsync("ChoosePlayer");
    }
}
