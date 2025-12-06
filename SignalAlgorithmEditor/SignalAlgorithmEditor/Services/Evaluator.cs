using Contract;
using SignalAlgorithmEditor.Models;

namespace SignalAlgorithmEditor.Services
{
    public class Evaluator
    {
        private readonly ExecutionContext _context;
        private readonly Dictionary<string, Contract.IOperation> _operations;
        private readonly IStateStore _stateStore;

        public Evaluator(ExecutionContext context, Dictionary<string, Contract.IOperation> operations, IStateStore stateStore)
        {
            _context = context;
            _operations = operations;
            _stateStore = stateStore;
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
            for (int i = 0; i < node.Arguments.Count; i++)
            {
                args[i] = EvaluateNode(node.Arguments[i]);
            }

            // Определяем имя операции для поиска в словаре
            var functionName = node.FunctionName switch
            {
                "TD" => "TimeDelayOperation",
                "PREV" => "PrevOperation", // ← обязательно совпадает с именем класса в DLL
                "LN" => "NaturalLogOperation",
                "PLF" => "PiecewiseLinearFunctionOperation",
                "SVLD" => "SetValidityOperation",
                "ASUB" => "AbsoluteSubtractionOperation",
                "BITS" => "BitsOperation",
                "CHS" => "ControlCharacteristicOperation",
                "TG" => "TriggerOperation",
                _ => throw new InvalidOperationException($"Неизвестная функция: {node.FunctionName}")
            };

            if (!_operations.TryGetValue(functionName, out var operation))
            {
                throw new InvalidOperationException($"Функция '{functionName}' не найдена.");
            }

            string callKey = GetCallKey(node);

            try
            {
                var statefulOp = operation as dynamic;
                return statefulOp.Execute(args, callKey, _stateStore);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                return operation.Execute(args);
            }
        }

        private string GetCallKey(FunctionCallNode node)
        {
            var argsStr = string.Join(",", node.Arguments.Select(RenderNode));
            return $"{node.FunctionName}({argsStr})";
        }

        private string RenderNode(ExpressionNode? node)
        {
            return node switch
            {
                IdentifierNode id => id.Name,
                ConstantNode c => c.Value.ToString(),
                UnaryOperationNode u => u.Operator + RenderNode(u.Operand),
                BinaryOperationNode b => $"({RenderNode(b.Left)}{b.Operator}{RenderNode(b.Right)})",
                FunctionCallNode f => GetCallKey(f),
                _ => "expr"
            };
        }
    }
}
