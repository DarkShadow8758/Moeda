using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int coins = 1;
    public float score;
    private int choice;
    private int round = 0;
    private int currentCoin = 0;
    private int totalRounds = 0;
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI txtCoins;
    [SerializeField] private TextMeshProUGUI txtPoints;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [Header("Interactables")]
    [SerializeField] private GameObject choiceHead;
    [SerializeField] private GameObject choiceTail;
    private GameObject coinInst;
    [SerializeField] private GameObject[] prefabMoeda;
    [Header("Interfaces")]
    [SerializeField] private GameObject scoreInterface;
    [SerializeField] private GameObject shopInterface;
    [SerializeField] private GameObject betInterface;
    [SerializeField] private GameObject characterInterface;


    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BetMode();
    }
    void Update()
    {
        txtCoins.text = coins.ToString();
        txtPoints.text = score.ToString();

        if (coinInst != null)
        {
            Coin coinScript = coinInst.GetComponent<Coin>();
            if (coinScript != null)
            {
                txtDescription.text = coinScript.GetDescription();
            }
        }
        else
        {
            txtDescription.text = "";
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                if (hitCollider != null)
                {
                    if (hitCollider.gameObject == choiceHead)
                    {
                        choice = 0;
                        Apostou();
                    }
                    else if (hitCollider.gameObject == choiceTail)
                    {
                        choice = 1;
                        Apostou();
                    }
                    betInterface.SetActive(false);
                }
            }
        }

    }

    private void Apostou()
    {
        if (prefabMoeda != null)
        {
            coinInst = Instantiate(prefabMoeda[currentCoin], Vector3.zero, Quaternion.identity);
        }
    }

    public void GetResultado(int resultado, float multiplicador)
    {
        if (resultado != choice)
        {
            SubtractCoin(1);
            currentCoin -= 1;
            Debug.Log("Você errouuu! resultado " + resultado + " aposta " + choice);
        }
        else
        {
            Debug.Log("Você ACERTOU! resultado " + resultado + " aposta " + choice);

            GainCoin(prefabMoeda[currentCoin]);
            score = score + (100f * multiplicador);

        }
        if (coins <= 0)
        {
            GameOver();
        }
        else
        {
            ProximaAposta();
        }
    }
    private void ProximaAposta()
    {
        if (round < totalRounds)
        {
            betInterface.SetActive(true);
            round++;
            currentCoin++;
        }
        else
        {
            Debug.Log("Vamos a loja!");
            ShopMode();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(2);
    }
    public void ShopMode()
    {
        scoreInterface.SetActive(false);
        shopInterface.SetActive(true);
    }
    public void BetMode()
    {
        scoreInterface.SetActive(true);
        betInterface.SetActive(true);
        shopInterface.SetActive(false);
        totalRounds = prefabMoeda.Length - 1;
        round = 0;
        currentCoin = 0;
    }

    public void GainCoin(GameObject newCoin)
    {

        var listCoins = new System.Collections.Generic.List<GameObject>(prefabMoeda);
        listCoins.Add(newCoin);
        prefabMoeda = listCoins.ToArray();
        coins++;
        characterInterface.GetComponent<Character>().Animate("Like");

    }
    public void SubtractCoin(int discount)
    {
        Debug.Log("dsicount:" + discount);
        for (int i = 0; i < discount; i++)
        {
            var listCoins = new System.Collections.Generic.List<GameObject>(prefabMoeda);
            listCoins.RemoveAt(currentCoin);
            Debug.Log("current:" + currentCoin + " i " + i);
            prefabMoeda = listCoins.ToArray();
        }
        coins -= discount;
        characterInterface.GetComponent<Character>().Animate("Damage");
    }

    public void CharacterLike()
    {
        characterInterface.GetComponent<Character>().Animate("Like");
    }
}
