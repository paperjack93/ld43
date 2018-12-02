using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour {

	public GameObject collectFx;
	public Vector3 collectFxRot;

    void OnTriggerEnter(Collider collider) {
        if(collider.transform.tag != "Player") return;
        if(collider.transform.GetComponent<GnomeScript>().sacrificed) return;
    	if(collectFx != null) {
    		Quaternion rotation = Quaternion.identity;
    		rotation.eulerAngles = collectFxRot;
    		Instantiate(collectFx, transform.position, rotation);
    	}
        SoundManager.PlaySFXAt("get", transform.position);
        GameManager.instance.CollectibleGet();
    	Destroy(gameObject);
    }
}
