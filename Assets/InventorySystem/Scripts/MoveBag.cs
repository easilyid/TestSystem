using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.Scripts
{
    public class MoveBag : MonoBehaviour,IDragHandler
    {
        private Canvas _canvas;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            //anchoredPosition是相对于父物体的位置
            //delta 是鼠标移动的距离
            _rectTransform.anchoredPosition += eventData.delta;
            
            
            //限制背包的移动范围，不让背包移出屏幕
             // if (_rectTransform.anchoredPosition.x < -Screen.width / 2 + 100 || _rectTransform.anchoredPosition.x > Screen.width / 2 - 100 ||
             //     _rectTransform.anchoredPosition.y < -Screen.height / 2 + 100 || _rectTransform.anchoredPosition.y > Screen.height / 2 - 100)
             // {
             //     _rectTransform.anchoredPosition = Vector2.zero;
             // }
            
             //如果拖离了屏幕就把背包放到最近的位置
             if (_rectTransform.anchoredPosition.x < -Screen.width / 2 + 100)
             {
                 _rectTransform.anchoredPosition = new Vector2(-Screen.width / 2 + 100,_rectTransform.anchoredPosition.y);
             }
             else if (_rectTransform.anchoredPosition.x > Screen.width / 2 - 100)
             {
                 _rectTransform.anchoredPosition = new Vector2(Screen.width / 2 - 100,_rectTransform.anchoredPosition.y);
             }
             else if (_rectTransform.anchoredPosition.y < -Screen.height / 2 + 100)
             {
                 _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x,-Screen.height / 2 + 100);
             }
             else if (_rectTransform.anchoredPosition.y > Screen.height / 2 - 100)
             {
                 _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x,Screen.height / 2 - 100);
             }
            
            //如果拖离了屏幕就把背包完整的放到最近的位置
            // if (_rectTransform.anchoredPosition.x < -Screen.width / 2 + 100)
            // {
            //     _rectTransform.anchoredPosition = new Vector2(-Screen.width / 2 + 100,_rectTransform.anchoredPosition.y);
            // }
            // else if (_rectTransform.anchoredPosition.x > Screen.width / 2 - 100)
            // {
            //     _rectTransform.anchoredPosition =
            //         new Vector2(Screen.width / 2 - 100, _rectTransform.anchoredPosition.y);
            // }
        }
    }
}