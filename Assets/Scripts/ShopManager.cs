using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private GameManager gameManager;
    [Header("Interactable")]
    [SerializeField] private GameObject skip;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);
                if (hitCollider != null && hitCollider.gameObject == skip)
                {
                    if (gameManager != null)
                    {
                        gameManager.BetMode();
                        Debug.Log("bet mode");
                    }
                }
            }
        }   
    }

    public void BuyCoin(GameObject coin)
    {
        if (gameManager == null)
        {
            Debug.Log("gamemanager"); return;
        }
        Debug.Log("coin card" + coin);

        Coin coinScript = coin.GetComponent<Coin>();
        if (coinScript == null) return;
        Debug.Log("coinscript");
        int cost = coinScript.GetCost();

        if (gameManager.coins >= cost)
        {
            gameManager.GainCoin(coin);
            gameManager.SubtractCoin(cost);

            Debug.Log("Moeda comprada!");
            gameManager.CharacterLike();
            gameManager.BetMode();
        }
        else
        {
            Debug.Log("Moedas insuficientes!");
        }
    }
}
