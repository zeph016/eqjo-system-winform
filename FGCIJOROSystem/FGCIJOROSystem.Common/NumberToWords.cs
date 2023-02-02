//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Microsoft.VisualBasic;
//using Microsoft.VisualBasic.CompilerServices;

//namespace FGCIJOROSystem.Common
//{
//    public class NumberToWords
//    {
//        public NumberToWords()
//        {
//        }

//        public String GetWords(String numb, bool isCurrency, bool CentOver100 = true, string currencyName = "Pesos", string centName = "Cents")
//        {
//            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
//            String endStr = (isCurrency) ? ("Only") : ("");
//            try
//            {
//                int decimalPlace = numb.IndexOf(".");
//                if (decimalPlace > 0)
//                {
//                    wholeNo = numb.Substring(0, decimalPlace);
//                    points = numb.Substring(decimalPlace + 1);
//                    if (Convert.ToInt32(points) > 0)
//                    {
//                        andStr = (isCurrency) ? (currencyName + " And") : ((CentOver100) ? ("And") : ("point"));// just to separate whole numbers from points/cents
//                        endStr = (isCurrency) ? ((CentOver100) ? (endStr) : (centName + " " + endStr)) : ("");
//                        pointStr = (CentOver100) ? (" " + points.ToString() + "/100") : translateCentsNew(points);

//                    }
//                    else
//                    {
//                        andStr = (isCurrency) ? (currencyName) : ("");
//                    }
//                }
//                else
//                {
//                    andStr = (isCurrency) ? (currencyName) : ("");
//                }
//                val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
//            }
//            catch { ;}
//            return val;
//        }
//        private String translateWholeNumber(String number)
//        {
//            string word = "";
//            try
//            {
//                bool beginsZero = false;//tests for 0XX
//                bool isDone = false;//test if already translated
//                double dblAmt = (Convert.ToDouble(number));
//                //if ((dblAmt > 0) && number.StartsWith("0"))
//                if (dblAmt > 0)
//                {//test for zero or digit zero in a nuemric
//                    beginsZero = number.StartsWith("0");

