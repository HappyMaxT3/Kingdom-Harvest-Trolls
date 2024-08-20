using UnityEngine;


namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject panel;
        public GameObject spawnPrefab; 
        public float interval = 2f;

        public GameObject spawnPointDownLeft;
        public GameObject spawnPointUpRight;

        private Vector3 spawnAreaMin; // ћинимальна€ позици€ спавна (левый нижний угол)
        private Vector3 spawnAreaMax; // ћаксимальна€ позици€ спавна (правый верхний угол)

        void Start()
        {
            spawnAreaMin = spawnPointDownLeft.transform.position;
            spawnAreaMax = spawnPointUpRight.transform.position;

            InvokeRepeating("Spawn", interval, interval);
        }

        private void Spawn()
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0
            );

            GameObject new_spawn = Instantiate(spawnPrefab, panel.transform);
            new_spawn.transform.position = spawnPosition;
        }
    }
}

