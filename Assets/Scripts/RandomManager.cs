using UnityEngine;

public class RandomManager : MonoBehaviour
{
    public int GetRandom(int chanceZero)
    {
        int roll = Random.Range(0, 100);
        return roll < chanceZero ? 0 : 1;
    }
}
