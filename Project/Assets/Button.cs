using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public	Light	controlledLight	;

	void Start(){
		controlledLight.intensity	= 0;
	}

	void OnTouchDown(Vector3 pos){
		controlledLight.intensity	= 8;
	}

	void OnTouchUp(Vector3 pos){
		controlledLight.intensity	= 0;
	}

	void OnTouchStay(Vector3 pos){
		controlledLight.intensity	= 8;
	}

	void OnTouchExit(){
		controlledLight.intensity	= 0;
	}
}
