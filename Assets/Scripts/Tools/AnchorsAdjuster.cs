using UnityEngine;
using UnityEditor;

public class AnchorsAdjuster : EditorWindow
{
	[MenuItem("Tools/AnchorsAdjuster _F1")]
	private static void SetAnchorsByRect()
	{
		var targetObject = GetSelectedObject();

		RectTransform rectTransform = targetObject.GetComponent<RectTransform>();

		Vector2 offsetMin = rectTransform.offsetMin;
		Vector2 offsetMax = rectTransform.offsetMax;

		RectTransform parent = rectTransform.parent.GetComponent<RectTransform>();

		Vector2 newAnchorMin = rectTransform.anchorMin + (offsetMin / parent.rect.size);
		Vector2 newAnchorMax = rectTransform.anchorMax + (offsetMax / parent.rect.size);

		rectTransform.anchorMin = newAnchorMin;
		rectTransform.anchorMax = newAnchorMax;

		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;
	}

	private static GameObject GetSelectedObject()
	{
		var selectedObject = Selection.activeGameObject.gameObject;

		if (selectedObject == null)
		{
			Debug.LogError("No GameObject selected.");
			return null;
		}

		return selectedObject;
	}
}