//                    int numDigits = number.Length;
//                    int pos = 0;//store digit grouping
//                    String place = "";//digit grouping name:hundres,thousand,etc...
//                    switch (numDigits)
//                    {
//                        case 1://ones' range
//                            word = ones(number);
//                            isDone = true;
//                            break;
//                        case 2://tens' range
//                            word = tens(number);
//                            isDone = true;
//                            break;
//                        case 3://hundreds' range
//                            pos = (numDigits % 3) + 1;
//                            if (!number.First().ToString().Equals("0"))
//                            {
//                                place = " Hundred ";
//                            }
//                            else
//                            {
//                                place = "";
//                            }
//                            break;
//                        case 4://thousands' range
//                        case 5:
//                        case 6:
//                            pos = (numDigits % 4) + 1;
//                            if (!number.First().ToString().Equals("0"))
//                            {
//                                place = " Thousand ";
//                            }
//                            else
//                            {
//                                place = "";
//                            }
//                            break;
//                        case 7://millions' range
//                        case 8:
//                        case 9:
//                            pos = (numDigits % 7) + 1;
//                            place = " Million ";
//                            break;
//                        case 10://Billions's range
//                            pos = (numDigits % 10) + 1;
//                            place = " Billion ";
//                            break;
//                        //add extra case options for anything above Billion...
//                        default:
//                            isDone = true;
//                            break;
//                    }
//                    if (!isDone)
//                    {//if transalation is not done, continue...(Recursion comes in now!!)
//                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
//                        //check for trailing zeros
//                        if (beginsZero) word = " and " + word.Trim();
//                    }
//                    //ignore digit grouping names
//                    if (word.Trim().Equals(place.Trim())) word = "";
//                }
//            }
//            catch { ;}
//            return word.Trim();
//        }
//        private String tens(String digit)
//        {
//            int digt = Convert.ToInt32(digit);
//            String name = null;
//            switch (digt)
//            {
//                case 10:
//                    name = "Ten";
//                    break;
//                case 11:
//                    name = "Eleven";
//                    break;
//                case 12:
//                    name = "Twelve";
//                    break;
//                case 13:
//                    name = "Thirteen";
//                    break;
//                case 14:
//                    name = "Fourteen";
//                    break;
//                case 15:
//                    name = "Fifteen";
//                    break;
//                case 16:
//                    name = "Sixteen";
//                    break;
//                case 17:
//                    name = "Seventeen";
//                    break;
//                case 18:
//                    name = "Eighteen";
//                    break;
//                case 19:
//                    name = "Nineteen";
//                    break;
//                case 20:
//                    name = "Twenty";
//                    break;
//                case 30:
//                    name = "Thirty";
//                    break;
//                case 40:
//                    name = "Forty";
//                    break;
//                case 50:
//                    name = "Fifty";
//                    break;
//                case 60:
//                    name = "Sixty";
//                    break;
//                case 70:
//                    name = "Seventy";
//                    break;
//                case 80:
//                    name = "Eighty";
//                    break;
//                case 90:
//                    name = "Ninety";
//                    break;
//                default:
//                    if (digt > 0)
//                    {
//                        name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
//                    }
//                    break;
//            }
//            return name;
//        }
//        private String ones(String digit)
//        {
//            int digt = Convert.ToInt32(digit);
//            String name = "";
//            switch (digt)
//            {
//                case 1:
//                    name = "One";
//                    break;
//                case 2:
//                    name = "Two";
//                    break;
//                case 3:
//                    name = "Three";
//                    break;
//                case 4:
//                    name = "Four";
//                    break;
//                case 5:
//                    name = "Five";
//                    break;
//                case 6:
//                    name = "Six";
//                    break;
//                case 7:
//                    name = "Seven";
//                    break;
//                case 8:
//                    name = "Eight";
//                    break;
//                case 9:
//                    name = "Nine";
//                    break;
//            }
//            return name;
//        }
//        private String translateCents(String cents)
//        {
//            String cts = "", digit = "", engOne = "";
//            for (int i = 0; i < cents.Length; i++)
//            {
//                digit = cents[i].ToString();
//                if (digit.Equals("0"))
//                {
//                    engOne = "Zero";
//                }
//                else
//                {
//                    engOne = ones(digit);
//                }
//                cts += " " + engOne;
//            }
//            return cts;
//        }
//        private String translateCentsNew(String cents)
//        {
//            string newCent = cents;
//            if (cents.Length == 1)
//            {
//                newCent = newCent + "0";
//            }

//            return " " + translateWholeNumber(newCent);
//        }

//        #region backupworking
//        public string Ones(string pStrNum, string pStatus)
//        {
//            string[] strArray = new string[10]
//      {
//        "",
//        " ONE",
//        " TWO",
//        " THREE",
//        " FOUR",
//        " FIVE",
//        " SIX",
//        " SEVEN",
//        " EIGHT",
//        " NINE"
//      };
//            if (Conversion.Val(pStrNum) == 0.0)
//                return "";
//            return strArray[checked((int)Math.Round(Conversion.Val(pStrNum)))] + pStatus;
//        }

//        public string Tens(string pStrNum, string pStatus)
//        {
//            string str;
//            if (Conversion.Val(pStrNum) == 0.0)
//                str = "";
//            else if (Conversion.Val(pStrNum) < 20.0 & Conversion.Val(pStrNum) > 10.0)
//                str = new string[10]
//        {
//          "",
//          " ELEVEN",
//          " TWELVE",
//          " THIRTEEN",
//          " FOURTEEN",
//          " FIFTEEN",
//          " SIXTEEN",
//          " SEVENTEEN",
//          " EIGHTEEN",
//          " NINETEEN"
//        }[Conversions.ToInteger(Strings.Mid(pStrNum, 2, 1))] + pStatus;
//            else
//                str = new string[10]
//        {
//          "",
//          " TEN",
//          " TWENTY",
//          " THIRTY",
//          " FORTY",
//          " FIFTY",
//          " SIXTY",
//          " SEVENTY",
//          " EIGHTY",
//          " NINETY"
//        }[Conversions.ToInteger(Strings.Mid(pStrNum, 1, 1))] + pStatus;
//            return str;
//        }

