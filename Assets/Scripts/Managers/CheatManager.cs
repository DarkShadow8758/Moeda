using UnityEngine;

public class CheatManager : MonoBehaviour
{
public static CheatManager Instance { get; private set; }

    [Header("Ativar cheats apenas no modo debug")]
    public bool enableCheatsInBuild = false;

    private bool cheatsEnabled;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

#if UNITY_EDITOR
        cheatsEnabled = true;
#else
        cheatsEnabled = enableCheatsInBuild;
#endif
    }

    void Update()
    {
        if (!cheatsEnabled) return;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            GivePlayerCoins(999);
        }
            

        if (Input.touchCount > 99)
        {
            SpawnParticle("ConfettiLeft");
            SpawnParticle("ConfettiRight");
        }
            

    }

    void GivePlayerCoins(int value)
    {
        var gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.coins = value;
            Debug.Log($"[CHEAT] Coins do jogador setada para {value}");
        }
    }

    void SpawnParticle(string ps_name)
    {
        var pm = FindObjectOfType<ParticleManager>();
        {
            pm.SpawnParticle(ps_name);
            Debug.Log($"[CHEAT] Particula criada {ps_name}");
        }
    }
}
