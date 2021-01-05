using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DblClick : MonoBehaviour,IPointerClickHandler
{
    



    public bool check;
    
   
    
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();

        if (eventData.clickCount > 1)
        {
            Debug.Log(eventData.clickCount);
            check = true;

        }


    }

    private void Start()
    {

        check = false;
    }



}
