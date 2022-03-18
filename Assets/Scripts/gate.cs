using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    public int value = 0;
    public string gateText = "";
    public Material greenGateMaterial,redGateMaterial;
    private void Start() {
        if (value>0)
        {
            this.transform.GetChild(0).GetComponent<Renderer>().material =  greenGateMaterial;          
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Renderer>().material =  redGateMaterial;
        }
        if (gateText.Length>0)
        {          
        this.transform.GetChild(1).GetComponent<TextMesh>().text = value + "\n" + gateText;
        }
        else
        {     
        this.transform.GetChild(1).GetComponent<TextMesh>().text = value.ToString();
        }
    }
}
