using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour{

    public void StartGameAnim(){
    	GetComponent<Animator>().SetTrigger("StartGame");
    }

    public void StartGame(){
    	SceneManager.LoadScene("Level1");
    }
}
