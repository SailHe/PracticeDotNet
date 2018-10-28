using System;
using System.Collections.Generic;
using SailHeCSharpClassLib;

namespace LearnDotNet
{
    using StringMapDouble = SortedDictionary<string, double>;
    using StringUnorderedMapDouble = Dictionary<string, double>;

    //using Shell = Debug;
    using Shell = Console;


    //学生类
    class Student
    {
        static int STU_ID = 0;
        static int STU_COUNT = 0;
        static StringMapDouble sumScoreMap = new StringMapDouble();

        string name;
        string stuId;
        double weightedSumScore;
        StringMapDouble scoreMap;
        public Student(string name)
        {
            this.name = name;
            this.scoreMap = new StringMapDouble();
            ++STU_COUNT;
            this.stuId = STU_ID++.ToString("00");
        }

        //直接获取之前需要计算
        public double getWeightedSumScore()
        {
            return weightedSumScore;
        }

        public static double getSumScore(string name)
        {
            return sumScoreMap[name];
        }

        public static int getStuCount()
        {
            return STU_COUNT;
        }

        public static void init()
        {
            STU_COUNT = 0;
            sumScoreMap.Clear();
        }

        //新增一个学科的成绩
        public void addScore(string name, double score)
        {
            scoreMap.Add(name, score);
            if (sumScoreMap.ContainsKey(name))
            {
                sumScoreMap[name] += score;
            }
            else
            {
                sumScoreMap.Add(name, score);
            }
        }

        public double getScore(string name)
        {
            return scoreMap[name];
        }

        //若含有不及格科目返回true
        public bool hasFail()
        {
            foreach (KeyValuePair<string, double> kv in scoreMap)
            {
                if (kv.Value < 60)
                {
                    return true;
                }
            }
            return false;
        }

        public string getSduId()
        {
            return stuId;
        }
        public string getName()
        {
            return name;
        }

        //计算该学生指定学科的加权平均积分值
        public void calcWeightedSumScore(string name)
        {
            double sumSc = Student.getSumScore(name);
            double avlSc = sumSc / Student.getStuCount();
            double weightedSumScore = 0;
            foreach (KeyValuePair<string, double> kv in scoreMap)
            {
                weightedSumScore += kv.Value / avlSc;
            }
            this.weightedSumScore = weightedSumScore;
        }

        //计算该学生所有学科的加权平均积分值
        public void calcWeightedSumScore()
        {
            double weightedSumScore = 0;
            foreach (KeyValuePair<string, double> kv in scoreMap)
            {
                double avlSc = Student.getSumScore(kv.Key) / Student.getStuCount();
                weightedSumScore += kv.Value / avlSc;
            }
            this.weightedSumScore = weightedSumScore;
        }

        override
        public string ToString()
        {
            string scString = "";
            int i = -1;
            foreach (KeyValuePair<string, double> kv in scoreMap)
            {
                scString += ++i == 0 ? "" : "; ";
                scString += kv.Key + ": " + kv.Value.ToString("0.00") + "分";
            }
            return "姓名: " + name + "; 学号: " + stuId + "; " + scString;
        }
    }
    //奖品类
    class Award
    {
        //获奖者的索引值
        int index;
        string name;
        public Award(int index, string name)
        {
            this.index = index;
            this.name = name;
        }

        public int getIndex()
        {
            return index;
        }

        public string getName()
        {
            return name;
        }
    }

    //控制台程序
    class Program
    {
        //对于[10-99]之间的正整数N, 计算N^2+N+41的值R, 若R为质数, 输出N, R; 否则输出N, R 以及R的所有因数(非1与R)
        static void solve2_10_3(int n)
        {
            double r = Math.Pow(n, 2) + n + 41;
            if (UtilityApi.isPrime((int)r))
            {
                Shell.Write("N={0}, R={1}", n, r);
            }
            else
            {
                List<int> temp = UtilityApi.calcFactorList((int)r);
                Shell.Write("N={0}, R={1}, R的因数:", n, r);
                /*
                 * foreach (int it in temp) {
                    if(it == 1 || it == r) {
                        temp.Remove(it);
                    }
                }
                */
                int pCnt = 0;
                foreach (int it in temp)
                {
                    if (it == 1 || it == r)
                    {
                        continue;
                    }
                    else
                    {
                        Shell.Write((++pCnt == 1 ? "" : " ") + it);
                    }
                }
            }
            Shell.WriteLine("");
        }

