using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {
    public Transform[] tAircrafts;
    public Transform ReturnLine;
    public bool Invert;
	

	void Update ()
    {
	    foreach(Transform t in tAircrafts)
        {
            if (Invert == (t.position.x < transform.position.x))
            {
                t.position = new Vector2(ReturnLine.position.x, t.position.y);
            }
        }
	}

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector2(transform.position.x, -1000), new Vector2(transform.position.x, 1000));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(ReturnLine.position.x, -1000), new Vector2(ReturnLine.position.x, 1000));
    }
#endif
}
