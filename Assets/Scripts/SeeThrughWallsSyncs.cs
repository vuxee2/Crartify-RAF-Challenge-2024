using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrughWallsSyncs : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_PlayerPosition");
    public static int SizeID = Shader.PropertyToID("_Size");

    public Material[] WallMaterials;
    public Camera Camera;
    public LayerMask Mask;

    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);

        if(Physics.Raycast(ray, 3000, Mask))
        {
            foreach(Material mat in WallMaterials)
                mat.SetFloat(SizeID, 1);
        }
        else
        {
            foreach(Material mat in WallMaterials)
                mat.SetFloat(SizeID, 0);
        }

        var view = Camera.WorldToViewportPoint(transform.position);
        foreach(Material mat in WallMaterials)
            mat.SetVector(PosID, view);
    }

}
