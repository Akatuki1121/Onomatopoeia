using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{
    // === �v���C���[�̈ړ��E���_�ݒ� ===
    private const float MoveSpeed = 5f; // �ړ����x
    private const float MouseSensitivity = 5f; // �}�E�X���x

    // === �J�����E�Փːݒ� ===
    private const float CameraDefaultY = 1.6f; // �J�����̃f�t�H���g����
    private const float CameraCollisionRadius = 0.2f; // �J�����Փ˂̔��蔼�a
    private const float SphereCastDistance = CameraDefaultY * 3.0f; // �J�����̏Փ˔��苗���i�ǂɂ߂荞�܂Ȃ��悤�g���j
    private const float VerticalRotationMin = -90f; // ������̎��_���E
    private const float VerticalRotationMax = 90f; // �������̎��_���E

    // === ���l�̈�ѐ���ۂ��߂̒萔�� ===
    private const float Zero = 0f;
    private const float One = 1f;
    private const float MinusOne = -1f;

    public Transform cameraTransform; // �J������Transform
    public LayerMask collisionMask; // �Փ˔���p�̃��C���[

    private float verticalRotation = Zero; // �J�����̏㉺��]�l
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
        transform.Rotate(Zero, lookInput.x, Zero); // ���E��]
        verticalRotation -= lookInput.y; // �㉺���_�̕ω�
        verticalRotation = Mathf.Clamp(verticalRotation, VerticalRotationMin, VerticalRotationMax); // ���_�͈̔͐���
        cameraTransform.localEulerAngles = new Vector3(verticalRotation, Zero, Zero); // �J�����p�x�ݒ�

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
        if (Keyboard.current == null)
            return Vector2.zero;

        float x = Zero;
        float y = Zero;
        if (Keyboard.current.wKey.isPressed) y += One; // �O�i
        if (Keyboard.current.sKey.isPressed) y += MinusOne; // ���
        if (Keyboard.current.aKey.isPressed) x += MinusOne; // ���ړ�
        if (Keyboard.current.dKey.isPressed) x += One; // �E�ړ�
        return new Vector2(x, y);
    }

    private Vector2 GetLookInput()
    {
        if (Mouse.current == null && Keyboard.current == null)
            return Vector2.zero;

        // === �}�E�X���_���� ===
        Vector2 mouseDelta = Mouse.current != null ? MouseSensitivity * Time.deltaTime * Mouse.current.delta.ReadValue() : Vector2.zero;

        // === ���L�[���_���� ===
        float arrowX = Keyboard.current.rightArrowKey.isPressed ? One : Keyboard.current.leftArrowKey.isPressed ? MinusOne : Zero;
        float arrowY = Keyboard.current.upArrowKey.isPressed ? One : Keyboard.current.downArrowKey.isPressed ? MinusOne : Zero;
        Vector2 arrowInput = MouseSensitivity * Time.deltaTime * new Vector2(arrowX, arrowY);

        return mouseDelta + arrowInput; // �����̓��͂𓝍�
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

        cameraTransform.localPosition = new Vector3(Zero, CameraDefaultY, Zero); // �J�����̒ʏ�ʒu
    }
}