using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class QuestPointer : MonoBehaviour
{
    [SerializeField] private Camera UICamera;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite crossSprite;

    private Transform target;
    private RectTransform pointerRect;
    private Image pointerImage;
    private float offsetZ = -90f;

    private void Awake()
    {
        pointerRect = GetComponent<RectTransform>();
        pointerImage = GetComponent<Image>();
    }
    private void OnEnable()
    {
        LevelDirector.OnQuestTargetChanged += SetQuestTarget;
    }
    private void OnDisable()
    {
        LevelDirector.OnQuestTargetChanged -= SetQuestTarget;
    }
    private void Update()
    {
        if (target == null)
        {
            pointerImage.enabled = false;
            return;
        }
        pointerImage.enabled = true;

        MoveIcon();
    }

    private void MoveIcon()
    {
        float borderSize = 100f;
        Vector3 targetPosScreenPoint = Camera.main.WorldToScreenPoint(target.position);
        bool isOffScreen = targetPosScreenPoint.x <= borderSize
            || targetPosScreenPoint.x >= Screen.width - borderSize
            || targetPosScreenPoint.y <= borderSize
            || targetPosScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
        {
            RotatePointerTowardsTargetPos();
            pointerImage.sprite = arrowSprite;
            Vector3 cappedTargetScreenPos = targetPosScreenPoint;
            if (cappedTargetScreenPos.x <= borderSize) cappedTargetScreenPos.x = borderSize;
            if (cappedTargetScreenPos.x >= Screen.width - borderSize) cappedTargetScreenPos.x = Screen.width - borderSize;
            if (cappedTargetScreenPos.y <= borderSize) cappedTargetScreenPos.y = borderSize;
            if (cappedTargetScreenPos.y >= Screen.height - borderSize) cappedTargetScreenPos.y = Screen.height - borderSize;

            Vector3 pointerWorldPos = UICamera.ScreenToWorldPoint(cappedTargetScreenPos);
            pointerRect.position = pointerWorldPos;
            pointerRect.localPosition = new Vector3(pointerRect.localPosition.x, pointerRect.localPosition.y, 0f);
        }
        else
        {
            pointerImage.sprite = crossSprite;
            Vector3 pointerWorldPos = UICamera.ScreenToWorldPoint(targetPosScreenPoint);
            pointerRect.position = pointerWorldPos;
            pointerRect.localPosition = new Vector3(pointerRect.localPosition.x, pointerRect.localPosition.y, 0f);
            pointerRect.localEulerAngles = Vector3.zero;
        }
    }
    private void RotatePointerTowardsTargetPos()
    {
        Vector3 toPos = target.position;
        Vector3 fromPos = Camera.main.transform.position;
        fromPos.z = 0f;
        Vector3 dir = (toPos - fromPos).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRect.localEulerAngles = new Vector3(0, 0, angle + offsetZ);
    }
    public void SetQuestTarget(Transform target)
    {
        this.target = target;
    }
}
