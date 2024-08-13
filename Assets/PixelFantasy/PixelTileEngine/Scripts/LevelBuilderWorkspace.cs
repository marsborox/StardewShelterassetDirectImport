using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public class LevelBuilderWorkspace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public LevelBuilder LevelBuilder;

        private Vector3 _pointerDown, _camPosition;

        public void Update()
        {
            LevelBuilder.MoveCursor(Input.mousePosition);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            LevelBuilder.EnableCursor(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LevelBuilder.EnableCursor(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
            {
                LevelBuilder.Draw(eventData.position);
            }
            else if (Input.GetMouseButton(1))
            {
                _pointerDown = eventData.position;
                _camPosition = Camera.main.transform.position;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
            {
                LevelBuilder.Draw(eventData.position);
            }
            else if (Input.GetMouseButton(1))
            {
                Camera.main.transform.position = _camPosition + Camera.main.ScreenToWorldPoint(_pointerDown) - Camera.main.ScreenToWorldPoint(eventData.position);
            }
        }
    }
}