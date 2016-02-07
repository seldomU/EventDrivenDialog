using RelationsInspector;
using RelationsInspector.Backend;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DialogBackend : MinimalBackend<DialogItem, string>
{
	public override IEnumerable<Relation<DialogItem, string>> GetRelations( DialogItem entity )
	{
		if ( entity.replies == null )
			yield break;

		foreach ( var reply in entity.replies )
		{
			if ( reply.nextDialog == null )
				continue;

			yield return new Relation<DialogItem, string>( entity, reply.nextDialog, reply.text );
		}
	}
	
	public override void OnEntityContextClick( IEnumerable<DialogItem> entities, GenericMenu menu )
	{
		menu.AddItem(new GUIContent("Add new reply"), false, () => { foreach ( var e in entities ) { AddReply( e ); } } );
		menu.AddItem( new GUIContent( "Link to reply" ), false, () => api.InitRelation( entities.ToArray() ) );
		menu.AddItem( new GUIContent( "Remove item" ), false, () => { foreach ( var e in entities.ToArray() ) { RemoveItem( e ); } } );
	}

	public override void OnRelationContextClick( Relation<DialogItem, string> relation, GenericMenu menu )
	{
		menu.AddItem( new GUIContent( "remove link" ), false, () => RemoveRelation( relation ) );
	}

	public void AddReply( DialogItem entity )
	{
		// create dialogitem and an object to hold it
		var container = new GameObject();
		var reply = container.AddComponent<DialogItem>();

		// link the two
		entity.replies.Add( new DialogReply() { nextDialog = reply } );

		// also link them in the hierarchy, just for orientation
		container.transform.parent = entity.gameObject.transform;

		// inform the API
		api.AddEntity( reply, Vector2.zero, false );
		api.AddRelation( entity, reply, string.Empty, false );
	}

	void RemoveItem(DialogItem entity)
	{
		// remove affected relations first
		var affectedRelations = api.FindRelations( entity ) as IEnumerable<Relation<DialogItem, string>>;
		foreach ( var relation in affectedRelations )
			RemoveRelation( relation );

		// remove entity
		api.RemoveEntity( entity, false );
		GameObject.DestroyImmediate( entity );
	}

	void RemoveRelation( Relation<DialogItem, string> relation )
	{
		var reply = relation.Source.replies
			.Where( r => r.nextDialog == relation.Target && r.text == relation.Tag )
			.FirstOrDefault();

		if ( reply != null )
			relation.Source.replies.Remove( reply );
		api.RemoveRelation( relation.Source, relation.Target, relation.Tag, false );
	}

	// UI wants to create a relation between source and target
	// to be implemented by subclass
	public override void CreateRelation( DialogItem source, DialogItem target )
	{
		var reply = new DialogReply() { text = string.Empty, nextDialog = target };
		source.replies.Add( reply );
		api.AddRelation( source, reply.nextDialog, reply.text );
	}

	public override string GetEntityTooltip( DialogItem entity )
	{
		return entity.text;
	}
}

