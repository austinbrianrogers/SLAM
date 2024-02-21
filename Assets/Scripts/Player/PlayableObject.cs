using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayableObject : MonoBehaviour
{
    [SerializeField]
    float Force;
    [SerializeField]
    CapsuleCollider Collider;
    private void OnEnable()
    {
        MakePlayer();
        if(Collider == null)
            Collider = GetComponentInChildren<CapsuleCollider>();

        if (Collider == null)
            Debug.LogError(this.name + ": Player object has no capsule collider. This is required for gameplay. Check inspector on the player object and resolve this issue.");
    }
    public void MakePlayer()
    {
        m_player = new ModulePlayer();
        m_player.RegisterObjectAsPlayer(gameObject);
        m_player.SetForwardForce(Force);
    }
    void OnCollisionEnter(Collision collision)
    {
        m_player.SetControlsAirborn(!(collision.gameObject.layer == Strings.LAYER_GROUND));
    }

    void OnCollisionExit(Collision collision)
    {
        m_player.SetControlsAirborn(collision.gameObject.layer == Strings.LAYER_GROUND);
    }

    //members
    //gameplay
    ModulePlayer m_player = null;
    //physics
}
