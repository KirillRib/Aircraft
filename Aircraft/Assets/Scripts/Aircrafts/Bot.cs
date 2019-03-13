using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {
    public GameObject Enemy;
    public ControlManagers ControlManager;

    void Start () {
        ControlManager.EngineThrust = 10;

    }
	

	void Update () {
        MovementToPoint(Enemy.transform);

    }

    public float zAngle;
    public void MovementToPoint(Transform _Transform)
    {
        Vector2 dir = _Transform.position - transform.position;
        dir.Normalize();

        //zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //zAngle = zAngle - transform.eulerAngles.z;

        float Angle = (transform.localRotation.eulerAngles.z) / 180 * Mathf.PI;
        Vector2 Route = -new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));
        zAngle = Mathf.Acos(dir.x * Route.x + dir.y * Route.y);
        if ((dir + Route).sqrMagnitude < 2)
            zAngle = Mathf.PI - zAngle;
        if (dir.y * Route.x - dir.x * Route.y < 0)
            zAngle *= -1;
        if (float.IsNaN(zAngle))
            zAngle = 0;

        if (Mathf.Abs(zAngle) > 0.3)
            if (zAngle < 0)
                ControlManager.SetDirectionHeightSteering(-10);
            else
                ControlManager.SetDirectionHeightSteering(10);
        else
            ControlManager.SetDirectionHeightSteering(0);

        if (Mathf.Abs(zAngle) < 0.35)
            ControlManager.ActivateFire();
        else
            ControlManager.DeactivateFire();
        //Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);
    }
}
