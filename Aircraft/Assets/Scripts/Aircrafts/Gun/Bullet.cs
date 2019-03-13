using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float maxSpeed = 5f;
    public float timer = 1f;
    public float Damage = 1;

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(maxSpeed * Time.deltaTime, 0, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            coll.gameObject.GetComponent<Aircrafts>().MakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
