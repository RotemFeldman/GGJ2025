using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int counter;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void CheckWinningCondition()
    {
        counter++;
        if (counter == 3)
        {
            Debug.Log("You won");        
        }
    }
}
