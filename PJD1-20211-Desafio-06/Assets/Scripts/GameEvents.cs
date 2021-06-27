using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventFloat : UnityEvent<float> { }
public class EventIntInt : UnityEvent<int, int> { }


public class GameEvents : MonoBehaviour
{
    static public EventFloat WeaponReloadEvent = new EventFloat();

    static public EventIntInt WeaponFireEvent = new EventIntInt();

    private void Awake()
    {
        
    }


}
