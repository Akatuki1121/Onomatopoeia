using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.InputSystem.Controls;

// ���̓f�o�C�X�i�L�[�{�[�h/�Q�[���p�b�h�j�̔�����s���X�N���v�g
public class CheckInputmodeScript : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }
    public static InputMode CurrentInputMode { get; private set; } = InputMode.Keyboard;

    // Update is called once per frame
    void Update()
    {
        // �L�[�{�[�h�E�}�E�X�̓��͂������Keyboard
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CurrentInputMode = InputMode.Keyboard;
        }
        // �Q�[���p�b�h�̓��͂������Gamepad
        else if (Gamepad.current != null && Gamepad.current.allControls
            .Any(c => c is ButtonControl btn && btn.wasPressedThisFrame))
        {
            CurrentInputMode = InputMode.Gamepad;
        }
    }
}
