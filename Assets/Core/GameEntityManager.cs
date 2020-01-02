using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void EntityDelegate<T0>(T0 a0);
public delegate void EntityDelegate<T0, T1>(T0 a0, T1 a1);
public delegate void EntityDelegate<A0, A1, A2>(A0 a0, A1 a1, A2 a2);
public delegate void EntityDelegate<A0, A1, A2, A3>(A0 a0, A1 a1, A2 a2, A3 a3);
public delegate void EntityDelegate<A0, A1, A2, A3, A4>(A0 a0, A1 a1, A2 a2, A3 a3, A4 a4);


public delegate object[] SelectorDelegate(GameObject gameObject);

public class GameEntityManager
{
    private Dictionary<GameObject, Dictionary<string, object>> Dict = new Dictionary<GameObject, Dictionary<string, object>>();
    //private Dictionary<GameObject, Dictionary<string, object[]>> SingleCache = new Dictionary<GameObject, Dictionary<string, object[]>>();
    private Dictionary<GameObject, Dictionary<SelectorDelegate, object[]>> SelectorSingleCache = new Dictionary<GameObject, Dictionary<SelectorDelegate, object[]>>();
    private Dictionary<SelectorDelegate, List<object[]>> SelectorCache = new Dictionary<SelectorDelegate, List<object[]>>();

    private Dictionary<string, List<object[]>> Cache = new Dictionary<string, List<object[]>>();

    public void SelectorSingle<T1>(GameObject go, SelectorDelegate selector, EntityDelegate<T1> action)
    {
        SelectorSingleGeneric(go, selector, action.Method, action.Target);
    }

    public void SelectorSingle<T1, T2>(GameObject go, SelectorDelegate selector, EntityDelegate<T1, T2> action)
    {
        SelectorSingleGeneric(go, selector, action.Method, action.Target);
    }

    public void SelectorSingle<T1, T2, T3>(GameObject go, SelectorDelegate selector, EntityDelegate<T1, T2, T3> action)
    {
        SelectorSingleGeneric(go, selector, action.Method, action.Target);
    }

    public void SelectorSingle<T1, T2, T3, T4>(GameObject go, SelectorDelegate selector, EntityDelegate<T1, T2, T3, T4> action)
    {
        SelectorSingleGeneric(go, selector, action.Method, action.Target);
    }

    private void SelectorSingleGeneric(GameObject go, SelectorDelegate selector, System.Reflection.MethodInfo methodInfo, object instance)
    {
        if (SelectorSingleCache.ContainsKey(go) && SelectorSingleCache[go].ContainsKey(selector)) {
            methodInfo.Invoke(instance, SelectorSingleCache[go][selector]);
        }
        else
        {
            var objects = selector(go);
            if (objects != null) {
                if (!SelectorSingleCache.ContainsKey(go)) {
                    SelectorSingleCache.Add(go, new Dictionary<SelectorDelegate, object[]>());
                }
                SelectorSingleCache[go].Add(selector, objects);
                methodInfo.Invoke(instance, objects);
            }
        }
    }

    public void SelectorForEach<T1>(SelectorDelegate selector, EntityDelegate<T1> action)
    {
        SelectorForEachGeneric(selector, action.Method, action.Target);
    }

    public void SelectorForEach<T1, T2>(SelectorDelegate selector, EntityDelegate<T1, T2> action)
    {
        SelectorForEachGeneric(selector, action.Method, action.Target);
    }

    public void SelectorForEach<T1, T2, T3>(SelectorDelegate selector, EntityDelegate<T1, T2, T3> action)
    {
        SelectorForEachGeneric(selector, action.Method, action.Target);
    }

    public void SelectorForEach<T1, T2, T3, T4>(SelectorDelegate selector, EntityDelegate<T1, T2, T3, T4> action)
    {
        SelectorForEachGeneric(selector, action.Method, action.Target);
    }

    private void SelectorForEachGeneric(SelectorDelegate selector, System.Reflection.MethodInfo methodInfo, object instance)
    {
        SelectorQuery(selector).ForEach(objects => {
            methodInfo.Invoke(instance, objects);
        });
    }

    private List<object[]> SelectorQuery(SelectorDelegate selector) {
        if (SelectorCache.ContainsKey(selector)) {
            return SelectorCache[selector];
        }
        else
        {
            List<object[]> list = new List<object[]>();
            foreach (KeyValuePair<GameObject, Dictionary<string, object>> entry in Dict)
            {
                var objects = selector(entry.Key);
                if (objects != null) {
                    list.Add(objects);
                }
            }
            SelectorCache.Add(selector, list);
            return list;
        }
    }

    public void Add(GameObject go) {
        Add(go, go.GetComponents(typeof(Component)));
    }

    public void Add(GameObject go, object[] objects) {
        var dict = new Dictionary<string, object>();
        foreach (var obj in objects)
        {
            dict.Add(obj.GetType().FullName, obj);
        }
        foreach (KeyValuePair<string, List<object[]>> cache in Cache)
        {
            var names = cache.Key.Split(' ');
            bool isContain = true;
            foreach (string name in names) {
                if (!dict.ContainsKey(name)) {
                    isContain = false;
                }
            }
            if (isContain) {
                object[] objectsCache = new object[names.Length];
                for (int i = 0; i < names.Length; i++) {
                    objectsCache[i] = dict[names[i]];
                }
                cache.Value.Add(objectsCache);
            }
        }
        Dict.Add(go, dict);
    }

