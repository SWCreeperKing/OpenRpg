using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenRpg;

public static class Extension
{
    public static T ToEnum<T>(this string s) where T : struct => Enum.Parse<T>(s, true);

    public static void Set<T>(this T t, T overrider, bool deep = false)
    {
        var type = typeof(T);
        var fields = deep ? type.GetRuntimeFields() : type.GetFields();
        foreach (var field in fields)
        {
            try
            {
                field.SetValue(t, field.GetValue(overrider));
            }
            catch (TargetException e)
            {
                Console.WriteLine($"FIELD: {field.Name} CORRUPT? {e.Message}");
            }
        }
    }

    public static string PickFromWeight(this Random r, params (string name, float weight)[] weights)
    {
        var totalWeight = weights.Select(w => w.weight).Sum();
        var picked = r.Next(totalWeight);
        return weights.All(w => w.weight < picked)
            ? weights[^1].name
            : weights.First(w => w.weight >= picked).name;
    }

    public static float Next(this Random r, float max) => r.Next(0f, max);
    public static double Next(this Random r, double max) => r.Next(0d, max);
    public static float Next(this Random r, float min, float max) => (float) (min + (max - min) * r.NextDouble());
    public static double Next(this Random r, double min, double max) => min + (max - min) * r.NextDouble();
}