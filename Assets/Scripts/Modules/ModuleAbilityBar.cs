using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ModuleAbilityBar : Module
{
    //callbacks
    public delegate void OnTimerElapsed();

    public ModuleAbilityBar()
    {
        ID = Strings.MODULE_ABILITYBAR;
        _onCreate();
    }

    public override void InternalUpdate()
    {
        foreach(AbilityTimer timer in m_timers.Values)
        {
            timer.Elapse();
        }
    }

    public void MakeTimer(string name, uint period, OnTimerElapsed callback)
    {
        var timer = new AbilityTimer(period, callback);
        m_timers.Add(name, timer);
    }

    public void RemoveTimer(string name)
    {
        m_timers[name].Delete();
        m_timers.Remove(name);
    }

    protected override void _onCreate()
    {
        throw new System.NotImplementedException();
    }

    protected override void _onDelete()
    {
        throw new System.NotImplementedException();
    }

    class AbilityTimer
    {
        public AbilityTimer(uint period, OnTimerElapsed callback)
        {
            m_period = period;
            elapsedCallback += callback;
        }

        public void Elapse()
        {
            if(!m_paused)
                m_elapsed += Time.deltaTime;

            if (m_elapsed >= m_period)
                _complete();
        }

        public void Delete()
        {
            //clear the list of listeners
            elapsedCallback = null;
        }

        public void Subscribe(OnTimerElapsed callback)
        {
            elapsedCallback += callback;
        }

        public void Pause()
        {
            m_paused = true;
        }
        /// <summary>
        /// Force complete and start it over.
        /// </summary>
        public void StartNew()
        {
            _complete();
            m_paused = false;
            m_elapsed = 0;
        }

        void _complete()
        {
            Pause();
            elapsedCallback?.Invoke();
        }

        //members
        //callbacks
        event OnTimerElapsed elapsedCallback;
        //init
        uint m_period;
        //tracking
        float m_elapsed = 0f;
        bool m_paused = false;
    }

    //members
    Dictionary<string, AbilityTimer> m_timers = new Dictionary<string, AbilityTimer>();
}
