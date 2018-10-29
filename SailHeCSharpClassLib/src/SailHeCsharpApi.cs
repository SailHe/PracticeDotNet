using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
* @see C# API: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/
* @see C# Collections API: https://docs.microsoft.com/zh-cn/dotnet/standard/generics/collections
* @see C# Tips: https://www.jb51.net/article/74272.htm; https://juejin.im/entry/58ae4348ac502e006c886b80
* 
* 一律使用小驼峰
* 前缀表示类型或泛型: int->n; double->d; unsigned->un; string->s;
* 后缀表示复杂类型(通常是容器名, 且可复合使用): S->[](通常指原生数组); Array->数组实现的线性表类型; List->邻接表实现的线性表类型; _->裸指针(通常是需要自行管理内存的)
* 例如定义C++指针: List<int> *nNameList_ = null;
* 优势: 虽然现在编译器已经能很好的提示, 但这么做有利于重构名称, 而且易于辨识
* 劣势: 不利于重构泛型或类型
* 结论: 前缀可只在类型敏感的地方使用; 对于一些通用的或类型无关的命名, 没必要(EG: Random); 但这种后缀命名比较通用
* 
* [] 是针对特定类型、固定长度的。
* Array 是针对任意类型、固定长度的。
* ArrayList 是针对任意类型、任意长度的。
* List 是针对特定类型、任意长度的。是ArrayList 的泛型版本(为什么不用List<Object>?)
* SortedSet
* SortedList
* 
* Array myArr = Array.CreateInstance(typeof(Int32), 2, 3, 4);
* 动态数组 Array的复杂版本: 可动态的增加和减少元素；实现了ICollection和IList接口；灵活的设置数组的大小。
* ArrayList myArrList = new ArrayList();
*/

namespace SailHeCSharpClassLib
{   using StringMapDouble = SortedDictionary<string, double>;
    using StringUnorderedMapDouble = Dictionary<string, double>;

    public static class StaticExtensionApi
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

    public class UtilityApi
    {
        public static string Test()
        {
            return "Hello world!";
        }

