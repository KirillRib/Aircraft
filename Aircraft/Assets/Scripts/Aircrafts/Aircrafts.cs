using UnityEngine;
using System.Collections;

public class Aircrafts : MonoBehaviour {
    protected Rigidbody2D _Rigidbody2D;


    void Start () {
        if (_CameraManager)
            _CameraManager.SetTarget(this.transform, false);
        _Rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    public bool isDead = false, isDestroy = false;
    public float Health = 10f;
    public float Speed = 0;
    public float AerodynamicForce = 0;//От -1 до 1
    public float EngineThrust = 0;//Тяга двигателя (0-1)
    public float CoefficientStabilization = 0.5f;
    public CameraManager _CameraManager;
    public GameObject Explosion;
    public Animator mAnimator;
    public Airfield mAirfield;

    [Header("Settings")]
    public float MaxSpeed = 10;
    public float GravityCoefficient = 1;
    public float ControllabilityCoefficient = 3;
    public float CruisingSpeed = 4;
    public float EnginePower = -1;
    public float AerodynamicPower = 1.5f;
    public float PCoefficientResistanceLinear = 10f;
    public float PCoefficientCornerResistance = 0.9f;
    public float PCoefficientGravity = -1.2f;
    public bool Inverse = false;



    void FixedUpdate ()
    {
        CoefficientStabilization = Mathf.Atan((Speed - CruisingSpeed) * ControllabilityCoefficient) / Mathf.PI + 0.5f;

        //Текущая скорость
        Speed = Mathf.Sqrt(_Rigidbody2D.velocity.sqrMagnitude);
        //Стабилизирующая сила
        Stabilization();
        //Линейные силы
        AddLineForces();

        //_Rigidbody2D.drag = Speed / MaxSpeed;
    }

    void Update()
    {
        if (_CameraManager && !isDestroy)
        {
            _CameraManager.SetOrthoSize(Speed + 12);
            _CameraManager.SetHeight(transform.position.y);
        }
            
    }

    protected void AddLineForces()
    {
        //Сила тяжести
        _Rigidbody2D.gravityScale = Mathf.Pow(Speed/MaxSpeed + 1, PCoefficientGravity);
        //Корректирующая сила + сила тяги двигателя
        _Rigidbody2D.AddRelativeForce(new Vector2(EnginePower * EngineThrust, CoefficientStabilization * AerodynamicForce * Speed * Speed / 50));
        //Линейное сопротивление
        _Rigidbody2D.drag = Mathf.Pow(Speed / MaxSpeed, PCoefficientResistanceLinear) * 1.5f + 0.1f;
    }
    protected void Stabilization()
    {
        float Fors = 0;
        Vector2 Velocity = _Rigidbody2D.velocity;
        Velocity.Normalize();
        float Angle = (transform.localRotation.eulerAngles.z) / 180 * Mathf.PI;
        Vector2 Route;
        if (Inverse)
            Route = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));
        else
            Route = -new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));
        Fors = Mathf.Acos(Velocity.x * Route.x + Velocity.y * Route.y);
        if ((Velocity + Route).sqrMagnitude < 2)
            Fors = Mathf.PI - Fors;
        if (Velocity.y * Route.x - Velocity.x * Route.y < 0)
            Fors *= -1;
        if (float.IsNaN( Fors ))
            Fors = 0;
        //Debug.Log("S "+Fors+" " + _Rigidbody2D.angularVelocity);
        Fors = (Fors / 2 - _Rigidbody2D.angularVelocity * Time.fixedDeltaTime / 180 * Mathf.PI) / Time.fixedDeltaTime;
        //Debug.Log(Fors);
        //_Rigidbody2D.angularVelocity* Time.fixedDeltaTime
        _Rigidbody2D.AddTorque(Fors * CoefficientStabilization, ForceMode2D.Force);
    }

    //Управление самолетом
    public void SetEngineThrust(float newEngineThrust)
    {
        if (Inverse)
            EngineThrust = -newEngineThrust;
        else
            EngineThrust = newEngineThrust;
    }
    public void SetAerodynamicForce(float newAerodynamicForce)
    {
        if (Inverse)
            AerodynamicForce = -newAerodynamicForce;
        else
            AerodynamicForce = newAerodynamicForce;
    }
    public void MakeDamage(float Damage)
    {
        Health -= Damage;
        mAnimator.SetTrigger("Damage");
        mAnimator.SetFloat("Health", Health);
        if (Health <= 0)
            OnDeath();
    }
    public void OnDeath()
    {
        isDead = true;
    }
    IEnumerator MyOnDestroy()
    {
        if (isDestroy)
            yield break;
        isDestroy = true;
        Instantiate(Explosion).transform.position = new Vector2(transform.position.x, 0);
        mAnimator.SetBool("Destroy", true);
        yield return new WaitForSeconds(1f);
        Resurrection();
        yield break;
    }
    public void Resurrection()
    {
        transform.position = mAirfield.Respawn.position;
        transform.rotation = mAirfield.Respawn.rotation;
        StartCoroutine(AnimatorResurrection());
        Health = 10;
        mAnimator.SetFloat("Health", Health);
        isDead = false;
        isDestroy = false;
    }
    IEnumerator AnimatorResurrection()
    {
        mAnimator.SetBool("Destroy", false);
        mAnimator.SetBool("Resurrection", true);
        yield return new WaitForSeconds(1f);
        mAnimator.SetBool("Resurrection", false);
        yield break;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.relativeVelocity.y < -7 || isDead)
        {
            MakeDamage(100);
            StartCoroutine(MyOnDestroy());
        }
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        Vector2 Velocity = _Rigidbody2D.velocity;
        Vector2 Route = -new Vector2(Mathf.Cos((transform.localRotation.eulerAngles.z) / 180 * Mathf.PI), Mathf.Sin((transform.localRotation.eulerAngles.z) / 180 * Mathf.PI));
        Vector2 RouteS = -new Vector2(Mathf.Cos((transform.localRotation.eulerAngles.z + _Rigidbody2D.angularVelocity * Time.fixedDeltaTime * CoefficientStabilization) / 180 * Mathf.PI), Mathf.Sin((transform.localRotation.eulerAngles.z + _Rigidbody2D.angularVelocity * Time.fixedDeltaTime * CoefficientStabilization) / 180 * Mathf.PI));
        Velocity.Normalize();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, transform.position + (Vector3)Velocity);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, transform.position + (Vector3)Route);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, transform.position + (Vector3)RouteS);
    }
#endif  
}
