using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    GameObject trajectory_ball;
    [SerializeField]
    GameObject trajectory_parent;

    private bool start_trajectory = false;
    private float interval_max = 0.06f;
    private float interval_time;
    public Vector3 trajectory_position;
    public Rigidbody use_gravity;
    TrajectoryController trajectory_controller;

    public Vector3 velocity
    {
        get
        {
            return this.GetComponent<Rigidbody>().velocity;
        }
    }

	// Use this for initialization
	void Start () {
        interval_time = interval_max;
        use_gravity = this.GetComponent<Rigidbody>();
		if (trajectory_parent == null) {
			trajectory_parent = GameObject.Find ("TrajectoryParent");
		}
		trajectory_controller = GameObject.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor/TrajectoryController").GetComponent<TrajectoryController>();
    }
	
	// Update is called once per frame
	void Update () {
        interval_time -= Time.deltaTime;
        if (interval_time < 0 && start_trajectory)
        {
            interval_time = interval_max;
            Create_trajectory();
        }

        if (gameObject.transform.position.y <= 0.2)
        {
            Destroy(gameObject);
        }
	}

    void Create_trajectory()
    {
        trajectory_position = transform.position;
        var trajectory = Instantiate(trajectory_ball, trajectory_position, Quaternion.identity);
        GameObject.Find("TrajectoryParent").GetComponent<Trajectory>().AddTrajectoryList(trajectory_position, velocity);
        trajectory.transform.parent = trajectory_parent.transform;
    }

    public void Start_trajectory()
    {
        GameObject.Find("TrajectoryControll").GetComponent<Grit>().position = transform.position;
        start_trajectory = true;
        trajectory_controller.SwitchAwake(true);

    }

    public void SwitchGravity()
    {
        if (use_gravity.useGravity)
        {
            use_gravity.useGravity = false;
        }
        else
        {
            use_gravity.useGravity = true;
        }
    }
}
