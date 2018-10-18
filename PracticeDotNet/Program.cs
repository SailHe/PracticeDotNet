using System;
using System.Collections.Generic;
//ArrayList
using System.Collections;
using GraphExtensionNameSpace;
using System.Linq.Expressions;
/**
* @see C#API: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/
* @see C# Collections API: https://docs.microsoft.com/zh-cn/dotnet/standard/generics/collections
* 
* 一律使用小驼峰
* 前缀表示类型或泛型: int->n; double->d; unsigned->un; string->s;
* 后缀表示复杂类型(通常是容器名, 且可复合使用): S->[](通常指原生数组); Array->数组实现的线性表类型; List->邻接表实现的线性表类型; _->裸指针(通常是需要自行管理内存的)
* 例如定义C++指针: List<int> *nNameList_ = null;
* 优势: 虽然现在编译器已经能很好的提示, 但这么做有利于重构名称, 而且易于辨识
* 劣势: 不利于重构泛型或类型
* 结论: 前缀可只在类型敏感的地方使用; 对于一些通用的或类型无关的命名, 没必要(EG: Random); 但这种后缀命名比较通用
*/
namespace LearnDotNet
{
    using StringMapDouble = SortedDictionary<string, double>;
    using StringUnorderedMapDouble = Dictionary<string, double>;

    static class StaticClass
    {
        /// <summary>
        /// 生成设置范围内的Double的随机数
        /// eg:_random.NextDouble(1.5, 2.5)
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="miniDouble">生成随机数的最大值</param>
        /// <param name="maxiDouble">生成随机数的最小值</param>
        /// <returns>当Random等于NULL的时候返回0;</returns>
        public static double NextDouble(this Random random, double miniDouble, double maxiDouble)
        {
            if (random != null)
            {
                return random.NextDouble() * (maxiDouble - miniDouble) + miniDouble;
            }
            else
            {
                return 0.0d;
            }
        }
    }

    class Program
    {

        //校验给定字符数组是否为一个正确的身份证号码
        static bool isValidIdCard(char[] cCheckIdS) {
            int i, sum, n;
            for (sum = i = 0; i < 17; i++) {
                sum += ((1 << (17 - i)) % 11) * (cCheckIdS[i] - '0');
            }
            n = (12 - (sum % 11)) % 11;
            return (n < 10) ? (n == cCheckIdS[17] - '0') : (char.ToUpper(cCheckIdS[17]) == 'X');
        }

        /**
         * @see https://zh.wikipedia.org/wiki/中华人民共和国公民身份号码
         * 110102  YYYYMMDD  888    X
         * 地址码 出生日期码  顺序码 校验码
         * 地址码指的是公民常住户口所在县（市、镇、区）的行政区划代码
         * 出生日期码表示公民出生的公历年（4位）、月（2位）、日（2位）。
         * 顺序码是给同地址码同出生日期码的人编定的顺序号，其中奇数分配给男性，偶数(和X)分配给女性。
         * 最后一位是校验码，这里采用的是ISO 7064:1983,MOD 11-2校验码系统。
         *  校验码为一位数，但如果最后采用校验码系统计算的校验码是“10”，
         *  碍于身份证号码为18位的规定，则以“X”代替校验码“10”。
         */
        static string generateRandomIdCard(System.Random rnd) {
            //简略起见: PIN(个人识别码)
            //Id = district + yearPrefix[19, 21) + yearPostfix[00, 100) + month[01, 13) + day[01, 30) + seq[001, 600)
            string[] sDistrictS = new string[] { "500230", "350202", "110102" };
            string[] sYearPreS = new string[] { "19", "20" };
            string sIdCard = string.Format(
                    "{0}{1}{2:00}{3:00}{4:00}{5:000}"
                    , sDistrictS[rnd.Next(0, 3)]
                    , sYearPreS[rnd.Next(0, 2)]
                    , rnd.Next(10, 99), rnd.Next(1, 13)
                    , rnd.Next(1, 30), rnd.Next(1, 600)
                );
            //校验码常量
            ///char[] cVerifyS = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            char[] cVerifyS = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'X' };
            //加权因子常量
            //int[] nWeightS = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            int nWeightSum = 0;
            //计算除校验码外的所有bit的权重和
            for (int i = 0; i < 17; ++i) {
                //nWeightSum += nWeightS[i] * toIntNum(sIdCard[i]);
                nWeightSum += ((1 << (17 - i)) % 11) * toIntNum(sIdCard[i]);
            }
            //cVerifyS=(12-(nWeightSum mod 11)) mod 11;
            sIdCard += cVerifyS[(12 - (nWeightSum % 11)) % 11];
            return sIdCard;
        }