    public void Remove(GameObject go) {
        if (!Dict.ContainsKey(go))
        {
            return;
        }
        var dict = Dict[go];
        foreach (KeyValuePair<string, List<object[]>> cache in Cache)
        {
            var names = cache.Key.Split(' ');
            bool isContain = true;
            foreach (string name in names) {
                if (!dict.ContainsKey(name)) {
                    isContain = false;
                }
            }
            if (isContain) {
                var itemToRemove = cache.Value.Find(list => {
                    return ((MonoBehaviour)list[0]).gameObject == go;
                });
                if (itemToRemove != null) {
                    cache.Value.Remove(itemToRemove);
                }
            }
        }
        //SingleCache.Remove(go);
        foreach (KeyValuePair<SelectorDelegate, List<object[]>> cache in SelectorCache)
        {
            if (cache.Key(go) != null) {
                var itemToRemove = cache.Value.Find(list => {
                    return ((MonoBehaviour)list[0]).gameObject == go;
                });
                if (itemToRemove != null) {
                    cache.Value.Remove(itemToRemove);
                }
            }
        }
        SelectorSingleCache.Remove(go);
        Dict.Remove(go);
    }

    public void ForEach<T1>(EntityDelegate<T1> action)
    {
        ForEachGeneric(action.Method, action.Target, action.Method.GetParameters());
    }

    public void ForEach<T1, T2>(EntityDelegate<T1, T2> action)
    {
        ForEachGeneric(action.Method, action.Target, action.Method.GetParameters());
    }

    public void ForEach<T1, T2, T3>(EntityDelegate<T1, T2, T3> action)
    {
        ForEachGeneric(action.Method, action.Target, action.Method.GetParameters());
    }

    public void ForEach<T1, T2, T3, T4>(EntityDelegate<T1, T2, T3, T4> action)
    {
        ForEachGeneric(action.Method, action.Target, action.Method.GetParameters());
    }

    public void ForEach<T1, T2, T3, T4, T5>(EntityDelegate<T1, T2, T3, T4, T5> action)
    {
        ForEachGeneric(action.Method, action.Target, action.Method.GetParameters());
    }

    public void ForEachGeneric(System.Reflection.MethodInfo methodInfo, object instance, System.Reflection.ParameterInfo[] parameters)
    {
        string[] names = new string[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            names[i] = parameters[i].ParameterType.FullName;
        }
        Query(names).ForEach(objects => {
            methodInfo.Invoke(instance, objects);
        });
    }

    public void Single<T1>(GameObject go, EntityDelegate<T1> action, bool isChild = true)
    {
        SingleGeneric(go, action.Method, action.Target, action.Method.GetParameters(), isChild);
    }

    public void Single<T1, T2>(GameObject go, EntityDelegate<T1, T2> action, bool isChild = true)
    {
        SingleGeneric(go, action.Method, action.Target, action.Method.GetParameters(), isChild);
    }

    public void Single<T1, T2, T3>(GameObject go, EntityDelegate<T1, T2, T3> action, bool isChild = true)
    {
        SingleGeneric(go, action.Method, action.Target, action.Method.GetParameters(), isChild);
    }

    public void Single<T1, T2, T3, T4>(GameObject go, EntityDelegate<T1, T2, T3, T4> action, bool isChild = true)
    {
        SingleGeneric(go, action.Method, action.Target, action.Method.GetParameters(), isChild);
    }

    private void SingleGeneric(GameObject go, System.Reflection.MethodInfo methodInfo, object instance, System.Reflection.ParameterInfo[] parameters, bool isChild)
    {
        if (isChild && !Dict.ContainsKey(go))
        {
            go = go.transform.parent.gameObject;
        }
        if (!Dict.ContainsKey(go)) {
            return;
        }
        var dict = Dict[go];
        bool isContains = true;
        object[] objects = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            var name = parameters[i].ParameterType.FullName;
            if (dict.ContainsKey(name))
            {
                objects[i] = dict[parameters[i].ParameterType.FullName];
            }
            else
            {
                isContains = false;
                break;
            }
        }
        if (isContains)
        {
            methodInfo.Invoke(instance, objects);
        }
    }

    private List<object[]> Query(string[] names) {
        string key = String.Join(" ", names);
        if (Cache.ContainsKey(key)) {
            return Cache[key];
        }
        else
        {
            List<object[]> list = new List<object[]>();
            foreach (KeyValuePair<GameObject, Dictionary<string, object>> entry in Dict)
            {
                bool isContain = true;
                foreach (string name in names) {
                    if (!entry.Value.ContainsKey(name)) {
                        isContain = false;
                    }
                }
                if (isContain) {
                    object[] objects = new object[names.Length];
                    for (int i = 0; i < names.Length; i++) {
                        objects[i] = entry.Value[names[i]];
                    }
                    list.Add(objects);
                }
            }
            Cache.Add(key, list);
            return list;
        }
    }
}
