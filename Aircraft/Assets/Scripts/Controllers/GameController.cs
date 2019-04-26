using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int[] score = new int[2];
    public Airship Airship;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addScore(int index)
    {
        score[index]++;
        UpdateScore();
    }
    void UpdateScore()
    {
        if (Airship != null)
            Airship.setCount(score[0], score[1]);
    }
}
