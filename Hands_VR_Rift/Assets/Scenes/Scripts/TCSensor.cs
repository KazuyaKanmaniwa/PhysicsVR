using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCSensor : MonoBehaviour {

	InfoAnchor[] info_anchor; 

	// Use this for initialization
	void Start () {
		info_anchor = new InfoAnchor[4];
		info_anchor[0]=GameObject.Find("OVRCameraRig/TrackingSpace/Vrg Left Grabber/LeftHandAnchor/MenuAnchor/GritAnchor").GetComponent<InfoAnchor>();
		info_anchor[1]=GameObject.Find("OVRCameraRig/TrackingSpace/Vrg Left Grabber/LeftHandAnchor/MenuAnchor/VectorAnchor").GetComponent<InfoAnchor>();
		info_anchor[2]=GameObject.Find("OVRCameraRig/TrackingSpace/Vrg Left Grabber/LeftHandAnchor/MenuAnchor/CoordinateAnchor").GetComponent<InfoAnchor>();
		info_anchor[3]=GameObject.Find("OVRCameraRig/TrackingSpace/Vrg Left Grabber/LeftHandAnchor/MenuAnchor/ResetAnchor").GetComponent<InfoAnchor>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "TrajectoryController")
		{
			return;
		}

		foreach (InfoAnchor clone in info_anchor) {
			clone.into_trajectory_controller = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag != "TrajectoryController")
		{
			return;
		}

		foreach (InfoAnchor clone in info_anchor) {
			clone.into_trajectory_controller = false;
		}
	}
}
