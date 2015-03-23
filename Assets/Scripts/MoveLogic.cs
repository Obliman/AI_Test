using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveLogic : MonoBehaviour {

	bool moveTo = false;
	Vector3 target = new Vector3(0.0f,0.0f,0.0f);
	float speed = 10.0f;
	public static List<Vector3> waypoints = new List<Vector3>();
	float timeSinceMove;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Cause it to make structures to expand a base and aquire resources

		if(Input.GetMouseButtonDown(0)){
			if(Input.GetKey(KeyCode.LeftShift)){
				waypoints.Add (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,1.0f)));
			}else{
				waypoints.Clear();
				waypoints.Add (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,1.0f)));
			}
			target = waypoints[0];
		}

		if(Input.GetMouseButtonDown(1)){
			moveTo = !moveTo;
		}

		if (target != new Vector3 (0.0f, 0.0f,0.0f) && moveTo) {
			timeSinceMove = Time.deltaTime;
			float distance = speed * timeSinceMove;
			Vector3 currentPos = transform.position;

			float dist = Vector3.Distance (target, currentPos);
			if (dist <= distance) {
				transform.position = target;
				waypoints.RemoveAt(0);
				timeSinceMove = 0.0f;
				if(waypoints.Count >= 1){
					target = waypoints[0];
				}else{
					target = new Vector3(0.0f,0.0f,0.0f);
				}
			} else {
				transform.position += (target - currentPos) / dist * distance / 2;
			}
		}

	}
}
