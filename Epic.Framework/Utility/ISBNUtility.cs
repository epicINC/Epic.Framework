using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Utility
{
    // http://www.cnblogs.com/ideal35500/archive/2010/11/22/1884711.html
    public static class ISBNUtility
    {

        /// <summary>        
        /// 10位数字中国标准书号校验码的计算。        
        /// <remarks>             
        /// 10位数字中国标准书号校验码采用模数11的加权算法计算得出。        
        ///         
        /// 数学公式为：        
        /// 校验码 = mod 11 {11-[mod 11 (加权乘积之和)]}        
        ///        = mod 11 {11-[mod 11 (248)]}        
        ///        = 5        
        ///         
        /// 以ISBN 7-5064-2595-5为例。        
        /// </remarks>        
        /// </summary>        
        /// <param name="barCode"></param>        
        /// <returns></returns>        
        public static string GetF10ISBN(string sCode)
        {
            string coreCode = sCode.Replace("-", ""); 
            coreCode = coreCode.Substring(0, 9); 
            int sum = 0; 
            for (int i = 10; i > 1; i--)
            {
                // 从高位至低位,分别乘以(10-i)                
                sum += i * Convert.ToInt32(coreCode.Substring((10 - i), 1));
            }
            
            string checkCode = "";
            if (sum % 11 == 0)
                checkCode = "0";
            else if (sum % 11 == 1)
                checkCode = "X"; 
            else
                checkCode = (11 - (sum % 11)).ToString();
            return String.Concat(coreCode, checkCode);
        }


        /// <summary>        
        /// 13位数字中国标准书号的校验码的计算。 
        /// <remarks>        
        /// 13位数字中国标准书号的校验码采用模数10的加权算法计算得出。        
        ///         
        /// 数学算式为：        
        /// 校验码 = mod 10 {10 – [mod 10 (中国标准书号前12位数字的加权乘积之和)]}        
        ///        = mod 10 {10 – [mod 10 (123)]}        
        ///        = 7        
        ///         
        /// 以ISBN 978-7-5064-2595-7为例。        
        /// </remarks>        
        /// </summary>        
        /// <param name="sCode"></param>        
        /// <returns></returns>        
        public static string GetF13ISBN(string sCode)
        {
            string coreCode = sCode.Replace("-", ""); coreCode = coreCode.Substring(0, 12);
            int oddSum = 0; //奇数和            
            int evenSum = 0;//偶数和            
            for (int i = 0; i < 12; i++)
            {
                if (i % 2 == 0)
                    evenSum += Convert.ToInt32(coreCode.Substring(i, 1));
                else
                    oddSum += Convert.ToInt32(coreCode.Substring(i, 1));
            } 
            int sum = oddSum + evenSum * 3;
            string checkCode = null;
            if (sum % 10 == 0)
                checkCode = "0";
            else
                checkCode = (10 - (sum % 10)).ToString();
            return String.Concat(coreCode, checkCode);
        }


        /// <summary>        
        /// 10位数字的中国标准书号转换为13位数字的中国标准书号。        
        /// <remarks>        
        /// 10位数字的中国标准书号转换为13位数字的中国标准书号，在其前9位数字之前加EAN•UCC前缀978，        
        /// 以模数10加权算法计算得出的校验码取代10位数字中国标准书号的校验码。        
        /// </remarks>        
        /// </summary>        
        /// <param name="s10ISBN"></param>        
        /// <returns></returns>        
        public static string ISBN10T13(string sISBN) 
        { 
            return GetF13ISBN(string.Concat("978-", sISBN));
        }

    }
}
