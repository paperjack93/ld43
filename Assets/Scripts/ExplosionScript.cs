using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public LayerMask targetMask;
    public float range;

    void Start() {
		CameraManager.instance.ExplosionShake();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, targetMask);
		SoundManager.PlaySFXAt("explosion", transform.position);
		foreach(Collider collider in hitColliders){
			collider.transform.SendMessage("Death", SendMessageOptions.DontRequireReceiver);
		}
    }
}
