using System;
using UnityEngine;

public class PlayCoin : MonoBehaviour
{
    void Start()
    {
        var random = UnityEngine.Random.Range(0, 2);
        Debug.Log(random);
    }
}
