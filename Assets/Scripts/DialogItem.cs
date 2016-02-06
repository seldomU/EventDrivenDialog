using UnityEngine;
using System;
using System.Collections.Generic;
using SelectionEvent = UnityEngine.UI.Button.ButtonClickedEvent;

public class DialogItem : MonoBehaviour
{
	public string text;
	public List<DialogReply> replies;
}

[Serializable]
public class DialogReply
{
	public string text;
	public DialogItem target;
	public SelectionEvent effect;
}
