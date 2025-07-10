using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// ���̓f�o�C�X�i�L�[�{�[�h/�Q�[���p�b�h�j�̔�����s���X�N���v�g
public class CheckInputmodeScript : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }
    public static InputMode CurrentInputMode { get; private set; } = InputMode.Keyboard;

    // Update is called once per frame
    void Update()
    {
        // �L�[�{�[�h�E�}�E�X�̓��͂������Keyboard
        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame))
        {
            CurrentInputMode = InputMode.Keyboard;
        }
        // �Q�[���p�b�h�̓��͂������Gamepad
        else if (Gamepad.all.Any(gp =>
            gp.buttonSouth.wasPressedThisFrame || // A (XBOX), �~ (PS)
             gp.buttonEast.wasPressedThisFrame || // B (XBOX), �� (PS)
             gp.buttonWest.wasPressedThisFrame || // X (XBOX), �� (PS)
             gp.buttonNorth.wasPressedThisFrame || // Y (XBOX), �� (PS)
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
             gp.startButton.wasPressedThisFrame ||      // ��start
             gp.selectButton.wasPressedThisFrame        // ��select
            ))
        {
            CurrentInputMode = InputMode.Gamepad;
        }
    }
}
