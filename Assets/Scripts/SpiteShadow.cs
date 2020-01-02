using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class SpiteShadow : MonoBehaviour
{
    public Vector2 offset = new Vector2(-3, -3);
    public Material shadowMaterial;
    public Color shadowColor;

    private SpriteRenderer prRndCaster;
    private SpriteRenderer prRndShadow;

    private Transform transCaster;
    private Transform transShadow;

    protected void Start()
    {
        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";
        transShadow.localRotation = Quaternion.identity;

        prRndCaster = GetComponent<SpriteRenderer>();
        prRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        prRndShadow.material = shadowMaterial;
        prRndShadow.color = shadowColor;
        prRndShadow.sortingLayerName = prRndCaster.sortingLayerName;
        prRndShadow.sortingOrder = prRndCaster.sortingOrder - 1;
    }

    private void LateUpdate()
    {
        prRndShadow.transform.position = new Vector2(
            transCaster.position.x + offset.x,
            transCaster.position.y + offset.y
        );
        prRndShadow.sprite = prRndCaster.sprite;
    }
}
