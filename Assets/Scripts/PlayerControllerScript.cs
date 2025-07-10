using UnityEngine;
using UnityEngine.InputSystem;

// プレイヤーの移動とカメラ操作を管理するスクリプト
public class PlayerControllerScript : MonoBehaviour
{
    // === プレイヤーの移動・視点設定 ===
    private const float MoveSpeed = 5f; // 移動速度
    private const float MouseSensitivity = 5f; // マウス感度
    private const float CameraSpeedGamepad = 15f; // コントローラー時のカメラ移動速度

    // === カメラ・衝突設定 ===
    private const float CameraDefaultX = 0f; // カメラの通常位置
    private const float CameraDefaultY = 1.6f; //カメラのデフォルト高さ
    private const float CameraDefaultZ = 1.0f; // カメラの通常位置

    private const float CameraCollisionRadius = 0.2f; // カメラ衝突の判定半径
    private const float SphereCastDistance = CameraDefaultY * 3.0f; // カメラの衝突判定距離（壁にめり込まないよう拡張）
    private const float VerticalRotationMin = -90f; // 上方向の視点限界
    private const float VerticalRotationMax = 90f; // 下方向の視点限界

    public Transform cameraTransform; // カメラのTransform
    public LayerMask collisionMask; // 衝突判定用のレイヤー

    private float verticalRotation = 0f; // カメラの上下回転値
    private Vector2 moveInput; // 移動入力（WSAD）
    private Vector2 lookInput; // 視点入力（マウス・矢印キー）
    private Rigidbody rb; // Rigidbody参照

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルをロック
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // プレイヤーの物理回転を固定（倒れないように）
    }

    void Update()
    {
        moveInput = GetMoveInput(); // 移動入力取得
        lookInput = GetLookInput(); // 視点入力取得

        // === 視点回転処理 ===
        transform.Rotate(0f, lookInput.x, 0f); // 左右回転
        verticalRotation -= lookInput.y; // 上下視点の変化
        verticalRotation = Mathf.Clamp(verticalRotation, VerticalRotationMin, VerticalRotationMax); // 視点の範囲制限
        cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f); // カメラ角度設定

        // === カメラの壁衝突判定 ===
        UpdateCameraPosition();
    }

    void FixedUpdate()
    {
        // === Rigidbodyによる移動処理 ===
        Vector3 move = MoveSpeed * Time.fixedDeltaTime * ((transform.right * moveInput.x) + (transform.forward * moveInput.y));
        rb.MovePosition(rb.position + move);
    }

    private Vector2 GetMoveInput()
    {
        // 入力モードで分岐
        if (CheckInputmodeScript.CurrentInputMode == CheckInputmodeScript.InputMode.Gamepad)
        {
            if (Gamepad.current == null) return Vector2.zero;
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            return new Vector2(stick.x, stick.y); // 左スティックで移動
        }
        else // Keyboard
        {
            if (Keyboard.current == null) return Vector2.zero;
            float x = 0f;
            float y = 0f;
            if (Keyboard.current.wKey.isPressed) y += 1f; // 前進
            if (Keyboard.current.sKey.isPressed) y += -1f; // 後退
            if (Keyboard.current.aKey.isPressed) x += -1f; // 左移動
            if (Keyboard.current.dKey.isPressed) x += 1f; // 右移動
            return new Vector2(x, y);
        }
    }

    private Vector2 GetLookInput()
    {
        // 入力モードで分岐
        if (CheckInputmodeScript.CurrentInputMode == CheckInputmodeScript.InputMode.Gamepad)
        {
            if (Gamepad.current == null) return Vector2.zero;
            Vector2 rightStick = Gamepad.current.rightStick.ReadValue();
            return MouseSensitivity * CameraSpeedGamepad * Time.deltaTime * rightStick; // 右スティックで視点移動
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
        // === カメラの壁衝突を処理 ===
        Vector3 targetPosition = transform.position + (Vector3.up * CameraDefaultY); // カメラのターゲット位置（プレイヤーの頭付近）
        Vector3 direction = (cameraTransform.position - targetPosition).normalized; // 判定方向を計算

        if (Physics.SphereCast(targetPosition, CameraCollisionRadius, direction, out RaycastHit hit, CameraDefaultY, collisionMask))
        {
            cameraTransform.position = hit.point - (CameraCollisionRadius * SphereCastDistance * direction); // 壁の奥へめり込まないよう調整
            return;
        }

        cameraTransform.localPosition = new Vector3(CameraDefaultX, CameraDefaultY, CameraDefaultZ); // カメラの通常位置
    }
}