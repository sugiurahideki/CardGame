﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<int> cards;


    public IEnumerable<int> Getcards()
    {
        foreach(int i in cards)
        {
         yield  return i;
        }
    }



    public void Shuffle()
    {
        if (cards == null)
        {
            cards = new List<int>();
        }
        else 
        {
            cards.Clear();

        }
        for(int i = 0;i<52; i++)
        {
            cards.Add(i);

        }
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
        
    }

   
}
