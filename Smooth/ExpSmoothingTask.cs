using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{

        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            if (1 < alpha || alpha < 0) yield break;
            DataPoint itemOld = null;
            bool flag = true;
            foreach (var item in data)
            {
                if (flag)
                {
                    item.ExpSmoothedY = item.OriginalY;
                    itemOld = item;
                    yield return item;
                    flag = false;
                    continue;
                }
                item.ExpSmoothedY = alpha * item.OriginalY + (1 - alpha) * itemOld.ExpSmoothedY;
                yield return item;
                item.X = itemOld.X + 0.5;
                itemOld = item;
            }
            yield break;           
        }
	}
}