        /**
        * 3.生成20个身份证号码
        * 从产生的身份证中抽出1个一等奖; 2个二等奖; 输出奖项, 身份证号, 性别出生日期
        */
        public static void solve3_10_3(Random rnd)
        {
            List<int> nUsedIndexList = new List<int>();
            List<Award> awardList = new List<Award>();
            int index1 = UtilityApi.randomSelectNonUsedIndex(nUsedIndexList, rnd, 20)
                , index2_1 = UtilityApi.randomSelectNonUsedIndex(nUsedIndexList, rnd, 20)
                , index2_2 = UtilityApi.randomSelectNonUsedIndex(nUsedIndexList, rnd, 20);
            awardList.Add(new Award(index1, "一等奖"));
            awardList.Add(new Award(index2_1, "二等奖"));
            awardList.Add(new Award(index2_2, "二等奖"));
            for (int i = 0; i < 20; ++i)
            {
                string PIN = UtilityApi.generateRandomIdCard(rnd);
                //使用校验码校验
                //Shell.WriteLine("校验" + (isValidIdCard(PIN.ToCharArray()) ? "成功" : "失败"));
                Award findAward = awardList.Find(ele => ele.getIndex() == i);
                if (null != findAward)
                {
                    Shell.Write("奖项: " + findAward.getName());
                    Shell.Write(" 身份证号码: " + PIN);
                    Shell.Write(" 性别: " + UtilityApi.idSex(PIN));
                    Shell.WriteLine();
                }
            }
        }

        public static void Main_HomeWork1(string[] args)
        {
            string lhsS, rhsS = "1";
            Random rnd = new Random(System.DateTime.Now.Millisecond);
            while ((lhsS = Shell.ReadLine()) != string.Empty)
            {
                //1.36进制大数加1
                string outString = UtilityApi.bigPlush(lhsS, rhsS, 36);
                //Shell.WriteLine(outString);

                //2.如题
                for (int i = 0; i < 99; ++i)
                {
                    //solveTwo(i);
                }

                //3.如题
                solve3_10_3(rnd);
            }
        }


        public static void solve1_10_10(Random rnd)
        {
            int maxCount = 30;
            List<int> randomList = new List<int>();
            List<int> randomList2 = new List<int>();
            for (int i = 0; i < maxCount; ++i)
            {
                int num = UtilityApi.randomNum(rnd, 2);
                if (randomList.Find(ele => ele == num) == 0)
                {
                    randomList.Add(num);
                }
                else
                {
                    ++maxCount;
                }
            }

            /*randomList.ForEach(ele => {
                if (isPrime((int)ele)) {
                    randomList.Remove(ele);
                }
            });*/
            /*for (int i = 0; i < randomList.Count; i++) {
                int ele = randomList[i];
                if (isPrime((int)ele) || (int)ele % 2 == 0) {
                    randomList.Remove(ele);
                }
            }*/
            randomList.ForEach(ele => {
                if (UtilityApi.isPrime((int)ele) || (int)ele % 2 == 0)
                {
                    //
                }
                else
                {
                    randomList2.Add(ele);
                }
            });
            randomList2.Sort((lhs, rhs) => (int)(lhs - rhs));
            int count = 0, pCnt = -1;
            randomList2.ForEach(ele => {
                Shell.Write((++pCnt == 0 ? "" : " ") + ele);
                if (++count % 5 == 0)
                {
                    Shell.WriteLine();
                    pCnt = -1;
                }
            });
            Shell.WriteLine();
        }

        public static void solve2_10_10()
        {
            //https://www.cnblogs.com/VisualStudio/archive/2008/11/16/1334645.html
            Shell.WriteLine("输入起始日期与中止日期: ");
            /**
             * DateTime dateValue = new DateTime(2008, 6, 11);
             * Shell.WriteLine(dateValue.ToString("dddd"));  

2017-10-9
2017-11-1

2018-10-9
2018-11-1
             */
            DateTime dtBegin, dtEnd, temp;
            int weekCount = 0;
            dtBegin = DateTime.Parse(Shell.ReadLine());
            dtEnd = DateTime.Parse(Shell.ReadLine());
            while ((temp = UtilityApi.nearlyWeekend(dtBegin)) <= dtEnd)
            {
                Shell.WriteLine("第" + ++weekCount + "周" + dtBegin.ToString("yyyy-MM-d") + " " + temp.ToString("yyyy-MM-d"));
                dtBegin = temp.AddDays(1);
            }
            if (temp > dtEnd)
            {
                Shell.WriteLine("第" + ++weekCount + "周" + dtBegin.ToString("yyyy-MM-d") + " " + dtEnd.ToString("yyyy-MM-d"));
            }
        }

        public static void Main_HomeWork2(string[] args)
        {
            string num;
            Random rnd = new Random(System.DateTime.Now.Millisecond);
            Shell.WriteLine("输入题目号码确定查阅题目: ");
            while ((num = Shell.ReadLine()) != string.Empty)
            {
                switch (int.Parse(num))
                {
                    case 1: { solve1_10_10(rnd); break; }
                    case 2: { solve2_10_10(); break; }
                }
                Shell.WriteLine("输入题目号码确定查阅题目: ");
            }
        }


