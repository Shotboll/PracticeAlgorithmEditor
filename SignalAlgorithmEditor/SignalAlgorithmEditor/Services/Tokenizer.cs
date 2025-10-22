using SignalAlgorithmEditor.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SignalAlgorithmEditor.Services
{
    public class Tokenizer
    {
        public List<Token> Tokenize(string input)
        {
            var tokens = new List<Token>();
            var position = 0;

            while (position < input.Length)
            {
                var currentChar = input[position];

                if (char.IsWhiteSpace(currentChar))
                {
                    position++;
                    continue;
                }

                //Идентификатор
                if(char.IsLetter(currentChar))
                {
                    var start = position;

                    while(position < input.Length && (char.IsLetterOrDigit(input[position]) || input[position] == '.'))
                    {
                        position++;
                    }

                    var identifier = input.Substring(start, position - start);
                    tokens.Add(new Token(TokenType.Identifier, identifier));
                    continue;
                }

                if(char.IsDigit(currentChar) || currentChar == '.')
                {
                    var start = position;
                    while (position < input.Length && (char.IsDigit(input[position]) || input[position] == ','))
                    {
                        position++;
                    }
                    var numberStr = input.Substring(start, position - start);
                    if (numberStr.Contains('.'))
                        tokens.Add(new Token(TokenType.Number, float.Parse(numberStr)));
                    else
                        tokens.Add(new Token(TokenType.Number, int.Parse(numberStr)));
                    continue;
                }

                switch (currentChar)
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                    case '#':
                    case '(':
                    case ')':
                    case '&':
                    case '|':
                    case '?':
                    case ':':
                    case ',':
                        tokens.Add(new Token(TokenType.Operator, currentChar.ToString()));
                        position++;
                        continue;

                    case '=':
                        if (position + 1 < input.Length && input[position + 1] == '=')
                        {
                            tokens.Add(new Token(TokenType.Operator, "=="));
                            position += 2;
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Operator, "="));
                            position++;
                        }
                        continue;

                    case '!':
                        if (position + 1 < input.Length && input[position + 1] == '=')
                        {
                            tokens.Add(new Token(TokenType.Operator, "!="));
                            position += 2;
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Operator, "!"));
                            position++;
                        }
                        continue;

                    case '<':
                        if(position + 1 < input.Length && input[position+1] == '=')
                        {
                            tokens.Add(new Token(TokenType.Operator, "<="));
                            position += 2;
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Operator, "<"));
                        }
                        continue;

                    case '>':
                        if (position + 1 < input.Length && input[position + 1] == '=')
                        {
                            tokens.Add(new Token(TokenType.Operator, ">="));
                            position += 2;
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Operator, ">"));
                            position++;
                        }
                        continue;
                }
            }

            return tokens;
        }
    }
}
