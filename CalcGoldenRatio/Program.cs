using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcGoldenRatio
{
    class Program
    {
        static double Sqrt(int n,int N = 1000)
        {
            int c = 0;
            for (int m = 1; c < n; m++)
            {
                if (m % 2 == 1)
                {
                    c++;
                    if(c == n)
                    {
                        return (m + 1) / 2;
                    }
                }
                else if(n>=c && n<c+m)
                {
                    double gr = CalculateGoldenRatio(m, n - c, N);
                    //m==(int)gr
                    return ((int)gr) / 2.0 + (gr - (int)gr);
                }
                else
                {
                    c += m;
                }
            }
            return 0.0;//error,should not be here.
        }
        static void SqrtTable(int count,int N=1000)
        {
            int c = 0;
            for(int i = 1; c < count; i++)
            {
                if (i % 2 == 1)
                {
                    c++;
                    //c is square of something
                    Console.WriteLine("Sqrt({0})={1},{2} ->({3})", c,(i+1)/2,Math.Sqrt(c), i);
                }
                else //i%2==0
                {
                    for(int j =1; j <= i;j++)
                    {
                        c++;
                        double gr = CalculateGoldenRatio(i, j, N);
                        double rs = ((int)gr)/2.0 + (gr - (int)gr);
                       
                        Console.WriteLine("Sqrt({0})={1},{2} -> ({3},{4})", c,rs,Math.Sqrt(c),i,j);

                        if (c == count)
                        {
                            break;
                        }
                    }
                }
            }
        }
        static double CalculateGoldenRatio(double i, double j, int N)
        {
            double r = 1.0;
            for (int c = 0; c < N; c++)
            {
                r = i + j / r;
            }
            return r;
        }
        static (int,double) IsPartialSqrt(double gr,int N)
        {
            int dx = (int)gr;
            gr = gr - dx;

            for(int i = 2; i < N; i++)
            {
                double gx = Math.Sqrt(i);
                int mx = (int)gx;

                gx = gx - mx;

                if (Math.Abs(gr - gx) < 10e-8)
                {
                    return (i,Math.Sqrt(i));
                }
            }
            return (0,0.0);
        }



        static void Main(string[] args)
        {
            //GR(i) = i + 1/(i+1/(1+1/(...
            //      = Real(1) + Imaginery(0) = Real(Full_Cycle) 
            //GR(1)=1.618
            //GR(2)=2.414 (Sqrt(2)+1)
            //1+1/(1+1/( = 1.618 = 1 + 0
            //1+2/(1+2/( = 2.0 
            //2+1/(2+1/( = 2.414 = 1 + Sqrt(2)
            //2+2/(2+2/( = 2.732 = 1 + Sqrt(3)

            const int c = 32;

            for (int i = 1; i < c; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    double gr = CalculateGoldenRatio(i, j, 1000);

                    var sq = IsPartialSqrt(gr, c * c);
                    Console.WriteLine("i={0},j={1},GR={2},({3},{4})", i, j, gr, sq.Item1, sq.Item2);
                }
            }

            SqrtTable(36);

            int d = 22;

            Console.WriteLine("Sqrt({0})={1}", d,Sqrt(d));

        }

    }
}
