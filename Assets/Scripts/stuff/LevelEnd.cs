using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerStay(Collider coll)
    {
        if(Input.GetMouseButtonDown(0))
        {
            EventsManager_PersistenceScene.LoadNextSceneEvent();
        }
    }
}
