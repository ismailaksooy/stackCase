using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SceneMovement : MonoBehaviour
{
    public float movementSpeed = 0f;
    public List<GameObject> sceneObjects = new List<GameObject>();
    public List<GameObject> gates = new List<GameObject>();


    public List<GameObject> sceneList =new List<GameObject>();
    private GameObject currentScene,nextScene;
    public int counter = 0,currentCount = 0;
    void Start()
    {
        Application.targetFrameRate = 60;
        sceneGenerator();
         sceneList[counterFirst].GetComponent<adjustGates>().setGate();
          sceneList[counterSecond].GetComponent<adjustGates>().setGate();
    }
    public void sceneGenerator(){
      currentScene = sceneList[0];
    }
    public int counterFirst = 0,counterSecond = 1;
    private void FixedUpdate() {
        if (sceneList[counterFirst].transform.position.z>=-100)
        {
            sceneList[counterFirst].transform.position -= new Vector3(0,0,Time.deltaTime*movementSpeed);
        }
        else
        {
            if (counterFirst+2<sceneList.Count)
            {
                counterFirst+=2;
            }
            else
            {
                counterFirst=0;
            }
            sceneList[counterFirst].GetComponent<adjustGates>().setGate();
            sceneList[counterFirst].transform.position = sceneList[counterSecond].transform.position+new Vector3(0,0,99);
            
        }

        if (sceneList[counterSecond].transform.position.z>=-100)
        {
            sceneList[counterSecond].transform.position -= new Vector3(0,0,Time.deltaTime*movementSpeed);
        }
        else
        {
            if (counterSecond+2<sceneList.Count)
            {
                counterSecond+=2;
            }
            else
            {
                counterSecond=1;
            }
             sceneList[counterSecond].GetComponent<adjustGates>().setGate();
             sceneList[counterSecond].transform.position = sceneList[counterFirst].transform.position+new Vector3(0,0,99);
           
        }
        
    }

































    /*
    void FixedUpdate()
    {
        foreach (GameObject perObject in sceneObjects)
        {     
            if (perObject.transform.position.z<0)
            {
                 if (perObject.name.Equals("ground"))
                {
                    counter++;
                    move(perObject);
                }
                if (perObject.name.Equals("gateActive"))
                {
                    perObject.SetActive(false);      
                    perObject.name = "gatePassive";  
                }
               
            }
            if (counter>currentCount && Random.Range(0,10)==0 && perObject.name.Equals("gatePassive"))
            {
                perObject.SetActive(true);  
                perObject.name= "gateActive";
                perObject.transform.position = new Vector3((Random.Range(0,2)*2-1)*2,0,80);
            }
        
            //perObject.transform.position = perObject.transform.position - new Vector3(0,0,movementSpeed);
        }
       
    }
*/
    void move(GameObject stackItem){  
        stackItem.transform.DOMoveZ(-5,movementSpeed).From(new Vector3(0,0,90));
    }

    IEnumerator starter(){
        foreach (GameObject ground in sceneObjects)
        {
            if (ground.name.Equals("ground"))
            {
                move(ground);
            }
             yield return new WaitForSeconds(movementSpeed);
        }
       
    }


}
