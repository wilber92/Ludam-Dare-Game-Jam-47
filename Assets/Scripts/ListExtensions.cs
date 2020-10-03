using System.Collections.Generic;

public static class ListExtensions {
    public static T GetRandomFromList<T>(this List<T> list) {
        var count = list.Count;
        var random = UnityEngine.Random.Range(0, count);
        return list[random];
    }
}