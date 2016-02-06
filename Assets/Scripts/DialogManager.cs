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
		ShowDialog( false );
	}

	// turn on/off
	public void ShowDialog( bool doShow )
	{
		panel.SetActive( doShow );
	}

	public void PopulateDialog( DialogItem item )
	{
		ShowDialog( true );

		question.text = item.text;

		for ( int i = 0; i < replyButtons.Length; i++ )
		{
			var button = replyButtons[ i ];
			bool show = i < item.replies.Count;
			button.gameObject.SetActive( show );
			if ( show )
			{
				var reply = item.replies[ i ];
				button.onClick = reply.effect;
				button.onClick.AddListener( () => ShowDialog( false ) );
				if ( reply.target != null )
					button.onClick.AddListener( () => PopulateDialog( reply.target ) );

				button.GetComponentInChildren<Text>().text = reply.text;
			}
		}
	}
}
