using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldGeneration
{
    public class WorldGeneration : MonoBehaviour
    {
        // Gameplay
        private float _chunkSpawnZ; // the last spawned chunk in Z axis
        private Queue<Chunk> _activeChunks = new Queue<Chunk>();
        private List<Chunk> _chunkPool = new List<Chunk>();
        
        // Configurable fields
        [SerializeField] private int firstChunkSpawnPosition = -10;
        [SerializeField] private int chunkOnScreen = 5; // max amount of spawned chunk at one time
        [SerializeField] private float despawnDistance = 5.0f;

        [SerializeField] private List<GameObject> chunkPrefabs;
        [SerializeField] private Transform cameraTransform;

        #region TO DELETE $$
        private void Awake()
        {
            ResetWorld();
        }
        #endregion

        private void Start()
        {
            // Check if we have an empty chunkPrefabs list
            if (chunkPrefabs.Count == 0)
            {
                Debug.LogError("No chunkPrefabs assigned to List of chunk prefabs!");
                return;
            }
            // Try to assign camera transform
            if (!cameraTransform)
            {
                cameraTransform = Camera.main.transform;
                Debug.Log("cameraTransform has assigned automatically");
            }
        }

        public void ScanPosition()
        {
            float cameraZ = cameraTransform.position.z;
            Chunk lastChunk = _activeChunks.Peek();
            
            if (cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + despawnDistance)
            {
                SpawnNewChunk();
                DeleteLastChunk();
            }
        }

        private void SpawnNewChunk()
        {
            // Get a random index for prefab
            int randomIndex = Random.Range(0, chunkPrefabs.Count);
            
            // Does it already exist within pool
            Chunk chunk = _chunkPool.Find(x => !x.gameObject.activeSelf &&
                                               x.name.Equals((chunkPrefabs[randomIndex].name) + "(Clone)"));
            
            // Create a new chunk if were unable to find in pool
            if (!chunk)
            {
                GameObject chunkGo = Instantiate(chunkPrefabs[randomIndex], transform);
                chunk = chunkGo.GetComponent<Chunk>();
            }

            // Place prefab and show it
            chunk.transform.position = new Vector3(0, 0, _chunkSpawnZ);
            // Prefer _chunkSpawnZ for next generations
            _chunkSpawnZ += chunk.chunkLength;
            
            // Store the value for reusing it in pool
            _activeChunks.Enqueue(chunk);
            chunk.ShowChunk();
        }

        private void DeleteLastChunk()
        {
            Chunk chunk = _activeChunks.Dequeue();
            chunk.HideChunk();
            // Add Chunk to the pool
            _chunkPool.Add(chunk);
        }

        public void ResetWorld()
        {
            // Reset the ChunkSpawnZ
            _chunkSpawnZ = firstChunkSpawnPosition;

            for (int i = _activeChunks.Count; i != 0; i--)
                DeleteLastChunk();

            for (int i = 0; i < chunkOnScreen; i++)
                SpawnNewChunk();
        }
    }
}
