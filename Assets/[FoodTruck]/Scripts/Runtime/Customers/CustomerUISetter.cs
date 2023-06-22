using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerUISetter : MonoBehaviour
{
    public Transform canvas;
    public Vector3 left, right, up, down = Vector3.zero;
    public bool isLeft, isRight, isUp, isDown = false;

    public void SetCanvasPosition(int value)
    {
        if (value >= 0 && value < 3)
        {
            isLeft = true;
            canvas.localPosition = left;
        }
        else if (value >= 3 && value < 5)
        {
            isDown = true;
            canvas.localPosition = down;
        }
        else if (value >= 5 && value < 8)
        {
            isRight = true;
            canvas.localPosition = right;
        }
        else if (value >= 8)
        {
            isUp = true;
            canvas.localPosition = up;
        }
    }
}
