using UnityEngine;

public class GameManager : MonoBehaviour
{
	public DialogManager dialogMgr;
	public DialogItem bgColorDialog;
	public DialogItem flowChart;

	void Update ()
	{
		if( Input.GetKeyUp(KeyCode.A ) )
			dialogMgr.ShowDialog( bgColorDialog );

		if ( Input.GetKeyUp( KeyCode.B ) )
			dialogMgr.ShowDialog( flowChart );

		if ( Input.GetKeyUp( KeyCode.C ) )
			dialogMgr.HideDialog( );
	}
}
