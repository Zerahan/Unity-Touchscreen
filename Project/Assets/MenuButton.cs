using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
	public	bool	useGUIButton		= true;
	public	Light	controlledLight		;
	public	Color	selectedColor		;
	
	private	Color		defaultColor	;
	private Rect		rectGUI1		= new Rect((Screen.width/2)-100,(Screen.height-40)/2-40,100,40);
	private Rect		rectGUI2		= new Rect((Screen.width/2),(Screen.height-40)/2-40,100,40);
	private Rect		rectTouch1		= new Rect((Screen.width/2)-100,(Screen.height-40)/2+40,100,40);
	private Rect		rectTouch2		= new Rect((Screen.width/2),(Screen.height-40)/2+40,100,40);
	private	bool		onState			= false;
	private	TouchInput	touchInput		;

	void Start(){
		defaultColor	= controlledLight.color;
		touchInput		= (TouchInput)FindObjectOfType(typeof(TouchInput));
		controlledLight.intensity	= 0;
	}

	void OnGUI(){
		bool	guiButton	= GUI.Button(rectGUI1,"Unlock 1");
		if( GUI.Button(rectGUI2,"Unlock 1") && guiButton ){
			onState	= !onState;
			if(onState){
				controlledLight.intensity	= 8;
			}else{
				controlledLight.intensity	= 0;
			}
		}else{
			GUI.Button(rectTouch1,"Unlock 2");
			GUI.Button(rectTouch2,"Unlock 2");
			if( touchInput != null ){
				onState	= (touchInput.AnyTouchWithin(rectTouch1) && touchInput.AnyTouchWithin(rectTouch2));
				if(onState){
					controlledLight.color		= selectedColor;
					controlledLight.intensity	= 8;
				}else{
					controlledLight.intensity	= 0;
					controlledLight.color		= defaultColor;
				}
			}
		}
	}
}
