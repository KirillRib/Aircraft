using UnityEngine;
using System.Collections;


public class ufo : MonoBehaviour {
    public GameObject UFO;
    public GameObject Aircraft;
    public int UFO_Height = 110;
    public int SearchHeight = 100;
    void Start () {
        transform.position = new Vector3(0, UFO_Height, 0);

    }
    
    void Update()
    {
        transform.position = new Vector3(Aircraft.transform.position.x, UFO_Height, 0);

        if (Aircraft.transform.position.y > SearchHeight)
        {
            Aircraft.GetComponent<Aircrafts>().MakeDamage(100);
        }
    }
}
