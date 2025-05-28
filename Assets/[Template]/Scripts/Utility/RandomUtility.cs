


using System.Collections.Generic;
using UnityEngine;

public static class RandomUtility
{
    public static bool ReturnTrueForChance(float chance)
    {
        return Random.Range(0f, 1f) <= chance;
    }

    public static void ActivateRandomObjectInCollection<T>(List<T> items) where T : Component
    {
        if (items == null || items.Count == 0)
        {
            Debug.LogWarning("The list is null or empty.");
            return;
        }

        int randomIndex = Random.Range(0, items.Count);

        for (int i = 0; i < items.Count; i++)
        {
            if(i == randomIndex) items[i].gameObject.SetActive(true);
            else items[i].gameObject.SetActive(false);
        }
    }

    public static void ActivateRandomObjectInCollection<T>(T[] items) where T : Component
    {
        if (items == null || items.Length == 0)
        {
            Debug.LogWarning("The array is null or empty.");
            return;
        }

        int randomIndex = Random.Range(0, items.Length);

        for (int i = 0; i < items.Length; i++)
        {
            if(i == randomIndex) items[i].gameObject.SetActive(true);
            else items[i].gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 
    /// with given size, 0 .... size-1  indexes
    /// returns a list of indexes in that range but randomly distributed
    /// 
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static List<int> GetRandomizedIndexes(int size)
    {
        List<int> rawList = new List<int>();
        List<int> resultList = new List<int>();

        for (int i = 0; i < size; i++)
        {
            rawList.Add(i);
        }

        for (int j = 0; j < size; j++)
        {
            int a = rawList[Random.Range(0, rawList.Count)];

            rawList.Remove(a);

            resultList.Add(a);
        }

        return resultList;
    }

    private static Vector3 randomVector3;
    public static Vector3 GetRandomVector(Vector3 center, float radius, Vector3 axis)
    {
        randomVector3.x = center.x + (UnityEngine.Random.Range(-radius, radius) * axis.x);
        randomVector3.y = center.y + (UnityEngine.Random.Range(-radius, radius) * axis.y);
        randomVector3.z = center.z + (UnityEngine.Random.Range(-radius, radius) * axis.z);
        return randomVector3;
    }
}