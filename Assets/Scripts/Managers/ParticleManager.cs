using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [System.Serializable]
    public class ParticleEntry
    {
        public string name;
        public GameObject prefab;
    }

    [Header("Lista de partículas registradas")]
    public List<ParticleEntry> particles = new List<ParticleEntry>();

    private Dictionary<string, GameObject> particleDict;
    float timeToDestroy = 5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        particleDict = new Dictionary<string, GameObject>();
        foreach (var entry in particles)
        {
            if (!particleDict.ContainsKey(entry.name))
                particleDict.Add(entry.name, entry.prefab);
        }
    }

    public void SpawnParticle(string name)
    {
        if (!particleDict.ContainsKey(name))
        {
            Debug.LogWarning($"Partícula '{name}' não encontrada!");
            return;
        }

        GameObject prefab = particleDict[name];
        Quaternion spawnRotation = prefab.transform.rotation;

        GameObject go = Instantiate(prefab, prefab.transform.position, spawnRotation);

        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            Destroy(go, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(go, timeToDestroy);
        }
    }
}
