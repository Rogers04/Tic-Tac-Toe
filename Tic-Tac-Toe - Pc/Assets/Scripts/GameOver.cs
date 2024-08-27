using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject GameVictory;
    public GameObject Myplayers;
    public GameObject WinnerScore;
    public GameObject WinnerO;
    public GameObject WinnerX;
    public GameObject WinnerE;
    //public GameObject alert;

    bool gameOver;
    Player player;
    
    

    
    void Start()
    {
        
        player = FindObjectOfType<Player>();
        player.OnGameOver += GameOverOperations;
    }
    

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameOver = false;
            WinnerScreen(false);
            // Aqui voce pode querer chamar algumas funcoes quando o jogo terminar, como mostrar a tela de game over ou atualizar a pontuação.
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    void GameOverOperations()
    {
        gameOver = true;
        // Adicione aqui a logica para exibir a tela de game over ou atualizar a pontuacao
        WinnerScreen(true);
        WinnerScoreController();
    }

    void WinnerScreen(bool isActive)
    {
       
        
        WinnerScore.SetActive(!isActive);
        GameVictory.SetActive(isActive);
        WinnerX.SetActive(false);
        WinnerO.SetActive(false);
        WinnerE.SetActive(false);
        

        if (player.Winner == 'X') // Verifique se a classe Player tem uma propriedade Winner
        {
            StartCoroutine(pX());
        }
        else if (player.Winner == 'O') // Verifique se a classe Player tem uma propriedade Winner
        {
          StartCoroutine(pO());
        }
        else 
        {
            StartCoroutine(pE());
        }
    }


    public IEnumerator pX ()
    {
        yield return new WaitForSeconds(1f);
        WinnerX.SetActive(true);
        alertaTime();
        StartCoroutine(Delay());
    }

    public IEnumerator pO ()
    {
        yield return new WaitForSeconds(1f);
        WinnerO.SetActive(true);
        alertaTime();
        StartCoroutine(Delay());
    }

    public IEnumerator pE ()
    {
        yield return new WaitForSeconds(1f);
        WinnerE.SetActive(true);
        alertaTime();
        StartCoroutine(Delay());
    }


    void alertaTime()
    {
        player.alert.text = "";
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        player.texto = false;
        
        WinnerX.SetActive(false);
        WinnerO.SetActive(false);
        WinnerE.SetActive(false);
        //alert.SetActive(true);
        

    }

   

    void WinnerScoreController()
    {
        // Logica para controlar a pontuacao do vencedor
    }
}
