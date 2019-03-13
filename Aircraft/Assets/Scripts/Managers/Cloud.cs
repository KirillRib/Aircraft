using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
    public Vector3 Speed;


	void Update () {
        transform.position += Speed;

    }  
}
