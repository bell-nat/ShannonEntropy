namespace ShannonEntropy;

public class PairChars(char first, char second)
{
    public string Value => $"{first}{second}";

    public override bool Equals(object? obj) => obj is PairChars pairChars && pairChars.Value == Value;

    public override int GetHashCode() => HashCode.Combine(Value);
}