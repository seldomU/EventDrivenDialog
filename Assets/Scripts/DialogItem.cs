using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogItem : MonoBehaviour
{
	public string text;
	public List<DialogReply> replies;
}

[Serializable]
public class DialogReply
{
	public string text;
	public DialogItem nextDialog;
	public Button.ButtonClickedEvent effect;
}
