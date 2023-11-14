using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<EventID> OnEventEmitted; 
    private void EmitEvent(EventID eventID)
    {
        OnEventEmitted?.Invoke(eventID);
    }
    
    public void EmitNextLevelEvent()
    {
        EmitEvent(EventID.OnNextLevel);
    }
    
    public void EmitCompleteLevelEvent()
    {
        EmitEvent(EventID.OnCompleteLevel);
    }
}

