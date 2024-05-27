using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColorChange : MonoBehaviour
{

    public void ChangeColor(Transform obj, Color color)
    {
        Transform ninjaChild = obj.transform.Find("Ninja");
        if (ninjaChild != null)
        {
            SkinnedMeshRenderer meshRenderer = ninjaChild.GetComponent<SkinnedMeshRenderer>();
            if (meshRenderer != null)
            {
                // If using a custom shader or the standard shader, the main color property might be "_Color"
                meshRenderer.material.SetColor("_Color", color);
            }
        }
    }
}
