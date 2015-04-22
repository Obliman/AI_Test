using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	float force = 0.0f;
	float timeCount = 0.0f;

	void OnCollider2DStay(Collider2D other){
		timeCount += Time.deltaTime;
		//force = 100.0f*(1+Mathf.Sin(timeCount));
		force = 1000.0f;
		other.transform.rigidbody2D.AddForce(new Vector2(force,force));

		//float SpringAngle = transform.eulerAngles.z + 90;
		//Vector2 direc = Quaternion.AngleAxis (SpringAngle, Vector3.forward) * Vector3.right;
		//other.gameObject.rigidbody2D.AddForce (direc * force);
		
		if(timeCount > 2*Mathf.PI){
			timeCount = 0;
		}
	}
}
