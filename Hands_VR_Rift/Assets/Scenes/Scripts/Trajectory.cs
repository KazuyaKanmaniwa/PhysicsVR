using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour {

    //public Vector3[] trajectory_data = new Vector3[2];
    public List<Vector3[]> trajectory_list = new List<Vector3[]>();
	[SerializeField]
	GameObject ball;
	InfoAnchor[] info_anchor;
    private Quaternion TProtation_;

    TrajectoryController trajectory_controller;

	public bool find_trajectory {
		get {
			if (trajectory_list.Count < 0) {
				return false;
			} else {
				return true;
			}
		}
	}

    public Quaternion rotation
    {
        get
        {
            return TProtation_;
        }
        set
        {
            TProtation_ = value;
        }
    }

    public void AddTrajectoryList(Vector3 position,Vector3 velocity)
    {
		var trajectory_data = new Vector3[2];
        trajectory_data[0] = position;
        trajectory_data[1] = velocity;

        trajectory_list.Add(trajectory_data);
    }

	// Use this for initialization
	void Start () {
		info_anchor = new InfoAnchor[4];
		info_anchor[0]=GameObject.Find("OVRCameraRig/TrackingSpace/Vrg Left Grabber/LeftHandAnchor/MenuAnchor/GritAnchor").GetComponent<InfoAnchor>();
		info_anchor[1]=GameObject.Find("OVRCameraRig/TrackingSpace/Vrg Left Grabber/LeftHandAnchor/MenuAnchor/VectorAnchor").GetComponent<InfoAnchor>();
		info_anchor[2]=GameObject.Find("OVRCameraRig/TrackingSpace/Vrg Left Grabber/LeftHandAnchor/MenuAnchor/CoordinateAnchor").GetComponent<InfoAnchor>();
		info_anchor[3]=GameObject.Find("OVRCameraRig/TrackingSpace/Vrg Left Grabber/LeftHandAnchor/MenuAnchor/ResetAnchor").GetComponent<InfoAnchor>();

		trajectory_controller = GameObject.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor/TrajectoryController").GetComponent<TrajectoryController>();
    }
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetDown(OVRInput.RawButton.X))
		{
            AlineTrajectory();
		}
	}

	public void ResetTrajectory(){
        if (find_trajectory)
        {
            foreach (Transform children in gameObject.transform) {
		    	Destroy (children.gameObject);
		    }
		    trajectory_list.Clear ();
			foreach (InfoAnchor clone in info_anchor) {
				clone.can_create = true;
			}
		    var restart_ball = Instantiate (ball, new Vector3 (0.111f, 1.1f, 0.1f), Quaternion.identity);
		    restart_ball.name="Ball";
            trajectory_controller.SwitchAwake(false);
        }
		
	}

    public void AlineTrajectory()
    {
        /*var firstBall = trajectory_list[0];

        this.transform.Translate(firstBall[0].x, 0, firstBall[0].z, Space.World);
        *///移動させる必要があるのかまだわからない
        transform.rotation = Quaternion.identity;
        //Debug.Log(transform.rotation.y);
        transform.rotation = rotation;

    }
}
