using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoAnchor : MonoBehaviour {
    [SerializeField]
    GameObject info_object;
	[SerializeField]
	GameObject delete_object;

    public bool can_create;
	public bool into_trajectory_controller;


    public bool has_child
    {
        get
        {
            return 0 < this.transform.childCount;
        }
    }

	// Use this for initialization
	void Start () {
        can_create = true;
		into_trajectory_controller = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateInfoObject()
    {
		if (!has_child && can_create) {

			var inst_object = Instantiate (info_object, this.transform);
			inst_object.transform.parent = this.transform;
		}

		if (!has_child && into_trajectory_controller) {
			var inst_object = Instantiate (delete_object, this.transform);
			inst_object.transform.parent = this.transform;
		}
    }

    public void DeleteInfoObject()
    {
        if (has_child)
        {
            foreach(Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
