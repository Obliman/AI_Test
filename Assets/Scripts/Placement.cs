using UnityEngine;
using System.Collections;

public class Placement : MonoBehaviour {

	public GameObject barrier;
	Vector3 pos;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Instantiate(barrier,new Vector3(pos.x,pos.y,0.0f),Quaternion.Euler(0.0f,0.0f,Random.Range(0.0f,360.0f)));
		}
	}
}
