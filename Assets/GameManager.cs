using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;      // カメラ参照用
    public Text scoreText;         // スコアを表示するText UI
    public Transform spawnPoint;   // 動物を生成する地点（SpawnPoint）
    private float highestPoint = 0f; // 現在の最高点（高さ）
    private float maxHeightReached = 0f; // 最高到達点（スコア）

    public float cameraOffset = 10f; // カメラの高さオフセット
    public float spawnOffset = 5f;  // SpawnPointを動物の上に配置するオフセット

    void Start()
    {
        // カメラが設定されていない場合はメインカメラを参照
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // スコアを初期化して表示
        UpdateScoreText();
    }

    // スコアを更新し、カメラとSpawnPointを調整するメソッド
    public void AddScoreAndAdjustCamera(float height)
    {
        // 新しい動物の高さが現在の最高点よりも高ければ更新
        if (height > highestPoint)
        {
            highestPoint = height;
            AdjustCameraPosition(); // カメラ位置を更新
        }

        // 新しい高さが今までの最高到達点（maxHeightReached）を超えた場合、スコアを更新
        if (height > maxHeightReached)
        {
            maxHeightReached = height;
            UpdateScoreText(); // スコア表示を更新
            AdjustSpawnPoint(); // SpawnPoint位置を更新
        }
    }

    // スコアを更新してUIに反映する
    void UpdateScoreText()
    {
        // 最高到達点をスコアとして表示
        scoreText.text = "Score: " + Mathf.RoundToInt(maxHeightReached);
    }

    // カメラの位置を調整する
    void AdjustCameraPosition()
    {
        // カメラの新しいY座標は最高点にオフセットを加えたもの
        Vector3 targetPosition = new Vector3(
            mainCamera.transform.position.x,   // X座標は固定
            highestPoint + cameraOffset,       // Y座標は最高点 + オフセット
            mainCamera.transform.position.z    // Z座標は固定
        );

        // カメラ位置を徐々に目標に移動させる
        mainCamera.transform.position = Vector3.Lerp(
            mainCamera.transform.position,    // 現在のカメラ位置
            targetPosition,                   // 目標位置
            Time.deltaTime * 2f               // 移動速度調整
        );
    }

    // SpawnPointの位置を調整する
    void AdjustSpawnPoint()
    {
        // maxHeightReachedに応じてオフセットを徐々に大きくする
        spawnOffset = Random.Range(3f,5f);

        // 新しいY座標を計算してSpawnPointを移動
        Vector3 newSpawnPosition = new Vector3(
            spawnPoint.position.x,            // X座標は固定
            maxHeightReached + spawnOffset,   // Y座標は最高到達点 + 増加するオフセット
            spawnPoint.position.z             // Z座標は固定
        );

        // 新しい位置にSpawnPointを移動
        spawnPoint.position = newSpawnPosition;
    }


}
