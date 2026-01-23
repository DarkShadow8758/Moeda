using UnityEngine;

public class RandomManager : MonoBehaviour
{
    public int FlipCoin(int chanceHead)
    {
        int roll = Random.Range(0, 100);
        return roll < chanceHead ? 0 : 1;
    }
}
