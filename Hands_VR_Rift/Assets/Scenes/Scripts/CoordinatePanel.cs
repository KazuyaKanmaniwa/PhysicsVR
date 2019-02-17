using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatePanel : MonoBehaviour {
    GameObject _player;
	public Transform player{
		get{ 
			return _player.transform;
		}
	}


    // Use this for initialization
    void Start () {
		_player = GameObject.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            Debug.Log("player is null");
            return;
        }
		//Vector3 target = player.position;
		//target.y = this.transform.position.y;
		this.transform.LookAt(player);
	}
}
