using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOfView : MonoBehaviour
{
    private Vector3 centerPoint;
    private Vector3 viewPointCenter;
    private Vector3 viewPointRight;
    private Vector3 viewPointLeft;
    private Vector3 viewPointTop;
    private List<Vector3[]> trajectoryList;
    private static int positionData = 0;

    private enum ViewPosition
    {
        Center,Right,Left,Top
    }

    private ViewPosition nowViewPosition;
    private bool canMoveView;

    // Start is called before the first frame update
    void Start()
    {
        trajectoryList = GameObject.Find("TrajectoryParent").GetComponent<Trajectory>().trajectory_list;
        nowViewPosition = ViewPosition.Center;
        canMoveView = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            SetCentorPoint();
            SetViewPoint();
            canMoveView = true;
        }

        if (canMoveView)
        {
            MoveView();
        }
    }

    void SetCentorPoint()
    {
        var firstBall = trajectoryList[0];
        var lastBall = trajectoryList[trajectoryList.Count - 1];

        centerPoint = new Vector3((lastBall[positionData].x + firstBall[positionData].x) / 2, 0, (lastBall[positionData].z + firstBall[positionData].z) / 2);
    }

    void SetViewPoint()
    {
        var firstBall = trajectoryList[0];
        var tallPoint = firstBall;
        foreach(Vector3[] clone in trajectoryList)
        {
            if (clone[positionData].y > tallPoint[positionData].y)
            {
                tallPoint = clone;
            }
        }

        viewPointCenter = new Vector3(firstBall[positionData].x + 1, 1.6f, firstBall[positionData].z);
        viewPointRight = new Vector3(centerPoint.x, 1.6f, centerPoint.z + 1.5f);
        viewPointLeft = new Vector3(centerPoint.x, 1.6f, centerPoint.z - 1.5f);
        viewPointTop = new Vector3(centerPoint.x, tallPoint[positionData].y + 5f, centerPoint.z);
    }

    void TestView()
    {
        SetCentorPoint();
        Debug.Log("End SetCentorPoint()");
        SetViewPoint();
        Debug.Log("End ViewPoint()");

        GameObject testCenterPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        testCenterPoint.transform.position = new Vector3(centerPoint.x, centerPoint.y, centerPoint.z);
        Debug.Log("End testCenter");
        GameObject centerView = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        centerView.transform.position = new Vector3(viewPointCenter.x, viewPointCenter.y, viewPointCenter.z);
        Debug.Log("End testCenterView");
        GameObject rightView = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        rightView.transform.position = new Vector3(viewPointRight.x, viewPointRight.y, viewPointRight.z);
        Debug.Log("End testRightView");
        GameObject leftView = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        leftView.transform.position = new Vector3(viewPointLeft.x, viewPointLeft.y, viewPointLeft.z);
        Debug.Log("End testLeftView");
        GameObject topView = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        topView.transform.position = new Vector3(viewPointTop.x, viewPointTop.y, viewPointTop.z);
        Debug.Log("End testTopView");
    }

    void MoveView()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickUp))
        {
            Debug.Log("右アナログスティックを上に倒した");
            if (nowViewPosition != ViewPosition.Top)
            {
                transform.position = viewPointTop;
                nowViewPosition = ViewPosition.Top;
            }
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickDown))
        {
            Debug.Log("右アナログスティックを下に倒した");
            if (nowViewPosition != ViewPosition.Center)
            {
                transform.position = viewPointCenter;
                nowViewPosition = ViewPosition.Center;
            }
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft))
        {
            Debug.Log("右アナログスティックを左に倒した");
            if (nowViewPosition == ViewPosition.Left || nowViewPosition == ViewPosition.Top)
                return;

            if (nowViewPosition == ViewPosition.Center)
            {
                transform.position = viewPointLeft;
                nowViewPosition = ViewPosition.Left;
            }else if (nowViewPosition == ViewPosition.Right)
            {
                transform.position = viewPointCenter;
                nowViewPosition = ViewPosition.Center;
            }
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight))
        {
            Debug.Log("右アナログスティックを右に倒した");
            if (nowViewPosition == ViewPosition.Right || nowViewPosition == ViewPosition.Top)
                return;

            if (nowViewPosition == ViewPosition.Center)
            {
                transform.position = viewPointRight;
                nowViewPosition = ViewPosition.Right;
            }
            else if (nowViewPosition == ViewPosition.Left)
            {
                transform.position = viewPointCenter;
                nowViewPosition = ViewPosition.Center;
            }
        }
    }
}
