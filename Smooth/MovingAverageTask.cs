using System.Collections.Generic;

namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            double summa = 0;
            int index = 0;
            bool flag = true;
            int countWinWidth = windowWidth - 1;
            Queue<DataPoint> qSumm = new Queue<DataPoint>();

            foreach (var item in data)
            {
                if (flag)
                {
                    index++;
                    summa = item.OriginalY;
                    qSumm.Enqueue(item);
                    item.AvgSmoothedY = item.OriginalY;
                    flag = false;
                    yield return item;
                    continue;
                }
                if (countWinWidth > 0)
                {
                    index++;
                    countWinWidth--;
                    summa += item.OriginalY;
                    qSumm.Enqueue(item);
                    item.AvgSmoothedY = summa / index;
                    yield return item;
                    continue;
                }
                if (countWinWidth <= 0)
                {
                    var oldItem = qSumm.Dequeue();
                    summa += item.OriginalY - oldItem.OriginalY;
                    qSumm.Enqueue(item);
                    item.AvgSmoothedY = summa / windowWidth;
                    yield return item;
                }
            }
            yield break;
        }
    }
}