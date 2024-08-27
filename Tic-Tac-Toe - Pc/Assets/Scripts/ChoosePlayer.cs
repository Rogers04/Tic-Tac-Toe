using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoosePlayer : MonoBehaviour
{
    [Header("variaveis")]
    bool jogador1 = false;
    bool jogador2 = false;
    public Text txtobs;
    public float Delay = 3f;


    [SerializeField] private Button Player1;
    [SerializeField] private Button Player2;


    private void Awake()
    {
        Player1.onClick.AddListener(btn1);
        Player2.onClick.AddListener(btn2);
    }


    private void btn1()
    {
        jogador1 = true;
        jogador2 = false;
        txtobs.text = "Você selecionou o Player O!";
        StartCoroutine(SceneDelay());
    }

    private void btn2()
    {
        jogador2 = true;
        jogador1 = false;
        txtobs.text = "Você selecionou o Player X!";
        StartCoroutine(SceneDelay());
    }

    public IEnumerator SceneDelay()
    {
        yield return new WaitForSeconds(Delay);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Loading");
        operation.allowSceneActivation = true;
    }







}