        //返回给定身份证号码的性别
        static string idSex(string idCard) {
            return int.Parse(idCard.Substring(14, 3)) % 2 == 1 ? "男" : "女";
        }

        static char toLowerAlph(char c)
        {
            if ('0' <= c && c <= '9')
            {
                return (char)('a' + c - '0');
            }
            else
            {
                if ('A' <= c && c <= 'Z')
                {
                    return (char)('a' + c - 'A');
                }
                else
                {
                    return c;
                }
            }
        }

        //将char字符转换为数字, 若不是字母或数字字符返回-1
        static int toIntNum(char alphOrCharNum){
            if ('0' <= alphOrCharNum && alphOrCharNum <= '9'){
                return alphOrCharNum - '0';
            }
            else{
                if ('A' <= alphOrCharNum && alphOrCharNum <= 'Z'){
                    return 10 + alphOrCharNum - 'A';
                }
                else if ('a' <= alphOrCharNum && alphOrCharNum <= 'z') {
                    return 10 + alphOrCharNum - 'a';
                } else {
                    return -1;
                }
            }
        }

        static int toBaseIntNum(char alphOrCharNum, int radix = 10) {
            int result = toIntNum(alphOrCharNum);
            if (result >= radix) {
                throw new Exception("不是指定进制的字符串");
            }
            return result;
        }

        //10ToA(Alph)
        static char toUppercaseAscllChar(int num){
            return num > 9 ? (char)(num - 10 + 'A') : (char)(num + '0');
        }

        //'A' == 'a' == '0' ... 'J' == 'j' == '9' 以此类推
        static bool isAa0Equal(char a, char b)
        {
            return toLowerAlph(a) == toLowerAlph(b);
        }
        
        //因子数目
        static int factorCount(int x)
        {
            int i, count = 0;
            double k = Math.Sqrt((double)x);//1 对应 x 计2   k对应k 计1
            for (i = 1; i <= k; i++)
            {
                if (x % i == 0)
                    count += i * i == x ? 1 : 2;//除因子k外   其余因子成对出现
            }
            return count;
        }

        static List<int> calcFactorList(int x)
        {
            int i, count = 0;
            List<int> factorList = new List<int>();
            double k = Math.Sqrt((double)x);//1 对应 x 计2   k对应k 计1
            for (i = 1; i <= k; i++)
            {
                if (x % i == 0)
                {
                    count += i * i == x ? 1 : 2;//除因子k外   其余因子成对出现
                    if(i*i == x)
                    {
                        factorList.Add(i);
                    }
                    else
                    {
                        factorList.Add(i);
                        factorList.Add(x / i);
                    }
                } 
            }
            return factorList;
        }

        //判断一个数n是否为质数
        static bool isPrime(int num)
        {
            int i = 0;
            for (i = 2; i * i <= num && num % i != 0; i++) ;
            return i * i > num && num > 1;
        }

        static void calcPrime()
        {
            for (int i = 2, j = 1; i < 2100000000 && j <= 1000; i++)
            //输出21亿内的所有质数，j控制只输出1000个。
            {
                if (isPrime(i))
                {
                    Console.WriteLine("{0,-10}{1}",j,i);
                    j++;
                }
            }
        }
        
