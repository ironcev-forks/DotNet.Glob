using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Globbing.Token;

namespace DotNet.Globbing.Generation
{
    public class CharacterListTokenMatchGenerator : IMatchGenerator
    {

        private CharacterListToken _token;
        private Random _random;
        private List<char> _nonMatches;

        public CharacterListTokenMatchGenerator(CharacterListToken token, Random random)
        {
            _token = token;
            _random = random;
            PopulateNonMatchingCharList();
        }

        public void AppendMatch(StringBuilder builder)
        {
            if (_token.IsNegated)
            {
                if (!_nonMatches.Any())
                {
                    throw new Exception("Could not generate characters that aren't in the character list!");
                }
                builder.Append(_nonMatches[_random.Next(0, _nonMatches.Count - 1)]);
            }
            else
            {
                builder.Append(_token.Characters[_random.Next(0, _token.Characters.Count() - 1)]);
            }
        }

        public void AppendNonMatch(StringBuilder builder)
        {
            if (_token.IsNegated)
            {
                builder.Append(_token.Characters[_random.Next(0, _token.Characters.Count() - 1)]);
            }
            else
            {
                if (!_nonMatches.Any())
                {
                    throw new Exception("Could not generate characters that aren't in the character list!");
                }
                builder.Append(_nonMatches[_random.Next(0, _nonMatches.Count - 1)]);
            }

        }

        private void PopulateNonMatchingCharList()
        {
            _nonMatches = new List<char>(30);

            for (int i = 'a'; i < 'z'; i++)
            {
                if (!_token.Characters.Contains((char)i))
                {
                    _nonMatches.Add((char)i);
                }
            }
           
        }

    }
}