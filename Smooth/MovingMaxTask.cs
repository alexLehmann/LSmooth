using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{

    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            int index = 0;
            double max = Double.MinValue;
            LinkedList<Tuple<int, DataPoint>> queue = new LinkedList<Tuple<int, DataPoint>>();
            foreach (var item in data)
            {
                if (queue.Count > 0)
                    while(queue.Count > 0 && queue.Last.Value.Item2.OriginalY <= item.OriginalY)
                        queue.RemoveLast();
                queue.AddLast(new Tuple<int, DataPoint>(index, item));
                while (index - queue.First().Item1 >= windowWidth && queue.Count() > 0)
                {
                    queue.RemoveFirst();
                    max = queue.First().Item2.OriginalY;
                }
                if (max < queue.Last().Item2.OriginalY)
                    max = queue.Last().Item2.OriginalY;
                index++;
                item.MaxY = max;
                yield return item;
            }
            yield break;
        }
    }

}