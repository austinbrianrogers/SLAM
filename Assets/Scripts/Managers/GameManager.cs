using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// Register a module created elsewhere in the game.
    /// </summary>
    /// <param name="mod"></param>
    /// the module you want to register
    /// <param name="overwrite"></param>
    /// will replace any existing mod of this type in the list and return true if true.
    /// will return false if the mod already exists in the list if false
    /// both will still attempt to add the new mod
    /// <returns>
    /// true if it was able to be added and false otherwise
    /// </returns>
    /// 

    public bool RegisterModule(Module mod, bool overwrite = false)
    {
        if(overwrite)
        {
            m_gameModules.Remove(mod.ID);
            m_gameModules.Add(mod.ID, mod);
            return true;
        }
        else
        {
            if (m_gameModules.ContainsKey(mod.ID))
                return false;

            m_gameModules.Add(mod.ID, mod);
            return true;
        }
    }

    public void SetActiveCamera(PlayerCamera camera)
    {
        m_playerCam = camera;
    }

    public ModulePlayer GetPlayer()
    {
        m_gameModules.TryGetValue(Strings.MODULE_PLAYER, out Module modPlayer);
        return (modPlayer == null) ? null : modPlayer as ModulePlayer;
        
    }

    private void Update()
    {
        foreach(var pair in m_gameModules)
        {
            pair.Value.InternalUpdate();
        }
    }

    Dictionary<string, Module> m_gameModules = new Dictionary<string, Module>();
    //globalized objects
    PlayerCamera m_playerCam;
}
