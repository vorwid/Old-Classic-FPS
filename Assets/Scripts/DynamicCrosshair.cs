using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCrosshair : MonoBehaviour
{
    static public float Spread = 0;
    public const int GUN_SHOOTING_SPREAD = 20;
    public const int JUMP_SPREAD = 50;
    public const int WALK_SPREAD = 10;
    public const int RUN_SPREAD = 20;

    public GameObject Crosshair;
    GameObject TopPart;
    GameObject BottomPart;
    GameObject LeftPart;
    GameObject RightPart;

    float InitialPosition;

    private void Start()
    {
        TopPart = Crosshair.transform.Find("TopPart").gameObject;                     //szuka obiektu w swoich podobiektach
        BottomPart = Crosshair.transform.Find("BottomPart").gameObject;
        LeftPart = Crosshair.transform.Find("LeftPart").gameObject;
        RightPart = Crosshair.transform.Find("RightPart").gameObject;

        InitialPosition = TopPart.GetComponent<RectTransform>().localPosition.y;

    }

    private void Update()
    {
        if(Spread != 0)
        {
            TopPart.GetComponent<RectTransform>().localPosition = new Vector3(0, InitialPosition + Spread, 0);
            BottomPart.GetComponent<RectTransform>().localPosition = new Vector3(0, -(InitialPosition + Spread), 0);
            LeftPart.GetComponent<RectTransform>().localPosition = new Vector3(-(InitialPosition + Spread), 0, 0);
            RightPart.GetComponent<RectTransform>().localPosition = new Vector3(InitialPosition + Spread, 0, 0);
            Spread -= 1;
        }
    }
}
