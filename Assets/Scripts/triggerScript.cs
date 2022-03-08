using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerScript : MonoBehaviour
{
      private void OnCollisionEnter(Collision other) {
        if (other.transform.name.Equals("gate"))
        {
            GameObject.FindWithTag("MainCamera").GetComponent<StackScript>().addItemToStack(other.transform.GetComponent<gateScript>().gateValue);
            
    
        }
        Debug.Log("Triggered");
    }
}
