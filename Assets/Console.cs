using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Console : NetworkBehaviour {

	private GameObject console;
	private InputField consoleInput;
	private Transform consoleScroll;
	private RectTransform consoleScrollContent;
	private Text chatLog;

	// rivate List<string> chatHistory;

	void Start () {

		console = GameObject.Find ("Console");
		if (console == null) Debug.Log("Unable to find console");
		consoleInput = console.transform.GetChild(1).GetComponent<InputField>();
		if (consoleInput == null) Debug.Log("Unable to find consoleInput");

		consoleScroll = console.transform.GetChild(0);
		if (consoleScroll == null) Debug.Log("Unable to find consoleScroll");
		consoleScrollContent = consoleScroll.GetChild(0).GetChild(0).GetComponent<RectTransform>();
		if (consoleScrollContent == null) Debug.Log("Unable to find consoleScrollContent");
		chatLog = consoleScroll.GetChild(0).GetChild(0).GetComponent<Text>();
		if (chatLog == null) Debug.Log("Unable to find Text");
		//chatHistory = new List<string>();
	}

	void Update () {
		HandleInput();
	}

	void HandleInput() {

		if (!isLocalPlayer) return;
		// Debug.Log("A Key Was Pressed");
		if (Input.GetKeyDown(KeyCode.Return)) {
			Debug.Log("Enter was Pressed");
			string chatMsg = consoleInput.text;
			consoleInput.text = "";
			CmdSendChatMsg(chatMsg);
		}
	}

	[ClientRpc]
	void RpcSendChatMsg(string msg) {
		AddContentMsg(msg);
	}

	[Command]
	void CmdSendChatMsg(string msg) {
		RpcSendChatMsg(msg);
	}

	void AddContentMsg(string msg) {
		chatLog.text += "\n PlayerID("+netId+"): " + msg;
	}
}
