using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Airship : MonoBehaviour
{
    public float pathLength = 100;
    public float Speed = 1;
    public bool LeftDirection = true;


    private GameObject AirshipImg;
    private Text textCounter;

    private Vector3 startingPosition = new Vector3();
    private float Airship_Height = 0;
    private float LeftPoint = 0;
    private float RightPoint = 0;

    void Start()
    {
        startingPosition = transform.position;

        LeftPoint = startingPosition.x - pathLength / 2;
        RightPoint = startingPosition.x + pathLength / 2;
        Airship_Height = startingPosition.y;

        AirshipImg = this.gameObject.transform.GetChild(1).gameObject;
        textCounter = this.gameObject.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
        setCount(0, 0);
    }


    void Update()
    {
        transform.position = new Vector3(this.transform.position.x, Airship_Height, 0);


        if (LeftDirection && this.transform.position.x < LeftPoint)
        {
            AirshipImg.transform.Rotate(new Vector3(0, 180, 0));
            LeftDirection = !LeftDirection;
        }

        if (!LeftDirection && this.transform.position.x > RightPoint)
        {
            AirshipImg.transform.Rotate(new Vector3(0, 180, 0));
            LeftDirection = !LeftDirection;
        }

        if (LeftDirection)
            this.transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
        else
            this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);

    }
    public void setCount(int A, int B)
    {
        textCounter.text = A+":"+B;
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector2(LeftPoint, Airship_Height - 10), new Vector2(LeftPoint, Airship_Height + 10));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(RightPoint, Airship_Height - 10), new Vector2(RightPoint, Airship_Height + 10));
    }
#endif
}
