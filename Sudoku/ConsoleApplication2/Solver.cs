using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication2
{
    internal class Solver
    {
        private readonly int[] _numbers = Enumerable.Range(1, 9).ToArray();
        private List<Element> _elements;

        public int StepCount { get; private set; }

        public int Solve(Board board)
        {
            StepCount++;
            _elements = board.Elements;
            var squares = _elements.GroupBy(e => e.Square).Select(g => g.ToArray());
            var allSolved = true;
            foreach (var square in squares)
            {
                var currentSolved = SolveSquare(square);
                allSolved = allSolved && currentSolved;
            }
            if (!allSolved)
                Solve(board);
            return StepCount;
        }

        private bool SolveSquare(Element[] square)
        {
            if (!square.Any(e => e.IsEmpty))
                return true;
            var empties = GetInitedEmpty(square);
            TrySet(empties);
            return false;
        }

        private void TrySet(Element[] squareElements)
        {
            foreach (var element in squareElements)
            {
                var otherEmpty = squareElements.Where(e => e != element).ToList();
                if (element.CanBe.Count == 1)
                {
                    element.Value = element.CanBe.First();
                    otherEmpty.ForEach(e => e.CantBe.Add(element.Value));
                    otherEmpty.ForEach(e => e.CanBe.Remove(element.Value));
                    continue;
                }
                foreach (var potentialValue in element.CanBe)
                {
                    if (otherEmpty.All(s => s.CantBe.Contains(potentialValue)))
                    {
                        element.Value = potentialValue;
                        break;
                    }
                }
            }
        }

        private Element[] GetInitedEmpty(Element[] square)
        {
            var empty = square.Where(e => e.IsEmpty).ToList();
            foreach (var element in empty)
            {
                var otherElement = square.Where(e => e.Value != element.Value).ToArray();
                InitElement(element, otherElement);
            }
            return empty.ToArray();
        }

        private void InitElement(Element element, Element[] otherInSquare)
        {
            var squareExist = otherInSquare.Where(e => !e.IsEmpty).Select(e => e.Value).ToList();
            var rowExist = _elements.Where(e => e.Row == element.Row && !e.IsEmpty).Select(e => e.Value).ToList();
            var columnExist = _elements.Where(e => e.Column == element.Column && !e.IsEmpty).Select(e => e.Value).ToList();

            element.CanBe = _numbers.Except(squareExist).Except(rowExist).Except(columnExist).Distinct().ToList();
            element.CantBe = squareExist.Union(rowExist).Union(columnExist).Distinct().ToList();
        }
    }
}