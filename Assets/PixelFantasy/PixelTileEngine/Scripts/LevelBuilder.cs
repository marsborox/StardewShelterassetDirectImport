using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public class LevelBuilder : MonoBehaviour
    {
        public Transform Parent;
        public SpriteCollection SpriteCollection;
        public SpriteRenderer Cursor;

        private int _type;
        private int _index;
        private int _layer;
        private readonly bool[] _layersEnabled = { true, true, true, true };
        private Position _positionMin;

        private TileMap _groundMap;
        private TileMap _coverMap;
        private TileMap _propsMap;

        private string _hash;

        public void Start()
        {
            _groundMap = new TileMap(1, 1, 4);
            _coverMap = new TileMap(1, 1, 4);
            _propsMap = new TileMap(1, 1, 4);

            _groundMap[0, 0, 0] = new Block(transform.Find("Humus"));
            _coverMap[0, 0, 0] = new Block(transform.Find("GrassA"));

            Popup.Instance.Show("<color=\"yellow\">[RMB]</color> Move camera");
        }

        public void SwitchTile(int type, int index)
        {
            _type = type;
            _index = index;

            if (index == -1)
            {
                Cursor.sprite = SpriteCollection.DeleteSprite;
            }
            else switch (type)
            {
                case 0:
                    Cursor.sprite = SpriteCollection.GroundTilesets[index].Sprites[20];
                    break;
                case 1:
                    Cursor.sprite = SpriteCollection.CoverTilesets[index].Sprites[0];
                    break;
                case 2:
                    Cursor.sprite = SpriteCollection.PropsSprites[index];
                    break;
                case 3:
                    Cursor.sprite = SpriteCollection.OtherSprites[index];
                    break;
            }
        }

        public void EnableCursor(bool value)
        {
            Cursor.enabled = value;
        }

        public void MoveCursor(Vector2 pointer)
        {
            if (!Cursor.enabled) return;

            var position = Position.FromPointer(pointer);

            Cursor.transform.position = new Vector3(position.X, position.Y);

            if (_type == 2 && _index != -1)
            {
                Cursor.transform.position -= new Vector3(0, 1f / 16f);
            }
        }

        public void Draw(Vector2 pointer)
        {
            if (!_layersEnabled[_layer]) return;

            var position = Position.FromPointer(pointer);
            var p = new Position(position.X - _positionMin.X, position.Y - _positionMin.Y);

            if (p.X < 0 || p.X >= _groundMap.Width || p.Y < 0 || p.Y >= _groundMap.Height)
            {
                _groundMap.ExtendMap(p);
                _coverMap.ExtendMap(p);
                _propsMap.ExtendMap(p);
                _positionMin = new Position(Mathf.Min(_positionMin.X, position.X), Mathf.Min(_positionMin.Y, position.Y));
                p = position - _positionMin;
                _hash = null;
            }

            var hash = $"{_type}.{_index}.{p}";

            if (hash == _hash) return;

            _hash = hash;

            switch (_type)
            {
                case 0: CreateGround(p.X, p.Y, _layer); break;
                case 1: CreateCover(p.X, p.Y, _layer); break;
                case 2:
                case 3: CreateProps(p.X, p.Y, _layer); break;
            }
        }

        public void SwitchLayer(int layer)
        {
            _layer = layer;
        }

        public void EnableLayer(int layer)
        {
            _layersEnabled[layer] = !_layersEnabled[layer];

            for (var x = 0; x < _groundMap.Width; x++)
            {
                for (var y = 0; y < _groundMap.Height; y++)
                {
                    if (_groundMap[x, y, layer] != null) _groundMap[x, y, layer].SpriteRenderer.enabled = _layersEnabled[layer];
                    if (_coverMap[x, y, layer] != null) _coverMap[x, y, layer].SpriteRenderer.enabled = _layersEnabled[layer];
                    if (_propsMap[x, y, layer] != null) _propsMap[x, y, layer].SpriteRenderer.enabled = _layersEnabled[layer];
                }
            }
        }

        private void CreateGround(int x, int y, int z)
        {
            if (x < 0 || x >= _groundMap.Width || y < 0 || y >= _groundMap.Height) return;

            _groundMap.Destroy(x, y, z);

            if (_index != -1)
            {
                var block = new Block(SpriteCollection.GroundTilesets[_index].Name);

                block.Transform.SetParent(Parent);
                block.Transform.localPosition = new Vector3(_positionMin.X + x, _positionMin.Y + y);
                block.Transform.localScale = Vector3.one;
                block.SpriteRenderer.sprite = SpriteCollection.GroundTilesets[_index].Sprites[0];
                block.GameObject.AddComponent<BoxCollider2D>().offset = new Vector3(0, 0.5f);

                _groundMap[x, y, z] = block;
            }

            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    SetGround(x + dx, y + dy, z);
                }
            }

            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    SetCover(x + dx, y + dy, z);
                }
            }
        }

        private void CreateCover(int x, int y, int z)
        {
            if (x < 0 || x >= _coverMap.Width || y < 0 || y >= _coverMap.Height) return;

            _coverMap.Destroy(x, y, z);

            if (_groundMap[x, y, z] == null || _groundMap[x, y + 1, z] != null)
            {
                Debug.LogWarning("Covers can be placed over the ground only.");
                return;
            }

            if (_index != -1)
            {
                var block = new Block(SpriteCollection.CoverTilesets[_index].Name);

                block.Transform.SetParent(Parent);
                block.Transform.localPosition = new Vector3(_positionMin.X + x, _positionMin.Y + y);
                block.Transform.localScale = Vector3.one;
                block.SpriteRenderer.sprite = SpriteCollection.CoverTilesets[_index].Sprites[0];

                _coverMap[x, y, z] = block;
            }
            
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    SetCover(x + i, y + j, z);
                }
            }
        }

        private void CreateProps(int x, int y, int z)
        {
            if (x < 0 || x >= _propsMap.Width || y <= 0 || y >= _propsMap.Height) return;

            if (_index != -1 && _type == 2 && (_groundMap[x, y, z] != null || _groundMap[x, y - 1, z] == null))
            {
                Debug.LogWarning("Props can be placed on the ground only.");
                return;
            }

            _propsMap.Destroy(x, y, z);

            if (_index == -1) return;

            var sprites = _type == 2 ? SpriteCollection.PropsSprites : SpriteCollection.OtherSprites;
            var block = new Block(sprites[_index].name);

            block.Transform.SetParent(Parent);
            block.Transform.localPosition = new Vector3(_positionMin.X + x, _positionMin.Y + y);
            block.Transform.localScale = Vector3.one;
            block.SpriteRenderer.sprite = sprites[_index];
            block.SpriteRenderer.sortingOrder = 100 * z + 30;

            if (_type == 2 && _index != -1)
            {
                block.OffsetY = -1;
                block.Transform.localPosition -= new Vector3(0, 1f / 16f);
            }

            _propsMap[x, y, z] = block;
        }

        private void SetGround(int x, int y, int z)
        {
            if (x < 0 || x >= _groundMap.Width || y < 0 || y >= _groundMap.Height) return;
            if (_groundMap[x, y, z] == null) return;

            var tileset = SpriteCollection.GroundTilesets.Single(i => i.Sprites.Contains(_groundMap[x, y, z].SpriteRenderer.sprite));
            var map = _groundMap.GetBitmap(x, y, z);
            var mask = GetMask(x, y, map);
            var sprite = tileset.ResolveSprite(mask, out var flipX);

            if (sprite == null)
            {
                _groundMap.Destroy(x, y, z);
            }
            else
            {
                _groundMap[x, y, z].SpriteRenderer.sprite = sprite;
                _groundMap[x, y, z].SpriteRenderer.flipX = flipX;
                _groundMap[x, y, z].SpriteRenderer.sortingOrder = 100 * z + 10;
            }
        }
        
        private void SetCover(int x, int y, int z)
        {
            if (x < 0 || x >= _groundMap.Width || y < 0 || y >= _groundMap.Height) return;
            if (_coverMap[x, y, z] == null) return;

            if (_groundMap[x, y, z] == null || _groundMap[x, y + 1, z] != null)
            {
                _coverMap.Destroy(x, y, z);
                return;
            }

            _coverMap[x, y, z].SpriteRenderer.flipX = false;
            _coverMap[x, y, z].SpriteRenderer.sortingOrder = 100 * z + 20;

            var tileset = SpriteCollection.CoverTilesets.Single(i => i.Sprites.Contains(_coverMap[x, y, z].SpriteRenderer.sprite));
            var map = _coverMap.GetBitmap(x, y, z);
            var mask = GetMask(x, y, map);

            if (_groundMap[x - 1, y + 1, z] != null && _groundMap[x - 1, y, z] != null) mask[1, 0] = 2;
            if (_groundMap[x + 1, y + 1, z] != null && _groundMap[x + 1, y, z] != null) mask[1, 2] = 2;

            var sprite = tileset.ResolveSprite(mask, out var flipX);

            if (sprite == null)
            {
                _coverMap.Destroy(x, y, z);
            }
            else
            {
                _coverMap[x, y, z].SpriteRenderer.sprite = sprite;
                _coverMap[x, y, z].SpriteRenderer.flipX = flipX;
            }
        }

        private static int[,] GetMask(int x, int y, int[,] map)
        {
            return new[,]
            {
                {
                    x > 0 && y < map.GetLength(1) - 1 && map[x - 1, y + 1] == 1 && map[x - 1, y] == 1 && map[x, y + 1] == 1 ? 1 : 0,
                    y < map.GetLength(1) - 1 && map[x, y + 1] == 1 ? 1 : 0,
                    x < map.GetLength(0) - 1 && y < map.GetLength(1) - 1 && map[x + 1, y + 1] == 1 && map[x + 1, y] == 1 && map[x, y + 1] == 1 ? 1 : 0
                },
                {
                    x > 0 && map[x - 1, y] == 1 ? 1 : 0,
                    1,
                    x < map.GetLength(0) - 1 && map[x + 1, y] == 1 ? 1 : 0 },
                {
                    x > 0 && y > 0 && map[x - 1, y - 1] == 1 && map[x - 1, y] == 1 && map[x, y - 1] == 1 ? 1 : 0,
                    y > 0 && map[x, y - 1] == 1 ? 1 : 0,
                    x < map.GetLength(0) - 1 && y > 0 && map[x + 1, y - 1] == 1 && map[x + 1, y] == 1 && map[x, y - 1] == 1 ? 1 : 0
                }
            };
        }

        public void SaveTexture()
        {
            var cellSize = (int) SpriteCollection.GroundTilesets[0].Sprites[0].textureRect.width;
            var texture = new Texture2D(_groundMap.Width * cellSize, _groundMap.Height * cellSize);

            texture.SetPixels(new Color[texture.width * texture.height]);

            void CopyBlock(TileMap tilemap, int x, int y, int z)
            {
                var block = tilemap[x, y, z];

                if (block != null)
                {
                    var sr = block.SpriteRenderer;
                    var sprite = sr.sprite;
                    var width = (int) sprite.textureRect.width;
                    var height = (int) sprite.textureRect.height;
                    var pixels = sprite.texture.GetPixels((int) sprite.textureRect.x, (int) sprite.textureRect.y, width, height);

                    pixels = TextureHelper.Flip(pixels, width, height, sr.flipX, sr.flipY);
                    pixels = TextureHelper.Rotate(pixels, width, height, (int) block.Transform.eulerAngles.z);
                    
                    for (var dx = 0; dx < width; dx++)
                    {
                        for (var dy = 0; dy < height; dy++)
                        {
                            var pixel = pixels[dx + dy * width];
                            var xPos = x * cellSize + dx - (width - cellSize) / 2;
                            var yPos = y * cellSize + dy - Mathf.RoundToInt(sprite.pivot.y) + block.OffsetY;

                            if (pixel.a > 0 && xPos >= 0 && xPos < texture.width && yPos >= 0 && yPos < texture.height)
                            {
                                texture.SetPixel(xPos, yPos, pixel);
                            }
                        }
                    }
                }
            }

            for (var x = 0; x < _groundMap.Width; x++)
            {
                for (var y = 0; y < _groundMap.Height; y++)
                {
                    for (var z = 0; z < _groundMap.Depth; z++)
                    {
                        if (!_layersEnabled[z]) continue;

                        CopyBlock(_groundMap, x, y, z);
                        CopyBlock(_coverMap, x, y, z);
                        CopyBlock(_propsMap, x, y, z);
                    }
                }
            }

            SaveFileDialog("Save level as texture", "Level", "Image", ".png", texture.EncodeToPNG());
        }

        public void SaveLevel()
        {
            if (_groundMap == null) return;

            var width = _groundMap.Width;
            var height = _groundMap.Height;
            var depth = _groundMap.Depth;
            var level = new Level(width, height, depth);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    for (var z = 0; z < depth; z++)
                    {
                        var groundIndex = -1;

                        if (_groundMap[x, y, z] != null)
                        {
                            groundIndex = level.AddTexture(_groundMap[x, y, z].SpriteRenderer.sprite.texture.name);
                        }

                        var coverIndex = -1;

                        if (_coverMap[x, y, z] != null)
                        {
                            coverIndex = level.AddTexture(_coverMap[x, y, z].SpriteRenderer.sprite.texture.name);
                        }

                        var propsIndex = -1;

                        if (_propsMap[x, y, z] != null)
                        {
                            propsIndex = level.AddTexture(_propsMap[x, y, z].SpriteRenderer.sprite.name);
                        }

                        level.GroundMap[x, y, z] = groundIndex + 1;
                        level.CoverMap[x, y, z] = coverIndex + 1;
                        level.PropsMap[x, y, z] = propsIndex + 1;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(level);

            SaveFileDialog("Save level as JSON", "Level", "JSON", ".json", Encoding.UTF8.GetBytes(json));
        }

        public void LoadLevel()
        {
            OpenFileDialog("Open level JSON", "JSON", ".json", bytes => BuildLevel(Encoding.UTF8.GetString(bytes)));
        }

        private void BuildLevel(string json)
        {
            var level = JsonConvert.DeserializeObject<Level>(json);

            if (_groundMap != null)
            {
                for (var x = 0; x < _groundMap.Width; x++)
                {
                    for (var y = 0; y < _groundMap.Height; y++)
                    {
                        for (var z = 0; z < _groundMap.Depth; z++)
                        {
                            _groundMap.Destroy(x, y, z);
                            _coverMap.Destroy(x, y, z);
                            _propsMap.Destroy(x, y, z);
                        }
                    }
                }
            }

            var width = level.GroundMap.GetLength(0);
            var height = level.GroundMap.GetLength(1);
            var depth = level.GroundMap.GetLength(2);

            _groundMap = new TileMap(width, height, depth);
            _coverMap = new TileMap(width, height, depth);
            _propsMap = new TileMap(width, height, depth);
            _positionMin = new Position(-width / 2, -height / 2);

            var index = _index;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    for (var z = 0; z < depth; z++)
                    {
                        var ground = level.GroundMap[x, y, z] == 0 ? null : level.TileTable[level.GroundMap[x, y, z] - 1];
                        var cover = level.CoverMap[x, y, z] == 0 ? null : level.TileTable[level.CoverMap[x, y, z] - 1];
                        var props = level.PropsMap[x, y, z] == 0 ? null : level.TileTable[level.PropsMap[x, y, z] - 1];

                        if (ground != null)
                        {
                            _type = 0;
                            _index = SpriteCollection.GroundTilesets.FindIndex(i => i.Name == ground);

                            if (_index != -1)
                            {
                                CreateGround(x, y, z);
                            }
                        }

                        if (cover != null)
                        {
                            _type = 1;
                            _index = SpriteCollection.CoverTilesets.FindIndex(i => i.Name == cover);

                            if (_index != -1)
                            {
                                CreateCover(x, y, z);
                            }
                        }

                        if (props != null)
                        {
                            _type = 2;
                            _index = SpriteCollection.PropsSprites.FindIndex(i => i.name == props);

                            if (_index == -1)
                            {
                                _type = 3;
                                _index = SpriteCollection.OtherSprites.FindIndex(i => i.name == props);
                            }

                            if (_index != -1)
                            {
                                CreateProps(x, y, z);
                            }
                        }
                    }
                }
            }

            _index = index;
        }

        public void LoadTileset(int type)
        {
            OpenFileDialog("Open tileset", "Image", ".png", bytes => LoadTileset(bytes, type));
        }

        private void LoadTileset(byte[] bytes, int type)
        {
            var texture = new Texture2D(2, 2) { filterMode = FilterMode.Point };
            var sprites = new List<Sprite>();

            texture.LoadImage(bytes);

            for (var y = 0; y < 4; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var sprite = Sprite.Create(texture, new Rect(16 * x, 16 * (3 - y), 16, 16), Vector2.one / 2, 16);

                    sprite.name = (x + 8 * y).ToString();
                    sprites.Add(sprite);
                }
            }

            if (type == 0)
            {
                SpriteCollection.GroundTilesets.Add(new Tileset(texture, sprites));
            }
            else
            {
                SpriteCollection.CoverTilesets.Add(new Tileset(texture, sprites));
            }

            FindObjectOfType<Inventory>().SwitchTab(type);
        }

        public void SwitchCover()
        {
            for (var x = 0; x < _coverMap.Width; x++)
            {
                for (var y = 0; y < _coverMap.Height; y++)
                {
                    if (_coverMap[x, y, _layer] != null)
                    {
                        _coverMap[x, y, _layer].SpriteRenderer.enabled = !_coverMap[x, y, _layer].SpriteRenderer.enabled;
                    } 
                }
            }
        }

        public void ExportSprite()
        {
            var texture = Cursor.sprite.texture;

            SaveFileDialog("Save sprite", texture.name, "Image", ".png", texture.EncodeToPNG());
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }

        private void SaveFileDialog(string title, string fileName, string fileType, string extension, byte[] bytes)
        {
            #if UNITY_EDITOR

            var path = UnityEditor.EditorUtility.SaveFilePanel(title, null, fileName + extension, extension.Replace(".", ""));

            if (string.IsNullOrEmpty(path)) return;

            File.WriteAllBytes(path, bytes);

            #elif UNITY_STANDALONE

            #if FILE_BROWSER

            StartCoroutine(SimpleFileBrowserForWindows.WindowsFileBrowser.SaveFile(title, "", fileName, fileType, extension, bytes, (success, p) => { }));

            #else

            Debug.LogWarning("Please import this asset: http://u3d.as/2QLg");
            
            #endif
            
            #elif UNITY_WEBGL

            #if FILE_BROWSER

            if (extension == ".png")
            {
                Popup.Instance.Show("This feature is unavailable in the demo version. Please purchase the full app.");
                return;
            }

            SimpleFileBrowserForWebGL.WebFileBrowser.Download(fileName + extension, bytes);

            #else

            Debug.LogWarning("Please import this asset: http://u3d.as/2W52");
            
            #endif

            #endif
        }

        private void OpenFileDialog(string title, string fileType, string extension, Action<byte[]> callback)
        {
            #if UNITY_EDITOR

            var path = UnityEditor.EditorUtility.OpenFilePanel(title, "", extension.Replace(".", ""));

            if (path == "") return;

            callback(File.ReadAllBytes(path));

            #elif UNITY_WEBGL

            #if FILE_BROWSER

            SimpleFileBrowserForWebGL.WebFileBrowser.Upload((fileName, mime, bytes) => callback(bytes), extension);

            #else

            Debug.LogWarning("Please import this asset: http://u3d.as/2W52");
            
            #endif

            #elif UNITY_STANDALONE

            #if FILE_BROWSER

            StartCoroutine(SimpleFileBrowserForWindows.WindowsFileBrowser.OpenFile(title, "", fileType, new[] { extension }, (success, _, bytes) => { if (success) callback(bytes); }));

            #else

            Debug.LogWarning("Please import this asset: http://u3d.as/2QLg");
            
            #endif

            #endif
        }
    }
}