using UnityEngine;

/// <summary>
/// Displays a configurable health bar for any object with a Damageable as a parent
/// </summary>
public class HealthBar : MonoBehaviour {

    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;
    IHPÑontroller HPconttroller;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        // get the damageable parent we're attached to
        HPconttroller = GetComponentInParent<IHPÑontroller>();
    }

    private void Start() {
        // Cache since Camera.main is super slow
        mainCamera = Camera.main;
    }

    private void FixedUpdate () {
        // Only display on partial health
        if (HPconttroller.CurrentHP < HPconttroller.MaxHP) {
            meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        } else {
            meshRenderer.enabled = false;
        }
    }

    private void UpdateParams() {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", HPconttroller.CurrentHP / (float)HPconttroller.MaxHP);
        meshRenderer.SetPropertyBlock(matBlock);
    }

    private void AlignCamera() {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

}