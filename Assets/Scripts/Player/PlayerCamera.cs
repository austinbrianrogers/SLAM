using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float RotationSensitivty;
    public Vector3 CameraOffset;
    public float FollowDistance;
    private void Awake()
    {
        GameManager.Instance.SetActiveCamera(this);
    }
    void OnEnable()
    {
        _findPlayer();
        StartTracking();
    }

    public void StartTracking()
    {
        transform.LookAt(m_currentTarget);
        transform.position = m_currentTarget.position + CameraOffset;
        _getCamVector();
        m_tracking = true;
    }

    public void StopTracking()
    {
        transform.SetParent(null);
        m_tracking = false;
    }
    private void Update()
    {
        if (m_tracking)
        {
            //cam position
            transform.position = m_currentTarget.position - m_lastCamVector;
            //cam angle
            var mouseDelta = Input.GetAxis("Mouse X") * Time.deltaTime;
            transform.RotateAround(m_currentTarget.transform.position, Vector3.up, mouseDelta * RotationSensitivty);
            _getCamVector();
            transform.LookAt(m_currentTarget);
        }
    }

    void _getCamVector()
    {
        var vec = m_currentTarget.position - transform.position;
        vec.Normalize();
        vec *= FollowDistance;
        m_lastCamVector = vec;
    }

    private void _findPlayer()
    {
        m_currentTarget = GameManager.Instance.GetPlayer().GetPlayerObject().transform;
    }

    //members
    Transform m_currentTarget;
    bool m_tracking = false;
    //input memory
    Vector3 m_lastCamVector; 
}