//        public string ConvertIt(string pNum, string pCurrency, string pCent, bool ShowCurrency_CentName, bool ShortCent)
//        {
//            string str1 = "";
//            string str2;
//            if (Operators.CompareString(Strings.Trim(pNum), "", false) == 0)
//                str2 = "";
//            else if (Conversion.Val(pNum) > 99999999999.99)
//            {
//                int num = (int)Interaction.MsgBox((object)"Please enter number less than 99,999,999,999.", MsgBoxStyle.Information, (object)"Conversion");
//                str2 = "";
//            }
//            else
//            {
//                if (Operators.CompareString(pCurrency, "", false) == 0 | Operators.CompareString(pCent, "", false) == 0)
//                    ShowCurrency_CentName = false;
//                string str3 = Strings.Trim(this.CheckCents(pNum));
//                pNum = Strings.Trim(this.CheckWholeNum(pNum));
//                int num1 = Strings.Len(pNum);
//                while (num1 >= 1)
//                {
//                    switch (num1)
//                    {
//                        case 1:
//                            str1 += this.Ones(Strings.Mid(pNum, Strings.Len(pNum), 1), "");
//                            break;
//                        case 2:
//                            if (Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 1), 2)) < 20.0 & Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 1), 2)) > 9.0)
//                            {
//                                str1 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 1), 2), "");
//                                checked { --num1; }
//                                break;
//                            }
//                            str1 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 1), 2), "");
//                            break;
//                        case 3:
//                            str1 += this.Ones(Strings.Mid(pNum, checked(Strings.Len(pNum) - 2), 1), " HUNDRED");
//                            break;
//                        case 4:
//                            str1 += this.Ones(Strings.Mid(pNum, checked(Strings.Len(pNum) - 3), 1), " THOUSAND");
//                            break;
//                        case 5:
//                            if (Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 4), 2)) < 20.0 & Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 4), 2)) > 9.0)
//                            {
//                                str1 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 4), 2), " THOUSAND");
//                                checked { --num1; }
//                                break;
//                            }
//                            str1 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 4), 2), Conversions.ToString(Interaction.IIf(Conversion.Val(Strings.Mid(pNum, checked(Strings.Len(pNum) - 3), Strings.Len(pNum))) < 1000.0, (object)" THOUSAND", (object)"")));
//                            break;
//                        case 6:
//                            str1 += this.Ones(Strings.Mid(pNum, checked(Strings.Len(pNum) - 5), 1), Conversions.ToString(Interaction.IIf(Conversion.Val(Strings.Mid(pNum, checked(Strings.Len(pNum) - 4), Strings.Len(pNum))) < 1000.0, (object)" HUNDRED THOUSAND", (object)" HUNDRED")));
//                            break;
//                        case 7:
//                            str1 += this.Ones(Strings.Mid(pNum, checked(Strings.Len(pNum) - 6), 1), " MILLION");
//                            break;
//                        case 8:
//                            if (Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 7), 2)) < 20.0 & Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 7), 2)) > 9.0)
//                            {
//                                str1 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 7), 2), " MILLION");
//                                checked { --num1; }
//                                break;
//                            }
//                            str1 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 7), 2), Conversions.ToString(Interaction.IIf(Conversion.Val(Strings.Mid(pNum, checked(Strings.Len(pNum) - 6), Strings.Len(pNum))) < 1000.0, (object)" MILLION", (object)"")));
//                            break;
//                        case 9:
//                            str1 += this.Ones(Strings.Mid(pNum, checked(Strings.Len(pNum) - 8), 1), Conversions.ToString(Interaction.IIf(Conversion.Val(Strings.Mid(pNum, checked(Strings.Len(pNum) - 7), Strings.Len(pNum))) < 1000.0, (object)" HUNDRED MILLION", (object)" HUNDRED")));
//                            break;
//                        case 10:
//                            str1 += this.Ones(Strings.Mid(pNum, checked(Strings.Len(pNum) - 9), 1), " BILLION");
//                            break;
//                        case 11:
//                            if (Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 10), 2)) < 20.0 & Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 10), 2)) > 9.0)
//                            {
//                                str1 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 10), 2), " BILLION");
//                                checked { --num1; }
//                            }
//                            else
//                                str1 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 10), 2), Conversions.ToString(Interaction.IIf(Conversion.Val(Strings.Mid(pNum, checked(Strings.Len(pNum) - 9), Strings.Len(pNum))) < 1000.0, (object)" BILLION", (object)"")));
//                            break;
//                    }
//                    checked { num1 += -1; }
//                }
//                if (Operators.CompareString(Strings.Trim(str1), "", false) == 0 & Operators.CompareString(str3, "", false) == 0)
//                    str2 = "";
//                else if (Conversion.Val(str3) == 0.0)
//                {
//                    str2 = !ShowCurrency_CentName ? str1 : (Operators.CompareString(Strings.Trim(str1), "ONE", false) != 0 ? str1 + " " + pCurrency + "S" : str1 + " " + pCurrency);
//                    if (ShortCent)
//                    {
//                        str2 = str1 + " AND " + str3 + "/100";
//                    }
//                }
//                else
//                {
//                    if (Operators.CompareString(Strings.Trim(str1), "", false) != 0 && ShowCurrency_CentName)
//                        str1 = (Operators.CompareString(Strings.Trim(str1), "ONE", false) != 0 ? str1 + " " + pCurrency + "S" : str1 + " " + pCurrency) + " AND";
//                    string str4 = "";
//                    pNum = Conversions.ToString(Interaction.IIf(Operators.CompareString(Strings.Trim(str3), "1", false) == 0 | Operators.CompareString(Strings.Trim(str3), "2", false) == 0 | Operators.CompareString(Strings.Trim(str3), "3", false) == 0 | Operators.CompareString(Strings.Trim(str3), "4", false) == 0 | Operators.CompareString(Strings.Trim(str3), "5", false) == 0 | Operators.CompareString(Strings.Trim(str3), "6", false) == 0 | Operators.CompareString(Strings.Trim(str3), "7", false) == 0 | Operators.CompareString(Strings.Trim(str3), "8", false) == 0 | Operators.CompareString(Strings.Trim(str3), "9", false) == 0, (object)(str3 + "0"), (object)str3));
//                    int num2 = Strings.Len(pNum);

