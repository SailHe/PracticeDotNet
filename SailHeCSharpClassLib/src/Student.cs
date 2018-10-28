using System;
using System.Collections.Generic;
using System.Text;

namespace SailHeCSharpClassLib
{
    using StringMapDouble = SortedDictionary<string, double>;
    using StringUnorderedMapDouble = Dictionary<string, double>;

    //学生类 姓名, 学号, 分数统计
    [Serializable]
    public class Student
    {
        static int STU_ID = 0;
        static int STU_COUNT = 0;
        static StringMapDouble sumScoreMap = new StringMapDouble();

        //姓名
        string name;
        //学号
        string stuId;
        //加权积分值
        double weightedSumScore;
        //分数图
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
}
