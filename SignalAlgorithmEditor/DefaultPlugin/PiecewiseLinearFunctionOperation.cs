using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin
{
    public class PiecewiseLinearFunctionOperation : IOperation
    {
        public Parameter Execute(Parameter[] args)
        {
            if (args == null || args.Length < 3 || args.Length % 2 != 1)
                throw new ArgumentException("PLF требует нечётное число аргументов ≥ 3: x, x1, y1, x2, y2, ...");

            foreach (var arg in args)
            {
                if (arg.Type == ParameterType.Boolean)
                    throw new ArgumentException("Все аргументы PLF должны быть числовыми.");
            }

            double x = GetNumericValue(args[0]);
            var points = new List<(double x, double y)>();

            for (int i = 1; i < args.Length; i += 2)
            {
                double xi = GetNumericValue(args[i]);
                double yi = GetNumericValue(args[i + 1]);
                points.Add((xi, yi));
            }

            if (points.Count < 1)
                throw new ArgumentException("PLF требует хотя бы одну точку (x1, y1).");

            points = points.OrderBy(p => p.x).ToList();

            double yResult = Interpolate(points, x);

            int resultState = args[0].State;

            return new Parameter(ParameterType.Analog, (float)yResult, resultState);
        }

        private double GetNumericValue(Parameter p)
        {
            return p.Type switch
            {
                ParameterType.Integer => (int)p.Value,
                ParameterType.Analog => (float)p.Value,
                _ => 0
            };
        }

        private double Interpolate(List<(double x, double y)> points, double x)
        {
            if (points.Count == 1)
                return points[0].y;

            var first = points[0];
            var last = points[points.Count - 1];

            if (x <= first.x)
            {
                var next = points[1];
                if (next.x == first.x) return first.y;
                double slope = (next.y - first.y) / (next.x - first.x);
                return first.y + slope * (x - first.x);
            }

            if (x >= last.x)
            {
                var prev = points[points.Count - 2];
                if (last.x == prev.x) return last.y;
                double slope = (last.y - prev.y) / (last.x - prev.x);
                return last.y + slope * (x - last.x);
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                var p1 = points[i];
                var p2 = points[i + 1];
                if (x >= p1.x && x <= p2.x)
                {
                    if (p2.x == p1.x) return p1.y;
                    double t = (x - p1.x) / (p2.x - p1.x);
                    return p1.y + t * (p2.y - p1.y);
                }
            }

            return last.y;
        }
    }
}
