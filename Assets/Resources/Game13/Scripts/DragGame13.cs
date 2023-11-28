using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragGame13 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float ScaleIn = 0.5f;
    [SerializeField] private float ScaleOut = 0.33f;
    private float ScalingTime = 0.12f;

    bool isDragging = false;

    [SerializeField] private Transform rightSlot;
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Initial Pos")]
    [SerializeField] private Transform initialPos;
    [SerializeField] private SpriteRenderer objetsToMove;

    [Header("Doors")]
    public GameObject openDoor;
    public Transform openDoorTr;
    public SpriteRenderer spriteRenererDoorOpen;
    public GameObject closedDoor;
    public Transform closedDoorTr;
    public SpriteRenderer spriteRenererDoorClose;



    [SerializeField] GameManagerScript gameManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        BlinkingOn();
        transform.DOScale(new Vector2(ScaleIn, ScaleIn), ScalingTime).OnComplete(() => transform.DOScale(new Vector2(ScaleOut, ScaleOut), ScalingTime));

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
       
    }
    private void Start()
    {
        Input.multiTouchEnabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
         if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            spriteRenererDoorOpen.DOKill();
            spriteRenererDoorClose.DOKill();
            if (Vector2.Distance(closedDoorTr.position, transform.position) < 4f)
            {//open door
                spriteRenererDoorOpen.DOFade(255f, 0.015f);
                spriteRenererDoorClose.DOFade(0f, 0.015f);
            }
            else if (Vector2.Distance(closedDoorTr.position, transform.position) > 3f)
            {//close door
                spriteRenererDoorOpen.DOFade(0f, 0.015f);
                spriteRenererDoorClose.DOFade(255f, 0.015f);
            }


            if (Vector2.Distance(transform.position, rightSlot.position) < 0.88f)
            {
                isDragging = false;
                collider2d.enabled = false;
                spriteRenderer.sortingOrder = 20;
                objetsToMove.DOKill();
                
                transform.DOMove(rightSlot.position, 0.7f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    gameManager.counterGame++;
                    spriteRenderer.sortingOrder = 19;
                    gameManager.MoveTopObjectToDown();
                    spriteRenererDoorOpen.DOFade(0f, 0.015f);
                    spriteRenererDoorClose.DOFade(255f, 0.015f);
                });
            }
            
            if (!isDragging)
            {
                return;
            }
        }

    }



    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging)
        {
            return;
        }

        if (collider2d.enabled)
        {

            // Close Door effect
            spriteRenererDoorOpen.DOFade(0f, 0.015f);
            spriteRenererDoorClose.DOFade(100f, 0.015f);
            objetsToMove.DOFade(0f, 0.11f).OnComplete(() => objetsToMove.DOKill());
            transform.DOMove(initialPos.position, 0.68f).SetEase(Ease.OutBack);
        }
    }
    void BlinkingOn()
    {
        if (isDragging)
        {
            objetsToMove.DOFade(0.55f, 0.9f).SetLoops(-1, LoopType.Yoyo);
        }
    }
    IEnumerator moveSlot()
    {

    }
}
