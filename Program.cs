using System.Linq;
using System.Collections;
internal class Program
{
    static int[] nearopt = { 22, 33, 39, 42, 44, 45, 46 };

    const int N = 7;
    const int fiddle = 4;
    static int[] candidateSet = new int[N];
    static int optimumSum = int.MaxValue;
    static string solution = "no solution";

    static List<List<int>>[] subSetsIndexes = new List<List<int>>[N + 1];

    private static int CountBitsSet(int v)
    {
        int c;
        for (c = 0; v > 0; c++)
        {
            v &= v - 1;
        }
        return c;
    }
    private static void Init()
    {
        for (int i = 1; i <= N; i++)
        {
            subSetsIndexes[i] = new List<List<int>>();
        }

        
        for (int i = 1, limit = (1<<N); i < limit; i++)
        {
            int c = CountBitsSet(i);
            List<int> indexList = new List<int>(c);
            subSetsIndexes[c].Add(indexList);
            
            for(int j = 0; j < N; j++){
                if((i & (1<<j))>0){
                    indexList.Add(j);
                }
            }
        }


    }

    private static void Main(string[] args)
    {
        Init();
        DeepSearch(0);
        Console.WriteLine(solution);
    }



    private static void DeepSearch(int i)
    {
        if (i == candidateSet.Length)
        {
            int sum = candidateSet.Sum();
            if (sum >= optimumSum) return; //faster to check
            if (IsSpecialSumSet())
            {
                optimumSum = sum;
                solution = String.Join(String.Empty, candidateSet);
            }
            return;
        }

        int vEnd = nearopt[i] + fiddle;

        for (int v = vEnd - 2*fiddle; v <= vEnd; v++)
        {
            candidateSet[i] = v;
            DeepSearch(i + 1);
        }
    }

    private static bool IsSpecialSumSet()
    {
        int maxSprev = 0;
        int maxS = 0;
        BitArray sumsCounted = new BitArray(400);
        for(int i = 1; i <= N; i++){
            foreach(List<int> indexesList in subSetsIndexes[i]){
                int sum = 0;
                foreach(int j in indexesList){
                    sum += candidateSet[j];
                }
                if(sum <= maxSprev) return false;
                if(sumsCounted[sum]) return false;
                sumsCounted.Set(sum, true);
                if(sum > maxS)
                    maxS = sum;
            }
            maxSprev = maxS;
        }

        return true;
    }
}