namespace CSharpExtensions.Text
{
    public class Tokenizer
    {
        private readonly string[] _tokens;

        public Tokenizer(string source)
        {
            _tokens = source.Split(' ');
        }

        public string GetToken(int index)
        {
            if (index >= 0 && index < _tokens.Length)
                return _tokens[index];
            return string.Empty;
        }

        public bool HasToken(int index)
        {
            return GetToken(index) != string.Empty;
        }

        public bool MatchesToken(int index, string @string)
        {
            return GetToken(index) == @string;
        }
    }
}
