using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grit : MonoBehaviour {

    [SerializeField]
    GameObject grit_model;

    private Quaternion grit_rotation_;
    private Vector3 grit_position_;
    private GameObject grit_object;
	private Trajectory trajectory;

    public Vector3 position
    {
        get
        {
            return grit_position_;
        }
        set
        {
            grit_position_ = value;
        }
    }

    public Quaternion rotation
    {
        get
        {
            return grit_rotation_;
        }
        set
        {
            grit_rotation_ = value;
        }
    }

    public bool find_grit
    {
        get
        {
            if (GameObject.Find("Grit") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

	// Use this for initialization
	void Start () {
		trajectory = GameObject.Find("TrajectoryParent").GetComponent<Trajectory>();
	}
	
	// Update is called once per frame
	void Update () {
		if (trajectory.trajectory_list.Count <= 0 && find_grit) {
			DeleteGrit ();
		}
		
        /*if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            if (find_grit)
            {
                DeleteGrit();
            }
            else
            {
                CreateGrit();
            }
        }*/
	}

    public void CreateGrit()
    {
        if (!find_grit)
        {
            grit_object = Instantiate(grit_model, position, rotation);
            grit_object.name = "Grit";
        }

    }

    public void DeleteGrit()
    {
        if (find_grit)
        {
            Destroy(grit_object);
        }

    }
}
