using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public event Action OnGameOver;

    [Header("My point")]
    public Text XScore; 
    public int _xpontos = 0;
    public Text OScore; 
    public int _opontos = 0;
    public Text alert;
    public bool texto = false;
   
    

    public int Xpontos // sistema de atualizacao dos pontos do X
    {
        get {return _xpontos; }
        set
        {
            _xpontos = value;
            XScore.text = _xpontos.ToString();
        }
    }

    public int Opontos // sistema de atualizacao dos pontos do O
    {
        get {return _opontos; }
        set 
        {
            _opontos = value;
            OScore.text = _opontos.ToString();
        }
    }

    [Header("Homes")]
    public GameObject[] input;
    public int matrixSize;
    public LayerMask mask;

    [Header("Players")]
    public XorO_Piece X;
    public XorO_Piece O;

    public int turnsCounter;
    public XorO_Piece[,] XO_Pieces;
    public char Winner;
    public float delay = 4f;


    Ganhador ganhador;

    
    //public bool xxs = false;
    //public bool oos = false;
     
   

    void Awake()
    {
       
         XScore.text = Xpontos.ToString();
         OScore.text = Opontos.ToString();
        

        GameObject obj = GameObject.Find("Ganhador");
        if(obj != null)
        {
            ganhador = obj.GetComponent<Ganhador>();
            if(ganhador != null)
            {
                //Debug.Log("valor de variavel X"+ ganhador);
            }
        }
        //Ganhador ganhador = obj.GetComponent<Ganhador>();
        //bool valorX = ganhador.xis;
        
            
    }
    void Start() // Void Start e chamado no inicio do jogo
    {
         
        XO_Pieces = new XorO_Piece[matrixSize, matrixSize];
        //ganhador.xis = xxs;
        //ganhador.ois = oos;
    }


    void Update() // Update e para verificar o clique do mouse a cada frame
    {
        XScore.text = Xpontos.ToString();
        OScore.text = Opontos.ToString();
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            InstantiateXOs(); 
            if (GameIsOver() && OnGameOver != null)
            {
                OnGameOver();
            }
        }    
        Cham();
    }

    public void InstantiateXOs() // Este metodo percorre toda a matriz do tabuleiro e chama RaycastOperations para cada celula, passando os indices da matriz e um contador.
    {
        int count = 0;
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                RaycastOperations(i, j, count);
                count++;
            }
        }
    }

    
    public void ResetDeparture() // Resetar o tabuleiro
    {
        
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                if (XO_Pieces[i, j] != null)
                {
                    Destroy(XO_Pieces[i, j].gameObject);
                    
                }
            }
        }


        turnsCounter = 0;
        Winner = 'N';
        
        
    }

    public void resetPontos() // Resetar os pontos
    {
        _opontos = 0;
        _xpontos = 0;
    }

    public void txtPlayers() // Texto dos pontos
    {
        
            if (texto == true)
            {
                alert.text = "Vez do jogador O !!";
            }        
            else if ( texto == false)
            {
                alert.text = "Vez do jogador X !!";
            }
        
    }

    public void RaycastOperations(int i, int j, int count) // Este metodo realiza operacoes de Raycast para determinar onde a peca deve ser colocada
    {
        if (Physics.Raycast(new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward), out RaycastHit hitinfo, 100f) && hitinfo.collider.gameObject == input[count])
        {
            
            if (XO_Pieces[i, j] == null)
            {
                if (turnsCounter % 2 == 0)
                {
                    XorO_Piece p = Instantiate(X, input[count].transform.position, Quaternion.identity);
                    XO_Pieces[i, j] = p;
                    texto = true;
                    //txtPlayers();
                    //print("Vez do jogador O");
                }
                else
                {
                    XorO_Piece p = Instantiate(O, input[count].transform.position, Quaternion.identity);
                    XO_Pieces[i, j] = p;
                    texto = false;
                    //print("vez do jogador X");
                    //txtPlayers();
                }
                txtPlayers();
                //print(texto ? "Vez do jogador O":"Vez do jogador X");
                WhoWins(XO_Pieces[i, j].XorO, new Vector2Int(i, j));
                turnsCounter++;
            }
        }
    }

    public void WhoWins(char xorO, Vector2Int position) // WhoWins verifica se ha um vencedor
    {
        int sum;

        sum = 0;

        for (int i = 0; i < matrixSize; i++)
        {
            XorO_Piece analysePiece = XO_Pieces[i, position.y];
            if (analysePiece != null && analysePiece.XorO == xorO)
            {
                sum += 1;
                
            }
        }

        if (sum == matrixSize)
        {
            Winner = xorO;
            return;
            
        }

        sum = 0;

        for (int i = 0; i < matrixSize; i++)
        {
            XorO_Piece analysePiece = XO_Pieces[position.x, i];
            if (analysePiece != null && analysePiece.XorO == xorO)
            {
                sum += 1;
                
            }
        }

        if (sum == matrixSize)
        {
            Winner = xorO;
            return;
            
        }

        sum = 0;

        for (int i = 0; i < matrixSize; i++)
        {
            XorO_Piece analysePiece = XO_Pieces[i, i];
            if (analysePiece != null && analysePiece.XorO == xorO)
            {
                sum += 1;
               
            }
        }

        if (sum == matrixSize)
        {
            Winner = xorO;
            return;
            
        }

        sum = 0;

        for (int i = 0; i < matrixSize; i++)
        {
            XorO_Piece analysePiece = XO_Pieces[i, (matrixSize - 1) - i];
            if (analysePiece != null && analysePiece.XorO == xorO)
            {
                sum += 1;
                
            }
        }

        if (sum == matrixSize)
        {
            Winner = xorO;
            return;
            
        }
    }

    public void vencedor () // Comparacao dos vencedores
    {
        
        
        if (Winner == 'X')
        {
            
            _xpontos += 1;
            print("Valor do X: " +_xpontos);
            

            
        }
        else if (Winner == 'O')
        {
            
            _opontos += 1;
            print("Valor do O:" +_opontos);
        }

        
        Cham();
    }

    void Cham () // Verificacao do grande campeao
    {
        
        if (_xpontos >= 3  )
        {    
            StartCoroutine(ScenePlayerX());
        }
        else if (_opontos >= 3)
        {
            
            StartCoroutine(ScenePlayerO());
        }
    }

    public bool GameIsOver() // GameIsOver verifica se o jogo terminou
    {
        int matrixSizeSquared = Mathf.RoundToInt(Mathf.Pow(matrixSize, 2));

        int count = 0;
        foreach (XorO_Piece p in XO_Pieces)
        {
            if (p != null)
            {
                count++;
            }
        }

        if (count == matrixSizeSquared || Winner == 'X' || Winner == 'O')
        {
            vencedor();
            StartCoroutine(DelayedReset());
            
        
            return true;
            
        }
        return false;
    }

    private IEnumerator DelayedReset() // Utilizo para fazer o jogo dar uma esperada de 2 segundos antes de resetar o tabuleiro
    {
        
        yield return new WaitForSeconds(2f);
        ResetDeparture();
    }

    
    public IEnumerator ScenePlayerO(/*bool xisVenceu*/) // Proxima cena e resetar os pontos 
    {
        yield return new WaitForSeconds(delay);
        AsyncOperation operation = SceneManager.LoadSceneAsync("ChampionO");
        yield return operation;

    }
    public IEnumerator ScenePlayerX(/*bool xisVenceu*/) // Proxima cena e resetar os pontos 
    {
        yield return new WaitForSeconds(delay);
        AsyncOperation operation = SceneManager.LoadSceneAsync("ChampionX");
        yield return operation;

    }
}