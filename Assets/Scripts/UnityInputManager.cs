using Core.Interfaces;
using UnityEngine;

public class UnityInputManager : MonoBehaviour, IInput
{
    public float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    public bool GetKeyDown(string keyName)
    {
        return Input.GetKeyDown(keyName);
    }
}
