using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    // 動物のPrefabを格納するための変数
    public GameObject[] animalPrefabs;
    // 現在の動物を格納する変数
    public GameObject currentAnimal;
    // 動物を生成する位置
    public Transform spawnPoint;

    void Update()
    {
        // 現在動物が存在しない場合、新しい動物を生成
        if (currentAnimal == null)
        {
            SpawnAnimal();
        }
    }

    // 新しい動物を生成するメソッド
    void SpawnAnimal()
    {
        // ランダムに動物を選択
        int randomIndex = Random.Range(0, animalPrefabs.Length);

        // 選択した動物を生成して currentAnimal に保存
        currentAnimal = Instantiate(animalPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
    }
}

