using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
	public static CameraManager instance;

	void Awake(){
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy(gameObject);
			return;
		}
    }

    public void ExplosionShake(){
    	DOTween.Kill(transform);
    	transform.localPosition = Vector3.zero;
    	transform.DOPunchPosition(Vector3.up*0.5f, 1f, 10, 1f, false);
    }


}
