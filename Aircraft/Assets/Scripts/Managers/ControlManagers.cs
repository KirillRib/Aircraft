using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlManagers : MonoBehaviour {
    [Header("Settings")]
    public bool Boot;
    public Aircrafts Aircraft;
    public Gun _Gum;
    public float EngineThrust = 0, AerodynamicForce = 0;

    void Start () {
	
	}
	
	void Update() {
        if (Aircraft.isDead)
        {
            DeactivateFire();
            Aircraft.SetEngineThrust(0);
            Aircraft.SetAerodynamicForce(0);
        }
        else
        {
            OnStandaloneInput();
            Aircraft.SetEngineThrust(EngineThrust);
            Aircraft.SetAerodynamicForce(AerodynamicForce);
        }
    }
    void OnStandaloneInput()
    {
        if (Boot || Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer || Aircraft.isDead)
            return;

        if (Input.GetKey(KeyCode.Space))
            ActivateFire();
        else
            DeactivateFire();

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //    SetDirectionHeightSteering();


        if (Input.GetKey(KeyCode.D))
            AerodynamicForce = 10;
        if (Input.GetKey(KeyCode.A))
            AerodynamicForce = -10;
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            AerodynamicForce = 0;

        if (Input.GetKey(KeyCode.W))
            EngineThrust = 10;
        if (Input.GetKey(KeyCode.S))
            EngineThrust = 0;
        if (Input.GetKeyUp(KeyCode.W))
            AerodynamicForce = 0;
    }

    public void SetDirectionHeightSteering(float Direction)
    {
        AerodynamicForce = Direction;
    }
    public void SetEngineThrust(Slider mSlider)
    {
        EngineThrust = mSlider.value / 3 * 10;
    }
    public void ActivateFire()
    {
        if (!Aircraft.isDead)
            _Gum.Fire = true;
        else
            _Gum.Fire = false;
    }
    public void DeactivateFire()
    {
        _Gum.Fire = false;
    }
}
