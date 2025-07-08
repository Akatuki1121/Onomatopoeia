using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.InputSystem.Controls;

// 入力デバイス（キーボード/ゲームパッド）の判定を行うスクリプト
public class CheckInputmodeScript : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }
    public static InputMode CurrentInputMode { get; private set; } = InputMode.Keyboard;

    // Update is called once per frame
    void Update()
    {
        // キーボード・マウスの入力があればKeyboard
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CurrentInputMode = InputMode.Keyboard;
        }
        // ゲームパッドの入力があればGamepad
        else if (Gamepad.current != null && Gamepad.current.allControls
            .Any(c => c is ButtonControl btn && btn.wasPressedThisFrame))
        {
            CurrentInputMode = InputMode.Gamepad;
        }
    }
}
