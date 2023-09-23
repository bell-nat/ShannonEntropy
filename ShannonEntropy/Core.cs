namespace ShannonEntropy;

public class Core
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

    private readonly Dictionary<char, int> _dictionary;
    private readonly Dictionary<PairChars, int> _pairChars;
    
    public int this[char key]
    {
        get
        {
            key = char.ToLower(key);
            if (!_alphabet.Contains(key))
            { return -1; }
            return _dictionary[key];
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
            _dictionary[key] = value;
        }
    }

    private int? _length;
    public int Length => _length ??= _dictionary.Select(pair => pair.Value).Sum();

    public Core()
    {
        _dictionary = _alphabet
        .Select(ch => new KeyValuePair<char, int>(ch, 0))
        .ToDictionary();
        _pairChars = new();
    }

    public bool TryGetValue(char key, out int value)
    {
        key = char.ToLower(key);
        bool isExists = _alphabet.Contains(key);
        value = isExists ? _dictionary[key] : -1;
        return isExists;
    }

    public void Counter(char key)
    {
        key = char.ToLower(key);
        if (_alphabet.Contains(key)) { _dictionary[key]++; }
    }

    public void Counter(char first, char second)
    {
        first = char.ToLower(first);
        second = char.ToLower(second);
        if (_alphabet.Contains(first) && _alphabet.Contains(second))
        {
            PairChars pair = new(first, second);
            if (!_pairChars.TryAdd(pair, 1))
            {
                _pairChars[pair]++;
            }
        }
    }

    public bool TryAddValue(char key)
    {
        key = char.ToLower(key);
        bool isExists = _alphabet.Contains(key);
        if(isExists) { _dictionary[key]++; }
        return isExists;
    }

    public void ViewSingle()
    {
        ReplaceSingle();
        double entropy = 0;
        foreach ((char key, int count) in _dictionary)
        {
            View(ref entropy, count, $"{key}");
        }

        GeneralInfo(entropy);
    }

    public void ViewPair()
    {
        ReplacePair('ё', 'е');
        ReplacePair('ъ', 'ь');
        double entropy = 0;
        foreach ((PairChars pair, int count) in _pairChars)
        {
            var g = _pairChars.Where(x => x.Value > 1).ToList();
            View(ref entropy, count, pair.Pair);
        }

        GeneralInfo(entropy);
    }

    private void ReplaceSingle()
    {
        _dictionary['е'] += _dictionary['ё'];
        _dictionary.Remove('ё');
        _dictionary['ь'] += _dictionary['ъ'];
        _dictionary.Remove('ъ');
    }

    private void ReplacePair(char ch, char newCh)
    {
        List<KeyValuePair<PairChars, int>> pairs = _pairChars.Where(pair => pair.Key.Pair.Contains(ch)).ToList();
        foreach ((PairChars pair, int count)  in pairs)
        {
            string key = pair.Pair.Replace(ch, newCh);
            PairChars newPair = new PairChars(key[0], key[1]);
            if (!_pairChars.TryAdd(newPair, count))
            {
                _pairChars[newPair] += count;
            }
        }
        pairs.ForEach(pair => _pairChars.Remove(pair.Key));
    }

    private void View(ref double entropy, int count, string key)
    {
        double probability = (double)count / Length;
        double currentEntropy = probability * Math.Log(probability, 2);
        Console.WriteLine($"{key}\t{count}\t{probability:0.00000000}\t{currentEntropy:0.00000000}");
        entropy -= currentEntropy;
    }

    private void GeneralInfo(double entropy)
    {
        Console.WriteLine();
        Console.WriteLine($"Entropy: {entropy:0.00000000}");
        Console.WriteLine($"Length: {Length}");
    }
}