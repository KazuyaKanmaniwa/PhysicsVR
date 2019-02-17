using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour {

    InfoObject info_object;
	DeleteInfo delete_info;
    Trajectory trajectory;
    Coordinate coordinate;
    Vector vector;
    Grit grid;

    public GameObject trajectory_controll
    {
        get
        {
            return GameObject.Find("TrajectoryControll");
        }
    }
    

	// Use this for initialization
	void Start () {
        trajectory = GameObject.Find("TrajectoryParent").GetComponent<Trajectory>();
        coordinate = trajectory_controll.GetComponent<Coordinate>();
        vector = trajectory_controll.GetComponent<Vector>();
        grid = trajectory_controll.GetComponent<Grit>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchAwake(bool state)
    {
        this.gameObject.SetActive(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "InfoObject")
        {
            return;
        }

        info_object = other.GetComponent<InfoObject>();
		if (info_object == null) {
			delete_info = other.GetComponent<DeleteInfo> ();
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "InfoObject")
        {
            return;
        }

        info_object = null;
		delete_info = null;
    }

    public void SetInfoObject()
    {
		if (info_object != null) {
			switch (info_object.my_type) {
			case (InfoObject.InfoType)0: //Grid
				grid.CreateGrit ();
				break;
			case (InfoObject.InfoType)1: //Vector
				vector.CreateVector ();
				break;
			case (InfoObject.InfoType)2: //Coordinate
				coordinate.CreateCoordinate ();
				break;
			case (InfoObject.InfoType)3: //Reset
				trajectory.ResetTrajectory ();
				break;
			}
		}
			
		if (delete_info != null) {
			switch (delete_info.my_type) {
			case (DeleteInfo.InfoType)4:
				grid.DeleteGrit ();
				break;
			case (DeleteInfo.InfoType)5:
				vector.DeleteVector ();
				break;
			case (DeleteInfo.InfoType)6:
				coordinate.DeleteCoordinate ();
				break;
			}
		}
    }
}
