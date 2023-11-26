using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GunMaterialChanger : MonoBehaviour
{
    [Header("Materials")] 
    [SerializeField] private List<MeshRenderer> renderers;
    [SerializeField] private Material lockedMaterial;
    [SerializeField] private Material unlockedMaterial;
    


    public void SetUnlockedMaterial()
    {
        renderers.ForEach(r => r.sharedMaterial = unlockedMaterial);
    }
    
    public void SetLockedMaterial()
    {
        renderers.ForEach(r => r.sharedMaterial = lockedMaterial);
    }
    
    [ContextMenu("Assign References")]
    private void Reset()
    {
        renderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
        unlockedMaterial = renderers[0].sharedMaterial;
    }
}
