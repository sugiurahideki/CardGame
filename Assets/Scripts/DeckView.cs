using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Deck) )]
public class DeckView : MonoBehaviour
{
    Deck deck;

    public Vector3 start;
    public float cardoffset;
    public GameObject cardPrefab;

    private void Start()
    {
        deck = GetComponent<Deck>();
        ShowCards();
    }

    void ShowCards()
    {
        int cardCout = 0;

        foreach(int i in deck.Getcards())
        {
            float co = cardoffset * cardCout;


            GameObject cardcopy = (GameObject)Instantiate(cardPrefab);
            Vector3 temp = start + new Vector3(co, 0f);
            cardcopy.transform.position =temp;

            CardModel cardModel = cardcopy.GetComponent<CardModel>();
            cardModel.cardIndex = i;
            cardModel.ToggleFace(true);






            cardCout++;

        }

    }
}
