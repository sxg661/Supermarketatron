using UnityEngine;
using System.Collections;

public class Wait : CustomerBehaviour
{
    public static float waitTime = 1f;

    public float myTime = 0f;

    public override void Init(SpriteRenderer renderer, Transform transform, DoneDel doneMethod)
    {
        done = doneMethod;
    }

    public override string MyType()
    {
        return "wait";
    }


    public override void OnCollision()
    {
        return;
    }

    public override void onCollsionEnd()
    {
        return;
    }

    public override void Update()
    {
        myTime += Time.deltaTime;

        if(myTime > waitTime)
        {
            done();
        }
    }
}
