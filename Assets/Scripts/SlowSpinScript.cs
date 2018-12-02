using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpinScript : MonoBehaviour {
	public Vector3 spinSpeed = new Vector3(0f,1f,0f);
	void Update () {
		transform.Rotate( spinSpeed * Time.deltaTime, Space.World);
	}
}
