using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    // 動物がプレイヤーによって解放されたかを確認するためのフラグ
    private bool isReleased = false;
    // Rigidbody2Dコンポーネントを格納するための変数
    private Rigidbody2D rb;
    // 動物の移動速度を調整するためのパラメータ
    public float moveSpeed = 5f;

    void Start()
    {
        // Rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
        // 初期状態では動物を固定し、動物が落下するまで物理演算を停止
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        // 動物がまだ解放されていない場合、プレイヤーによる操作を許可
        if (!isReleased)
        {
            // マウスのX座標を取得し、動物の位置をそれに合わせる
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, transform.position.y, 0);

            // 左クリック（もしくはタップ）で動物を落下させる
            if (Input.GetMouseButtonDown(0))
            {
                ReleaseAnimal();
            }
        }
    }

    // 動物を落下させるメソッド
    void ReleaseAnimal()
    {
        // 動物が解放されたことを示すフラグをtrueにする
        isReleased = true;
        // 物理演算を有効化（Dynamicに変更することで動物が落下）
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

     // 衝突時に高さをスコアに加算し、カメラを調整する
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isReleased)
        {
            // 動物のY座標（高さ）を取得
            float height = transform.position.y;

            // ゲームマネージャーに高さを渡し、カメラ位置を調整してスコアを加算
            FindObjectOfType<GameManager>().AddScoreAndAdjustCamera(height);

            // 現在の動物をnullに設定して新しい動物を生成可能にする
            FindObjectOfType<AnimalSpawner>().currentAnimal = null;

            // この動物オブジェクトを削除
            Destroy(this);
        }
    }

}
