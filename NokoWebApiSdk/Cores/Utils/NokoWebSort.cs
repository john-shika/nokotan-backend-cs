namespace NokoWebApiSdk.Cores.Utils;

public static class NokoWebSort
{
    public class MergeSort<T> 
        where T : IComparable<T>, IEquatable<T>
    {
        public static void Merge(IList<T> data, int left, int middle, int right)
        {
            int i, j, k;
            var n1 = middle - left + 1;
            var n2 = right - middle;
            
            if (n1 <= 0 && n2 <= 0) return;

            var arrLeft = new T[n1];
            var arrRight = new T[n2];

            for (i = 0; i < n1; i++)
            {
                arrLeft[i] = data[left + i];
            }

            for (j = 0; j < n2; j++)
            {
                arrRight[j] = data[middle + 1 + j];
            }

            i = 0;
            j = 0;
            k = left;

            while (i < n1 && j < n2)
            {
                if (arrLeft[i].CompareTo(arrRight[j]) <= 0)
                {
                    data[k] = arrLeft[i];
                    i++;
                }
                else
                {
                    data[k] = arrRight[j];
                    j++;
                }

                k++;
            }

            while (i < n1)
            {
                data[k] = arrLeft[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                data[k] = arrRight[j];
                j++;
                k++;
            }
        }

        public static void Init(IList<T> data, int left, int right)
        {
            if (left >= right) return;
            var mid = left + ((right - left) >> 1);
            Init(data, left, mid);
            Init(data, mid + 1, right);
                
            Merge(data, left, mid, right);
        }

        public MergeSort(IList<T> data)
        {
            var size = data.Count(); 
            Init(data, 0, size - 1);
        }
    }

    public class QuickSort<T>
        where T : IComparable<T>, IEquatable<T>
    {
        public static void Swap(IList<T> data, int left, int right)
        {
            (data[left], data[right]) = (data[right], data[left]);
        }

        public static int Partition(IList<T> data, int low, int high)
        {
            var pivot = data[high];
            var i = low;

            for (var j = i; j < high; j++)
            {
                if (data[j].CompareTo(pivot) >= 0) continue;
                Swap(data, i, j);
                i++;
            }
            
            Swap(data, i, high);
            return i;
        }

        public static void Init(IList<T> data, int low, int high)
        {
            while (true)
            {
                if (low >= high) break;
                var m = Partition(data, low, high);
                if (0 < m) Init(data, low, m - 1);
                low = m + 1;
            }
        }

        public QuickSort(IList<T> data)
        {
            var size = data.Count();
            Init(data, 0, size - 1);
        }
    }
}