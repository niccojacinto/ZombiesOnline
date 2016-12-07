using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// Class for Networking Setup Only


public class SetupLocalPlayer : NetworkBehaviour {

	void Start () {
	    if (isLocalPlayer) {
            // If you are this player...
            GetComponent<PlayerController>().enabled = true;
            GetComponent<SoldierCharacter>().enabled = true;
            GetComponent<Inventory>().enabled = true;
			// FindObjectOfType<UIConsole> ().enabled = true;
			FindObjectOfType<UIDebug> ().BindPlayer(GetComponent<SoldierCharacter>());
            FindObjectOfType<UIQuickslot>().BindInventory(GetComponent<Inventory>());
        } else {
            // If the player isnt you...

            // Disable Camera
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
	}

}
