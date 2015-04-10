using System;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CustomerDefinedStyle : MonoBehaviour
{

	protected BaseFrameForUI myUI;
	protected Dictionary<string,string> mydictionary = new Dictionary<string, string> ();
	protected bool ready = false;

	void Start ()
	{
		// for prepare the dictionary.
		if (!ready)
		{
			Init ();
		}
	}

	void Init ()
	{
		myUI = GetComponent<BaseFrameForUI> ();
		if (myUI != null && myUI.baseFrame != null && myUI.baseFrame.customerdefined != null)
		{
			mydictionary = myUI.baseFrame.customerdefined;
			ready = true;
		}
		BuildMyStyle ();
	}

	public virtual void BuildMyStyle ()
	{

	}

	public bool hasProperty (string name)
	{
		if (!ready)
		{
			Init ();
		}
		return mydictionary.ContainsKey (name);
	}

	public string getProperty (string name)
	{
		string value = mydictionary [name];
		return value;
	}

	public class CustomerClickEvent : UnityEvent
	{

	};

	public CustomerClickEvent onCustomerClick = new CustomerClickEvent ();

	public class CustomerSubmitEvent : UnityEvent<string>
	{

	};

	public CustomerSubmitEvent onCustomerSubmit = new CustomerSubmitEvent ();

	public void click ()
	{
		onCustomerClick.Invoke ();
	}

	public void submit (string str)
	{
		onCustomerSubmit.Invoke (str);
	}
}
