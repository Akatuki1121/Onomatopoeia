using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{
    // === プレイヤーの移動・視点設定 ===
    private const float MoveSpeed = 5f; // 移動速度
    private const float MouseSensitivity = 5f; // マウス感度

    // === カメラ・衝突設定 ===
    private const float CameraDefaultY = 1.6f; // カメラのデフォルト高さ
    private const float CameraCollisionRadius = 0.2f; // カメラ衝突の判定半径
    private const float SphereCastDistance = CameraDefaultY * 3.0f; // カメラの衝突判定距離（壁にめり込まないよう拡張）
    private const float VerticalRotationMin = -90f; // 上方向の視点限界
    private const float VerticalRotationMax = 90f; // 下方向の視点限界

    // === 数値の一貫性を保つための定数化 ===
    private const float Zero = 0f;
    private const float One = 1f;
    private const float MinusOne = -1f;

    public Transform cameraTransform; // カメラのTransform
    public LayerMask collisionMask; // 衝突判定用のレイヤー

    private float verticalRotation = Zero; // カメラの上下回転値
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
        transform.Rotate(Zero, lookInput.x, Zero); // 左右回転
        verticalRotation -= lookInput.y; // 上下視点の変化
        verticalRotation = Mathf.Clamp(verticalRotation, VerticalRotationMin, VerticalRotationMax); // 視点の範囲制限
        cameraTransform.localEulerAngles = new Vector3(verticalRotation, Zero, Zero); // カメラ角度設定

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
        if (Keyboard.current == null)
            return Vector2.zero;

        float x = Zero;
        float y = Zero;
        if (Keyboard.current.wKey.isPressed) y += One; // 前進
        if (Keyboard.current.sKey.isPressed) y += MinusOne; // 後退
        if (Keyboard.current.aKey.isPressed) x += MinusOne; // 左移動
        if (Keyboard.current.dKey.isPressed) x += One; // 右移動
        return new Vector2(x, y);
    }

    private Vector2 GetLookInput()
    {
        if (Mouse.current == null && Keyboard.current == null)
            return Vector2.zero;

        // === マウス視点入力 ===
        Vector2 mouseDelta = Mouse.current != null ? MouseSensitivity * Time.deltaTime * Mouse.current.delta.ReadValue() : Vector2.zero;

        // === 矢印キー視点入力 ===
        float arrowX = Keyboard.current.rightArrowKey.isPressed ? One : Keyboard.current.leftArrowKey.isPressed ? MinusOne : Zero;
        float arrowY = Keyboard.current.upArrowKey.isPressed ? One : Keyboard.current.downArrowKey.isPressed ? MinusOne : Zero;
        Vector2 arrowInput = MouseSensitivity * Time.deltaTime * new Vector2(arrowX, arrowY);

        return mouseDelta + arrowInput; // 両方の入力を統合
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

        cameraTransform.localPosition = new Vector3(Zero, CameraDefaultY, Zero); // カメラの通常位置
    }
}