using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retry : MonoBehaviour {

    Vector3 start_position;

	// Use this for initialization
	void Start () {
        start_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            transform.position = start_position;
        }
    }
}
