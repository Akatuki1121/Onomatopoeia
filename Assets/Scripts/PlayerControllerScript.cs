using UnityEngine;
using UnityEngine.InputSystem;

// �v���C���[�̈ړ��ƃJ����������Ǘ�����X�N���v�g
public class PlayerControllerScript : MonoBehaviour
{
    // === �v���C���[�̈ړ��E���_�ݒ� ===
    private const float MoveSpeed = 5f; // �ړ����x
    private const float MouseSensitivity = 5f; // �}�E�X���x
    private const float CameraSpeedGamepad = 15f; // �R���g���[���[���̃J�����ړ����x

    // === �J�����E�Փːݒ� ===
    private const float CameraDefaultX = 0f; // �J�����̒ʏ�ʒu
    private const float CameraDefaultY = 1.6f; //�J�����̃f�t�H���g����
    private const float CameraDefaultZ = 1.0f; // �J�����̒ʏ�ʒu

    private const float CameraCollisionRadius = 0.2f; // �J�����Փ˂̔��蔼�a
    private const float SphereCastDistance = CameraDefaultY * 3.0f; // �J�����̏Փ˔��苗���i�ǂɂ߂荞�܂Ȃ��悤�g���j
    private const float VerticalRotationMin = -90f; // ������̎��_���E
    private const float VerticalRotationMax = 90f; // �������̎��_���E

    public Transform cameraTransform; // �J������Transform
    public LayerMask collisionMask; // �Փ˔���p�̃��C���[

    private float verticalRotation = 0f; // �J�����̏㉺��]�l
    private Vector2 moveInput; // �ړ����́iWSAD�j
    private Vector2 lookInput; // ���_���́i�}�E�X�E���L�[�j
    private Rigidbody rb; // Rigidbody�Q��

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\�������b�N
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // �v���C���[�̕�����]���Œ�i�|��Ȃ��悤�Ɂj
    }

    void Update()
    {
        moveInput = GetMoveInput(); // �ړ����͎擾
        lookInput = GetLookInput(); // ���_���͎擾

        // === ���_��]���� ===
        transform.Rotate(0f, lookInput.x, 0f); // ���E��]
        verticalRotation -= lookInput.y; // �㉺���_�̕ω�
        verticalRotation = Mathf.Clamp(verticalRotation, VerticalRotationMin, VerticalRotationMax); // ���_�͈̔͐���
        cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f); // �J�����p�x�ݒ�

        // === �J�����̕ǏՓ˔��� ===
        UpdateCameraPosition();
    }

    void FixedUpdate()
    {
        // === Rigidbody�ɂ��ړ����� ===
        Vector3 move = MoveSpeed * Time.fixedDeltaTime * ((transform.right * moveInput.x) + (transform.forward * moveInput.y));
        rb.MovePosition(rb.position + move);
    }

    private Vector2 GetMoveInput()
    {
        // ���̓��[�h�ŕ���
        if (CheckInputmodeScript.CurrentInputMode == CheckInputmodeScript.InputMode.Gamepad)
        {
            if (Gamepad.current == null) return Vector2.zero;
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            return new Vector2(stick.x, stick.y); // ���X�e�B�b�N�ňړ�
        }
        else // Keyboard
        {
            if (Keyboard.current == null) return Vector2.zero;
            float x = 0f;
            float y = 0f;
            if (Keyboard.current.wKey.isPressed) y += 1f; // �O�i
            if (Keyboard.current.sKey.isPressed) y += -1f; // ���
            if (Keyboard.current.aKey.isPressed) x += -1f; // ���ړ�
            if (Keyboard.current.dKey.isPressed) x += 1f; // �E�ړ�
            return new Vector2(x, y);
        }
    }

    private Vector2 GetLookInput()
    {
        // ���̓��[�h�ŕ���
        if (CheckInputmodeScript.CurrentInputMode == CheckInputmodeScript.InputMode.Gamepad)
        {
            if (Gamepad.current == null) return Vector2.zero;
            Vector2 rightStick = Gamepad.current.rightStick.ReadValue();
            return MouseSensitivity * CameraSpeedGamepad * Time.deltaTime * rightStick; // �E�X�e�B�b�N�Ŏ��_�ړ�
        }
        else // Keyboard+Mouse
        {
            if (Mouse.current == null && Keyboard.current == null) return Vector2.zero;
            Vector2 mouseDelta = Mouse.current != null ? MouseSensitivity * Time.deltaTime * Mouse.current.delta.ReadValue() : Vector2.zero;
            float arrowX = Keyboard.current.rightArrowKey.isPressed ? 1f : Keyboard.current.leftArrowKey.isPressed ? -1f : 0f;
            float arrowY = Keyboard.current.upArrowKey.isPressed ? 1f : Keyboard.current.downArrowKey.isPressed ? -1f : 0f;
            Vector2 arrowInput = MouseSensitivity * Time.deltaTime * new Vector2(arrowX, arrowY);
            return mouseDelta + arrowInput;
        }
    }

    private void UpdateCameraPosition()
    {
        // === �J�����̕ǏՓ˂����� ===
        Vector3 targetPosition = transform.position + (Vector3.up * CameraDefaultY); // �J�����̃^�[�Q�b�g�ʒu�i�v���C���[�̓��t�߁j
        Vector3 direction = (cameraTransform.position - targetPosition).normalized; // ����������v�Z

        if (Physics.SphereCast(targetPosition, CameraCollisionRadius, direction, out RaycastHit hit, CameraDefaultY, collisionMask))
        {
            cameraTransform.position = hit.point - (CameraCollisionRadius * SphereCastDistance * direction); // �ǂ̉��ւ߂荞�܂Ȃ��悤����
            return;
        }

        cameraTransform.localPosition = new Vector3(CameraDefaultX, CameraDefaultY, CameraDefaultZ); // �J�����̒ʏ�ʒu
    }
}