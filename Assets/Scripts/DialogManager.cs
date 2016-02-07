using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	// references to UI elements
	public GameObject panel;
	public Text question;
	public Button[] replyButtons;

	// turn off by default
	void Awake()
	{
		HideDialog();
	}

	// turn on/off
	public void HideDialog()
	{
		panel.SetActive( false );
	}

	public void ShowDialog( DialogItem item )
	{
		if ( item == null || string.IsNullOrEmpty( item.text ) )
			throw new System.ArgumentException( "item" );

		panel.SetActive( true );

		question.text = item.text;

		for ( int i = 0; i < replyButtons.Length; i++ )
		{
			var button = replyButtons[ i ];

			// show only as many buttons as we have replies
			bool show = i < item.replies.Count;
			button.gameObject.SetActive( show );
			if ( show )
			{
				var reply = item.replies[ i ];
				button.onClick = reply.effect;
				
				// hide dialog when button is pressed
				button.onClick.AddListener( () => HideDialog() );

				// show target dialog when button is pressed
				if ( reply.nextDialog != null )
					button.onClick.AddListener( () => ShowDialog( reply.nextDialog ) );

				// set the label text
				button.GetComponentInChildren<Text>().text = reply.text;
			}
		}
	}
}
