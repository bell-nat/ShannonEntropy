using ShannonEntropy;

Core core = new();
using StreamReader stream = new("source.txt");
string source = stream.ReadToEnd();
source.ToList().ForEach(core.Counter);