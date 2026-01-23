using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class Coin : MonoBehaviour
{
    [Header("Coin Attributes")]
    [SerializeField] private string descriptionCoin;
    [SerializeField] private string nameCoin;
    [SerializeField] private int costCoin;
    [Range(0, 10)]
    [SerializeField] private float multScore;
    [Range(0, 100)]
    [SerializeField]private int chanceHead = 50;

    private Animator animator;
    private int random;
    private GameManager gameManager;
    private RandomManager randomManager;
    private float spd = 5f;
    private bool anim = false;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
        if (randomManager == null)
        {
            randomManager = FindObjectOfType<RandomManager>();
        }
    }

    void Update()
    {
        if (anim)
        {
            transform.position = Vector3.MoveTowards(transform.position, gameManager.coinUI.transform.position, spd * Time.deltaTime); 
            transform.localScale = Vector3.Lerp(transform.localScale, gameManager.coinUI.transform.localScale, spd/2 * Time.deltaTime);
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);
                if (hitCollider != null && hitCollider.gameObject == gameObject)
                {
                    random = randomManager != null ? randomManager.FlipCoin(chanceHead) : Random.Range(0, 2);
                    switch (random)
                    {
                        case 0:
                            animator.SetTrigger("Cara");
                            break;
                        case 1:
                            animator.SetTrigger("Coroa");
                            break;
                    }
                    if (gameManager != null)
                    {
                        OnAnimationComplete();
                    }
                }
            }
        }   
    }

    public string GetDescription()
    {
        return descriptionCoin;
    }
    public string GetName()
    {
        return nameCoin;
    }
    public int GetCost()
    {
        return costCoin;
    }

    private void OnAnimationComplete()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    { 
        yield return new WaitForSeconds(5f);
        if (gameManager.GetResult(random, multScore))
        {
            anim = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}



