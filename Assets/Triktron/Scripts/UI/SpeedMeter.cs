using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
    public Image Bar;
    public Rigidbody Player;
    Material BarMat;

    public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    private void Start()
    {
        BarMat = Bar.material;
    }

    void Update()
    {
        BarMat.SetFloat("_Velocety", curve.Evaluate(Player.velocity.sqrMagnitude));
    }
}
