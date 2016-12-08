using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIConsole : NetworkBehaviour {

	public ScrollRect scrollRect;
	public InputField inputField;

	Text chatLog;

	void Start () {
		chatLog = scrollRect.content.GetComponent<Text>();
		if (!chatLog) 	Debug.Log ("chatLog is null");
	}

	public string Enter() {
		string msg = popInputField ();
		AddContentMsg (msg);
		return msg;
	}

	public string AddContentMsg(string msg) {
		string tmp = "\n Player(ID:) "  + msg;
		chatLog.text += tmp;
		return tmp;
	}

	public string popInputField() {
		string chatMsg = inputField.text;
		inputField.text = "";
		return chatMsg;
	}

	public void ChangeContents(string newContent) {
		chatLog.text = newContent;
	}
}

