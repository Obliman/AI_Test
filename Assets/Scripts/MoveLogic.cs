using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveLogic : MonoBehaviour {

	bool moveTo = false;
	Vector3 target = new Vector3(0.0f,0.0f,0.0f);
	float speed = 20.0f;
	public static List<Vector3> waypoints = new List<Vector3>();
	public static List<bool> type = new List<bool>(); //false = "real" waypoint, true = calculated waypoint
	float timeSinceMove;
	RaycastHit2D hit;
	float dist;
	float distance;
	float xTB;
	float yTB;
	float xMB;
	float yMB;
	float rayDist;
	int checkCount;
	string temp = "Untagged";
	int sig;
	public GameObject indicator;

	void Start(){
		xMB = transform.renderer.bounds.extents.x;
		yMB = transform.renderer.bounds.extents.y;
	}

	void FixedUpdate () {

		//Add waypoints if left-clicked with left shift held
		//Clear waypoint list and add new waypoint if only left click
		if(Input.GetMouseButtonDown(0)){
			if(Input.GetKey(KeyCode.LeftShift)){
				waypoints.Add (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,1.0f)));
				type.Add(false);
			}else{
				waypoints.Clear();
				type.Clear();
				waypoints.Add (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,1.0f)));
				type.Add(false);
			}
			if(GameObject.FindGameObjectWithTag("Last")){
				GameObject.FindGameObjectWithTag("Last").tag = temp;
			}
			target = waypoints[0];
			checkRay(transform.position);
		}

		if(Input.GetKeyDown(KeyCode.R)){
			checkRay(transform.position);
		}

		//Toggle movement
		if(Input.GetMouseButtonDown(1)){
			moveTo = !moveTo;
		}

		Vector3 currentPos = transform.position;
		dist = Vector3.Distance (target, currentPos);
		rayDist = Vector3.Distance (Camera.main.WorldToScreenPoint(target),Camera.main.WorldToScreenPoint(currentPos));

		//Only move to a nonzero target and when allowed to 
		if (target != new Vector3 (0.0f, 0.0f,0.0f) && moveTo) {
			timeSinceMove = Time.deltaTime;
			distance = speed * timeSinceMove;

			//If target reached, switch to next waypoint
			if (dist <= 2*distance) {
				transform.position = target;
				waypoints.RemoveAt(0);
				type.RemoveAt(0);
				timeSinceMove = 0.0f;
				if(waypoints.Count >= 1){
					target = waypoints[0];
					checkRay(transform.position);
				}else{
					target = new Vector3(0.0f,0.0f,0.0f);
				}
			} else {
				transform.position += (target - currentPos) / dist * distance / 2;
			}
		}

	}

	void checkRay(Vector3 pPos){
		//Check between the current position and the next waypoint for obstacles

		//Make sure we can't hit our own collider with the raycast
		collider2D.enabled = false;

		//Raycast towards our target, but with only the distance to reach it
		hit = Physics2D.Raycast(pPos,target-pPos,rayDist);

		//If we hit something that is not an enemy and has not just been scanned
		if(hit.transform != null){
			if(hit.transform.tag != "Enemy"){
				if(hit.transform.tag != "Last"){

					//Debug tool to show scan path
					Instantiate (indicator,hit.transform.position,Quaternion.identity);

					//Obstacle bounding box size
					xTB = hit.transform.renderer.bounds.extents.x;
					yTB = hit.transform.renderer.bounds.extents.y;
	
					//Save the object's tag, then retag it so we don't rescan it
					temp = hit.transform.tag;
					hit.transform.tag = "Last";
		
					//We only want one computed waypoint to move towards at a time, so remove the old one
					if(type[0]){
						waypoints.RemoveAt(0);
						type.RemoveAt(0);
					}
	
					//Do we need to move generally upwards or downwards to reach the target
					//(Y-Coordinates are inverted)
					if(pPos.y>target.y){
						sig = -1;
					}else{
						sig = 1;
					}

					//Add a new waypoint at the top or bottom of the bounding box of our target, 
					//with room for our object to move past
					waypoints.Insert(0,hit.transform.position + new Vector3(0,sig*(yTB/2+yMB),0));
					type.Insert(0,true);
	
					//Set that waypoint to our target
					target = waypoints[0];

					//Call this method recursively until the edge of the group of obstacles is reached and a valid waypoint found
					//Or until we have scanned the same object twice
					checkRay(transform.position);
				}else{
					//Reset the tag of our last scanned object
					GameObject.FindGameObjectWithTag("Last").tag = temp;
				}
			}
		}
		collider2D.enabled = true;
	}


}
