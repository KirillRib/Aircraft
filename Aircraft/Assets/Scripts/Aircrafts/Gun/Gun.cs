using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    public bool Fire = false;

    [Header("Settings")]
    public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
    public GameObject bulletPrefab;
    public float fireDelay = 0.25f;
    public float Scatter = 0.1f;
    public AudioClip Shot;
    public AudioSource mAudioSource;

    int bulletLayer;
    float cooldownTimer = 0;


    void Start ()
    {
        bulletLayer = gameObject.layer;
    }
	

	void Update ()
    {
        cooldownTimer -= Time.deltaTime;

        if (Fire && cooldownTimer <= 0)
        {
            // SHOOT!
            cooldownTimer = fireDelay;

            Vector3 offset = transform.rotation * bulletOffset;

            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
            bulletGO.transform.eulerAngles += new Vector3(0, 0, Random.Range(-Scatter, Scatter));
            //bulletGO.layer = bulletLayer;
            mAudioSource.clip = Shot;
            mAudioSource.Play();
        }
    }
}
