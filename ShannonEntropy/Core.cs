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

    public Core()
    {
        _dictionary = _alphabet
        .Select(ch => new KeyValuePair<char, int>(ch, 0))
        .ToDictionary();
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

    public bool TryAddValue(char key)
    {
        key = char.ToLower(key);
        bool isExists = _alphabet.Contains(key);
        if(isExists) { _dictionary[key]++; }
        return isExists;
    }
}