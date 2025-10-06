using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI txtScore;
    void Start()
    {
        txtScore.text = GameManager.Instance.score.ToString();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene(0);
            }
        }         
    }
}
