using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coins = 1;
    public int aposta;
    [SerializeField] private TextMeshProUGUI txtCoins;

    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject prefabToInstantiate;

    void Update()
    {
        txtCoins.text = coins.ToString();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                if (hitCollider != null)
                {
                    if (hitCollider.gameObject == object1)
                    {
                        aposta = 0;
                        Apostou();
                    }
                    else if (hitCollider.gameObject == object2)
                    {
                        aposta = 1;
                        Apostou();
                    }
                    Debug.Log("aposta: " + aposta);
                }
            }
        }
        if (coins <= 0)
        {
            GameOver();
        }
    }

    private void Apostou()
    {
        if (prefabToInstantiate != null)
        {
            //GameObject obj =
            Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);
            // // Exemplo: supondo que o prefab tenha um script chamado "MeuScript"
            // Coin script = obj.GetComponent<Coin>();
            // if (script != null)
            // {
            //     script.GetAposta(aposta);
            // }
        }
    }

    public void GetResultado(int resultado)
    {
        Debug.Log("Resultado da moeda: " + resultado);
        if (resultado != aposta)
        {
            Debug.Log("Você errouuu! resultado " + resultado + " aposta " + aposta);
            coins--;
        }
        else
        {
            Debug.Log("Você ACERTOU! resultado " + resultado + " aposta " + aposta);
            coins++;
        }

    }

    private void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
