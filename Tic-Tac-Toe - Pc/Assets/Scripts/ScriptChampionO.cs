using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ganhador : MonoBehaviour
{
    [Header("Meus Objetos")]
    public GameObject O;
    public Text txt1;
    public Text txt2;

    private Player play;

    [SerializeField]
    private Button resetar;

    private void Awake()
    {
        play = FindObjectOfType<Player>();
        resetar.onClick.AddListener(btn);
    }

    void Start()
    {
        O.SetActive(true);
        txt1.text = "MEUS PARABÉNS, JOGADOR O!";
        txt2.text = "Apresento a vocês o maior campeão de todos!!!";
        
    }



    private void btn()
    {
        if (play != null)
        {
            play.resetPontos();
            play.ResetDeparture();
        }

        AsyncOperation teste = SceneManager.LoadSceneAsync("ChoosePlayer");
    }

    
}
