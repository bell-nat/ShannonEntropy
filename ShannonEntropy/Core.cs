namespace ShannonEntropy;

public sealed class Core
{
    private readonly char[] _alphabet =
    {
        'а', 'б', 'в', 'г', 'д', 'е',
        'ё', 'ж', 'з', 'и', 'й', 'к',
        'л', 'м', 'н', 'о', 'п', 'р',
        'с', 'т', 'у', 'ф', 'х', 'ц',
        'ч', 'ш', 'щ', 'ъ', 'ы', 'ь',
        'э', 'ю', 'я', ' '
    };

    private readonly Dictionary<char, int> _singleChars;
    public Dictionary<char, int> SingleChars => _singleChars;
    private List<Node<char>>? _singleNodes;
    public List<Node<char>> SingleNodes => _singleNodes ??= CreateNode(_singleChars);

    private readonly Dictionary<PairChars, int> _pairChars;
    public Dictionary<PairChars, int> PairChars => _pairChars;
    private List<Node<PairChars>>? _pairNodes;
    public List<Node<PairChars>> PairNodes => _pairNodes ??= CreateNode(_pairChars);

    private List<Node<T>> CreateNode<T>(Dictionary<T, int> d) where T: notnull => d.Select(pair => new Node<T>(pair.Key, pair.Value, string.Empty)).ToList();
    
    private int? _length;
    public int Length => _length ??= _singleChars.Select(pair => pair.Value).Sum();

    public Core()
    {
        _singleChars = _alphabet
        .Select(ch => new KeyValuePair<char, int>(ch, 0))
        .ToDictionary();
        _pairChars = [];
    }

    public void Fill(string source)
    {
        List<char> chars = source.ToList();
        chars.ForEach(Counter);
        for (int i = 0; i + 1 < chars.Count;)
        {
            Counter(chars[i++], chars[i]);
        }

        FinishFill();
    }
    
    private void Counter(char key)
    {
        key = char.ToLower(key);
        if (_alphabet.Contains(key)) { _singleChars[key]++; }
    }

    private void Counter(char first, char second)
    {
        first = char.ToLower(first);
        second = char.ToLower(second);
        if (!_alphabet.Contains(first) || !_alphabet.Contains(second)) { return; }
        PairChars pair = new(first, second);
        if (!_pairChars.TryAdd(pair, 1)) { _pairChars[pair]++; }
    }

    private void FinishFill()
    {
        SingleReplace('ё', 'е');
        SingleReplace('ъ', 'ь');
        PairReplace('ё', 'е');
        PairReplace('ъ', 'ь');
    }

    private void SingleReplace(char ch, char newCh)
    {
        _singleChars[newCh] += _singleChars[ch];
        _singleChars.Remove(ch);
    }

    private void PairReplace(char ch, char newCh)
    {
        List<KeyValuePair<PairChars, int>> pairs = _pairChars.Where(pair => pair.Key.Value.Contains(ch)).ToList();
        foreach ((PairChars pair, int count) in pairs)
        {
            string key = pair.Value.Replace(ch, newCh);
            PairChars newPair = new(key[0], key[1]);
            if (!_pairChars.TryAdd(newPair, count))
            {
                _pairChars[newPair] += count;
            }
        }
        pairs.ForEach(pair => _pairChars.Remove(pair.Key));
    }

    #region Extra
    public int this[char key]
    {
        get
        {
            key = char.ToLower(key);
            if (!_alphabet.Contains(key))
            { return -1; }
            return _singleChars[key];
        }
        private set
        {
            if (value is < 0)
            {
                throw new ArgumentException("Значение не может быть меньше нуля.");
            }

            if (!_alphabet.Contains(key))
            {
                throw new Exception("Запрещённый ключ.");
            }
            _singleChars[key] = value;
        }
    }
    public bool TryGetValue(char key, out int value)
    {
        key = char.ToLower(key);
        bool isExists = _alphabet.Contains(key);
        value = isExists ? _singleChars[key] : -1;
        return isExists;
    }
    public bool TryAddValue(char key)
    {
        key = char.ToLower(key);
        bool isExists = _alphabet.Contains(key);
        if (isExists) { _singleChars[key]++; }
        return isExists;
    }
    #endregion
}