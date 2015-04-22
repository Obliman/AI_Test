using UnityEngine;
using System.Collections;

public class WaypointRender : MonoBehaviour {

	Vector3 target;
	
	//Draw markers for each waypoint
	void OnGUI(){
		for(int i = 0;i < MoveLogic.waypoints.Count;i++){
			target = Camera.main.WorldToScreenPoint(MoveLogic.waypoints[i]);
			DrawQuad(new Rect(target.x-10,Screen.height-target.y-10,20,20),new Color(0.0f,0.0f,0.0f),"" + (i+1),12);
		}
	}

	void DrawQuad(Rect position, Color color, string strng, int fontSize) {
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.skin.box.fontSize = fontSize;
		GUI.Box(position, strng);
	}
}
