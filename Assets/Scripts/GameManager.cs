using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int counter;
    private void Awake()
    {
        Instance = this;
    }

    public void CheckWinningCondition()
    {
        counter++;
        if (counter == 1)
        {
            Debug.Log("You won");        
        }
    }
}
