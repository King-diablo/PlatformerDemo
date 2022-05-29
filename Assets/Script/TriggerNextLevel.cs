using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextLevel : MonoBehaviour
{
    EndLogic endLogic;

    private void Awake()
    {
         endLogic = FindObjectOfType<EndLogic>();
    }

    public void GoToNextLevel()
    {
        endLogic.EndLevel();
    }
}
