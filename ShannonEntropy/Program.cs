using ShannonEntropy;

Core core = new();
using StreamReader stream = new("source.txt");
string source = stream.ReadToEnd();
List<char> chars = source.ToList();
chars.ForEach(core.Counter);
core.ViewSingle();
for (int i = 0; i + 1 < chars.Count;)
{
    char first = chars[i++];
    char second = chars[i];
    core.Counter(first, second);
}

core.ViewPair();
var g = "";