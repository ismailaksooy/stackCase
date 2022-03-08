using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjustGates : MonoBehaviour
{
    public List<GameObject> gateList = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGate(){
       
        foreach (GameObject gate in gateList)
        {
            int posX;
            if (Random.Range(0,2)==1)posX=2;
            else posX=-2;
            gate.transform.position = new Vector3(posX,1,gate.transform.position.z);
            gate.GetComponent<gateScript>().setGateValue();
        }
    }
}
