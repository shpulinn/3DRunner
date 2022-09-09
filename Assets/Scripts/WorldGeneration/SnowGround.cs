using System;
using UnityEngine;

namespace WorldGeneration
{
    public class SnowGround : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Material material;
        public float offsetSpeed = 0.25f;

        // reference value from ShaderGraph
        private string materialOffsetReference = "Vector2_d9f27b06057b44d18265785f464f8453";
        
        private void Update()
        {
            transform.position = Vector3.forward * playerTransform.position.z;
            material.SetVector(materialOffsetReference, new Vector2(0, -transform.position.z * offsetSpeed));
        }
    }
}
