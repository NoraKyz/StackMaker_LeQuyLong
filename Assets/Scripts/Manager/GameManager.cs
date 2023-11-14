using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<EventID> OnEventEmitted; 
    
    private bool enableResetLevel = true;
    
    private void EmitEvent(EventID eventID)
    {
        OnEventEmitted?.Invoke(eventID);
    }
    
    public void EmitNextLevelEvent()
    {
        enableResetLevel = true;
        EmitEvent(EventID.OnNextLevel);
    }
    
    public void EmitCompleteLevelEvent()
    {
        enableResetLevel = false;
        EmitEvent(EventID.OnCompleteLevel);
    }

    public void EmitResetLevelEvent()
    {
        if (!enableResetLevel)
        {
            return;
        }
        EmitEvent(EventID.OnResetLevel);
    }
}

