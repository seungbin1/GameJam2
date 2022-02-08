using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Playing,
        Stop,
        GameOver
    }

    public GameState gameState;

    [HideInInspector]
    private int score;


    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void GetScore(int score)
    {
        this.score = this.score + score;
    }

    public int SetScore()
    {
        return score;
    }


}
