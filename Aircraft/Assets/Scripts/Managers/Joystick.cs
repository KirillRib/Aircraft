using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Joystick : MonoBehaviour {
    private int _moveTouchId;
    private Vector2 _moveTouchPos;
    public RectTransform Big, Little;
    public ControlManagers pControlManagers;
    public float MaxJoyDiff = 100;
    private bool _moveAreaTouched;


    void Start ()
    {

	}
	

	void Update ()
    {
        MovePlayer();

    }

    public void OnSwitchMoveArea(bool touched)
    {
        if (Application.isEditor)
            return;
        if (touched)
        {
            _moveTouchId = Input.touchCount - 1;
            _moveTouchPos = Input.GetTouch(Input.touchCount - 1).position;
            Big.localPosition = _moveTouchPos;
            Little.localPosition = _moveTouchPos;
        }
        else
        {
            pControlManagers.SetDirectionHeightSteering(0);
        }
        Big.gameObject.SetActive(touched);
        Little.gameObject.SetActive(touched);
        _moveAreaTouched = touched;
    }
    void MovePlayer()
    {
        if (!_moveAreaTouched || _moveTouchId >= Input.touchCount)
            return;
        

        float xTouch = Input.GetTouch(_moveTouchId).position.x;
        float xDiff = xTouch - _moveTouchPos.x;
        if (Mathf.Abs(xDiff) < MaxJoyDiff)
        {
            Little.localPosition = new Vector2(xTouch, _moveTouchPos.y);
            pControlManagers.SetDirectionHeightSteering(xDiff * 10 / MaxJoyDiff);
        }
    }
}