        /**
         * 任意进制大数加法: 支持大小写传入, 只支持大写输出; 
         * 支持10个数字+26个字符(大小写等价) 表示的[2, 36]进制
         * 若传入参数与进制不符报异常(比如10进制输入A);
         * 由于使用了c#string 此处没法更改string的内存 所以sum不能与加数相同(或许可以更改为char[])
         */
        static string bigPlush(string topLowNumA, string topLowNumB, int radix = 10) {
            string topLowSum = new string("");
            int lenA = topLowNumA.Length, lenB = topLowNumB.Length, lenAB;
            //补0用
            string patchStr;
            //低位在右, 短者高位0补齐
            if (lenA > lenB){
                patchStr = new string('0', lenA - lenB);
                topLowNumB = patchStr + topLowNumB;
                lenAB = lenA;
            } else {
                patchStr = new string('0', lenB - lenA);
                topLowNumA = patchStr + topLowNumA;
                lenAB = lenB;
            }
            int carryNum = 0;
            for (int i = lenAB - 1; i >= 0; --i){
                //int sumBit = (topLowNumA[i] - '0') + (topLowNumB[i] - '0') + carryNum;
                int sumBit = toBaseIntNum(topLowNumA[i], radix) + toBaseIntNum(topLowNumB[i], radix) + carryNum;
                //char topBitChar = (char)(sumBit % radix + '0');
                char topBitChar = toUppercaseAscllChar(sumBit % radix);
                topLowSum = topBitChar.ToString() + topLowSum;
                carryNum = sumBit / radix;
            }
            return carryNum == 0 ? topLowSum : (topLowSum = "1" + topLowSum);
        }

        //数字转换为字符10->A
        //对一个字符进行增加 若增加后的值为10则转为A, 以此类推, 最后返回增加后的字符
        static char charPlush(char c, int addNum) {
            //Convert.ToInt32(lhsS[i])
            int assic = ((int)c + addNum);
            int num = assic - '0';
            if (num > 9 && (int)c < 'A') {
                assic = Convert.ToChar('A' + (assic - '9' - 1));
            }
            return ((char)assic);
        }
        static string _plush(string lhsS, string rhsS){
            string outString = new string("");
            int cNum = 0, i = lhsS.Length - 1;
            int sumBitNum = 0;
            for (; i >= 0; --i){
                sumBitNum = lhsS[i] + 1;
                if (sumBitNum > 'Z'){
                    cNum = sumBitNum / 'Z';
                    sumBitNum %= 'Z';
                } else {
                    sumBitNum = toIntNum((char)(lhsS[i] + 1));
                }
                outString += toUppercaseAscllChar(sumBitNum);
                outString = lhsS.Substring(0, i) + outString;
            }
            if (cNum != 0){
                outString = "1" + outString;
            }
            return outString;
        }

        //对于[10-99]之间的正整数N, 计算N^2+N+41的值R, 若R为质数, 输出N, R; 否则输出N, R 以及R的所有因数(非1与R)
        static void solveTwo(int n){
            double r = Math.Pow(n, 2) + n + 41;
            if (isPrime((int)r)){
                Console.Write("N={0}, R={1}", n, r);
            }else{
                List<int> temp = calcFactorList((int)r);
                Console.Write("N={0}, R={1}, R的因数:", n, r);
                /*
                 * foreach (int it in temp) {
                    if(it == 1 || it == r) {
                        temp.Remove(it);
                    }
                }
                */
                int pCnt = 0;
                foreach (int it in temp) {
                    if (it == 1 || it == r) {
                        continue;
                    } else {
                        Console.Write((++pCnt == 1 ? "" : " ") + it);
                    }
                }
            }
            Console.WriteLine("");
        }

        //选取一个没被用过的值
        static int randomSelectNonUsedIndex(List<int> nUsedIndexList, Random rnd, int maxIndex) {
            int currentIndex = -1;
            do {
                currentIndex = rnd.Next(0, maxIndex);
            } while (nUsedIndexList.FindIndex(item => item == currentIndex) != -1);
            nUsedIndexList.Add(currentIndex);
            return currentIndex;
        }

