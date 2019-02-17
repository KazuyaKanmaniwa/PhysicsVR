using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_hand_controller : MonoBehaviour {

    Animator my_animator;

    private void Awake()
    {
        my_animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update ()
    {
		if(OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            my_animator.SetBool("Idle", false);
            my_animator.SetBool("Fist", true);
            my_animator.SetFloat("FistAmount", 1.0f);
        }
        else
        {
            my_animator.SetBool("Fist", false);
            my_animator.SetBool("Idle", true);
        }
	}
}
