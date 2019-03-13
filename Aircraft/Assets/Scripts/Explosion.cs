using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    public float timer = 1f;


	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }

    public void OnDisable()
    {
        if (this.enabled)
        {
            Destroy(gameObject);
            // Do stuff.
        }
    }
}
