using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviour {

    private Rigidbody use_gravity;
    private InfoAnchor parent_anchor;
    public enum InfoType
    {
        Grid,Vector,Coordinate,Reset
    }
    [SerializeField]
    public InfoType my_type;
    TrajectoryController trajectory_controller;

	// Use this for initialization
	void Start () {
        use_gravity = this.GetComponent<Rigidbody>();
        if (this.transform.parent)
        {
            parent_anchor = this.transform.parent.GetComponent<InfoAnchor>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GrabInfoObject()
    {
        if (!this.transform.parent) return;
        this.transform.parent = null;
        parent_anchor.can_create = false;
    }

    public void ReleseInfoObject()
    {
        if (trajectory_controller != null)
        {
            trajectory_controller.SetInfoObject();
			parent_anchor.into_trajectory_controller = true;
        }
        else
        {
            parent_anchor.can_create = true;
        }
        
		Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "TrajectoryController")
        {
            return;
        }

        trajectory_controller = other.GetComponent<TrajectoryController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "TrajectoryController")
        {
            return;
        }

        trajectory_controller = null;
    }
}
