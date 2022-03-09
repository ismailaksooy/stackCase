using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class stackScriptV2 : MonoBehaviour
{
    //SLIDE COMPONENTS
    private Vector2 firstTouchPoint,currentTouchPoint,mousePosition;
    private bool isSliding = false,isSlidingToLeft;


    //STACK COMPONENTS
    public List<GameObject> leftStack = new List<GameObject>();
    public List<GameObject> rightStack = new List<GameObject>();
    public float exchangeSpeed = 1f,stackOffset = 0.1f,sensivity = 1f;
    private float elapsedTime = 0;
    public GameObject stackItem,stackHolder;

    void Start()
    {
        Application.targetFrameRate = 60;
       editStack(true,10);
    }

    

   
    void Update()
    {
        mousePosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            firstTouchPoint = currentTouchPoint = mousePosition; 
            isSliding = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (currentTouchPoint.x>mousePosition.x+sensivity)
            {
                isSliding = true;
                isSlidingToLeft = true;
            }
            else if (currentTouchPoint.x<mousePosition.x-sensivity)
            {
                isSliding = true;
                isSlidingToLeft = false;
            }
            else{
                isSliding = false;
            }
            currentTouchPoint = mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSliding = false;
        }

        if (isSliding)
        {
            if (elapsedTime>=exchangeSpeed)
            {
                if (isSlidingToLeft && rightStack.Count>0)
                {      
                    leftStack.Add(rightStack[rightStack.Count-1]);
                    rightStack.Remove(rightStack[rightStack.Count-1]);
                    leftStack[leftStack.Count-1].transform.DORotate(new Vector3(0,0,0),exchangeSpeed*2);
                    leftStack[leftStack.Count-1].transform.DOJump(new Vector3(-1.25f,(leftStack[leftStack.Count-1].transform.localScale.y+stackOffset)*leftStack.Count,10),4,1,exchangeSpeed*2);
                }
                else if(!isSlidingToLeft&&leftStack.Count>0)
                {          
                    
                    rightStack.Add(leftStack[leftStack.Count-1]);
                    leftStack.Remove(leftStack[leftStack.Count-1]);
                    rightStack[rightStack.Count-1].transform.DORotate(new Vector3(0,0,-180),exchangeSpeed*2);
                    rightStack[rightStack.Count-1].transform.DOJump(new Vector3(1.25f,(rightStack[rightStack.Count-1].transform.localScale.y+stackOffset)*rightStack.Count,10),4,1,exchangeSpeed*2);
                }
                elapsedTime = 0;
            }
            elapsedTime+=Time.deltaTime;
        }       
    }

    public void editStack(bool isLeft,int count){
        if (isLeft)
        {
            if (count>0)
            {
                for (int i = 0; i < count; i++)
                {
                    leftStack.Add(GameObject.Instantiate(stackItem,new Vector3(-10,-10,10),Quaternion.identity,stackHolder.transform));
                     leftStack[leftStack.Count-1].transform.DORotate(new Vector3(0,0,0),exchangeSpeed*2);
                    leftStack[leftStack.Count-1].transform.DOJump(new Vector3(-1.25f,(leftStack[leftStack.Count-1].transform.localScale.y+stackOffset)*leftStack.Count,10),4,1,exchangeSpeed*2); 
                }    
            }
            else
            {
                count = count*-1;
                if (count>leftStack.Count)
                {
                    count = leftStack.Count;
                }

                for (int i = 0; i < count; i++)
                {
                    GameObject destroyedStackItem = leftStack[leftStack.Count-1];
                    leftStack.Remove(destroyedStackItem);
                    destroyedStackItem.transform.DOJump(new Vector3(-4,5,10),1,1,exchangeSpeed*2).OnComplete(()=>Destroy(destroyedStackItem)); ;       
                   
                }             
            }
        }
        else
        {
            if (count>0)
            {
                for (int i = 0; i < count; i++)
                {
                    rightStack.Add(GameObject.Instantiate(stackItem,new Vector3(10,-10,10),Quaternion.identity,stackHolder.transform));
                    rightStack[rightStack.Count-1].transform.DORotate(new Vector3(0,0,-180),exchangeSpeed*2);
                    rightStack[rightStack.Count-1].transform.DOJump(new Vector3(1.25f,(rightStack[rightStack.Count-1].transform.localScale.y+stackOffset)*rightStack.Count,10),4,1,exchangeSpeed*2);  
                }       
            }
            else
            {
                
                count = count*-1;
                if (count>rightStack.Count)
                {
                    count = rightStack.Count;
                }           
                for (int i = 0; i < count; i++)
                {           
                    GameObject destroyedStackItem = rightStack[rightStack.Count-1];
                    rightStack.Remove(destroyedStackItem);
                    destroyedStackItem.transform.DOJump(new Vector3(4,5,10),1,1,exchangeSpeed*2).OnComplete(()=>Destroy(destroyedStackItem));               
                }  
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.GetComponent<gateScript>())
        {
            bool isLeft;
            if (other.transform.position.x>0) isLeft = false;
            else isLeft = true;
            editStack(isLeft,other.transform.GetComponent<gateScript>().gateValue);
           
        }
    }
}
