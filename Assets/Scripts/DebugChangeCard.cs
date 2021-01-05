using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChangeCard : MonoBehaviour
{
    CardModel cardModel;
    CardFlipper Flipper;


    int cardIndex = 0;



    public GameObject card;



    private void Awake()
    {
        cardModel = card.GetComponent<CardModel>();
        Flipper = card.GetComponent<CardFlipper>();

    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,100,28),"Hit Me!"))
        {
            if (cardIndex >= cardModel.faces.Length)
            {
                cardIndex = 0;
                
              Flipper.FlipCard(cardModel.faces[cardModel.faces.Length - 1], cardModel.cardBack, -1);
            }
            else
            {

                if (cardIndex > 0)
                {
                    Flipper.FlipCard(cardModel.cardBack, cardModel.faces[cardIndex], cardIndex);
                }
                else
                {
                 
                    Flipper.FlipCard(cardModel.cardBack, cardModel.faces[cardIndex], cardIndex);
                }
                cardIndex++;
            }

           

        
        }
    }
}
