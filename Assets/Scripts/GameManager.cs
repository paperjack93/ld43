using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public AudioSource music;
	public float timeLeft = 1000f;
	public Text timeLeftText;
	public Text grinchKilledText;
	public Text gnomesLeftText;
	public Text pointsText;
	public Text giftsText;
	public Text rankText;
	public int collectibleCount = 0;
	public int greenchKilled = 0;
	public Animator animator;

	bool _over = false;

	void Awake(){
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy(gameObject);
			return;
		}
		//ShowResults();
    }

	void Update() {
		if(timeLeft == 0f) return;
		timeLeft = Mathf.Max(0f, timeLeft - Time.deltaTime);
		timeLeftText.text = (Mathf.RoundToInt(timeLeft)).ToString();
        if (Input.GetKeyDown(KeyCode.M)) music.mute = !	music.mute;
        if(timeLeft == 0f) GameOver();
    }

    public void GameOver(){
    	if(_over) return;
    	animator.SetTrigger("GameOver");
    	_over = true;
    }

    public void Lose(){
       	if(_over) return;
    	animator.SetTrigger("Lose");
    	_over = true;
    }

    public void Restart(){
    	SceneManager.LoadScene("Level1");
    }

    public void GreenchKilled(){
    	greenchKilled++;
    }

    public void CollectibleGet(){
    	collectibleCount++;
    }

    public void ShowResults(){
    	StartCoroutine("ShowResult");
    }

    public IEnumerator ShowResult(){
    	int currentResult = 0;
    	int counter = 0;
        while (counter <= greenchKilled)
        {
        	grinchKilledText.text = counter.ToString();
        	pointsText.text = currentResult.ToString();
			SoundManager.PlaySFX("step", 0.05f);
			currentResult += 1000;
        	counter++;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.25f);
        counter = 0;
        while (counter <= GnomeController.instance.gnomes.Count)
        {
        	gnomesLeftText.text = counter.ToString();
        	pointsText.text = currentResult.ToString();
			SoundManager.PlaySFX("step", 0.05f);
			currentResult += 1000;
        	counter++;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.25f);
        counter = 0;
        while (counter <= collectibleCount)
        {
        	giftsText.text = counter.ToString();
        	pointsText.text = currentResult.ToString();
			SoundManager.PlaySFX("step", 0.05f);
			currentResult += 2500;
        	counter++;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.25f);

        if(currentResult < 5000){
            rankText.text = "F";
        } else if(currentResult >= 5000 && currentResult < 15000){
            rankText.text = "D";
        } else if(currentResult >= 15000 && currentResult < 25000){
            rankText.text = "C";
        } else if(currentResult >= 25000 && currentResult < 40000){
            rankText.text = "B";
        } else if(currentResult >= 40000 && currentResult < 55000){
            rankText.text = "A--";
        } else if(currentResult >= 55000 && currentResult < 60000){
            rankText.text = "A-";
        } else if(currentResult >= 60000 && currentResult < 70000){
            rankText.text = "A";
        } else if(currentResult >= 70000 && currentResult < 100000){
            rankText.text = "A+";
        } else if(currentResult >= 100000 && currentResult < 115000){
            rankText.text = "S";
        } else if(currentResult >= 115000){
            rankText.text = "SSS";
        }
    }

}
