using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float RotationSensitivty;
    public Vector3 Offset;
    void Awake()
    {
        _findPlayer();
        GameManager.Instance.SetActiveCamera(this);
        StartTracking();
    }

    public void StartTracking()
    {
        transform.LookAt(m_currentTarget);
        transform.position = m_currentTarget.position + Offset;
        transform.SetParent(m_currentTarget);
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
            transform.LookAt(m_currentTarget);
        }
    }

    private void _findPlayer()
    {
        m_currentTarget = GameManager.Instance.GetPlayer().GetPlayerObject().transform;
    }

    //members
    Transform m_currentTarget;
    bool m_tracking = false;
}
