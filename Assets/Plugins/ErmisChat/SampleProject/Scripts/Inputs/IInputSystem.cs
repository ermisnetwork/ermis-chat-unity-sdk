using UnityEngine;

namespace ErmisChat.SampleProject.Inputs
{
    /// <summary>
    /// Input system
    /// </summary>
    public interface IInputSystem
    {
        Vector2 MousePosition { get; }
        bool WasEnteredPressedThisFrame { get; }

        bool GetMouseButton(int index);
    }
}