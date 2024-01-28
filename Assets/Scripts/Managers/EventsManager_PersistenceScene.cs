using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager_PersistenceScene
{
    // TransitionType is purely for visual
    public delegate void VoidDelegate();

    public static VoidDelegate ReloadSceneEvent;
    public static VoidDelegate LoadNextSceneEvent;
}
