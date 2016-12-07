using UnityEngine;
using UnityEngine.UI;

public class UIDebug : MonoBehaviour {

	SoldierCharacter sc;
	Text[] uiDebugText;

	void Start() {
		uiDebugText = GetComponentsInChildren<Text> ();
	}

	void Update() {
		if (sc == null)
			return;

		Gun gun = sc.currentGun;
		if (gun != null) 
			SetText (1, "GunID: " + gun.netId + " Rot: " + gun.transform.rotation);
		else 
			SetText (1, "No Gun Equipped");

		Camera cam = sc.GetComponentInChildren<Camera> ();
		if (cam != null) 
			SetText (2, "CameraRotation: " + cam.transform.rotation);
	}

	public void BindPlayer(SoldierCharacter _sc) {
		Debug.Log (uiDebugText.Length);
		sc = _sc;
		SetText (0, "NetworkID: " + sc.netId);
	}

	public void SetText(int _index, string _text) {
		uiDebugText[_index].text = _text;
	}
}
