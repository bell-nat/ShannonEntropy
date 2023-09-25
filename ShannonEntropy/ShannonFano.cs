using System.Xml.Linq;

namespace ShannonEntropy;

public class ShannonFano
{
    public List<Node<T>> BuildTree<T>(List<Node<T>> nodes) where T : notnull
    {
        if (nodes.Count is 0) { return nodes; }

        nodes = nodes.OrderByDescending(node => node.Count).ToList();

        Encode(nodes, 0, nodes.Count - 1, "");

        return nodes;
    }

    private void Encode<T>(List<Node<T>> nodes, int start, int end, string prefix) where T : notnull
    {
        if (start == end)
        {
            nodes[start].Code = prefix;
            return;
        }

        double totalProbability = nodes.Skip(start).Take(end - start + 1).Sum(node => node.Count);

        double halfProbability = 0;
        int index = start;

        while (halfProbability < totalProbability / 2)
        {
            halfProbability += nodes[index].Count;
            index++;
        }

        for (int i = start; i < index; i++)
            nodes[i].Code += "0";

        for (int i = index; i <= end; i++)
            nodes[i].Code += "1";

        Encode(nodes, start, index - 1, prefix + "0");
        Encode(nodes, index, end, prefix + "1");
    }
}