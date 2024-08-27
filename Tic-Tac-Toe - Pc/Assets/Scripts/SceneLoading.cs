using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;     // caso de errado nessa linha, procure ver se voce importou o DOTween no seu projeto;

public class SceneLoading : MonoBehaviour
{
    [Header("Porcentagem")]
        public Slider sloading;
        public Text Porcentagem;
        public Text alert;

        private float UpInterval = 4.0f;
        private float timer = 1.0f;
        private bool menssage = true;

    void Awake()
    {
        timer = UpInterval;
    }
   
    void Start()
    {
        
        StartCoroutine(LoadScene());
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            AtualizationText();
            timer = UpInterval;
        }

    }

    private void AtualizationText()
    {
        if(menssage)
        {
            alert.text = "Os jogadores jogarão apenas 3 rounds.";
        }
        else 
        {
            alert.text = "Quem sempre inicia é o Player X.";
        }
        
        menssage = !menssage;
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
        operation.allowSceneActivation = false;

        int progresso = 0;

        while (progresso < 100)
        {
          // Adiciona um valor aleatorio ao progresso
            progresso += Random.Range(1,13);
            progresso = Mathf.Clamp(progresso, 0, 100);



        // Atualiza o valor do slider e do texto da porcentagem

        sloading.DOValue(progresso /100f, 0.6f);
        Porcentagem.text = progresso + "%";

        yield return new WaitForSeconds(1f);

        }

        // Permite a ativacao da cena quando a barra de progresso atinge 100
        operation.allowSceneActivation = true;

        sloading.value = 1;
        Porcentagem.text = "100%";
    }
}
