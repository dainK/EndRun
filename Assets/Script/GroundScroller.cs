using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EndRun
{

    public class GroundScroller : MonoBehaviour
    {
        public Action onAddScore;
        [SerializeField] private SpriteRenderer[] sprites;

        private int endPosX = -5;
        private int gap = 1;
        private int tileCount = 10;
        private List<SpriteRenderer> tiles = new List<SpriteRenderer>();

        private void Start()
        {
            for (int i = 0; i < tileCount; i++)
            {
                CreateTile();
            }
        }

        void Update()
        {
            if( GameSettings.gameData.isEnd )
                return;
            
            foreach (var tile in tiles)
            {
                tile.transform.Translate(Vector3.left * Time.deltaTime * GameSettings.gameData.moveSpeed);
            }

            if (tiles[0].transform.position.x < endPosX)
            {
                SpriteRenderer tile = tiles[0];
                tiles.Remove(tile);
                Destroy(tile.gameObject);
                CreateTile();

                onAddScore?.Invoke();
            }
        }

        private void CreateTile()
        {
            Debug.Assert(sprites.Length > 0);
            SpriteRenderer tile = Instantiate(sprites[Random.Range(0, sprites.Length)], gameObject.transform);
            if (tiles.Count > 0)
            {
                tile.transform.position = tiles[tiles.Count - 1].transform.position + Vector3.right * gap;
            }
            else
            {
                tile.transform.position = new Vector3(endPosX, 0, 0);
            }

            tiles.Add(tile);
        }
        
        
        private void OnDestroy()
        {
            foreach (var tile in tiles)
            {
                Destroy(tile);
            }
            tiles.Clear();
        }
    }

}
