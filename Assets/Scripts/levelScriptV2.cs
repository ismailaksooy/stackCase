using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class levelScriptV2 : MonoBehaviour
{
    public int currentLevel = 0;
    public List<level> levelList = new List<level>();
    private bool levelPause= false;
    public level thisLevel;
    private void Start() {
        setLevel();
        playLevel();
    }
   
    void setLevel(){
        thisLevel = levelList[currentLevel];
        thisLevel.gameObject.SetActive(true);
        thisLevel.transform.position = new Vector3(0,0,thisLevel.levelStartZ);
        foreach(level level in levelList){
            if (level!=thisLevel && level.gameObject.activeSelf)
            {
                level.gameObject.SetActive(true);
            }
        }
    }
    public void pauseLevel(){
        levelPause = true;
    }
    public void playLevel(){
        levelPause = false;
    }

    private void Update() {
        if (!levelPause)
        {
            thisLevel.transform.position -= new Vector3(0,0,thisLevel.levelSpeed*Time.deltaTime);
        }
    }

    public void resetLevel(){
        SceneManager.LoadScene(0);
    }

}