        //校验给定字符数组是否为一个正确的身份证号码
        public static bool isValidIdCard(char[] cCheckIdS)
        {
            int i, sum, n;
            for (sum = i = 0; i < 17; i++)
            {
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
        public static string generateRandomIdCard(System.Random rnd)
        {
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
            for (int i = 0; i < 17; ++i)
            {
                //nWeightSum += nWeightS[i] * toIntNum(sIdCard[i]);
                nWeightSum += ((1 << (17 - i)) % 11) * toIntNum(sIdCard[i]);
            }
            //cVerifyS=(12-(nWeightSum mod 11)) mod 11;
            sIdCard += cVerifyS[(12 - (nWeightSum % 11)) % 11];
            return sIdCard;
        }

        //返回给定身份证号码的性别
        public static string idSex(string idCard)
        {
            return int.Parse(idCard.Substring(14, 3)) % 2 == 1 ? "男" : "女";
        }

        public static char toLowerAlph(char c)
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
        public static int toIntNum(char alphOrCharNum)
        {
            if ('0' <= alphOrCharNum && alphOrCharNum <= '9')
            {
                return alphOrCharNum - '0';
            }
            else
            {
                if ('A' <= alphOrCharNum && alphOrCharNum <= 'Z')
                {
                    return 10 + alphOrCharNum - 'A';
                }
                else if ('a' <= alphOrCharNum && alphOrCharNum <= 'z')
                {
                    return 10 + alphOrCharNum - 'a';
                }
                else
                {
                    return -1;
                }
            }
        }

        public static int toBaseIntNum(char alphOrCharNum, int radix = 10)
        {
            int result = toIntNum(alphOrCharNum);
            if (result >= radix)
            {
                throw new Exception("不是指定进制的字符串");
            }
            return result;
        }

        //10ToA(Alph)
        public static char toUppercaseAscllChar(int num)
        {
            return num > 9 ? (char)(num - 10 + 'A') : (char)(num + '0');
        }

        //'A' == 'a' == '0' ... 'J' == 'j' == '9' 以此类推
        public static bool isAa0Equal(char a, char b)
        {
            return toLowerAlph(a) == toLowerAlph(b);
        }

        //因子数目
        public static int factorCount(int x)
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

        public static List<int> calcFactorList(int x)
        {
            int i, count = 0;
            List<int> factorList = new List<int>();
            double k = Math.Sqrt((double)x);//1 对应 x 计2   k对应k 计1
            for (i = 1; i <= k; i++)
            {
                if (x % i == 0)
                {
                    count += i * i == x ? 1 : 2;//除因子k外   其余因子成对出现
                    if (i * i == x)
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
        public static bool isPrime(int num)
        {
            int i = 0;
            for (i = 2; i * i <= num && num % i != 0; i++) ;
            return i * i > num && num > 1;
        }

        public static void calcPrime()
        {
            for (int i = 2, j = 1; i < 2100000000 && j <= 1000; i++)
            //输出21亿内的所有质数，j控制只输出1000个。
            {
                if (isPrime(i))
                {
                    Console.WriteLine("{0,-10}{1}", j, i);
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
        public static string bigPlush(string topLowNumA, string topLowNumB, int radix = 10)
        {
            string topLowSum = "";
            int lenA = topLowNumA.Length, lenB = topLowNumB.Length, lenAB;
            //补0用
            string patchStr;
            //低位在右, 短者高位0补齐
            if (lenA > lenB)
            {
                patchStr = new string('0', lenA - lenB);
                topLowNumB = patchStr + topLowNumB;
                lenAB = lenA;
            }
            else
            {
                patchStr = new string('0', lenB - lenA);
                topLowNumA = patchStr + topLowNumA;
                lenAB = lenB;
            }
            int carryNum = 0;
            for (int i = lenAB - 1; i >= 0; --i)
            {
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
        public static char charPlush(char c, int addNum)
        {
            //Convert.ToInt32(lhsS[i])
            int assic = ((int)c + addNum);
            int num = assic - '0';
            if (num > 9 && (int)c < 'A')
            {
                assic = Convert.ToChar('A' + (assic - '9' - 1));
            }
            return ((char)assic);
        }
        public static string _plush(string lhsS, string rhsS)
        {
            string outString = "";
            int cNum = 0, i = lhsS.Length - 1;
            int sumBitNum = 0;
            for (; i >= 0; --i)
            {
                sumBitNum = lhsS[i] + 1;
                if (sumBitNum > 'Z')
                {
                    cNum = sumBitNum / 'Z';
                    sumBitNum %= 'Z';
                }
                else
                {
                    sumBitNum = toIntNum((char)(lhsS[i] + 1));
                }
                outString += toUppercaseAscllChar(sumBitNum);
                outString = lhsS.Substring(0, i) + outString;
            }
            if (cNum != 0)
            {
                outString = "1" + outString;
            }
            return outString;
        }
        
        //选取一个没被用过的值
        public static int randomSelectNonUsedIndex(List<int> nUsedIndexList, Random rnd, int maxIndex)
        {
            int currentIndex = -1;
            do
            {
                currentIndex = rnd.Next(0, maxIndex);
            } while (nUsedIndexList.FindIndex(item => item == currentIndex) != -1);
            nUsedIndexList.Add(currentIndex);
            return currentIndex;
        }


        //生成一个n位随机数
        public static int randomNum(Random rnd, int n)
        {
            int num = 0;
            while (n-- > 0)
            {
                num += (rnd.Next() % 9 + 1) * (int)Math.Pow(10, n);
            }
            return num;
        }

        //返回传入日期的最近一个星期六
        public static DateTime nearlyWeekend(DateTime dtBegin)
        {
            while (((int)dtBegin.DayOfWeek) != 6)
            {
                dtBegin = dtBegin.AddDays(1);
            }
            return dtBegin;
        }

    }
}
