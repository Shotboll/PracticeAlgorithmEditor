using Contract;
using SignalAlgorithmEditor.Models;

namespace SignalAlgorithmEditor.Services
{
    public class Evaluator
    {
        private readonly ExecutionContext _context;
        private readonly Dictionary<string, Contract.IOperation> _operations;

        public Evaluator(ExecutionContext context, Dictionary<string, Contract.IOperation> operations)
        {
            _context = context;
            _operations = operations;
        }

        public Parameter Evaluate(ExpressionNode node)
        {
            return EvaluateNode(node);
        }

        private Parameter EvaluateNode(ExpressionNode node)
        {
            return node switch
            {
                ConstantNode constantNode => EvaluateConstant(constantNode),
                IdentifierNode identifierNode => EvaluateIdentifier(identifierNode),
                UnaryOperationNode unaryNode => EvaluateUnary(unaryNode),
                BinaryOperationNode binaryNode => EvaluateBinary(binaryNode),
                TernaryNode ternaryNode => EvaluateTernary(ternaryNode),
                FunctionCallNode functionNode => EvaluateFunction(functionNode),
                _ => throw new InvalidOperationException($"Неизвестный тип узла: {node.GetType()}")
            };
        }

        private Parameter EvaluateConstant(ConstantNode node)
        {
            return node.Value switch
            {
                int intValue => new Parameter(ParameterType.Integer, intValue, 0),
                float floatValue => new Parameter(ParameterType.Analog, floatValue, 0),
                _ => throw new InvalidOperationException($"Неподдерживаемый тип константы: {node.Value.GetType()}")
            };
        }

        private Parameter EvaluateIdentifier(IdentifierNode node)
        {
            return _context.GetParameter(node.Name);
        }

        private Parameter EvaluateUnary(UnaryOperationNode node)
        {
            var operand = EvaluateNode(node.Operand);

            var operationName = node.Operator switch
            {
                "!" => "LogicalNotOperation",
                "#" => "ReliabilityOperation",
                "+" => "UnaryPlusOperation",
                "-" => "UnaryMinusOperation",
                _ => throw new InvalidOperationException($"Неизвестный унарный оператор: {node.Operator}")
            };

            if(!_operations.TryGetValue(operationName, out var operation))
            {
                throw new InvalidOperationException($"Операция '{operationName}' не найдена.");
            }

            return operation.Execute(new[] { operand });
        }

        private Parameter EvaluateBinary(BinaryOperationNode node)
        {
            var left = EvaluateNode(node.Left);
            var right = EvaluateNode(node.Right);

            var operationName = node.Operator switch
            {
                "+" => "AdditionOperation",
                "-" => "SubtractionOperation",
                "*" => "MultiplicationOperation",
                "/" => "DivisionOperation",
                "^" => "PowerOperation",
                "&" => "LogicalAndOperation",
                "|" => "LogicalOrOperation",
                "==" => "EqualOperation",
                "!=" => "NotEqualOperation",
                "<" => "LessThanOperation",
                ">" => "GreaterThanOperation",
                "<=" => "LessThanOrEqualOperation",
                ">=" => "GreaterThanOrEqualOperation",
                _ => throw new InvalidOperationException($"Неизвестный бинарный оператор: {node.Operator}")
            };

            if(!_operations.TryGetValue(operationName, out var operation))
            {
                throw new InvalidOperationException($"Операция '{operationName}' не найдена.");
            }

            return operation.Execute(new[] { left, right });
        }

        private Parameter EvaluateTernary(TernaryNode node)
        {
            var condition = EvaluateNode(node.Condition);

            if(condition.Type != ParameterType.Boolean)
            {
                throw new InvalidOperationException("Условие в тернарной операции должно быть логическим.");
            }

            var conditionValue = condition.BoolValue;

            var resultNode = conditionValue ? node.TrueExpression : node.FalseExpression;
            return EvaluateNode(resultNode);
        }

        private Parameter EvaluateFunction(FunctionCallNode node)
        {
            var args = new Parameter[node.Arguments.Count];
            for(int i = 0; i< node.Arguments.Count; i++)
            {
                args[i] = EvaluateNode(node.Arguments[i]);
            }

            var functionName = node.FunctionName switch
            {
                "TD" => "TimeDelayOperation",
                "PREV" => "PreviousValueOperation",
                "LN" => "NaturalLogOperation",
                "PLF" => "PiecewiseLinearFunctionOperation",
                "SVLD" => "SetValidityOperation",
                "ASUB" => "AbsoluteSubtractionOperation",
                "BITS" => "BitsOperation",
                "CHS" => "ControlCharacteristicOperation",
                "TG" => "TriggerOperation",
                _ => throw new InvalidOperationException($"Неизвестная функция: {node.FunctionName}")
            };

            if(!_operations.TryGetValue(functionName, out var operation))
            {
                throw new InvalidOperationException($"Функция '{functionName}' не найдена.");
            }

            return operation.Execute(args);
        }
    }
}
