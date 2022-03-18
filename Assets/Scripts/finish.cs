using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class finish : MonoBehaviour
{
    

    public void finisSceneCameraPoisition(level level,int stackCount){
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Sequence cameraSeq = DOTween.Sequence();
        cameraSeq.Append(camera.transform.DOMove(new Vector3(6.5f,6,20),2));
        cameraSeq.Join(camera.transform.DORotate(new Vector3(37.7f,-140,0),2));

        Animator characterFirst = GameObject.FindGameObjectWithTag("characterFirst").GetComponent<Animator>();
        Animator characterSecond = GameObject.FindGameObjectWithTag("characterSecond").GetComponent<Animator>();
        if (level.minimumFinishStackCount<=stackCount)
        {
            characterFirst.SetBool("isWin",true);
            characterSecond.SetBool("isWin",true);
        }
        else
        {
            characterFirst.SetBool("isWin",false);
            characterSecond.SetBool("isWin",false);
        }
        characterFirst.SetBool("isFinished",true);
        characterSecond.SetBool("isFinished",true);
        
       

    }
}
