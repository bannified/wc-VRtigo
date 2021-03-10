using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomNextStepActivatable : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        ClassroomManager.Instance.NextStep();
    }
}
