using DG.Tweening;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;
    private Vector3 offset;
    public SpriteRenderer spriteRendere;
    public MoteToStart backStart;
    public BoxCollider2D BC2D;
    public Transform rightSlot;
    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.11f).OnComplete(() => transform.DOScale(new Vector2(1f, 1f), 0.11f));
        BlinkingOn();
    }

    void OnMouseUp()
    {
        isDragging = false;
        spriteRendere.DOKill();
        spriteRendere.DOFade(0f, 0.22f);
        if (BC2D.enabled == true) {
            backStart.initialPosition();
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
        if (Vector2.Distance(transform.position, rightSlot.position) < 2f)
        {
            BC2D.enabled = false;
            isDragging = false;
            transform.DOMove(rightSlot.position, 0.6f);
        }
    }
    void BlinkingOn()
    {
        if (isDragging)
        {
            spriteRendere.DOFade(0.65f, 0.7f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