        /**
        * 3.生成20个身份证号码
        * 从产生的身份证中抽出1个一等奖; 2个二等奖; 输出奖项, 身份证号, 性别出生日期
        */
        static void solveThree(Random rnd) {
            List<int> nUsedIndexList = new List<int>();
            List<Award> awardList = new List<Award>();
            int index1 = randomSelectNonUsedIndex(nUsedIndexList, rnd, 20)
                , index2_1 = randomSelectNonUsedIndex(nUsedIndexList, rnd, 20)
                , index2_2 = randomSelectNonUsedIndex(nUsedIndexList, rnd, 20);
            awardList.Add(new Award(index1, "一等奖"));
            awardList.Add(new Award(index2_1, "二等奖"));
            awardList.Add(new Award(index2_2, "二等奖"));
            for (int i = 0; i < 20; ++i) {
                string PIN = generateRandomIdCard(rnd);
                //使用校验码校验
                //Console.WriteLine("校验" + (isValidIdCard(PIN.ToCharArray()) ? "成功" : "失败"));
                Award findAward = awardList.Find(ele => ele.getIndex() == i);
                if (null != findAward) {
                    Console.Write("奖项: " + findAward.getName());
                    Console.Write(" 身份证号码: " + PIN);
                    Console.Write(" 性别: " + idSex(PIN));
                    Console.WriteLine();
                }
            }
        }

        static void Main_HomeWork1(string[] args){
            string lhsS, rhsS = "1";
            Random rnd = new Random(System.DateTime.Now.Millisecond);
            while ((lhsS = Console.ReadLine()) != string.Empty){
                //1.36进制大数加1
                string outString = bigPlush(lhsS, rhsS, 36);
                //Console.WriteLine(outString);

                //2.如题
                for(int i = 0; i < 99; ++i) {
                    //solveTwo(i);
                }

                //3.如题
                solveThree(rnd);
            }
        }

        //生成一个n位随机数
        static int randomNum(Random rnd, int n) {
            int num = 0;
            while(n-- > 0) {
                num += (rnd.Next() % 9 + 1) * (int)Math.Pow(10, n);
            }
            return num;
        }

        static void solve1_10_10(Random rnd) {
            int maxCount = 30;
            List<int> randomList = new List<int>();
            List<int> randomList2 = new List<int>();
            for (int i = 0; i < maxCount; ++i) {
                int num = randomNum(rnd, 2);
                if(randomList.Find(ele => ele == num) == 0) {
                    randomList.Add(num);
                } else {
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
                if (isPrime((int)ele) || (int)ele % 2 == 0) {
                    //
                } else {
                    randomList2.Add(ele);
                }
            });
            randomList2.Sort((lhs, rhs) => (int)(lhs - rhs));
            int count = 0, pCnt = -1;
            randomList2.ForEach(ele => {
                Console.Write((++pCnt == 0 ? "" : " ") + ele);
                if(++count % 5 == 0) {
                    Console.WriteLine();
                    pCnt = -1;
                }
            });
            Console.WriteLine();
        }

        //返回传入日期的最近一个星期六
        static DateTime nearlyWeekend(DateTime dtBegin) {
            while (((int)dtBegin.DayOfWeek) != 6) {
                dtBegin = dtBegin.AddDays(1);
            }
            return dtBegin;
        }
        static void solve2_10_10() {
            //https://www.cnblogs.com/VisualStudio/archive/2008/11/16/1334645.html
            Console.WriteLine("输入起始日期与中止日期: ");
            /**
             * DateTime dateValue = new DateTime(2008, 6, 11);
             * Console.WriteLine(dateValue.ToString("dddd"));  

2017-10-9
2017-11-1

2018-10-9
2018-11-1
             */
            DateTime dtBegin, dtEnd, temp;
            int weekCount = 0;
            dtBegin = DateTime.Parse(Console.ReadLine());
            dtEnd = DateTime.Parse(Console.ReadLine());
            while((temp = nearlyWeekend(dtBegin)) <= dtEnd) {
                Console.WriteLine("第" + ++weekCount + "周" + dtBegin.ToString("yyyy-MM-d") + " " + temp.ToString("yyyy-MM-d"));
                dtBegin = temp.AddDays(1);
            }
            if(temp > dtEnd) {
                Console.WriteLine("第" + ++weekCount + "周" + dtBegin.ToString("yyyy-MM-d") + " " + dtEnd.ToString("yyyy-MM-d"));
            }
        }

