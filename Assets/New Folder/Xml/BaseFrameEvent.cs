using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[Serializable]
public class BaseFrameEvent {

    public string gameObjectName = null;
    public string componentName = null;
    public string methodName = null;
    
    public BaseFrameEvent(string goName, string cpName, string funcName) {
        gameObjectName = goName;
        componentName = cpName;
        methodName = funcName;
    }

    public override string ToString() {
        return string.Format ("BaseFrameEvent {0}.{1}.{2}", gameObjectName, componentName, methodName);
    }
}

[Serializable]
public class BaseFrameEventList {
	//Event trigger
    public List<BaseFrameEvent> pointerEnterEvents  = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> pointerExitEvents   = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> pointerDownEvents   = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> pointerUpEvents     = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> pointerClickEvents  = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> dragEvents          = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> dropEvents          = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> scrollEvents        = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> updateSelectedEvents = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> selectEvents        = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> deselectEvents      = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> moveEvents          = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> initializePotentialDragEvents = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> beginDragEvents     = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> endDragEvents       = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> submitEvents        = new List<BaseFrameEvent>();
    public List<BaseFrameEvent> cancelEvents        = new List<BaseFrameEvent>();

    public void copy(BaseFrameEventList src) {
        pointerEnterEvents      = src.pointerEnterEvents;
        pointerExitEvents       = src.pointerExitEvents;
        pointerDownEvents       = src.pointerDownEvents;
        pointerUpEvents         = src.pointerUpEvents;
        pointerClickEvents      = src.pointerClickEvents;
        dragEvents              = src.dragEvents;
        dropEvents              = src.dropEvents;
        scrollEvents            = src.scrollEvents;
        updateSelectedEvents    = src.updateSelectedEvents;
        selectEvents            = src.selectEvents;
        deselectEvents          = src.deselectEvents;
        moveEvents              = src.moveEvents;
        initializePotentialDragEvents = src.initializePotentialDragEvents;
        beginDragEvents         = src.beginDragEvents;
        endDragEvents           = src.endDragEvents;
        submitEvents            = src.submitEvents;
        cancelEvents            = src.cancelEvents;
    }

    public bool isEmpty() {
        return (pointerEnterEvents.Count + pointerExitEvents.Count +
                pointerDownEvents.Count + pointerUpEvents.Count +
                pointerClickEvents.Count + dragEvents.Count +
                dropEvents.Count + scrollEvents.Count + updateSelectedEvents.Count +
                selectEvents.Count + deselectEvents.Count + 
                moveEvents.Count + initializePotentialDragEvents.Count + 
                beginDragEvents.Count + endDragEvents.Count + 
                submitEvents.Count + cancelEvents.Count) == 0;
    }
}
       
[Serializable]
public class DefaultBaseFrameEventList {
	
	//Button
	public List<BaseFrameEvent> onClickEvents  			= new List<BaseFrameEvent>();
	//Slider,Scrollbar,Toggle,InputField
	public List<BaseFrameEvent> onValueChangedEvents   	= new List<BaseFrameEvent>();
	//InputField
	public List<BaseFrameEvent> endEditEvents   		= new List<BaseFrameEvent>();

	public List<BaseFrameEvent> onJsonDataEvents   		= new List<BaseFrameEvent>();
	
	public void copy(DefaultBaseFrameEventList src) {
		onClickEvents      		= src.onClickEvents;
		onValueChangedEvents    = src.onValueChangedEvents;
		endEditEvents       	= src.endEditEvents;
	}
	
	public bool isEmpty() {
		return (onClickEvents.Count + onValueChangedEvents.Count + endEditEvents.Count ) == 0;
	}
}


[Serializable]
public class CustomerEventList {
	
	public List<BaseFrameEvent> onClickEvents  			= new List<BaseFrameEvent>();
	public List<BaseFrameEvent> onSubmitEvents   		= new List<BaseFrameEvent>();
	
	public void copy(CustomerEventList src) {
		onClickEvents      		= src.onClickEvents;
		onSubmitEvents   	  	= src.onSubmitEvents;
	}
	
	public bool isEmpty() {
		return (onClickEvents.Count + onSubmitEvents.Count ) == 0;
	}
}


public class NullGameObjectException: Exception {
}

public class NullComponentException: Exception {
}

public class NullMethodInfoException: Exception {
}

public static class Finder {

    public static GameObject findGameObject(string name) {
        var rc = GameObject.Find(name);
        if(rc==null) throw new NullGameObjectException();
        return rc;
    }

    public static Component findComponent(GameObject obj, string name) {
        var rc = obj.GetComponent(name);
        if(rc==null) throw new NullComponentException();
        return rc;
    }
    
//    public static MethodInfo findMethodInfo(Component cp, string name, BaseEventData data ) {
//        Type[] types = {data.GetType()};
//        var method = UnityEventBase.GetValidMethodInfo((object)cp, name, types);
//        if(method==null) throw new NullMethodInfoException();
//        return method;
//    }

	public static MethodInfo findMethodInfo(Component cp, string name, Type[] types ) {
		var method = UnityEventBase.GetValidMethodInfo((object)cp, name, types);
		if(method==null) throw new NullMethodInfoException();
		return method;
	}

}