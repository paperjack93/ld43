using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyableWallScript : MonoBehaviour
{
	public int hp;

	Vector3 initPos;

	void Start(){
		initPos = transform.position;
	}

	public void Death(){
		hp--;
		if(hp == 0) {
			SoundManager.PlaySFXAt("explode2", transform.position);
			Destroy(gameObject);
			return;
		}
		SoundManager.PlaySFXAt("hurt1", transform.position);

    	DOTween.Kill(transform);
    	initPos += Vector3.down;
    	transform.position = initPos;
    	transform.DOShakePosition(1f, 1f, 10, 90f, false);
    }

}
