using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
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

    public Vector3 stackPos;
    

    //****************
    private int stackCount = 0;
    public Text stackCountText;
    public levelScriptV2 levelScriptV2;


    void Start()
    {
        Application.targetFrameRate = 60;
       //editStack(true,10);
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
                    leftStack[leftStack.Count-1].transform.DOJump(new Vector3(-stackPos.x,(leftStack[leftStack.Count-1].transform.localScale.y+stackOffset)*leftStack.Count+stackPos.y,stackPos.z),4,1,exchangeSpeed*2);
                }
                else if(!isSlidingToLeft&&leftStack.Count>0)
                {          
                    
                    rightStack.Add(leftStack[leftStack.Count-1]);
                    leftStack.Remove(leftStack[leftStack.Count-1]);
                    rightStack[rightStack.Count-1].transform.DORotate(new Vector3(0,0,-180),exchangeSpeed*2);
                    rightStack[rightStack.Count-1].transform.DOJump(new Vector3(stackPos.x,(rightStack[rightStack.Count-1].transform.localScale.y+stackOffset)*rightStack.Count+stackPos.y,stackPos.z),4,1,exchangeSpeed*2);
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
                    leftStack[leftStack.Count-1].transform.DOJump(new Vector3(-stackPos.x,(leftStack[leftStack.Count-1].transform.localScale.y+stackOffset)*leftStack.Count+stackPos.y,stackPos.z),4,1,exchangeSpeed*2); 
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
                    rightStack[rightStack.Count-1].transform.DOJump(new Vector3(stackPos.x,(rightStack[rightStack.Count-1].transform.localScale.y+stackOffset)*rightStack.Count+stackPos.y,stackPos.z),4,1,exchangeSpeed*2);  
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
        stackCount = leftStack.Count+rightStack.Count;
        stackCountText.text = stackCount.ToString();
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log(other.transform.name);
        if (other.transform.GetComponent<gate>())
        {
            bool isLeft;
            if (other.transform.position.x>0) isLeft = false;
            else isLeft = true;
            editStack(isLeft,other.transform.GetComponent<gate>().value);
           
        }
        else if (other.transform.GetComponent<finish>())
        {
            
            levelScriptV2.pauseLevel();
            other.transform.GetComponent<finish>().finisSceneCameraPoisition(levelScriptV2.thisLevel,stackCount);
        }
        else if (other.gameObject.name.Equals("EmptyGround")){
            bool isLeft;
            if (other.transform.position.x>0) isLeft = false;
            else isLeft = true;
            stackToGround(other.transform.localScale.z,isLeft,other.gameObject);
            editStack(!isLeft,-1); 
        }
        Debug.Log("boom");
    }

    public void stackToGround(float zLenght,bool isLeft, GameObject emptyGroundObject){
        if (!isLeft && rightStack.Count>0)
        {                 
                GameObject stackItem = rightStack[rightStack.Count-1];
                
                stackItem.transform.parent = emptyGroundObject.transform;
                stackItem.transform.DOMoveY(0,0.1f);
                stackItem.transform.DOMoveZ(emptyGroundObject.transform.position.z+5,0.1f);     
                Debug.Log("22222222222");
        }
    }
}
