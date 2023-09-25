using ShannonEntropy;

using StreamReader stream = new("source.txt");
string source = stream.ReadToEnd();

Core core = new();
core.Fill(source);

CoreHelper helper = new(core.Length);
helper.ViewSingle(core.SingleChars);
helper.ViewPair(core.PairChars);

ShannonFano shannonFano = new();
List<Node<char>> charNodes = shannonFano.BuildTree(core.SingleNodes);
List<Node<PairChars>> pairNodes = shannonFano.BuildTree(core.PairNodes);
helper.ViewSingle(charNodes);
helper.ViewPair(pairNodes);


var g = "";