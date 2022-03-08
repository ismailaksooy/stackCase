using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiSlideMovement : MonoBehaviour
{

    public Vector2 firstPoint,lastPoint;
    public bool waitingForUp = false;
 
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)&&!waitingForUp)
        {
            waitingForUp = true;
           firstPoint = Input.mousePosition;
            
        }
        if (Input.GetMouseButtonUp(0)&&waitingForUp)
        {
            lastPoint = Input.mousePosition;
             
             waitingForUp = false;
                if (firstPoint.x-lastPoint.x<-10)
                {
                    Debug.Log("Sağa");
                }
                if (firstPoint.x-lastPoint.x>10)
                {
                    Debug.Log("Sola");
                }
        }
       
        
    }
}