        static void Main_HomeWork2(string[] args) {
            string num;
            Random rnd = new Random(System.DateTime.Now.Millisecond);
            Console.WriteLine("输入题目号码确定查阅题目: ");
            while ((num = Console.ReadLine()) != string.Empty) {
                switch (int.Parse(num)) {
                    case 1: { solve1_10_10(rnd); break; }
                    case 2: { solve2_10_10(); break; }
                }
                Console.WriteLine("输入题目号码确定查阅题目: ");
            }
        }

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

        static void solve1_10_17(Random rnd)
        {
            Student.init();
            List<Student> stuList = new List<Student>();
            for(int i = 0; i < 35; ++i)
            {
                Student stu = new Student("学生" + i.ToString("00"));
                stu.addScore("学科0", StaticClass.NextDouble(rnd, 40, 100));
                stu.addScore("学科1", StaticClass.NextDouble(rnd, 40, 100));
                stu.addScore("学科2", StaticClass.NextDouble(rnd, 40, 100));
                stu.addScore("学科3", StaticClass.NextDouble(rnd, 40, 100));
                stuList.Add(stu);
            }
            //计算加权积分
            stuList.ForEach(ele => {
                ele.calcWeightedSumScore();
                //Console.WriteLine(ele.ToString() + "; 加权积分: " + ele.getWeightedSumScore().ToString(".00"));
            });
            Console.WriteLine();
            //计算所有学生的成绩的加权积分
            stuList.Sort((lhs, rhs) => {
                //保留两位小数计算
                return (int)(rhs.getWeightedSumScore()*100 - lhs.getWeightedSumScore()*100);
            });
            
            int award1Count = (int)Math.Ceiling(Student.getStuCount() * 0.05);
            int award2Count = (int)Math.Ceiling(Student.getStuCount() * 0.10);
            int award3Count = (int)Math.Ceiling(Student.getStuCount() * 0.15);
            //输出所有学生的所有信息
            stuList.ForEach(ele => {
                string result = ele.ToString() + "; 加权积分: " + ele.getWeightedSumScore().ToString(".00");
                int awardCount = award1Count + award2Count + award3Count;
                if(awardCount > 0)
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
                Console.WriteLine(result);
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
                if(ele < int.MaxValue)
                {
                    Console.WriteLine("目的地" + targetCityId + "; 最小代价: " + ele);
                    int parentKey = targetCityId;
                    List<int> path = new List<int>();
                    while(parentKey >= 0)
                    {
                        path.Add(parentKey);
                        parentKey = prList[parentKey];
                    }
                    path.Reverse();
                    int trackIndex = 0;
                    path.ForEach(track => Console.Write((++trackIndex == 1 ? "" : "->") + track));
                    Console.WriteLine();
                }
                ++targetCityId;
            });
        }

        static void Main(string[] args){
            string num;
            Random rnd = new Random(System.DateTime.Now.Millisecond);
            Console.WriteLine("输入题目号码确定查阅题目: ");
            while ((num = Console.ReadLine()) != string.Empty)
            {
                switch (int.Parse(num))
                {
                    case 1: { solve1_10_17(rnd); break; }
                    case 2: { solve2_10_17(rnd); break; }
                }
                Console.WriteLine("输入题目号码确定查阅题目: ");
            }
        }
    }
}

//奖品类
class Award
{
    //获奖者的索引值
    int index;
    string name;
    public Award(int index, string name) {
        this.index = index;
        this.name = name;
    }

    public int getIndex() {
        return index;
    }

