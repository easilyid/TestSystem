using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.Scripts
{
    public class ItemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private CanvasGroup _canvasGroup; //用于控制物品的显示和隐藏
        private Vector3 _originalPosition; //用于记录物品原来的位置
        private Transform _originalParent; //用于记录物品原来的父物体

        public Inventory mybag; //用于记录物品属于哪个背包
        public int currentId; //用于记录物品在背包中的位置

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalParent = transform.parent;
            currentId = _originalParent.GetComponent<Slots>().slotId;
            transform.SetParent(transform.parent.parent);
            transform.position = eventData.position;
            _canvasGroup.blocksRaycasts = false;
        }


        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            var pointerGameObject = eventData.pointerCurrentRaycast.gameObject;
            //如果拖拽到了世界UI上就把物品放回原来的位置
            if (pointerGameObject != null)
            {
                //两个物品交换位置
                if (pointerGameObject.name == "ItemImage")
                {
                    transform.SetParent(pointerGameObject.transform.parent.parent);
                    transform.position = pointerGameObject.transform.parent.parent.position;
                    var temp = mybag.itemsList[currentId];
                    mybag.itemsList[currentId] = mybag.itemsList[pointerGameObject.GetComponentInParent<Slots>().slotId];
                    mybag.itemsList[pointerGameObject.GetComponentInParent<Slots>().slotId] = temp;

                    //交换位置
                    pointerGameObject.transform.parent.position = _originalParent.position;
                    //将物品的父物体设置为物品槽
                    pointerGameObject.transform.parent.SetParent(_originalParent);
                    _canvasGroup.blocksRaycasts = true;
                    return;
                }

                //防止物品拖拽到背包外面
                if (pointerGameObject.gameObject.name == "Slots(Clone)")
                {
                    //如果没有拖拽到物品槽中，就将物品放回原来的位置
                    transform.SetParent(pointerGameObject.transform);
                    transform.position = pointerGameObject.transform.position;
                    mybag.itemsList[pointerGameObject.GetComponentInParent<Slots>().slotId] = mybag.itemsList[currentId];
                    //mybag.itemsList[currentId] = null; 这样写在没有移动位置的情况下会出现空指针异常
                    if (pointerGameObject.gameObject.GetComponent<Slots>().slotId != currentId)
                    {
                        //这样写在没有移动位置的情况下不会出现空指针异常
                        mybag.itemsList[currentId] = null;
                    }

                    _canvasGroup.blocksRaycasts = true;
                    return;
                }
            }

            //其他位置都将物品放回原来的位置
            transform.SetParent(_originalParent);
            transform.position = _originalParent.position;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}