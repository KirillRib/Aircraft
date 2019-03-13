using UnityEngine;
using System.Collections;


public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        //Config
        public Vector2 Offset = new Vector2(0, 2);
        

        [Header("References")]
        public Camera GameCamera;
        public float DefaultOrthoSize = 1, FollowSpeed = 1;
        //Process
        public Transform CameraTarget { get; private set; }

        void Awake()
        {
            Instance = this;

            GameCamera.orthographicSize = DefaultOrthoSize;
        }
        
        void FixedUpdate()
        {
            MoveCamera(false);
        }

        public void SetTarget(Transform target, bool followImmediatly)
        {
            CameraTarget = target;

            if (followImmediatly)
                MoveCamera(true);
        }

        public void SetOrthoSize(float Size)
        {
            GameCamera.orthographicSize = GameCamera.orthographicSize + (Size - GameCamera.orthographicSize) / 10;
        }
    public Color NormalColor, HeightColor;
    public void SetHeight(float Height)
    {
        if (Height > 75)
        {
            Height -= 75;
            Height /= 25;
            GameCamera.backgroundColor = NormalColor - (NormalColor - HeightColor) * Height;
            if (Height > 1)
                GameCamera.backgroundColor = HeightColor;
        }
        
    }

    void MoveCamera(bool immediatly)
            {
            if (CameraTarget == null)
                return;
            Vector3 followPos;
            followPos = new Vector3(CameraTarget.position.x, CameraTarget.position.y, 0);
            followPos += new Vector3(Offset.x, Offset.y, 0);
            if (BorderL.position.x > followPos.x)
                followPos = new Vector2(BorderL.position.x, followPos.y);
            if (BorderR.position.x < followPos.x)
                followPos = new Vector2(BorderR.position.x, followPos.y);
            if (!immediatly)
            {
                transform.position = Vector3.Lerp(GameCamera.transform.position, followPos, Time.fixedDeltaTime* FollowSpeed);
            }
            else
            {
                transform.position = followPos;
            }
        }

        public static Camera GetGameCamera()
        {
            return Instance.GameCamera;
        }

        public Transform BorderR, BorderL;
    #if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(new Vector2(BorderR.position.x, -1000), new Vector2(BorderR.position.x, 1000));
            Gizmos.DrawLine(new Vector2(BorderL.position.x, -1000), new Vector2(BorderL.position.x, 1000));
        }
    #endif

}


