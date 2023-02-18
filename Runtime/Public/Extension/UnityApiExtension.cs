﻿using UnityEngine;

public static partial class UnityApiExtension
{
    public static T _Find<T>(this GameObject go, string path)
    {
        return go.transform.Find(path).GetComponent<T>();
    }

    public static T[] _Finds<T>(this GameObject go, string path)
    {
        return go.transform.Find(path).GetComponents<T>();
    }

    public static void _TryRemoveComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        if (go.TryGetComponent<T>(out var comp))
        {
            GameObject.Destroy(comp);
        }
    }

    public static void _SetParent(this GameObject gameObject, Transform parent, bool reset = false)
    {
        gameObject.transform.SetParent(parent);
        if (reset)
            gameObject.transform._Reset();
    }

    public static void _SetParent(this Transform transform, Transform parent, bool reset = false)
    {
        transform.SetParent(parent.transform);
        if (reset)
            transform.transform._Reset();
    }

    public static Transform _Position(this Transform tf, Vector3 pos)
    {
        tf.position = pos;
        return tf;
    }

    public static Transform _Roration(this Transform tf, Quaternion rotation)
    {
        tf.rotation = rotation;
        return tf;
    }

    public static Transform _LocalPosition(this Transform tf, Vector3 pos)
    {
        tf.localPosition = pos;
        return tf;
    }

    public static Transform _LocalRotation(this Transform tf, Quaternion rotation)
    {
        tf.localRotation = rotation;
        return tf;
    }

    public static Transform _LocalScale(this Transform tf, Vector3 scale)
    {
        tf.localScale = scale;
        return tf;
    }

    public static void _Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static RectTransform _RectTransform(this Transform transform)
    {
        return transform.GetComponent<RectTransform>();
    }

    public static void _MaxAnchors(this RectTransform rectTransform)
    {
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}