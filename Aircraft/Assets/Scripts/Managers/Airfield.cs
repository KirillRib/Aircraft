using UnityEngine;
using System.Collections;

public class Airfield : MonoBehaviour {
    public float Height, Length, MaxSpeed;
    public Transform AlliedAircraft, Respawn;
    private Rigidbody2D RB2DAlliedAircraft;
    private Aircrafts Aircraft;
    void Start ()
    {
        RB2DAlliedAircraft = AlliedAircraft.gameObject.GetComponent<Rigidbody2D>();
        Aircraft = AlliedAircraft.gameObject.GetComponent<Aircrafts>();
    }
	

	void Update ()
    {
        if ((AlliedAircraft.position.x > transform.position.x && AlliedAircraft.position.y < transform.position.y + Height)
            && (AlliedAircraft.position.y > transform.position.y && AlliedAircraft.position.x < transform.position.x + Length))
        {
            if (MaxSpeed * MaxSpeed > RB2DAlliedAircraft.velocity.sqrMagnitude && Aircraft.Health < 7.55)
                Aircraft.Resurrection();
        }
	}

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * Height);
        Gizmos.DrawLine(transform.position + Vector3.up * Height, transform.position + Vector3.up * Height + Vector3.right * Length);
        Gizmos.DrawLine(transform.position + Vector3.up * Height + Vector3.right * Length, transform.position + Vector3.right * Length);
        Gizmos.DrawLine(this.transform.position, transform.position + Vector3.right * Length);
        Gizmos.DrawWireSphere(Respawn.transform.position, 1);
    }
#endif  
}
