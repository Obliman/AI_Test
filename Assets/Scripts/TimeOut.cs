using UnityEngine;
using System.Collections;

public class TimeOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Despawn(0.5f));
	}
	
	// we don't need bullets piling up everywhere, but instant despawn doesn't allow for reliable hit detection
	IEnumerator Despawn(float delay){
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
}
