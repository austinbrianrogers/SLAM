using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableObject : MonoBehaviour
{
    private void OnEnable()
    {
        MakePlayer();
    }
    public void MakePlayer()
    {
        m_player = new ModulePlayer();
        m_player.RegisterObjectAsPlayer(gameObject);
    }

    ModulePlayer m_player = null;
}
