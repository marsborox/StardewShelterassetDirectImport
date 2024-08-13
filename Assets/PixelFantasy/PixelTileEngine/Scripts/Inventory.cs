using UnityEngine;
using UnityEngine.UI;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public class Inventory : MonoBehaviour
    {
        public SpriteCollection SpriteCollection;
        public LevelBuilder LevelBuilder;
        public Transform Grid;
        public GameObject InventoryItem;

        public void Start()
        {
            SwitchTab(0);
        }

        public void SwitchTab(int tab)
        {
            foreach (Transform child in Grid)
            {
                Destroy(child.gameObject);
            }

            CreateInventoryItem("Empty", SpriteCollection.DeleteSprite, tab, -1);
            
            switch (tab)
            {
                case 0:
                case 1:
                    var tilesets = tab == 0 ? SpriteCollection.GroundTilesets : SpriteCollection.CoverTilesets;

                    for (var i = 0; i < tilesets.Count; i++)
                    {
                        CreateInventoryItem(tilesets[i].Name, tilesets[i].Sprites[tab == 0 ? 20 : 0], tab, i);
                    }
                    break;
                case 2:
                case 3:
                    var sprites = tab == 2 ? SpriteCollection.PropsSprites : SpriteCollection.OtherSprites;

                    for (var i = 0; i < sprites.Count; i++)
                    {
                        CreateInventoryItem(sprites[i].name, sprites[i], tab, i);
                    }
                    break;
            }
        }

        private void CreateInventoryItem(string itemName, Sprite sprite, int type, int index)
        {
            var item = Instantiate(InventoryItem, Grid);
            var toggle = item.GetComponent<Toggle>();

            item.name = itemName;
            item.transform.Find("Icon").GetComponent<Image>().sprite = sprite;
            toggle.onValueChanged.AddListener(value => { if (value) LevelBuilder.SwitchTile(type, index); });
            toggle.group = Grid.GetComponent<ToggleGroup>();
            toggle.isOn = index == 0;
        }
    }
}