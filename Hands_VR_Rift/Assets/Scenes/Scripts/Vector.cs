using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector : MonoBehaviour {

    [SerializeField]
    GameObject vector_pole;
    [SerializeField]
    GameObject vector_cone;
    [SerializeField]
    GameObject vector_parent;
    [SerializeField]
    Material vertical_color;
    [SerializeField]
    Material horizontal_color;

    private Trajectory trajectory;
    private List<GameObject> vector_list = new List<GameObject>();

    public bool find_vector
    {
        get
        {
            if (vector_list.Count > 0)
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
		if (trajectory.trajectory_list.Count <= 0 && find_vector) {
			DeleteVector ();
		}
		
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            if (find_vector)
            {
                DeleteVector();
            }
            else
            {
                CreateVector();
            }
        }


	}

    public void CreateVector()
    {
        if (!find_vector)
        {
            var trajectory_list = trajectory.trajectory_list;
            var trajectory_list_count = trajectory_list.Count;
            for (int count = 0; count < trajectory_list_count; count++)
            {
                var data = trajectory_list[count];
                var position = data[0];
                var velocity = data[1];

                var vector_vertical = velocity.y / 3;
                var vector_horizontal = Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.z, 2)) / 3;

                Quaternion rotation_vertical;
                if (vector_vertical < 0)
                {
                    rotation_vertical = Quaternion.Euler(180, 0, 0);
                    Debug.Log("Test" + rotation_vertical);
                }
                else
                {
                    rotation_vertical = Quaternion.identity;
                }
                var rotation_horizontal = Quaternion.Euler(0, -1 * Mathf.Atan2(velocity.z, velocity.x) * Mathf.Rad2Deg + 180, 90);
                var cone_position = position;


                var vertical_pole = Instantiate(vector_pole, position, rotation_vertical);
                vertical_pole.GetComponentInChildren<Renderer>().material = vertical_color;
                cone_position.y = position.y + (0.2f * vector_vertical);
                var vertical_cone = Instantiate(vector_cone, cone_position, rotation_vertical);
                vertical_cone.GetComponentInChildren<Renderer>().material = vertical_color;
                vertical_pole.transform.localScale = new Vector3(1, Mathf.Abs(vector_vertical), 1);
                vertical_pole.transform.parent = vector_parent.transform;
                vertical_cone.transform.parent = vector_parent.transform;
                vector_list.Add(vertical_pole);
                vector_list.Add(vertical_cone);

                cone_position = position;
                var horizontal_pole = Instantiate(vector_pole, position, rotation_horizontal);
                horizontal_pole.GetComponentInChildren<Renderer>().material = horizontal_color;
                cone_position.x = position.x + velocity.x / 15;
                cone_position.z = position.z + velocity.z / 15;
                var horizontal_cone = Instantiate(vector_cone, cone_position, rotation_horizontal);
                horizontal_cone.GetComponentInChildren<Renderer>().material = horizontal_color;
                horizontal_pole.transform.localScale = new Vector3(1, vector_horizontal, 1);
                horizontal_pole.transform.parent = vector_parent.transform;
                horizontal_cone.transform.parent = vector_parent.transform;
                vector_list.Add(horizontal_pole);
                vector_list.Add(horizontal_cone);
            }

        }
    }

    public void DeleteVector()
    {
        if (find_vector)
        {
            for (int count = 0; count < vector_list.Count; count++)
            {
                Destroy(vector_list[count]);
            }
            vector_list.Clear();
        }

    }
}
