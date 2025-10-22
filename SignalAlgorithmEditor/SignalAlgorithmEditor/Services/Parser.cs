using SignalAlgorithmEditor.Models;
using System.Reflection.Metadata;
using System.Security.AccessControl;

namespace SignalAlgorithmEditor.Services
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _position;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _position = 0;
        }

        public ExpressionNode Parse()
        {
            return ParseExpression();
        }

        private ExpressionNode ParseExpression()
        {
            return ParseTernary();
        }

        private ExpressionNode ParseTernary()
        {
            var expr = ParseLogicalOr();

            if(Match(TokenType.Operator, "?"))
            {
                var trueExpr = ParseExpression();
                Expect(TokenType.Operator, ":");
                var falseExpr = ParseExpression();

                return new TernaryNode(expr, trueExpr, falseExpr);
            }

            return expr;
        }

        private ExpressionNode ParseLogicalOr()
        {
            var expr = ParseLogicalAnd();

            while(Match(TokenType.Operator, "|"))
            {
                var right = ParseLogicalAnd();
                expr = new BinaryOperationNode(expr, "|", right);
            }
            return expr;
        }

        private ExpressionNode ParseLogicalAnd()
        {
            var expr = ParseEquality();

            while(Match(TokenType.Operator, "&"))
            {
                var right = ParseEquality();
                expr = new BinaryOperationNode(expr, "&", right);
            }

            return expr;
        }

        private ExpressionNode ParseEquality()
        {
            var expr = ParseRelational();

            while (MatchOneOf(new[] {"==", "!=" }))
            {
                var op = Previous().Value.ToString();
                var right = ParseRelational();
                expr = new BinaryOperationNode(expr, op, right);
            }

            return expr;
        }

        private ExpressionNode ParseRelational()
        {
            var expr = ParseAdditive();

            while (MatchOneOf(new[] {"<", ">", "<=", ">="}))
            {
                var op = Previous().Value.ToString();
                var right = ParseAdditive();
                expr = new BinaryOperationNode(expr, op, right);
            }

            return expr;
        }

        private ExpressionNode ParseAdditive()
        {
            var expr = ParseMultiplicative();

            while (MatchOneOf(new[] { "+", "-" }))
            {
                var op = Previous().Value.ToString();
                var right = ParseMultiplicative();
                expr = new BinaryOperationNode(expr, op, right);
            }

            return expr;
        }

        private ExpressionNode ParseMultiplicative()
        {
            var expr = ParsePower();

            while (MatchOneOf(new[] {"*", "/" }))
            {
                var op = Previous().Value.ToString();
                var right = ParsePower();
                expr = new BinaryOperationNode(expr, op, right);
            }

            return expr;
        }

        private ExpressionNode ParsePower()
        {
            var expr = ParseUnary();

            if(Match(TokenType.Operator, "^"))
            {
                var right = ParsePower();
                expr = new BinaryOperationNode(expr, "^", right);
            }

            return expr;
        }

        private ExpressionNode ParseUnary()
        {
            if (MatchOneOf(new[] {"+", "-", "!", "#" }))
            {
                var op = Previous().Value.ToString();
                var operand = ParseUnary();
                return new UnaryOperationNode(op, operand);
            }

            return ParsePrimary();
        }

        private ExpressionNode ParsePrimary()
        {
            if(Check(TokenType.Identifier) && PeekNextIs(TokenType.Operator, "("))
            {
                return ParseFunctionCall();
            }

            if (Match(TokenType.Identifier))
            {
                var name = Previous().Value.ToString();
                return new IdentifierNode(name);
            }

            if (Match(TokenType.Number))
            {
                var value = Previous().Value;
                return new ConstantNode(value);
            }

            if(Match(TokenType.Operator, "("))
            {
                var expr = ParseExpression();
                Expect(TokenType.Operator, ")");
                return expr;
            }

            throw new SyntaxException($"Неожиданный токен: {Peek()?.Value}");
        }

        private ExpressionNode ParseFunctionCall()
        {
            var funName = Consume(TokenType.Identifier, "Ожидалось имя функции").Value.ToString();
            Consume(TokenType.Operator, "(");

            var arguments = new List<ExpressionNode>();
            if(!Check(TokenType.Operator, ")"))
            {
                do
                {
                    arguments.Add(ParseExpression());
                } while (Match(TokenType.Operator, ","));
            }

            Consume(TokenType.Operator, ")");

            return new FunctionCallNode(funName!, arguments);
        }

        //Вспомогательные методы
        private bool Match(TokenType type, string value)
        {
            if(Check(type, value))
            {
                _position++;
                return true;
            }
            return false;
        }

        private bool Match(TokenType type)
        {
            if(Check(type))
            {
                _position++;
                return true;
            }
            return false;
        }

        private bool MatchOneOf(string[] values)
        {
            if(Check(TokenType.Operator) && System.Array.IndexOf(values, Peek()?.Value.ToString()) >= 0)
            {
                _position++;
                return true;
            }
            return false;
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd()) return false;
            return _tokens[_position].Type == type;
        }

        private bool Check(TokenType type, string value)
        {
            if (IsAtEnd()) return false;
            return _tokens[_position].Type == type && _tokens[_position].Value.ToString() == value;
        }

        private bool PeekNextIs(TokenType type, string value)
        {
            if(_position + 1 >= _tokens.Count) return false;
            return _tokens[_position + 1].Type == type && _tokens[_position + 1].Value.ToString() == value;
        }

        private Token Consume(TokenType type, string message)
        {
            if (Check(type))
            {
                return _tokens[_position++];
            }
            throw new SyntaxException(message);
        }

        private Token Expect(TokenType type, string value)
        {
            if (Match(type, value)) return Previous();
            throw new SyntaxException($"Ожидался '{value}'");
        }

        private bool IsAtEnd() => _position >= _tokens.Count;
        private Token? Peek() => IsAtEnd() ? null : _tokens[_position];
        private Token Previous() => _tokens[_position - 1];
    }



    public abstract class ExpressionNode { }

    public class ConstantNode : ExpressionNode
    {
        public object Value { get; }

        public ConstantNode(object value)
        {
            Value = value;
        } 
    }

    public class IdentifierNode : ExpressionNode
    {
        public string Name { get; }
        public IdentifierNode(string name)
        {
            Name = name;
        }
    }

    public class UnaryOperationNode : ExpressionNode
    {
        public string Operator { get; }
        public ExpressionNode Operand { get; }

        public UnaryOperationNode(string op, ExpressionNode operand)
        {
            Operator = op;
            Operand = operand;
        }
    }

    public class BinaryOperationNode : ExpressionNode
    {
        public ExpressionNode Left { get; }
        public string Operator { get; }
        public ExpressionNode Right { get; }

        public BinaryOperationNode(ExpressionNode left, string op, ExpressionNode right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }
    }

    public class TernaryNode : ExpressionNode
    {
        public ExpressionNode Condition { get; }
        public ExpressionNode TrueExpression { get; }
        public ExpressionNode FalseExpression { get; }

        public TernaryNode(ExpressionNode condition, ExpressionNode trueExpr, ExpressionNode falseExpr)
        {
            Condition = condition;
            TrueExpression = trueExpr;
            FalseExpression = falseExpr;
        }
    }

    public class FunctionCallNode : ExpressionNode
    {
        public string FunctionName { get; }
        public List<ExpressionNode> Arguments { get; }

        public FunctionCallNode(string functionName, List<ExpressionNode> arguments)
        {
            FunctionName = functionName;
            Arguments = arguments;
        }
    }

    public class SyntaxException : Exception
    {
        public SyntaxException(string message) : base(message) { }
    }
}
