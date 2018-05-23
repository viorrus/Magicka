using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public static class EssentialExtensions
{
    #region IEnumerable Extensions

    public static IEnumerable<int> GetIndexes(this IEnumerable ienum)
    {
        var i = 0;

        var en = ienum.GetEnumerator();
        while (en.MoveNext())
        {
            yield return i++;
        }
        en = null;
    }

    public struct IndexerPair<T>
    {
        public T item;
        public int index;
    }

    public static IEnumerable<IndexerPair<T>> Enumerated<T>(this IEnumerable<T> ienum)
    {
        var i = 0;

        var en = ienum.GetEnumerator();
        try
        {
            while (en.MoveNext())
            {
                yield return new IndexerPair<T> { index = i++, item = en.Current };
            }
        }
        finally
        {
            if (en != null)
            {
                en.Dispose();
            }
        }
        en = null;
    }

    public static void Each<T>(this IEnumerable<T> ienum, Action<T> action = null)
    {
        foreach (var elem in ienum)
        {
            if (action != null)
            {
                action(elem);
            }
        }
    }

    #endregion IEnumerable Extensions

    #region Transform Extensions

    public static IEnumerable<IndexerPair<Transform>> ChildsEnumerated(this Transform tr)
    {
        return Enumerable.Range(0, tr.childCount)
            .Select(x => new IndexerPair<Transform> { index = x, item = tr.GetChild(x) });
    }

    public static IEnumerable<IndexerPair<T>> ChildsEnumerated<T>(this Transform tr) where T : Component
    {
        return ChildsEnumerated(tr)
            .Select(x => new IndexerPair<T> { index = x.index, item = x.item.GetComponent<T>() });
    }

    /// <summary>
    /// Скопировать позицию и поворот (свои и всех дочерних элементов).
    /// Используется, например, при спавне рэгдоллов.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public static void CopyTo(this Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;

        var min = Mathf.Min(from.childCount, to.childCount);
        for (var i = 0; i < min; i++)
        {
            from.GetChild(i).CopyTo(to.GetChild(i));
        }
    }

    #endregion Transform Extensions

    #region Object.Instantiate Extensions

    public static T InstantiateMe<T>(this T original) where T : Object
    {
        return Object.Instantiate(original);
    }

    public static T InstantiateMe<T>(this T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return Object.Instantiate(original, position, rotation);
    }

    public static T InstantiateMe<T>(this T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        return Object.Instantiate(original, position, rotation, parent);
    }

    public static T InstantiateMe<T>(this T original, Transform parent, bool worldPositionStays) where T : Object
    {
        return Object.Instantiate(original, parent, worldPositionStays);
    }

    public static T InstantiateMe<T>(this T original, Transform parent) where T : Object
    {
        return original.InstantiateMe(parent, false);
    }

    #endregion Object.Instantiate Extensions
}