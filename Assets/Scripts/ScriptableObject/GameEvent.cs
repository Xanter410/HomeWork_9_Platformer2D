using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent_", menuName = "ScriptableObjects/Game Event", order = 1)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();
    private UnityAction _listenerActions;
    public void TriggerEvent()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTriggered();
        }
        _listenerActions?.Invoke();
    }
    public void AddListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }
    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
    public void AddListener(UnityAction callback)
    {
        _listenerActions += callback;
    }
    public void RemoveListener(UnityAction callback)
    {
        _listenerActions -= callback;
    }
}
