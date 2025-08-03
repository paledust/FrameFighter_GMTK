using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A basic C# Event System
public static class EventHandler
{
    public static event Action E_BeforeUnloadScene;
    public static void Call_BeforeUnloadScene() { E_BeforeUnloadScene?.Invoke(); }
    public static event Action E_AfterLoadScene;
    public static void Call_AfterLoadScene() { E_AfterLoadScene?.Invoke(); }
    public static event Action E_OnBeginSave;
    public static void Call_OnBeginSave() => E_OnBeginSave?.Invoke();
    public static event Action E_OnCompleteSave;
    public static void Call_OnCompleteSave() => E_OnCompleteSave?.Invoke();

    #region Interaction
    public static event Action<int> E_OnRefreshFrame;
    public static void Call_OnRefreshFrame(int frameindex) => E_OnRefreshFrame?.Invoke(frameindex);
    public static event Action<ThrowingObject> E_OnParried;
    public static void Call_OnParried(ThrowingObject throwingObj) => E_OnParried?.Invoke(throwingObj);
    public static event Action E_OnLoop;
    public static void Call_OnLoop() => E_OnLoop?.Invoke();
    public static event Action<string> E_OnTriggerAbility;
    public static void Call_OnTriggerAbility(string abilityName) => E_OnTriggerAbility?.Invoke(abilityName);
    public static event Action<string> E_OnCancelCharge;
    public static void Call_OnCancelCharge(string abilityName) => E_OnCancelCharge?.Invoke(abilityName);
    public static event Action<string> E_OnChargedAbility;
    public static void Call_OnChargedAbility(string abilityName) => E_OnChargedAbility?.Invoke(abilityName);
    public static event Action<int> E_OnPlayerHealthChange;
    public static void Call_OnPlayerHealthChange(int health) => E_OnPlayerHealthChange?.Invoke(health);
    public static event Action E_OnPlayerDie;
    public static void Call_OnPlayerDie() => E_OnPlayerDie?.Invoke();
    #endregion
}

//A More Strict Event System
namespace SimpleEventSystem{
    public abstract class Event{
        public delegate void Handler(Event e);
    }
    public class E_OnTestEvent:Event{
        public float value;
        public E_OnTestEvent(float data){value = data;}
    }

    public class EventManager{
        private static  EventManager instance;
        public static EventManager Instance{
            get{
                if(instance == null) instance = new EventManager();
                return instance;
            }
        }

        private Dictionary<Type, Event.Handler> RegisteredHandlers = new Dictionary<Type, Event.Handler>();
        public void Register<T>(Event.Handler handler) where T: Event{
            Type type = typeof(T);

            if(RegisteredHandlers.ContainsKey(type)){
                RegisteredHandlers[type] += handler;
            }
            else{
                RegisteredHandlers[type] = handler;
            }
        }
        public void UnRegister<T>(Event.Handler handler) where T: Event{
            Type type = typeof(T);
            Event.Handler handlers;

            if(RegisteredHandlers.TryGetValue(type, out handlers)){
                handlers -= handler;
                if(handlers == null){
                    RegisteredHandlers.Remove(type);
                }
                else{
                    RegisteredHandlers[type] = handlers;
                }
            }
        }
        public void FireEvent<T>(T e) where T:Event {
            Type type = e.GetType();
            Event.Handler handlers;

            if(RegisteredHandlers.TryGetValue(type, out handlers)){
                handlers?.Invoke(e);
            }
        }
        public void ClearList(){RegisteredHandlers.Clear();}
    }
}
