using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePlayer : Module
{
    public ModulePlayer()
    {
        ID = Strings.MODULE_PLAYER;
        _onCreate();
    }

    public void RegisterObjectAsPlayer(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>() != null)
        {
            m_playerObject = obj;
            m_playerRigidbody = obj.GetComponent<Rigidbody>();
        }
        else
            Debug.LogError(this.GetType().Name + ": ERROR ~ The object registering to the player does not have a kinematic body. Therefore, the object is not suitable.");
    }

    public GameObject GetPlayerObject()
    {
        return m_playerObject;
    }

    public override void Delete()
    {
        _onDelete();
    }

    public override void InternalUpdate()
    {
        var camForward = GameManager.Instance.Camera().transform.forward;
        camForward.Normalize();
        var forceDirection = new Vector3(camForward.x, 0f, camForward.z);
        Debug.Log("airborn: " + m_airborn.ToString());
        if (!m_airborn)
        {
            if (Input.GetKey(KeyCode.W))
            {
                m_playerRigidbody.AddForce(forceDirection * m_forwardForce);
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_playerRigidbody.AddForce(-forceDirection * m_forwardForce);
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_playerRigidbody.AddForce(-Vector3.Cross(forceDirection, Vector3.up * m_forwardForce));
            }
            if (Input.GetKey(KeyCode.A))
            {
                m_playerRigidbody.AddForce(Vector3.Cross(forceDirection, Vector3.up * m_forwardForce));
            }
        } 
        else
        {
            m_playerRigidbody.AddForce(Vector3.down * m_forwardForce);
        }
    }

    public void SetControlsAirborn(bool enabled)
    {
        m_airborn = enabled;
    }

    public void SetForwardForce(float force)
    {
        m_forwardForce = force;
    }

    protected override void _onCreate()
    {
        GameManager.Instance.RegisterModule(this);
    }

    protected override void _onDelete()
    {
        throw new System.NotImplementedException();
    }

    //members
    //physics
    GameObject m_playerObject;
    Rigidbody m_playerRigidbody;
    //statistics
    float m_forwardForce = 5f;
    //schema
    bool m_airborn = false;
}

