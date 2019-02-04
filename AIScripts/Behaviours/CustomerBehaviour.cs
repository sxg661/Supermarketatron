using UnityEngine;
using UnityEditor;

public abstract class CustomerBehaviour
{
    public delegate void DoneDel();
    public DoneDel done;

    public abstract void Init(SpriteRenderer renderer, Transform transform, DoneDel doneMethod);

    public abstract void Update();

    public abstract string MyType();

    public abstract void OnCollision();
    public abstract void onCollsionEnd();

}