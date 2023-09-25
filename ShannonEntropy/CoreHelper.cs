namespace ShannonEntropy;

public class CoreHelper(int length)
{
    public void ViewSingle(Dictionary<char, int> singleChars)
    {
        double entropy = 0;
        foreach ((char key, int count) in singleChars)
        {
            View(ref entropy, count, $"{key}");
        }

        GeneralInfo(entropy);
    }
    public void ViewSingle(List<Node<char>> charNodes)
    {
        foreach (Node<char> node in charNodes)
        {
           Console.WriteLine(node.Data);
           Console.WriteLine((double)node.Count / length);
           Console.WriteLine(node.Code);
        }
    }

    public void ViewPair(Dictionary<PairChars, int> pairChars)
    {
        double entropy = 0;
        foreach ((PairChars pair, int count) in pairChars)
        {
            View(ref entropy, count, pair.Value);
        }

        GeneralInfo(entropy);
    }

    public void ViewPair(List<Node<PairChars>> pairNodes)
    {
        foreach (Node<PairChars> node in pairNodes)
        {
            Console.WriteLine(node.Data.Value);
            Console.WriteLine((double)node.Count / length);
            Console.WriteLine(node.Code);
        }
    }

    private void View(ref double entropy, int count, string key)
    {
        double probability = (double)count / length;
        double currentEntropy = probability * Math.Log(probability, 2);
        Console.WriteLine($"{key}\t{count}\t{probability:0.00000000}\t{currentEntropy:0.00000000}");
        entropy -= currentEntropy;
    }

    private void GeneralInfo(double entropy)
    {
        Console.WriteLine();
        Console.WriteLine($"Entropy: {entropy:0.00000000}");
        Console.WriteLine($"Length: {length}");
    }
}