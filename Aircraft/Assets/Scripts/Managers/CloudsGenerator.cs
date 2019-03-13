using UnityEngine;
using System.Collections;

public class CloudsGenerator : MonoBehaviour {
    public Vector2 StartPoint, EndPoint;
    public float MaxSpeed = 1;
    public GameObject mCloud;
    public int NClouds = 100;


    void Start () {
        for (int i = 0; i < NClouds; i++)
        {
            GameObject newCloud = Instantiate(mCloud);
            newCloud.transform.SetParent(transform);
            newCloud.transform.position = new Vector2(Random.Range(StartPoint.x, EndPoint.x), Random.Range(StartPoint.y, EndPoint.y));
            newCloud.GetComponent<Cloud>().Speed = new Vector3(Mathf.Pow(-1, (int)Random.Range(1, 3)) * MaxSpeed, 0, 0);
        }
    }
#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(StartPoint, EndPoint);
        Gizmos.DrawLine(EndPoint, StartPoint);
    }
#endif
}
