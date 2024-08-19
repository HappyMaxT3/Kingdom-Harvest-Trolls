using UnityEngine;


namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab; 
        public float spawnInterval = 2f;
        public Vector2 spawnAreaMin; // ћинимальна€ позици€ спавна (левый нижний угол)
        public Vector2 spawnAreaMax; // ћаксимальна€ позици€ спавна (правый верхний угол)

        private float spawnTimer;

        void Start()
        {
            spawnTimer = spawnInterval; 
        }

        void Update()
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval; 
            }
        }

        private void SpawnEnemy()
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

