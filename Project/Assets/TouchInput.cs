using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {
	public	LayerMask			touchInputMask	;

	private	List<GameObject>	touchList		= new List<GameObject>();
	private	GameObject[]		touchesOld		;
	private	List<Vector2>		touchPositions	= new List<Vector2>();
	private	Vector2[]			touchPositionsOld;

	private	RaycastHit			hit				;

	void Update(){
#if UNITY_EDITOR
		if( Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) ){

			touchesOld	= new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray,out hit, touchInputMask)){
				GameObject recipient = hit.transform.gameObject;
				touchList.Add(recipient);

				if( Input.GetMouseButtonDown(0) ){
					recipient.SendMessage("OnTouchDown",hit.point,SendMessageOptions.DontRequireReceiver);
				}
				if( Input.GetMouseButtonUp(0) ){
					recipient.SendMessage("OnTouchUp",hit.point,SendMessageOptions.DontRequireReceiver);
				}
				if( Input.GetMouseButton(0) ){
					recipient.SendMessage("OnTouchStay",hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}
			
			foreach(GameObject obj in touchesOld){
				if( !touchList.Contains(obj) ){
					obj.SendMessage("OnTouchExit",SendMessageOptions.DontRequireReceiver);
				}
			}

		}
#endif
		if(Input.touchCount > 0){
			touchesOld	= new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

			touchPositionsOld	= new Vector2[touchPositions.Count];
			touchPositions.CopyTo(touchPositionsOld);
			touchPositions.Clear();

			foreach(Touch touch in Input.touches){
				touchPositions.Add(touch.position);
				Ray ray = camera.ScreenPointToRay(touch.position);
				if (Physics.Raycast(ray,out hit, touchInputMask)){
					GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);

					switch(touch.phase){
						case TouchPhase.Began:
							recipient.SendMessage("OnTouchDown",hit.point,SendMessageOptions.DontRequireReceiver);
							break;
						case TouchPhase.Ended:
							recipient.SendMessage("OnTouchUp",hit.point,SendMessageOptions.DontRequireReceiver);
							break;
						case TouchPhase.Canceled:
							recipient.SendMessage("OnTouchExit",hit.point,SendMessageOptions.DontRequireReceiver);
							break;
						case TouchPhase.Stationary:
						case TouchPhase.Moved:
							recipient.SendMessage("OnTouchStay",hit.point,SendMessageOptions.DontRequireReceiver);
							break;
					}
				}
			}

			foreach(GameObject obj in touchesOld){
				if( !touchList.Contains(obj) ){
					obj.SendMessage("OnTouchExit",hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}

		}
	}

	public bool AnyTouchWithin(Rect rect){
		bool	isTouchingButton	= false;
		foreach(Vector2 pos in touchPositions){
			if( rect.Contains(pos) ){
				isTouchingButton	= true;
			}
		}
		return isTouchingButton;
	}
}
