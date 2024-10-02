#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class AnchorTool : MonoBehaviour
{
    [MenuItem("SimpleTool/Anchors to Corners _F1")]
    private static void AnchorsToCorners()
    {
        foreach (var transform in Selection.transforms)
        {
            var t = transform as RectTransform;
            var pt = Selection.activeTransform.parent as RectTransform;

            if (t == null || pt == null) return;

            var newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                t.anchorMin.y + t.offsetMin.y / pt.rect.height);
            var newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                t.anchorMax.y + t.offsetMax.y / pt.rect.height);

            t.anchorMin = newAnchorsMin;
            t.anchorMax = newAnchorsMax;
            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }
    }
}
#endif