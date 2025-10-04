using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Coin : MonoBehaviour
{
    public string type;
    private Animator animator;
    private int random;
    private GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
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
                if (hitCollider != null && hitCollider.gameObject == gameObject)
                {
                     random = UnityEngine.Random.Range(0, 2);
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

    public void GetAposta(int aposta)
    {
        //apostado = aposta;
    }

    private void OnAnimationComplete()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        gameManager.GetResultado(random);
    }
}