    public string getName() {
        return name;
    }
}
/*
* [] 是针对特定类型、固定长度的。
* Array 是针对任意类型、固定长度的。
* ArrayList 是针对任意类型、任意长度的。
* List 是针对特定类型、任意长度的。是ArrayList 的泛型版本(为什么不用List<Object>?)
* 
* SortedSet
* SortedList
*/
//Array myArr = Array.CreateInstance(typeof(Int32), 2, 3, 4);
//动态数组 Array的复杂版本: 可动态的增加和减少元素；实现了ICollection和IList接口；灵活的设置数组的大小。
//ArrayList myArrList = new ArrayList();

namespace GraphExtensionNameSpace
{
    using size_t = UInt32;
    using VertexKey = Int32;//顶点的键类型
    using WeightType = Int32;//边的权值类型
    using Vector = ArrayList;
    using EdgesType = LinkedList<IndexEdge>;

    //顶点数据类型
    class VertexValue
    {
        //VertexKey ID;
        char data = '0';
    };
    //权边(没有出发点信息的带权值的边) 排序时按照权值排序 等权值者按目标点id排序
    class IndexEdge : IComparable
    {
        public VertexKey targetID = (VertexKey)int.MaxValue;//边的对象顶点ID
        public int weight = -1;//不存在负值称长度 存在负值称权值
        public IndexEdge(VertexKey targetID, int weight)
        {
            this.weight = weight;
            this.targetID = targetID;
        }
        //可用于: Dijkstra的优先队列优化 跳表的实现
        public static bool operator <(IndexEdge lhs, IndexEdge rhs)
        {
            return lhs.weight == rhs.weight ? lhs.targetID < rhs.targetID
                : lhs.weight < rhs.weight;
        }
        public static bool operator >(IndexEdge lhs, IndexEdge rhs)
        {
            return lhs.weight == rhs.weight ? lhs.targetID > rhs.targetID
                : lhs.weight > rhs.weight;
        }
        //为了便于list的最短路径计算时的初始化
        // user-defined conversion from Fraction to double
        public static implicit operator int(IndexEdge rhs)
        {
            return (int)rhs.weight;
        }

        override
        public string ToString()
        {
            return "->" + this.targetID + "; ";
        }

        int IComparable.CompareTo(object obj)
        {
            IndexEdge lhs = this;
            IndexEdge rhs = (IndexEdge)obj;
            /*
             * return lhs.weight == rhs.weight ? lhs.targetID - rhs.targetID
                : lhs.weight - rhs.weight;
             */
            return lhs.weight - rhs.weight;
        }
    }
    /*有向边(边<=>直接关系)的定义  tips: ownerID targetID有(Relation)关系应该称作ownerID targetID连通*/
    class Edge : IndexEdge
    {
        public VertexKey ownerID;//边的拥有者ID
        public Edge(VertexKey ownerID, VertexKey targetID, WeightType weight): base(targetID, weight)
        {
            this.ownerID = ownerID;
        }
        public Edge(ref Edge rhs) : this(rhs.ownerID, rhs.targetID, rhs.weight){}
    };/*有向边<ownerID, targetID>*/

    public class PriorityQueue<T>
    {
        IComparer<T> comparer;
        T[] heap;

        public int Count { get; private set; }

        public PriorityQueue() : this(null) { }
        public PriorityQueue(int capacity) : this(capacity, null) { }
        public PriorityQueue(IComparer<T> comparer) : this(16, comparer) { }

        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
            this.heap = new T[capacity];
        }

        public void Push(T v)
        {
            if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
            heap[Count] = v;
            SiftUp(Count++);
        }

        public T Pop()
        {
            var v = Top();
            heap[0] = heap[--Count];
            if (Count > 0) SiftDown(0);
            return v;
        }

        public T Top()
        {
            if (Count > 0) return heap[0];
            throw new InvalidOperationException("优先队列为空");
        }