        static void solve1_10_17(Random rnd)
        {
            Student.init();
            List<Student> stuList = new List<Student>();
            for (int i = 0; i < 35; ++i)
            {
                Student stu = new Student("学生" + i.ToString("00"));
                stu.addScore("学科0", StaticExtensionApi.NextDouble(rnd, 40, 100));
                stu.addScore("学科1", StaticExtensionApi.NextDouble(rnd, 40, 100));
                stu.addScore("学科2", StaticExtensionApi.NextDouble(rnd, 40, 100));
                stu.addScore("学科3", StaticExtensionApi.NextDouble(rnd, 40, 100));
                stuList.Add(stu);
            }
            //计算加权积分
            stuList.ForEach(ele => {
                ele.calcWeightedSumScore();
                //Shell.WriteLine(ele.ToString() + "; 加权积分: " + ele.getWeightedSumScore().ToString(".00"));
            });
            Shell.WriteLine();
            //计算所有学生的成绩的加权积分
            stuList.Sort((lhs, rhs) => {
                //保留两位小数计算
                return (int)(rhs.getWeightedSumScore() * 100 - lhs.getWeightedSumScore() * 100);
            });

            int award1Count = (int)Math.Ceiling(Student.getStuCount() * 0.05);
            int award2Count = (int)Math.Ceiling(Student.getStuCount() * 0.10);
            int award3Count = (int)Math.Ceiling(Student.getStuCount() * 0.15);
            //输出所有学生的所有信息
            stuList.ForEach(ele => {
                string result = ele.ToString() + "; 加权积分: " + ele.getWeightedSumScore().ToString(".00");
                int awardCount = award1Count + award2Count + award3Count;
                if (awardCount > 0)
                {
                    if (ele.hasFail())
                    {
                        //do nothing
                    }
                    else
                    {
                        if (award1Count > 0)
                        {
                            result += "; 一等奖学金";
                            --award1Count;
                        }
                        else
                        {
                            if (award2Count > 0)
                            {
                                result += "; 二等奖学金";
                                --award2Count;
                            }
                            else
                            {
                                if (award3Count > 0)
                                {
                                    result += "; 三等奖学金";
                                    --award3Count;
                                }
                                else
                                {
                                    //do nothing
                                }
                            }
                        }
                    }
                }
                else
                {
                    //do nothing
                }
                Shell.WriteLine(result);
            });
        }

        static void solve2_10_17(Random rnd)
        {
            Graph graph = new Graph(10);
            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(0, 1, 21));
            edges.Add(new Edge(1, 2, 21));
            edges.Add(new Edge(2, 4, 21));
            edges.Add(new Edge(4, 6, 21));
            edges.Add(new Edge(0, 3, 25));
            edges.Add(new Edge(3, 6, 21));
            edges.Add(new Edge(2, 5, 21));
            edges.Add(new Edge(5, 8, 21));
            edges.Add(new Edge(4, 8, 24));
            edges.Add(new Edge(8, 9, 21));
            for (int i = 1; i < 15; ++i)
            {
                //edges.Add(new Edge(rnd.Next(0, 10), rnd.Next(0, 10), rnd.Next(20, 100)));
            }

            edges.ForEach(ele => graph.insertEdgeUndirected(ele));

            graph.print();
            List<int> distList = new List<int>();
            List<int> prList = new List<int>();
            //start->stop; begain->end
            int originCityId = 1;
            int targetCityId = 0;
            graph.Dijkstra(originCityId, out distList, out prList);
            distList.ForEach(ele => {
                if (ele < int.MaxValue)
                {
                    Shell.WriteLine("目的地" + targetCityId + "; 最小代价: " + ele);
                    int parentKey = targetCityId;
                    List<int> path = new List<int>();
                    while (parentKey >= 0)
                    {
                        path.Add(parentKey);
                        parentKey = prList[parentKey];
                    }
                    path.Reverse();
                    int trackIndex = 0;
                    path.ForEach(track => Shell.Write((++trackIndex == 1 ? "" : "->") + track));
                    Shell.WriteLine();
                }
                ++targetCityId;
            });
        }

        static void Main_HomeWork3(string[] args)
        {
            string num;
            Random rnd = new Random(System.DateTime.Now.Millisecond);
            Shell.WriteLine("输入题目号码确定查阅题目: ");
            while ((num = Shell.ReadLine()) != string.Empty)
            {
                switch (int.Parse(num))
                {
                    case 1: { solve1_10_17(rnd); break; }
                    case 2: { solve2_10_17(rnd); break; }
                }
                Shell.WriteLine("输入题目号码确定查阅题目: ");
            }
        }

        static void Main(string[] args){
            Main_HomeWork3(args);
        }
    }
}
