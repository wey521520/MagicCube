using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Reflection;
using System;
using LitJson;

public class BFEventHandler : MonoBehaviour
{

	public DefaultBaseFrameEventList defaultevents = new DefaultBaseFrameEventList ();
	
	public BaseFrameEventList events = new BaseFrameEventList ();

	public CustomerEventList customerevents = new CustomerEventList ();
	//add in 15.1.26

	public Dictionary<string, MethodInfo> cache = new Dictionary<string, MethodInfo> ();
	public Dictionary<string, Component> cache_cp = new Dictionary<string, Component> ();

	public void execute (BaseFrameEvent ev, Type[] types, params object[] args)
	{
		string key = ev.ToString ();
		MethodInfo method = null;
		GameObject obj = null;
		Component cp = null;
		if (cache.ContainsKey (key)) {
			method = cache [key];
			cp = cache_cp [key];
		} else {
			try {
				// find go
				obj = Finder.findGameObject (ev.gameObjectName);
				// find cp
				cp = Finder.findComponent (obj, ev.componentName);
				// find method
				method = Finder.findMethodInfo (cp, ev.methodName, types);
			} catch (NullGameObjectException e) {
				Utils.error ("Can't find GameObject({0}) : {1}", e.ToString (), ev.gameObjectName);
			} catch (NullComponentException e) {
				Utils.error ("Can't find Component({0}) : {1}", e.ToString (), ev.componentName);
			} catch (NullMethodInfoException e) {
				Utils.error ("Can't find MethodInfo({0}) : {1}", e.ToString (), ev.methodName);
			}
		}
		if (method != null) {
			method.Invoke (cp, args);
		}
	}

	// For customer event.
	public void OnCustomerClick ()
	{
		Type[] types = new Type[0];
		object[] args = null;
		foreach (BaseFrameEvent ev in customerevents.onClickEvents) {
			execute (ev, types, args);
		}
	}

	public void OnCustomerSubmit (string s)
	{
		Type[] types = { s.GetType () };
		object[] args = { s };
		foreach (BaseFrameEvent ev in customerevents.onSubmitEvents) {
			execute (ev, types, args);
		}
	}

	// For default event.
	public void OnClick ()
	{
		Type[] types = new Type[0];
		object[] args = null;
		foreach (BaseFrameEvent ev in defaultevents.onClickEvents) {
			execute (ev, types, args);
		}
	}

	public void OnValueChanged (float v)
	{
		Type[] types = { v.GetType () };
		object[] args = { v };
		foreach (BaseFrameEvent ev in defaultevents.onValueChangedEvents) {
			execute (ev, types, args);
		}
	}

	public void OnValueChanged (bool b)
	{
		Type[] types = { b.GetType () };
		object[] args = { b };
		foreach (BaseFrameEvent ev in defaultevents.onValueChangedEvents) {
			execute (ev, types, args);
		}
	}

	public void OnValueChanged (string s)
	{
		Type[] types = { s.GetType () };
		object[] args = { s };
		foreach (BaseFrameEvent ev in defaultevents.onValueChangedEvents) {
			execute (ev, types, args);
		}
	}

	public void EndEdit (string s)
	{
		Type[] types = { s.GetType () };
		object[] args = { s };
		foreach (BaseFrameEvent ev in defaultevents.endEditEvents) {
			execute (ev, types, args);
		}
	}

	/// <summary>
	/// Raises the json data event.
	/// </summary>
	/// <param name="s">S.</param>
	public void OnJsonData (JsonData s)
	{
		Type[] types = { s.GetType () };
		object[] args = { s };
		foreach (BaseFrameEvent ev in defaultevents.onJsonDataEvents) {
			execute (ev, types, args);
		}
	}

	public void OnPointerEnter (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.pointerEnterEvents) {
			execute (ev, types, args);
		}
	}

	public void OnPointerExit (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.pointerExitEvents) {
			execute (ev, types, args);
		}
	}

	public void OnPointerDown (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.pointerDownEvents) {
			execute (ev, types, args);
		}
	}

	public void OnPointerUp (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.pointerUpEvents) {
			execute (ev, types, args);
		}
	}

	public void OnPointerClick (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.pointerClickEvents) {
			execute (ev, types, args);
		}
	}

	public void OnDrag (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.dragEvents) {
			execute (ev, types, args);
		}
	}

	public void OnDrop (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.dropEvents) {
			execute (ev, types, args);
		}
	}

	public void OnScroll (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.scrollEvents) {
			execute (ev, types, args);
		}
	}

	public void OnUpdateSelected (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.updateSelectedEvents) {
			execute (ev, types, args);
		}
	}

	public void OnSelect (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.selectEvents) {
			execute (ev, types, args);
		}
	}

	public void OnDeselect (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.deselectEvents) {
			execute (ev, types, args);
		}
	}

	public void OnMove (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.moveEvents) {
			execute (ev, types, args);
		}
	}

	public void OnInitializePotentialDrag (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.initializePotentialDragEvents) {
			execute (ev, types, args);
		}
	}

	public void OnBeginDrag (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.beginDragEvents) {
			execute (ev, types, args);
		}
	}

	public void OnEndDrag (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.endDragEvents) {
			execute (ev, types, args);
		}
	}

	public void OnSubmit (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.submitEvents) {
			execute (ev, types, args);
		}
	}

	public void OnCancel (BaseEventData data)
	{
		Type[] types = { data.GetType () };
		object[] args = { data };
		foreach (BaseFrameEvent ev in events.cancelEvents) {
			execute (ev, types, args);
		}
	}

}
