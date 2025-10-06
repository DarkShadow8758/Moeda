using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class CoinCard : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtCost;
    private ShopManager shopManager;
    private int cost;
    public GameObject coinPrefab;
    [Header("Interactable")]
    [SerializeField] private GameObject buy;


    void Start()
    {
        if (shopManager == null)
        {
            shopManager = FindObjectOfType<ShopManager>();
        }
        txtDescription.text = coinPrefab.GetComponent<Coin>().GetDescription();
        txtName.text = coinPrefab.GetComponent<Coin>().GetName();
        cost = coinPrefab.GetComponent<Coin>().GetCost();
        txtCost.text = "Custo " + cost.ToString();

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
                if (hitCollider != null && hitCollider.gameObject == buy)
                {
                    if (shopManager != null)
                    {
                        Debug.Log("comprou");
                        shopManager.BuyCoin(coinPrefab);
                    }
                }
            }
        }   
    }
    
}



