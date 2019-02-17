 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnchor : MonoBehaviour {

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject grid_anchor;
    [SerializeField]
    GameObject vector_anchor;
    [SerializeField]
    GameObject coordinate_anchor;
    [SerializeField]
    GameObject reset_anchor;

    InfoAnchor grid, vector, coordinate, reset;

    public Transform target
    {
        get
        {
            return player.transform;
        }
    }


	// Use this for initialization
	void Start () {
        grid = grid_anchor.GetComponent<InfoAnchor>();
        vector = vector_anchor.GetComponent<InfoAnchor>();
        coordinate = coordinate_anchor.GetComponent<InfoAnchor>();
        reset = reset_anchor.GetComponent<InfoAnchor>();
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(target);
        if(GetHold())
        {
            CreateMenu();
        }else if(!GetHold())
        {
            DeleteMenu();
        }
	}

    bool GetHold()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CreateMenu()
    {
        grid.CreateInfoObject();
        vector.CreateInfoObject();
        coordinate.CreateInfoObject();
        reset.CreateInfoObject();
    }

    void DeleteMenu()
    {
        grid.DeleteInfoObject();
        vector.DeleteInfoObject();
        coordinate.DeleteInfoObject();
        reset.DeleteInfoObject();
    }
}
