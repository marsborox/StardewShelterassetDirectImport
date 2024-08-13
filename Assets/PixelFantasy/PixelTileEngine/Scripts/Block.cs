using UnityEngine;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public class Block
    {
        public string Name;
        public GameObject GameObject;
        public Transform Transform;
        public SpriteRenderer SpriteRenderer;
        public int OffsetY;
        
        public Block(Transform transform)
        {
            Name = transform.name;
            GameObject = transform.gameObject;
            Transform = transform;
            SpriteRenderer = GameObject.GetComponent<SpriteRenderer>();
        }

        public Block(string name)
        {
            Name = name;
            GameObject = new GameObject(name);
            Transform = GameObject.transform;
            SpriteRenderer = GameObject.AddComponent<SpriteRenderer>();
        }
    }
}