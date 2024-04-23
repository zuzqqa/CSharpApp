namespace platformyTechnologiczne7.Services;
[Serializable]
public class CustomStringComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        int lengthComparision = x.Length.CompareTo(y.Length);

        return lengthComparision switch
        {
            0 => string.Compare(x, y, StringComparison.Ordinal),
            _ => lengthComparision
        };
    }
}