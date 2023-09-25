namespace ShannonEntropy;

public class Node<T>(T data, int count, string code)
{
    public T Data => data;
    public int Count => count;
    public string Code { get; set; } = code;
}