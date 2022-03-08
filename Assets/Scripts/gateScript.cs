using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class gateScript : MonoBehaviour
{
    
    public int gateValue=0;
    public Material positiveMat,negativeMat;
    public void setGateValue(){
        while (gateValue==0)
        {
            gateValue = Random.Range(-10,10);
        }
     
        this.transform.GetChild(0).GetComponent<TextMesh>().text=gateValue.ToString();
        if (gateValue>0)
        {
        this.transform.GetChild(1).GetComponent<Renderer>().material =  positiveMat;
            
        }
        else
        {
            
        this.transform.GetChild(1).GetComponent<Renderer>().material = negativeMat;
        }

    }

  
        
}
