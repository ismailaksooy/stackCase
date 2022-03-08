using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StackScript : MonoBehaviour
{
    public List<GameObject> leftStack = new List<GameObject>();
    public List<GameObject> rightStack = new List<GameObject>();

    public bool isLeftStackActive = true,stackingComplate = true;
    public int stackItemCounter = 0;
    public float stackItemHeight = 0.5f,exchangeSpeed = 1f,exchangeTotalSpeed = 0.5f;

    public GameObject stackItem,trigger;
           
    private void Start() {
        StartCoroutine(exchange());
        addItemToStack(10);
    }

    public void change(){
        if (stackingComplate)
        {
            StartCoroutine(exchange());
            stackingComplate = false;
        }
        
    }

     public Vector2 firstPoint,lastPoint;
    public bool waitingForUp = false;

    private void Update() {
        if (Input.GetMouseButtonDown(0)&&!waitingForUp)
        {
            waitingForUp = true;
           firstPoint = Input.mousePosition;
            
        }
        if (Input.GetMouseButtonUp(0)&&waitingForUp)
        {
            lastPoint = Input.mousePosition;
             
             waitingForUp = false;
                if (firstPoint.x-lastPoint.x<-10 && isLeftStackActive && stackingComplate)
                {
                   StartCoroutine(exchange());
                    stackingComplate = false;
                }
                else if (firstPoint.x-lastPoint.x>10&& !isLeftStackActive && stackingComplate)
                {
                    StartCoroutine(exchange());
                    stackingComplate = false;
                }
        }
    }

    IEnumerator exchange(){
        stackItemCounter = 0;
        if (isLeftStackActive)
        {
            trigger.transform.position = new Vector3(2,1,10);
            leftStack.Reverse();
            foreach (GameObject stackItem in leftStack)
            {
                stackItem.name="rightStackItem";
                rightStack.Add(stackItem); 
                stackItemCounter++;
                stackItem.transform.DORotate(new Vector3(0,0,180),exchangeSpeed);
                stackItem.transform.DOJump(new Vector3(1.5f,1+(stackItemCounter*stackItemHeight+0.5f),10),2,1,exchangeSpeed); 
                yield return new WaitForSeconds(exchangeTotalSpeed); 
            }
             
            leftStack.Clear();
            isLeftStackActive=false;
        }
        else
        {
            trigger.transform.position = new Vector3(-2,1,10);
            rightStack.Reverse();
            foreach (GameObject stackItem in rightStack)
            {
                stackItem.name="leftStackItem";
                leftStack.Add(stackItem); 
                stackItemCounter++;
                stackItem.transform.DORotate(new Vector3(0,0,0),exchangeSpeed);
                stackItem.transform.DOJump(new Vector3(-1.5f,1+(stackItemCounter*stackItemHeight+0.5f),10),2,1,exchangeSpeed);    
                yield return new WaitForSeconds(exchangeTotalSpeed);       
            }
             
            rightStack.Clear();
            isLeftStackActive = true;
        }
        stackingComplate = true;
    }


    public void addItemToStack(int itemCount){
        if (stackingComplate)
        {
            stackingComplate=false;
            int leftStackCount = leftStack.Count;
            int rightStackCount = rightStack.Count;
            if (itemCount>0)
            {
                for (int i = 0; i < itemCount; i++)
                {
                    GameObject item = GameObject.Instantiate(stackItem,Vector3.zero,Quaternion.identity);
                    if (isLeftStackActive)
                    {
                        leftStack.Add(item);
                        item.transform.DORotate(new Vector3(0,0,0),exchangeSpeed);
                        item.transform.DOJump(new Vector3(-1.5f,1+((leftStackCount+i+1)*stackItemHeight+0.5f),10),2,1,exchangeSpeed); 
                    }
                    if (!isLeftStackActive)
                    {
                        rightStack.Add(item);
                        item.transform.DORotate(new Vector3(0,0,180),exchangeSpeed);
                        item.transform.DOJump(new Vector3(1.5f,1+((rightStackCount+i+1)*stackItemHeight+0.5f),10),2,1,exchangeSpeed); 
                    }
                }
            }
            else if(itemCount<0)
            {
                
                
                for (int i = 0; i < itemCount*-1; i++)
                {
                    
                    if (isLeftStackActive)
                    {
                        if ((leftStackCount-i-1)<=0)
                        {
                            break;
                        }
                        GameObject destroy = leftStack[leftStackCount-i-1];
                        destroy.transform.DOJump(Vector3.zero,2,1,exchangeSpeed);
                        leftStack.RemoveAt(leftStackCount-i-1);
                        Destroy(destroy);
                        
                    }
                    if (!isLeftStackActive)
                    {
                        if ((rightStackCount-i-1)<=0)
                        {
                            break;
                        }
                        GameObject destroy = rightStack[rightStackCount-i-1];
                        destroy.transform.DOJump(Vector3.zero,2,1,exchangeSpeed); 
                        rightStack.RemoveAt(rightStackCount-i-1);
                        Destroy(destroy);
                    }
                }
            }

             stackingComplate=true;
        }
       
    

    }
   
}
