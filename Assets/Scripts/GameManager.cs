using UnityEngine;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour
{
	public DialogManager dialogMgr;
	public DialogItem bgColorDialog;
	public DialogItem flowChart;

	void Update ()
	{
		if( Input.GetKeyUp(KeyCode.A ) )
			OpenDialog( bgColorDialog );

		if ( Input.GetKeyUp( KeyCode.B ) )
			OpenDialog( flowChart );

		if ( Input.GetKeyUp( KeyCode.C ) )
			dialogMgr.ShowDialog( false );
	}

	void OpenDialog( DialogItem item )
	{
		if ( item == null || string.IsNullOrEmpty( item.text ) )
			throw new System.ArgumentException( "item" );

		dialogMgr.PopulateDialog( item );
		dialogMgr.ShowDialog( true );
	}
}