        void SiftUp(int n)
        {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2)
                heap[n] = heap[n2];
            heap[n] = v;
        }

        void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0) n2++;
                if (comparer.Compare(v, heap[n2]) >= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }
    }

    class Graph
    {
        int vertexNum;
        //邻接表(链表解决冲突的Hash表)
        List<EdgesType> edgeData;

        public Graph(int vertexNum)
        {
            this.vertexNum = vertexNum;
            initList(out edgeData, vertexNum, () => new EdgesType());
        }

        public static void initList(out Vector list, int cap, Object initEle)
        {
            list = new Vector();
            for (int i = 0; i < cap; ++i)
            {
                list.Add(initEle);
            }
        }
        public static void initList<T>(out List<T> list, int cap, Func<T> initFun)
        {
            list = new List<T>();
            for (int i = 0; i < cap; ++i)
            {
                list.Add(initFun());
            }
        }
        public static void initList<T>(out List<T> list, int cap, T initEle)
        {
            list = new List<T>();
            for (int i = 0; i < cap; ++i)
            {
                list.Add(initEle);
            }
        }

        public static IndexEdge listFind(EdgesType edges, VertexKey key)
        {
            foreach (IndexEdge ele in edges)
            {
                if (ele.targetID == key)
                {
                    return ele;
                }
            }
            return null;
        }

        public static void Swap<T>(ref T lhs, ref T rhs){
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        //插入 无向边
        public void insertEdgeUndirected(Edge e)
        {
            insertEdge(new Edge(ref e));
            Swap(ref e.ownerID, ref e.targetID);
            insertEdge(new Edge(ref e));
            Swap(ref e.ownerID, ref e.targetID);
        }
        public void insertEdge(Edge e)
        {
            IndexEdge it = listFind(edgeData[e.ownerID], e.targetID);
            if (it == null)
            {
                edgeData[e.ownerID].AddLast(e);
            }
            else
            {
                //已存在: 边数不增加 直接更新就行
                it.weight = e.weight;
            }
        }
        public void deleteEdge(VertexKey ownerID, VertexKey targetID){
            IndexEdge it = listFind(edgeData[ownerID], targetID);
		    if (it == null){
			    //do nothig
		    }else{
			        edgeData[ownerID].Remove(it);
		    }
        }
        public LinkedList<IndexEdge> getEdgeList(VertexKey v)
        {
            return edgeData[v];
        }

        //输出所有边
        public void print()
        {
            for (int i = 0; i < 10; ++i)
            {
                int prCnt = 0;
                foreach (IndexEdge indexEdge in this.getEdgeList(i))
                {
                    ++prCnt;
                    Console.Write(i.ToString() + indexEdge.ToString());
                }
                Console.Write(prCnt == 0 ? "" : Environment.NewLine);
            }
        }

        public bool Dijkstra(VertexKey origin, out List<WeightType> dist, out List<VertexKey> predecessor)
            {
                //最短距离估计dist优先队列 每个顶点入队一次 松弛一次
                //static priority_queue<IndexEdge> q;
                //C# 不允许使用 static 修饰符来声明方法内部的变量。SortedSet
                PriorityQueue<IndexEdge> q = new PriorityQueue<IndexEdge>();
                VertexKey v = -1;
                //dist.assign(vertexNum, INF);
                initList(out dist, vertexNum, int.MaxValue);
                //predecessor.assign(vertexNum, -1);
                initList(out predecessor, vertexNum, -1);
                dist[origin] = 0;
                //q.push({ origin, dist[origin] });//{targetID, weight}
                q.Push(new IndexEdge(origin, dist[origin]));

                //核心算法
                while (q.Count != 0)
                {
                    //v = q.top().targetID; q.pop();
                    v = q.Pop().targetID;
                    LinkedList<IndexEdge> edges = getEdgeList(v);
                    //对图中由v出发的每条边<v, w>的顶点w进行拓展松弛操作
                    foreach(IndexEdge element in edges){
                        //松弛操作: 先松弛过的顶点不会被后松弛的优化 若还能只可能是负权边的情况
                        if (dist[v] + element.weight < dist[element.targetID])
                        {
                            if (element.weight < 0)
                                return false;//错误:有负权边
                            dist[element.targetID] = dist[v] + element.weight;//更新w的最短路径距离估计
                            predecessor[element.targetID] = v;//更新w的最短路径前驱结点
                            q.Push(new IndexEdge(element.targetID, dist[element.targetID]));
                        }
                    }
                }
                return true;
            }
    }
}


