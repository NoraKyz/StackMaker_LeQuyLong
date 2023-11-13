using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    #endregion
    public event Action<EventID> OnEventEmitted; 
    
    #region Unity Functions

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    private void EmitEvent(EventID eventID)
    {
        OnEventEmitted?.Invoke(eventID);
    }
    
    public void EmitOnNextLevelEvent()
    {
        EmitEvent(EventID.OnNextLevel);
    }
    
    public void EmitOnCompleteLevelEvent()
    {
        EmitEvent(EventID.OnCompleteLevel);
    }
}

