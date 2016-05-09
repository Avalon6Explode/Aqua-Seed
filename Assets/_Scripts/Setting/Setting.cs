using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

	ControlSetting controlSetting;


	public ControlSetting Control { get { return controlSetting; } }


	public Setting() {
		controlSetting = new ControlSetting();
	}
}
