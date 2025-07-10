using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// 入力デバイス（キーボード/ゲームパッド）の判定を行うスクリプト
public class CheckInputmodeScript : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }
    public static InputMode CurrentInputMode { get; private set; } = InputMode.Keyboard;

    // Update is called once per frame
    void Update()
    {
        // キーボード・マウスの入力があればKeyboard
        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame))
        {
            CurrentInputMode = InputMode.Keyboard;
        }
        // ゲームパッドの入力があればGamepad
        else if (Gamepad.all.Any(gp =>
            gp.buttonSouth.wasPressedThisFrame || // A (XBOX), × (PS)
             gp.buttonEast.wasPressedThisFrame || // B (XBOX), ○ (PS)
             gp.buttonWest.wasPressedThisFrame || // X (XBOX), □ (PS)
             gp.buttonNorth.wasPressedThisFrame || // Y (XBOX), △ (PS)
             gp.leftShoulder.wasPressedThisFrame || // LB/L1
             gp.rightShoulder.wasPressedThisFrame || // RB/R1
             gp.leftTrigger.wasPressedThisFrame || // LT/L2
             gp.rightTrigger.wasPressedThisFrame || // RT/R2
             gp.dpad.up.wasPressedThisFrame ||
             gp.dpad.down.wasPressedThisFrame ||
             gp.dpad.left.wasPressedThisFrame ||
             gp.dpad.right.wasPressedThisFrame ||
             gp.leftStick.ReadValue() != Vector2.zero ||
             gp.rightStick.ReadValue() != Vector2.zero ||
             gp.startButton.wasPressedThisFrame ||      // ←start
             gp.selectButton.wasPressedThisFrame        // ←select
            ))
        {
            CurrentInputMode = InputMode.Gamepad;
        }
    }
}
