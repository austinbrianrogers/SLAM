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
        if (Input.GetKeyDown(KeyCode.W))
            m_playerRigidbody.AddRelativeForce(Vector3.forward * m_forwardForce);
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
}

