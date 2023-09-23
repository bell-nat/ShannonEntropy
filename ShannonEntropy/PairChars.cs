namespace ShannonEntropy;

public class PairChars(char first, char second)
{
    public string Pair => $"{first}{second}";

    public override bool Equals(object? obj) => obj is PairChars pairChars && pairChars.Pair == Pair;

    public override int GetHashCode() => HashCode.Combine(Pair);
}