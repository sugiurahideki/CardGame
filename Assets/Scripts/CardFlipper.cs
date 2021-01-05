using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardFlipper : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CardModel Model;
  public  GameObject card;

    public AnimationCurve scaleCurve;
    public float duration= 0.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Model = GetComponent<CardModel>();
       
    }

    public void FlipCard(Sprite firstImage,Sprite endEmage,int cardIndex)
    {
        
        StopCoroutine(Flip(firstImage,endEmage,cardIndex));
        StartCoroutine(Flip(firstImage, endEmage, cardIndex));
    }
    IEnumerator Flip(Sprite firstImage, Sprite endEmage, int cardIndex)
    {
        spriteRenderer.sprite = firstImage;

        float time = 0f;
     
            while (time <= 1)
            {

                time = time + Time.deltaTime / duration;

                card.transform.DORotate(new Vector3(0, 360f, 0), 1f, RotateMode.FastBeyond360);
                          
                if (time >= 0.5f)
                {
                    spriteRenderer.sprite = endEmage;

                }

            }

        
       

        yield return new WaitForFixedUpdate();

        if (cardIndex == -1)
        {
            Model.ToggleFace(false);
        }
        else
        {
            Model.cardIndex = cardIndex;
            Model.ToggleFace(true);
        }
    
    }
    

}
