using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class CuteResources
{
    public static string CUTEDANCER_RUNTIME = Path.Combine("Packages", "ca.theunifox.cutedancer", "Runtime");

    public static VisualTreeAsset LoadView(string name)
    {
        return Resources.Load<VisualTreeAsset>(Path.Combine("CuteDancer", "Views", name));
    }

}
