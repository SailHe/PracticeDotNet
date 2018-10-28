using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailHeCSharpClassLib;


namespace WinFormsApp.src
{

    using Shell = Debug;
    //using Shell = Console;

    class SolveHomeworkProblem
    {
        public static string SellApiForHomeWork()
        {
            Win32API.MainForTest();
            CInterFace.MainForTest();
            return SailHeCsharpApi.bigPlush("2147483647", "10");
        }
    }
}
