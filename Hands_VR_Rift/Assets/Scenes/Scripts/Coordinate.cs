using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate : MonoBehaviour {

    [SerializeField]
    GameObject coordinate_panel;
    private Trajectory trajectory;
    private List<GameObject> coordinate_list = new List<GameObject>();

    public bool find_coordinate
    {
        get
        {
            if (coordinate_list.Count > 0)
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
		if (trajectory.trajectory_list.Count <= 0 && find_coordinate) {
			DeleteCoordinate ();
		}
		
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            if (find_coordinate)
            {
                DeleteCoordinate();
            }
            else
            {
                CreateCoordinate();
            }
        }
    }

    public void CreateCoordinate()
    {
        if (!find_coordinate)
        {
            float[] starting_point = new float[2];
            var trajectory_list = trajectory.trajectory_list;
            var trajectory_list_count = trajectory_list.Count;
            for (int count = 0; count < trajectory_list_count; count++)
            {
                var data = trajectory_list[count];
                var instant_position = data[0];
                if (count == 0)
                {
                    starting_point[0] = Mathf.Sqrt(Mathf.Pow(data[0].x, 2) + Mathf.Pow(data[0].z, 2));
                    starting_point[1] = data[0].y;

                }
                var coordinate_score = SetScore(starting_point, instant_position);
                var trajectory_coordinate_panel = Instantiate(coordinate_panel, instant_position, Quaternion.identity);
                var text_mesh = trajectory_coordinate_panel.GetComponentInChildren<TextMesh>();
                text_mesh.text = ("X:" + coordinate_score[0].ToString("F2") + " Y:" + coordinate_score[1].ToString("F2"));
                coordinate_list.Add(trajectory_coordinate_panel);
            }
        }

    }

    public void DeleteCoordinate()
    {
        if (find_coordinate)
        {
            for (int count = 0; count < coordinate_list.Count; count++)
            {
                Destroy(coordinate_list[count]);
            }
            coordinate_list.Clear();
        }

    }

    float[] SetScore(float[] starting_point, Vector3 instant_position)
    {
        float[] coordinate_score = new float[2];
        float[] instant_point = new float[2];
        instant_point[0] = Mathf.Sqrt(Mathf.Pow(instant_position.x, 2) + Mathf.Pow(instant_position.z, 2));
        instant_point[1] = instant_position.y;
        coordinate_score[0] = instant_point[0] - starting_point[0];
        coordinate_score[1] = instant_point[1] - starting_point[1];
        return coordinate_score;
    }
}
