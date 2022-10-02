using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Cutout : Image
{
    public override Material materialForRendering
    {
        get{
            Material copy = new Material(base.materialForRendering);

            copy.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return copy;
        }
    }
}