//                    while (num2 >= 1)
//                    {
//                        switch (num2)
//                        {
//                            case 1:
//                                str4 += this.Ones(Strings.Mid(pNum, Strings.Len(pNum), 1), "");
//                                break;
//                            case 2:
//                                if (Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 1), 2)) < 20.0 & Conversions.ToDouble(Strings.Mid(pNum, checked(Strings.Len(pNum) - 1), 2)) > 9.0)
//                                {
//                                    str4 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 1), 2), "");
//                                    checked { --num2; }
//                                }
//                                else
//                                    str4 += this.Tens(Strings.Mid(pNum, checked(Strings.Len(pNum) - 1), 2), "");
//                                break;
//                        }
//                        checked { num2 += -1; }
//                    }


//                    if (ShowCurrency_CentName)
//                    {
//                        if (Operators.CompareString(Strings.Trim(str4), "ONE", false) == 0)
//                            str2 = str1 + str4 + " " + pCent;
//                        else
//                            str2 = str1 + str4 + " " + pCent + "S";
//                    }

//                    else
//                    {
//                        ///problem sa 0/100
//                        if (ShortCent)
//                        {
//                            str2 = str1 + " AND " + str3 + "/100";
//                        }
//                        else
//                        {
//                            str2 = str1 + " Point" + str4;
//                        }
//                    }

//                }
//            }
//            return str2;
//        }

//        public string CheckWholeNum(string pStr)
//        {
//            string str = "";
//            int num1 = 1;
//            int num2 = Strings.Len(pStr);
//            int Start = num1;
//            while (Start <= num2)
//            {
//                string Left = Strings.Mid(pStr, Start, 1);
//                if (Operators.CompareString(Left, ".", false) != 0)
//                {
//                    if (Operators.CompareString(Left, ",", false) != 0)
//                        str += Left;
//                    checked { ++Start; }
//                }
//                else
//                    break;
//            }
//            return str;
//        }

//        public string CheckCents(string pStr)
//        {
//            string str = "";
//            int Start = Strings.Len(pStr);
//            while (Start >= 1)
//            {
//                if (Operators.CompareString(Strings.Mid(pStr, Start, 1), ".", false) == 0)
//                {
//                    str = Strings.Mid(pStr, checked(Start + 1), Strings.Len(pStr));
//                    break;
//                }
//                str = "0";
//                checked { Start += -1; }
//            }
//            return str;
//        }
//        #endregion

//    }
//}
