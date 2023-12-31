using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections;
using System.Drawing.Text;
using static Advent23.frm_Advent2023;
using System.Windows.Forms.Design;
using System.Drawing;
using static System.Windows.Forms.LinkLabel;
using System.Linq;

namespace Advent23
{
    public partial class frm_Advent2023 : Form
    {
        public Dictionary<string, long> dictPuzzle12Cache = new Dictionary<string, long>();

        public frm_Advent2023()
        {
            InitializeComponent();
        }
        #region Initialization
        private void frm_Advent2023_Load(object sender, EventArgs e)
        {

        }
        private void frm_Advent2023_FormClosing(object sender, FormClosingEventArgs e)
        {
            Advent23.Properties.Settings.Default.Save();
        }
        #endregion

        #region Puzzle 01
        private long Puzzle01_PartOne()
        {
            string strInput01 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string intDigit = "";
            int intSum = 0;
            string[] allString = strInput01.Split("\r\n");


            foreach (string strCalibration in allString)
            {
                bool boolFirst = false;
                char chTemp = '0';

                foreach (char chChar in strCalibration)
                {
                    if (char.IsDigit(chChar))
                    {
                        chTemp = chChar;
                        if (boolFirst == false) intDigit = chChar.ToString();
                        boolFirst = true;
                    }
                }
                intDigit += chTemp.ToString();
                intSum += int.Parse(intDigit);
            }

            return intSum;
        }
        private long Puzzle01_PartTwo()
        {
            string strInput01 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string intDigit = "";
            int intSum = 0;
            string[] allString = strInput01.Split("\r\n");
            foreach (string strCalibration in allString)
            {
                bool boolFirst = false;
                char chTemp = '0';
                string strCompile = "";
                foreach (char chChar in strCalibration)
                {
                    if (char.IsDigit(chChar))
                    {
                        strCompile = ""; //we wipe we go a number
                        chTemp = chChar;
                        if (boolFirst == false) intDigit = chChar.ToString();
                        boolFirst = true;
                    }
                    else
                    {
                        strCompile += chChar.ToString().ToUpper();
                        char chTranform = 'a'; //default false

                        if (strCompile.Contains("ONE"))
                        {
                            chTranform = '1';
                            strCompile = strCompile.Replace("ONE", "O1NE");
                        }
                        if (strCompile.Contains("TWO"))
                        {
                            chTranform = '2';
                            strCompile = strCompile.Replace("TWO", "T2WO");
                        }
                        if (strCompile.Contains("THREE"))
                        {
                            chTranform = '3';
                            strCompile = strCompile.Replace("THREE", "T3HREE");
                        }
                        if (strCompile.Contains("FOUR"))
                        {
                            chTranform = '4';
                            strCompile = strCompile.Replace("FOUR", "F4OUR");
                        }
                        if (strCompile.Contains("FIVE"))
                        {
                            chTranform = '5';
                            strCompile = strCompile.Replace("FIVE", "F5IVE");
                        }
                        if (strCompile.Contains("SIX"))
                        {
                            chTranform = '6';
                            strCompile = strCompile.Replace("SIX", "S6IX");
                        }
                        if (strCompile.Contains("SEVEN"))
                        {
                            chTranform = '7';
                            strCompile = strCompile.Replace("SEVEN", "S7EVEN");
                        }
                        if (strCompile.Contains("EIGHT"))
                        {
                            chTranform = '8';
                            strCompile = strCompile.Replace("EIGHT", "E8IGHT");
                        }
                        if (strCompile.Contains("NINE"))
                        {
                            chTranform = '9';
                            strCompile = strCompile.Replace("NINE", "N9INE");
                        }

                        if (char.IsDigit(chTranform))
                        {
                            chTemp = chTranform;
                            if (boolFirst == false) intDigit = chTranform.ToString();
                            boolFirst = true;
                        }
                    }
                }
                intDigit += chTemp.ToString();
                intSum += int.Parse(intDigit);
            }
            return intSum;
        }

        private void bntRun01_Click(object sender, EventArgs e)
        {
            long lngSolution01P1 = Puzzle01_PartOne();
            txt_output01P1.Text = lngSolution01P1.ToString();

            Puzzle01_PartTwo();
            long lngSolution01P2 = Puzzle01_PartTwo();
            txt_output01P2.Text = lngSolution01P2.ToString();
        }
        #endregion

        #region Puzzle 02
        private long Puzzle02_PartOne(int intMaxRed, int intMaxGreen, int intMaxBlue)
        {
            string strInput02 = Advent23.Properties.Settings.Default.Puzzle_Input;
            int intSum = 0;
            string[] allString = strInput02.Split("\r\n");

            foreach (string strGames in allString)
            {
                //hold the max shown in that game
                int intTopRed = 0;
                int intTopGreen = 0;
                int intTopBlue = 0;


                try
                {
                    //First we get Games #
                    int intGameNum = int.Parse(strGames.Substring(5, strGames.IndexOf(":") - 5));

                    //then clean/take the rest
                    string strStatements = strGames.Substring(strGames.IndexOf(":") + 1).Trim();
                    string[] strArrStatement = strStatements.Split(";");
                    foreach (string aStatement in strArrStatement)
                    {
                        //This is that they took in 1 set, we split again.
                        //now for each of them we split again if there is a ,
                        string[] strStoneStatement = aStatement.Split(",");
                        foreach (string aStoneValue in strStoneStatement)
                        {
                            //for that set, we check how many stone and of which color and records if that beat the max on that game.
                            int intCount = int.Parse(aStoneValue.Trim().Substring(0, aStoneValue.Trim().IndexOf(" ")).Trim());

                            switch (aStoneValue.Trim().Substring(aStoneValue.Trim().IndexOf(" ") + 1).Trim())
                            {
                                case "green":
                                    if (intTopGreen < intCount) intTopGreen = intCount;
                                    break;
                                case "red":
                                    if (intTopRed < intCount) intTopRed = intCount;
                                    break;
                                case "blue":
                                    if (intTopBlue < intCount) intTopBlue = intCount;
                                    break;
                            }
                        }
                    }

                    //now we have the max stone of each color on that game statements.
                    //we compare with that is the max given and if valid we add it to aggregated sum.
                    if (intTopGreen <= intMaxGreen && intTopRed <= intMaxRed && intTopBlue <= intMaxBlue) { intSum += intGameNum; }
                }
                catch (Exception ex)
                {
                    int i = 3; //for debugging in case of errors.
                }
            }

            return intSum;
        }
        private long Puzzle02_PartTwo()
        {
            string strInput02 = Advent23.Properties.Settings.Default.Puzzle_Input;
            long lngSum = 0;
            string[] allString = strInput02.Split("\r\n");

            foreach (string strGames in allString)
            {
                //hold the max shown in that game
                int intTopRed = 0;
                int intTopGreen = 0;
                int intTopBlue = 0;

                try
                {
                    //First we get Games #
                    int intGameNum = int.Parse(strGames.Substring(5, strGames.IndexOf(":") - 5));

                    //then clean/take the rest
                    string strStatements = strGames.Substring(strGames.IndexOf(":") + 1).Trim();
                    string[] strArrStatement = strStatements.Split(";");
                    foreach (string aStatement in strArrStatement)
                    {
                        //This is that they took in 1 set, we split again.
                        //now for each of them we split again if there is a ,
                        string[] strStoneStatement = aStatement.Split(",");
                        foreach (string aStoneValue in strStoneStatement)
                        {
                            int intCount = int.Parse(aStoneValue.Trim().Substring(0, aStoneValue.Trim().IndexOf(" ")).Trim());

                            switch (aStoneValue.Trim().Substring(aStoneValue.Trim().IndexOf(" ") + 1).Trim())
                            {
                                case "green":
                                    if (intTopGreen < intCount) intTopGreen = intCount;
                                    break;
                                case "red":
                                    if (intTopRed < intCount) intTopRed = intCount;
                                    break;
                                case "blue":
                                    if (intTopBlue < intCount) intTopBlue = intCount;
                                    break;
                            }
                        }
                    }

                    //now we have the max stone on that game statements.
                    //Part 2 was easier, we just power them and add it on the long sum (that I converted as I expected a bigger number)
                    lngSum += (intTopGreen * intTopRed * intTopBlue);
                }
                catch (Exception ex)
                {
                    int i = 3; //for debugging in case of errors.
                }
            }

            return lngSum;
        }

        private void bntRun02_Click(object sender, EventArgs e)
        {
            long lngSolution02P1 = Puzzle02_PartOne(12, 13, 14);
            txt_output02P1.Text = lngSolution02P1.ToString();

            long lngSolution02P2 = Puzzle02_PartTwo();
            txt_output02P2.Text = lngSolution02P2.ToString();
        }

        #endregion

        #region Puzzle 03
        private long Puzzle03_PartOne()
        {
            //Grid is 140x140
            string[,] strArray = new string[142, 142]; //to make it easy we pad around
            string strInput03 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput03.Split("\r\n");
            long lngSum = 0;

            int intX = 0;
            int intY;

            //we fill the array, we keep note of where we need to go in a list
            List<int[]> lstSymbolMark = new List<int[]>();
            List<int> lstNumberArray = new List<int>();
            int indexReference = -1;

            string strNumberFound = "";
            bool boolNumberFound = false;

            foreach (string strGames in allString)
            {
                intX++;
                intY = -1;
                foreach (char aChar in ("." + strGames + ".")) //we pad them to be easier to reference numbers
                {

                    intY++;
                    if (char.IsDigit(aChar))
                    {
                        if (boolNumberFound == false)
                        {
                            //we found a new number
                            //we reset the string and start the reference
                            strNumberFound = "";
                            indexReference++;
                            boolNumberFound = true;
                        }

                        //that we start or continue, we add the number and replace in the array the reference of it.
                        strNumberFound += aChar;
                        strArray[intX, intY] = indexReference.ToString();
                    }
                    else
                    {
                        if (boolNumberFound == true)
                        {
                            //we had a number we need to keep reference
                            lstNumberArray.Add(int.Parse(strNumberFound));
                            boolNumberFound = false;
                        }

                        //so if it was not a number, then let check if it was a symbol (and take note), we fill blank we don't care of them
                        if (aChar == '*' || aChar == '@' || aChar == '#' || aChar == '-' || aChar == '=' ||
                            aChar == '/' || aChar == '+' || aChar == '%' || aChar == '$' || aChar == '&')
                        {
                            lstSymbolMark.Add(new int[] { intX, intY });
                        }
                    }
                }
            }

            //at this point we got an array with reference to numbers
            //and we got a list of all symbol we need to check around
            foreach (int[] intGridValue in lstSymbolMark)
            {
                //we check around, if not empty then its a reference, which we add only if it wasn't the one we have in memory
                string strLastReference = "";

                for (int x = intGridValue[0] - 1; x <= intGridValue[0] + 1; x++)
                {
                    for (int y = intGridValue[1] - 1; y <= intGridValue[1] + 1; y++)
                    {
                        if (strArray[x, y] != null && strArray[x, y] != strLastReference)
                        {
                            //we got a number
                            strLastReference = strArray[x, y];
                            lngSum += lstNumberArray[int.Parse(strLastReference)];
                        }
                    }
                }
            }

            //now we got everything, we will

            return lngSum;
        }
        private long Puzzle03_PartTwo()
        {
            //Grid is 140x140
            string[,] strArray = new string[142, 142]; //to make it easy we pad around
            string strInput03 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput03.Split("\r\n");
            long lngSum = 0;

            int intX = 0;
            int intY;

            //we fill the array, we keep note of where we need to go in a list
            List<int[]> lstSymbolMark = new List<int[]>();
            List<int> lstNumberArray = new List<int>();
            int indexReference = -1;

            string strNumberFound = "";
            bool boolNumberFound = false;

            foreach (string strGames in allString)
            {
                intX++;
                intY = -1;
                foreach (char aChar in ("." + strGames + ".")) //we pad them to be easier to reference numbers
                {

                    intY++;
                    if (char.IsDigit(aChar))
                    {
                        if (boolNumberFound == false)
                        {
                            //we found a new number
                            //we reset the string and start the reference
                            strNumberFound = "";
                            indexReference++;
                            boolNumberFound = true;
                        }

                        //that we start or continue, we add the number and replace in the array the reference of it.
                        strNumberFound += aChar;
                        strArray[intX, intY] = indexReference.ToString();
                    }
                    else
                    {
                        if (boolNumberFound == true)
                        {
                            //we had a number we need to keep reference
                            lstNumberArray.Add(int.Parse(strNumberFound));
                            boolNumberFound = false;
                        }

                        //so if it was not a number, then let check if it was a symbol (and take note), we fill blank we don't care of them
                        if (aChar == '*')
                        {
                            lstSymbolMark.Add(new int[] { intX, intY });
                        }
                    }
                }
            }

            //at this point we got an array with reference to numbers
            //and we got a list of all symbol we need to check around
            foreach (int[] intGridValue in lstSymbolMark)
            {
                //we check around, if not empty then its a reference, which we add only if it wasn't the one we have in memory
                string strLastReference = "";
                long lngGear = 1;
                int intNumberCount = 0;

                for (int x = intGridValue[0] - 1; x <= intGridValue[0] + 1; x++)
                {
                    for (int y = intGridValue[1] - 1; y <= intGridValue[1] + 1; y++)
                    {
                        if (strArray[x, y] != null && strArray[x, y] != strLastReference)
                        {
                            //we got a number
                            strLastReference = strArray[x, y];
                            intNumberCount++;
                            lngGear *= lstNumberArray[int.Parse(strLastReference)];
                        }
                    }
                }
                if (lngGear != 1 && intNumberCount == 2)
                {
                    lngSum += lngGear;
                }
            }

            //now we got everything, we will

            return lngSum;
        }

        private void bntRun03_Click(object sender, EventArgs e)
        {
            long lngSolution03P1 = Puzzle03_PartOne();
            txt_output03P1.Text = lngSolution03P1.ToString();

            long lngSolution03P2 = Puzzle03_PartTwo();
            txt_output03P2.Text = lngSolution03P2.ToString();
        }

        #endregion

        #region Puzzle 04
        private long Puzzle04_PartOne()
        {
            string strInput04 = Advent23.Properties.Settings.Default.Puzzle_Input;
            long lnmgTotalPoint = 0;
            string[] allString = strInput04.Split("\r\n");

            foreach (string strGames in allString)
            {
                //hold the max shown in that game
                int[] pointValue = new int[11] { 0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };
                int intMatchCount = 0;
                List<int> intMyNumber;
                //First we get Games #
                int intGameNum = int.Parse(strGames.Substring(4, strGames.IndexOf(":") - 4).Trim());
                Dictionary<int, int> dictGamesResults = new Dictionary<int, int>();


                //then clean/take the rest
                string strStatements = strGames.Substring(strGames.IndexOf(":") + 1).Trim();
                string[] allNumber = strStatements.Split("|");

                string[] myNumber = allNumber[0].Trim().Split(" ");
                string[] cardsNumber = allNumber[1].Trim().Split(" ");

                //let put the winning one
                intMyNumber = new List<int>();
                foreach (string aNumber in myNumber)
                {
                    if (aNumber.Trim().Length > 0)
                    {
                        intMyNumber.Add(int.Parse(aNumber.Trim()));
                    }
                }

                foreach (string aNumber in cardsNumber)
                {
                    if (aNumber.Trim().Length > 0)
                    {
                        if (intMyNumber.Contains(int.Parse(aNumber.Trim()))) intMatchCount++;
                    }
                }
                lnmgTotalPoint += pointValue[intMatchCount];

            }

            return lnmgTotalPoint;
        }
        private long Puzzle04_PartTwo()
        {

            string strInput04 = Advent23.Properties.Settings.Default.Puzzle_Input;
            long lngTotalScratched = 0;

            string[] allString = strInput04.Split("\r\n");
            int[] intScratchGames = new int[250];
            List<int> intCardScratch = new List<int>();


            foreach (string strGames in allString)
            {
                //hold the max shown in that game
                int intMatchCount = 0;

                List<int> intMyNumber;

                //First we get Games #
                int intGameNum = int.Parse(strGames.Substring(4, strGames.IndexOf(":") - 4).Trim());
                intScratchGames[intGameNum]++; //the one we scratch now
                Dictionary<int, int> dictGamesResults = new Dictionary<int, int>();

                //then clean/take the rest
                string strStatements = strGames.Substring(strGames.IndexOf(":") + 1).Trim();
                string[] allNumber = strStatements.Split("|");

                string[] myNumber = allNumber[0].Trim().Split(" ");
                string[] cardsNumber = allNumber[1].Trim().Split(" ");

                //let put the winning one
                intMyNumber = new List<int>();
                foreach (string aNumber in myNumber)
                {
                    if (aNumber.Trim().Length > 0)
                    {
                        intMyNumber.Add(int.Parse(aNumber.Trim()));
                    }
                }

                foreach (string aNumber in cardsNumber)
                {
                    if (aNumber.Trim().Length > 0)
                    {
                        if (intMyNumber.Contains(int.Parse(aNumber.Trim()))) intMatchCount++;
                    }
                }
                //Now for each intMatchCount we add 1 future scratchpad
                for (int i = 1; i <= intMatchCount; i++)
                {
                    intScratchGames[intGameNum + i] += intScratchGames[intGameNum];
                }
            }

            lngTotalScratched = intScratchGames.Sum();
            return lngTotalScratched;
        }

        private void bntRun04_Click(object sender, EventArgs e)
        {
            long lngSolution04P1 = Puzzle04_PartOne();
            txt_output04P1.Text = lngSolution04P1.ToString();

            long lngSolution04P2 = Puzzle04_PartTwo();
            txt_output04P2.Text = lngSolution04P2.ToString();
        }

        #endregion

        #region Puzzle 05
        private long Puzzle05_PartOne()
        {
            //I did modify the input to be easier to parse

            string str_Seeds = @"950527520 85181200 546703948 123777711 63627802 279111951 1141059215 246466925 1655973293 98210926 3948361820 92804510 2424412143 247735408 4140139679 82572647 2009732824 325159757 3575518161 370114248";

            long lngReturmNumber = 0;

            long[] lngSeed = new long[20]; //there is 20 seeds

            string[] strArrSeed = str_Seeds.Split(" ");
            int x = 0;
            foreach (string aSeed in strArrSeed)
            {
                lngSeed[x++] = long.Parse(aSeed);
            }

            //now we got the array of seed that we will convert 7 time
            long[,,] lngConverter = new long[33, 3, 7]; //array is oversized, but we will have limited when determined

            string strInputTransfo = Advent23.Properties.Settings.Default.Puzzle_Input;


            string[] allString = strInputTransfo.Split("\r\n\r\n");
            //Now we shoudl have the 7 tranformation array

            int[] intConverterMax = new int[7];

            int intConvertCount = -1;

            foreach (string strConvertion in allString)
            {
                //for each of the convertion we 
                intConvertCount++;
                int intLineNum = -1;

                string[] aCommand = strConvertion.Split("\r\n");
                foreach (string aLine in aCommand)
                {
                    intLineNum++;

                    string[] str_convert_Value = aLine.Split(" ");
                    //this should now be an array of 3
                    lngConverter[intLineNum, 0, intConvertCount] = long.Parse(str_convert_Value[1]);
                    lngConverter[intLineNum, 1, intConvertCount] = long.Parse(str_convert_Value[1]) + long.Parse(str_convert_Value[2]) - 1;
                    lngConverter[intLineNum, 2, intConvertCount] = long.Parse(str_convert_Value[0]);
                }
                intConverterMax[intConvertCount] = intLineNum;
            }

            //now we convert in 7 steps
            for (int a = 0; a < 7; a++)
            {
                for (int b = 0; b < 20; b++) //for the 20 seeds
                {
                    for (int c = 0; c <= intConverterMax[a]; c++)  //for all the possible convertion we got
                    {
                        if (lngSeed[b] >= lngConverter[c, 0, a] && lngSeed[b] <= lngConverter[c, 1, a])
                        {
                            if (lngConverter[c, 2, a] == 0) { int i = 3; }
                            lngSeed[b] = (lngConverter[c, 2, a] + lngSeed[b] - lngConverter[c, 0, a]);
                            break;
                        }
                    }
                }
            }


            return lngSeed.Min();
        }
        private long Puzzle05_PartTwo()
        {
            //I did modify the input to be easier to parse, all convertion are just in the setting, and seed is below.

            string str_Seeds = @"950527520 85181200
546703948 123777711
63627802 279111951
1141059215 246466925
1655973293 98210926
3948361820 92804510
2424412143 247735408
4140139679 82572647
2009732824 325159757
3575518161 370114248";

            Dictionary<string, Tuple<long, long>> dictSeeds = new Dictionary<string, Tuple<long, long>>();
            string[] strArrSeed = str_Seeds.Split("\r\n");
            Tuple<long, long> aSeed;
            int intIncrementUID = 0; // to have a unique key for each convertion step as number can collide

            //let go through each line and create Tuple of sX1 to sX2 which are kept in dictionary
            foreach (string aSeedArr in strArrSeed)
            {
                string[] strSeedRange = aSeedArr.Split(" ");

                long lngSeedInit = long.Parse(strSeedRange[0]);
                long lngSeedRange = long.Parse(strSeedRange[0]) + long.Parse(strSeedRange[1]) - 1; // <- I forgot the -1 before it is what all my problem came from

                aSeed = new Tuple<long, long>(lngSeedInit, lngSeedRange);
                dictSeeds.Add("0_" + intIncrementUID++.ToString(), aSeed);
            }

            //now we got the array of seed that we will convert 7 time
            long[,,] lngConverter = new long[100, 3, 7]; //array is oversized, but we will have limiter (intConverterMax) when read
            //[A,B,C]
            //A = iteration of convertion (aka different line entries),
            //B = the line B[0] = X1 of the line, B[1] = X2 of the line, B[2] = new converted value of X1
            //C = Step of the convertion, as this is a 7 steps conversion mapping. [0]= seed->soil , [1]= soil->fertilizer ......

            string strInputTransfo = Advent23.Properties.Settings.Default.Puzzle_Input; //let get the simplified input of convertion (I just removed the text and made them in 7 block

            string[] allString = strInputTransfo.Split("\r\n\r\n");

            //As the array above is oversized to 100 possible line per convertion, we keep an array(intConverterMax) of where its truly end.
            int[] intConverterMax = new int[7];
            int intConvertStepCount = -1;

            foreach (string strConvertion in allString)
            {
                //for each of the convertion line, we determine the cX1 and cX2 range, and the value of convertion from cX1 to tX1
                intConvertStepCount++;
                int intLineNum = -1;

                string[] aCommand = strConvertion.Split("\r\n");
                foreach (string aLine in aCommand)
                {
                    intLineNum++;

                    string[] str_convert_Value = aLine.Split(" ");
                    //this should now be an array of 3
                    //as mentionned before B = the line B[0] = X1 of the line, B[1] = X2 of the line, B[2] = new converted value of X1
                    lngConverter[intLineNum, 0, intConvertStepCount] = long.Parse(str_convert_Value[1]);
                    lngConverter[intLineNum, 1, intConvertStepCount] = long.Parse(str_convert_Value[1]) + long.Parse(str_convert_Value[2]) - 1;// <- I forgot the -1 before it is what all my problem came from
                    lngConverter[intLineNum, 2, intConvertStepCount] = long.Parse(str_convert_Value[0]);
                }

                intConverterMax[intConvertStepCount] = intLineNum; //we read each line, we keep the iteration max in the array
            }

            //now we got all the information
            //We got the seeds lines in dictSeeds, and all convertion lines in lngConverter
            //we now compare seed to each convertion and convert until its all processed.
            for (int a = 0; a < 7; a++) //we will do more than 7 loop as we will play with a
            {
                List<string> lstKeys = new List<string>();
                foreach (string strKey in dictSeeds.Keys)
                {
                    if (int.Parse(strKey.Substring(0, 1)) == a) //is that dict entry for the step we are doing (the dictionary may have multiple steps entries, we only do the one we are currently on.
                    {
                        lstKeys.Add(strKey);
                    }
                }

                if (lstKeys.Count > 0) //if there is some seed on the currents steps, we will do them all (some cut might stay in current step, we do multi-pass)
                {
                    foreach (string strKey in lstKeys)
                    {
                        //we read the Tuple, we apply the for each and re-add any updated
                        //we then delete the key
                        Tuple<long, long> tpData = dictSeeds[strKey];
                        long seedX1 = tpData.Item1; //easier to read
                        long seedX2 = tpData.Item2; //easier to read

                        //initilisation of variable we will use
                        long convX1;
                        long convX2;
                        long convBaseValue;
                        Tuple<long, long> tpNewEnty;
                        long lngDifferencial_Min;
                        long lngDifferencial_Max;
                        bool boolConvertRest = true; //if we have a left-over we convert as is
                        dictSeeds.Remove(strKey); //we processed this seed, we remove it, we will add them back as process or leftover.


                        //we compare with each conversion
                        for (int c = 0; c <= intConverterMax[a]; c++)  //for all the possible convertion we got
                        {
                            convX1 = lngConverter[c, 0, a]; //easier to read
                            convX2 = lngConverter[c, 1, a]; //easier to read
                            convBaseValue = lngConverter[c, 2, a]; //easier to read

                            //Concept is a bit different, we have linear map
                            //in general we can continu if a full edge was removed, if the convertion is in between min/max, then we will need to split it and do another loop

                            if (seedX1 >= convX1 && seedX1 <= convX2)
                            {
                                //seed X1 is inside
                                if (seedX2 >= convX1 && seedX2 <= convX2)
                                {
                                    //Both sX1 and sX2 is inside

                                    /*
                                     * This is a scenario like this
                                     * ----->
                                     * cX1---sX1-------sX2----cX2
                                     * sX1 and sX2 is inside
                                     * We need to convert the whole see to the converted value that's it.
                                     */
                                    lngDifferencial_Min = seedX1 - convX1 + convBaseValue; //sX1 converted is the difference it have with cX1
                                    lngDifferencial_Max = seedX2 - convX1 + convBaseValue; //sX2 converted is the difference it have with cX1

                                    tpNewEnty = new Tuple<long, long>(lngDifferencial_Min, lngDifferencial_Max);
                                    dictSeeds.Add((a + 1).ToString() + "_" + (intIncrementUID++).ToString(), tpNewEnty);

                                    boolConvertRest = false; //no left over to convert, we flag it to skip it
                                    break; //we can stop that WHOLE seed was converted
                                }
                                else
                                {
                                    //Only sX1 inside

                                    /*
                                     * This is a scenario like this
                                     * ----->
                                     * cX1---sX1-----cX2----sX2
                                     * only sX1 and sX2 is further than cX2
                                     * We need to slice and convert sX1 to cX2, then slice the current seed as there is cX2+1 to sX2 left to convert
                                     */

                                    lngDifferencial_Min = seedX1 - convX1 + convBaseValue; //sX1 converted is the difference it have with cX1
                                    lngDifferencial_Max = convX2 - convX1 + convBaseValue; //sX2 it too far, we only convert up to cX2

                                    tpNewEnty = new Tuple<long, long>(lngDifferencial_Min, lngDifferencial_Max);
                                    dictSeeds.Add((a + 1).ToString() + "_" + (intIncrementUID++).ToString(), tpNewEnty);


                                    //Now we slice what is left to convert (we need re/write both the tuple and the seed
                                    seedX1 = convX2 + 1;
                                    tpData = new Tuple<long, long>(seedX1, seedX2);
                                }
                            }
                            else
                            {
                                //sX1 was not inside, let check sX2
                                if (seedX2 >= convX1 && seedX2 <= convX2)
                                {
                                    //only sX2 is inside
                                    /*
                                    * This is a scenario like this
                                    * ----->
                                    * sX1----cX1--sX2---cX2
                                    * sX1 was before and part of the range is inside 
                                    * We need to slice and convert cX1 to sX2, then slice the current seed as there is csX1 to cX1-1 left to convert
                                    */
                                    lngDifferencial_Min = 0 + convBaseValue; //cX1 converted base
                                    lngDifferencial_Max = seedX2 - convX1 + convBaseValue; //sX2 converted is the difference it have with cX1

                                    tpNewEnty = new Tuple<long, long>(lngDifferencial_Min, lngDifferencial_Max);
                                    dictSeeds.Add((a + 1).ToString() + "_" + (intIncrementUID++).ToString(), tpNewEnty);

                                    //Now we slice what is left to convert (we need re/write both the tuple and the seed
                                    seedX2 = convX1 - 1;
                                    tpData = new Tuple<long, long>(seedX1, seedX2);
                                }
                                else if (seedX1 < convX1 && seedX2 > convX2)
                                {
                                    //last possible scenario
                                    // the convertion is fully inside the seed range
                                    /*
                                    * This is a scenario like this
                                    * ----->
                                    * sX1----cX1--cX2---sX2
                                    * in this case we need to create to sub slice to reprocess
                                    * and we convert the full cX1 and cX2
                                    */
                                    lngDifferencial_Min = 0 + convBaseValue; //cX1 converted base
                                    lngDifferencial_Max = convX2 - convX1 + convBaseValue; //cX2 converted is the difference it have with cX1

                                    tpNewEnty = new Tuple<long, long>(lngDifferencial_Min, lngDifferencial_Max);
                                    dictSeeds.Add((a + 1).ToString() + "_" + (intIncrementUID++).ToString(), tpNewEnty);

                                    //we won't keep processing as we now split the current seed in 2
                                    //we will add them back to the stack for a repass
                                    tpNewEnty = new Tuple<long, long>(seedX1, convX1 - 1); //first bunch, sX1 to cX1-1
                                    dictSeeds.Add((a).ToString() + "_" + (intIncrementUID++).ToString(), tpNewEnty);


                                    tpNewEnty = new Tuple<long, long>(convX2 + 1, seedX2); //second bunch cX2+1 to sX2
                                    dictSeeds.Add((a).ToString() + "_" + (intIncrementUID++).ToString(), tpNewEnty);
                                    boolConvertRest = false; // we don't want to convert the left over
                                    break; //we can stop as we splitted, will go back in a repass
                                }
                            }
                        }
                        if (boolConvertRest)
                        {
                            //if we had a left over range that didn'T fit in any convertion range convert as is.
                            dictSeeds.Add((a + 1).ToString() + "_" + (intIncrementUID++).ToString(), tpData);
                        }
                    }

                    //we had keys to convert, in the scenario that we splitted in two, we might still have
                    //things to process, we reset the pass (a--), until we didn't get any seeds still on this pass.
                    a--;
                }
            }


            long lngLowest = 99999999999999999; //easier to start with random super high :P then fetch the first entry in the dict.
            //now we converted everything we get the lowest
            foreach (Tuple<long, long> tpLocation in dictSeeds.Values)
            {
                if (lngLowest > tpLocation.Item1) { lngLowest = tpLocation.Item1; }
            }

            return lngLowest;
        }

        private void bntRun05_Click(object sender, EventArgs e)
        {
            long lngSolution05P1 = Puzzle05_PartOne();
            txt_output05P1.Text = lngSolution05P1.ToString();

            long lngSolution05P2 = Puzzle05_PartTwo();
            txt_output05P2.Text = lngSolution05P2.ToString();
        }

        #endregion

        #region Puzzle 06
        private long Puzzle06_PartOne()
        {
            //updating for quadratic
            //ax^2 +bx + c = 0
            //then use -b +- square(b^2-4ac) / 2a
            //in this case its 
            //a = 1
            //b = -Time
            //c = +Distance
            //we trust it will compute and we are not checking if the quadratic is valid b^2 -4ac > 0

            long lnmgTotalPoint = 1;
            long[] lngTime = new long[4] { 55, 99, 97, 93 };
            long[] lngDistance = new long[4] { 401, 1485, 2274, 1405 };
            long lngNumWin = 0;

            for (int i = 0; i < lngTime.Length; i++)
            {
                //to find x1 and x2 its 
                //x1 = -b + square(b^2-4ac) / 2a
                //x2 = -b - square(b^2-4ac) / 2a
                long b = lngTime[i];
                long c = lngDistance[i];

                double X2 = (b + Math.Sqrt((b * b - (4 * c)))) / 2;
                double X1 = (b - Math.Sqrt((b * b - (4 * c)))) / 2;
                lngNumWin = (long)(Math.Floor(X2) - Math.Ceiling(X1)) + 1;
                if (lngNumWin > 0) lnmgTotalPoint = lnmgTotalPoint * lngNumWin;
            }

            return lnmgTotalPoint;
        }
        private long Puzzle06_PartTwo()
        {
            long lngNumWin = 0;
            long b = 55999793;
            long c = 401148522741405;

            double X2 = (b + Math.Sqrt((b * b - (4 * c)))) / 2;
            double X1 = (b - Math.Sqrt((b * b - (4 * c)))) / 2;
            lngNumWin = (long)(Math.Floor(X2) - Math.Ceiling(X1)) + 1;

            return lngNumWin;
        }

        private void bntRun06_Click(object sender, EventArgs e)
        {
            long lngSolution06P1 = Puzzle06_PartOne();
            txt_output06P1.Text = lngSolution06P1.ToString();

            long lngSolution06P2 = Puzzle06_PartTwo();
            txt_output06P2.Text = lngSolution06P2.ToString();
        }

        #endregion

        #region Puzzle 07
        private long Puzzle07_PartOne()
        {
            string strInput07 = Advent23.Properties.Settings.Default.Puzzle_Input;

            long lngGameCount = 0;
            long lnmgTotalPoint = 0;
            string[] allString = strInput07.Split("\r\n");
            Dictionary<string, int> dictGamesValue = new Dictionary<string, int>();
            List<string> lstGamesRank = new List<string>();

            foreach (string strGames in allString)
            {
                //we read all and add on top
                string[] strGameInfo = strGames.Split(" ");

                dictGamesValue.Add(strGameInfo[0].Trim(), int.Parse(strGameInfo[1].Trim()));
                lstGamesRank.Add(strGameInfo[0].Trim());
            }
            //now we got all the data, we du a quick easy bubble sort but with caveats
            for (int a = 0; a < lstGamesRank.Count(); a++)
            {
                for (int b = a + 1; b < lstGamesRank.Count(); b++)
                {
                    if (CalcWeakestHand(lstGamesRank[a], lstGamesRank[b]) == lstGamesRank[b])
                    {
                        //we swap
                        string strTempHand = lstGamesRank[a];
                        lstGamesRank[a] = lstGamesRank[b];
                        lstGamesRank[b] = strTempHand;
                    }
                }

                //now we got the weakest in position a, we can calculate that score right away
                lnmgTotalPoint += dictGamesValue[lstGamesRank[a]] * (a + 1);
            }

            return lnmgTotalPoint;
        }

        private string CalcWeakestHand(string strHand1, string strHand2)
        {
            // We do amoun
            Dictionary<char, int> dictHand1 = new Dictionary<char, int>();
            Dictionary<char, int> dictHand2 = new Dictionary<char, int>();
            int intHighestHand1 = 1;
            int intHighestHand2 = 1;
            string strReturn = "";


            for (int i = 0; i < 5; i++)
            {
                if (dictHand1.ContainsKey(strHand1[i]))
                {
                    dictHand1[strHand1[i]] = dictHand1[strHand1[i]] + 1;
                    if (intHighestHand1 < dictHand1[strHand1[i]]) intHighestHand1 = dictHand1[strHand1[i]];
                }
                else
                {
                    dictHand1.Add(strHand1[i], 1);
                }

                if (dictHand2.ContainsKey(strHand2[i]))
                {
                    dictHand2[strHand2[i]] = dictHand2[strHand2[i]] + 1;
                    if (intHighestHand2 < dictHand2[strHand2[i]]) intHighestHand2 = dictHand2[strHand2[i]];
                }
                else
                {
                    dictHand2.Add(strHand2[i], 1);
                }
            }

            //right away if one have highest amount int intHighestHand we can stop here and return it.
            if (intHighestHand1 > intHighestHand2)
            {
                strReturn = strHand2;
            }
            else if (intHighestHand1 < intHighestHand2)
            {
                strReturn = strHand1;
            }
            else
            {
                //more complicated next scenario
                //now we check if they have less itteration, if they do one might have double pair or full house and win over the other one
                if (dictHand1.Count > dictHand2.Count)
                {
                    strReturn = strHand1;
                }
                else if (dictHand1.Count < dictHand2.Count)
                {
                    strReturn = strHand2;
                }
                else
                {
                    //At this point they have the same hand tie breaker is by card value in order of first
                    byte intCardValue1;
                    byte intCardValue2;

                    for (int i = 0; i < 5; i++)
                    {
                        intCardValue1 = btGetCardNumericValue(strHand1[i]);
                        intCardValue2 = btGetCardNumericValue(strHand2[i]);

                        if (intCardValue1 > intCardValue2)
                        {
                            strReturn = strHand2;
                            break;
                        }
                        else if (intCardValue1 < intCardValue2)
                        {
                            strReturn = strHand1;
                            break;
                        }
                    }
                }
            }
            return strReturn;
        }

        private string CalcWeakestHandPart2(string strHand1, string strHand2)
        {
            // We do amoun
            Dictionary<char, int> dictHand1 = new Dictionary<char, int>();
            Dictionary<char, int> dictHand2 = new Dictionary<char, int>();
            int intHighestHand1 = 0;
            int intHighestHand2 = 0;
            string strReturn = "";

            int intHandJokerHold_1 = 0;
            int intHandJokerHold_2 = 0;

            for (int i = 0; i < 5; i++)
            {
                if (strHand1[i] == 'J')
                {
                    intHandJokerHold_1++;
                }
                else
                {
                    if (dictHand1.ContainsKey(strHand1[i]))
                    {
                        dictHand1[strHand1[i]] = dictHand1[strHand1[i]] + 1;
                        if (intHighestHand1 < dictHand1[strHand1[i]]) intHighestHand1 = dictHand1[strHand1[i]];
                    }
                    else
                    {
                        dictHand1.Add(strHand1[i], 1);
                    }
                    if (intHighestHand1 < dictHand1[strHand1[i]]) intHighestHand1 = dictHand1[strHand1[i]];
                }

                if (strHand2[i] == 'J')
                {
                    intHandJokerHold_2++;
                }
                else
                {
                    if (dictHand2.ContainsKey(strHand2[i]))
                    {
                        dictHand2[strHand2[i]] = dictHand2[strHand2[i]] + 1;
                    }
                    else
                    {
                        dictHand2.Add(strHand2[i], 1);
                    }
                    if (intHighestHand2 < dictHand2[strHand2[i]]) intHighestHand2 = dictHand2[strHand2[i]];
                }
            }

            //right away if one have highest amount int intHighestHand we can stop here and return it.
            if ((intHighestHand1 + intHandJokerHold_1) > (intHighestHand2 + intHandJokerHold_2))
            {
                strReturn = strHand2;
            }
            else if ((intHighestHand1 + intHandJokerHold_1) < (intHighestHand2 + intHandJokerHold_2))
            {
                strReturn = strHand1;
            }
            else
            {

                if (strHand1 == "JJJJJ" || strHand2 == "JJJJJ") { int i = 3; }


                //more complicated next scenario
                //now we check if they have less itteration, if they do one might have double pair or full house and win over the other one
                if ((dictHand1.Count + (Math.Floor((decimal)(intHandJokerHold_1 / 5)))) > dictHand2.Count + (Math.Floor((decimal)(intHandJokerHold_2 / 5))))
                {
                    strReturn = strHand1;
                }
                else if ((dictHand1.Count + (Math.Floor((decimal)(intHandJokerHold_1 / 5)))) < dictHand2.Count + (Math.Floor((decimal)(intHandJokerHold_2 / 5))))
                {
                    strReturn = strHand2;
                }
                else
                {
                    //At this point they have the same hand tie breaker is by card value in order of first
                    byte intCardValue1;
                    byte intCardValue2;

                    for (int i = 0; i < 5; i++)
                    {
                        intCardValue1 = btGetCardNumericValuePart2(strHand1[i]);
                        intCardValue2 = btGetCardNumericValuePart2(strHand2[i]);

                        if (intCardValue1 > intCardValue2)
                        {
                            strReturn = strHand2;
                            break;
                        }
                        else if (intCardValue1 < intCardValue2)
                        {
                            strReturn = strHand1;
                            break;
                        }
                    }
                }
            }
            return strReturn;
        }
        private byte btGetCardNumericValue(char chrCard)
        {
            byte btCardValue = 0;

            if (char.IsDigit(chrCard))
            {
                btCardValue = ((byte)chrCard);
            }
            else
            {
                switch (chrCard)
                {
                    case 'T':
                        btCardValue = 58;
                        break;
                    case 'J':
                        btCardValue = 59;
                        break;
                    case 'Q':
                        btCardValue = 60;
                        break;
                    case 'K':
                        btCardValue = 61;
                        break;
                    case 'A':
                        btCardValue = 62;
                        break;
                }
            }

            return btCardValue;
        }
        private byte btGetCardNumericValuePart2(char chrCard)
        {
            byte btCardValue = 0;

            if (char.IsDigit(chrCard))
            {
                btCardValue = ((byte)chrCard);
            }
            else
            {
                switch (chrCard)
                {
                    case 'T':
                        btCardValue = 58;
                        break;
                    case 'J':
                        btCardValue = 30;
                        break;
                    case 'Q':
                        btCardValue = 60;
                        break;
                    case 'K':
                        btCardValue = 61;
                        break;
                    case 'A':
                        btCardValue = 62;
                        break;
                }
            }

            return btCardValue;
        }

        private long Puzzle07_PartTwo()
        {
            //249628565
            //249299962
            //249298950
            string strInput07 = Advent23.Properties.Settings.Default.Puzzle_Input;

            long lngGameCount = 0;
            long lnmgTotalPoint = 0;
            string[] allString = strInput07.Split("\r\n");
            Dictionary<string, int> dictGamesValue = new Dictionary<string, int>();
            List<string> lstGamesRank = new List<string>();

            foreach (string strGames in allString)
            {
                //we read all and add on top
                string[] strGameInfo = strGames.Split(" ");

                dictGamesValue.Add(strGameInfo[0].Trim(), int.Parse(strGameInfo[1].Trim()));
                lstGamesRank.Add(strGameInfo[0].Trim());
            }
            //now we got all the data, we du a quick easy bubble sort but with caveats
            for (int a = 0; a < lstGamesRank.Count(); a++)
            {

                for (int b = a + 1; b < lstGamesRank.Count(); b++)
                {
                    if (a == 973 && (lstGamesRank[a] == "JJJJJ" || lstGamesRank[b] == "JJJJJ")) { int ssss = 2; }

                    if (CalcWeakestHandPart2(lstGamesRank[a], lstGamesRank[b]) == lstGamesRank[b])
                    {
                        //we swap
                        string strTempHand = lstGamesRank[a];
                        lstGamesRank[a] = lstGamesRank[b];
                        lstGamesRank[b] = strTempHand;
                    }
                }

                //now we got the weakest in position a, we can calculate that score right away
                lnmgTotalPoint += dictGamesValue[lstGamesRank[a]] * (a + 1);
            }


            return lnmgTotalPoint;
        }

        private void bntRun07_Click(object sender, EventArgs e)
        {
            long lngSolution07P1 = Puzzle07_PartOne();
            txt_output07P1.Text = lngSolution07P1.ToString();

            long lngSolution07P2 = Puzzle07_PartTwo();
            txt_output07P2.Text = lngSolution07P2.ToString();
        }

        #endregion

        #region Puzzle 08


        private class P8_Instructions
        {
            protected string strInstruction = "LLLRRRLLRLRLLRRRLRLRRLRRLRRRLRRLLLRLRRLLRLRRRLRRRLLLRLLLLRLRRLLLRRRLRRRLRLRRRLLLLRLRLLRRLLRRRLRRLRLRRRLRRRLLLRLRRRLRRRLRRLLRRLRRRLLRLRLRLRLRLRRRLRLRRLRLRLRLRRLRRLRLRLRRLLRRLRRRLRRLRRLRRRLRRLRLLRLRLLRRLRRRLRLRLRRLLRRLRRRLRRLRRRLRLRRRLRRLRLRRLRLRRLLLRRLRRLRRRLRLRRLRRRLRLRLRRLRLLRRRR";
            protected int intPointerCounter = 0;

            public char NextInstruction()
            {
                if (intPointerCounter == strInstruction.Length) intPointerCounter = 0;
                return strInstruction[intPointerCounter++];
            }
        }

        private long Puzzle08_PartOne()
        {
            //preparsed some of it
            //answers

            //
            string strInput08 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput08.Split("\n");
            Dictionary<string, Tuple<string, string>> dictMap = new Dictionary<string, Tuple<string, string>>();
            P8_Instructions instructions = new P8_Instructions();

            Tuple<string, string> coordinate;


            foreach (string strGames in allString)
            {
                //I trimmed it so its eaiser to read those instruction
                coordinate = new Tuple<string, string>(strGames.Substring(7, 3), strGames.Substring(12, 3));
                dictMap.Add(strGames.Substring(0, 3), coordinate);
            }

            //now we path
            string strPosition = "AAA";
            long lngSteps = 0;
            do
            {
                //we make one step
                lngSteps++;
                coordinate = dictMap[strPosition];

                if (instructions.NextInstruction() == 'L')
                {
                    strPosition = coordinate.Item1;
                }
                else
                {
                    strPosition = coordinate.Item2;
                }
            } while (strPosition != "ZZZ");

            return lngSteps;
        }
        private long Puzzle08_PartTwo()
        {
            //this one is weird
            //such a weird .... the puzzle seem to still have 1 path per ending A
            //so if you got 6 ending A, you will got 6 ending Z
            //and they are linear 100% so after you walk it twice the number stay as is
            //The code will get those ending, then will parse the stepings on each,
            //then the proper way would be do to LCM (Least Common Multiple) but way over complicated to code
            //so I get the highest, then keep powering it until its mod all other number correctly

            string strInput08 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput08.Split("\n");
            Dictionary<string, Tuple<string, string>> dictMap = new Dictionary<string, Tuple<string, string>>();
            P8_Instructions instructions = new P8_Instructions();

            Tuple<string, string> coordinate;

            List<string> lstPositions = new List<string>();
            Dictionary<string, int> dictEnding = new Dictionary<string, int>();
            int intEnding = 0;

            foreach (string strGames in allString)
            {
                //I trimmed it so its eaiser to read those instruction
                coordinate = new Tuple<string, string>(strGames.Substring(7, 3), strGames.Substring(12, 3));
                dictMap.Add(strGames.Substring(0, 3), coordinate);
                if (strGames[2] == 'A') lstPositions.Add(strGames.Substring(0, 3));
                if (strGames[2] == 'Z') dictEnding.Add(strGames.Substring(0, 3), intEnding++);
            }

            //now we path

            List<long>[] lstEndZ = new List<long>[dictEnding.Count];
            for (int i = 0; i < dictEnding.Count; i++)
            {
                lstEndZ[i] = new List<long>();
            }

            for (long a = 0; a < 1000000; a++)
            {
                char chDirection = instructions.NextInstruction();
                string strPosition;
                for (int i = 0; i < lstPositions.Count; i++)
                {
                    strPosition = lstPositions[i];

                    //we make one step
                    coordinate = dictMap[strPosition];
                    if (chDirection == 'L')
                    {
                        strPosition = coordinate.Item1;
                    }
                    else
                    {
                        strPosition = coordinate.Item2;
                    }

                    lstPositions[i] = strPosition;
                    if (strPosition[2] == 'Z') lstEndZ[dictEnding[strPosition]].Add(a);
                }
            }


            List<long> lstInterval = new List<long>();
            for (int i = 0; i < lstPositions.Count; i++)
            {
                lstInterval.Add(lstEndZ[i][2] - lstEndZ[i][1]);
            }
            long lngValue = LCM(lstInterval);

            return lngValue;
        }

        private void bntRun08_Click(object sender, EventArgs e)
        {
            long lngSolution08P1 = Puzzle08_PartOne();
            txt_output08P1.Text = lngSolution08P1.ToString();

            long lngSolution08P2 = Puzzle08_PartTwo();
            txt_output08P2.Text = lngSolution08P2.ToString();
        }

        #endregion

        #region Puzzle 09
        private long Puzzle09_PartOne()
        {
            string strInput09 = Advent23.Properties.Settings.Default.Puzzle_Input;
            long lngSumResult = 0;
            string[] allString = strInput09.Split("\r\n");

            foreach (string strHistory in allString)
            {
                //Now we need to do the history
                long[] lngHistory = Puzzle9FindAllHistory(strHistory);
                lngSumResult += lngHistory[0];
            }

            return lngSumResult;
        }

        private long[] Puzzle9FindAllHistory(string strLines)
        {
            /*
             *  1   3   6  10  15  21  28
                  2   3   4   5   6   7
                    1   1   1   1   1
                      0   0   0   0
             *From exemple above it will return [28,7,1,0]
             *I'm expecting Part B to play with those number (instead of just last)
             */

            long[] lngResult;
            //I will simply just create an array for all (simpler/cleaner)

            long[,] lngAll = new long[100, 100]; //easier 100,100, won't use all
            long stepping = 9; //to finish, this must be equal to 0

            long lngLenght; //to keep lenght in the array above
            long lngRow = 0;//keep track of how many row we have (are are at when we calculate stepping)


            string[] allNumbers = strLines.Split(" ");
            lngLenght = allNumbers.Length;
            for (int i = 0; i < allNumbers.Count(); i++)
            {
                lngAll[lngRow, i] = long.Parse(allNumbers[i]);
            }


            //now this one iterate until we find stepping of 0
            do
            {
                lngLenght--; //we reduce lenght by 1 for each steps
                stepping = 0;

                for (int lngCol = 0; lngCol < lngLenght; lngCol++)
                {
                    lngAll[lngRow + 1, lngCol] = lngAll[lngRow, lngCol + 1] - lngAll[lngRow, lngCol];
                    stepping += lngAll[lngRow + 1, lngCol];
                }

                //we are done with the row
                lngRow++;
            } while (stepping != 0);

            //Now we got all answer, we reverse and build the long

            lngResult = new long[lngRow + 1]; //lenght will be the amount of row we did, starting with the answer (but we in theory calculate reverse)

            lngResult[lngRow] = 0; //we start with 0
            for (long x = lngRow - 1; x >= 0; x--)
            {
                lngResult[x] = lngAll[x, lngLenght] + lngResult[x + 1];

                lngLenght++;//as we go reverse the lenght is re-increasing
            }



            return lngResult;
        }

        private long[] Puzzle8Part2FindAllHistory(string strLines)
        {
            //it was just a tweak from Part1 just above, instead of calculating from the end, we calculated from the start

            long[] lngResult;
            //I will simply just create an array for all (simpler/cleaner)

            long[,] lngAll = new long[100, 100]; //easier 100,100, won't use all
            long stepping = 9; //to finish, this must be equal to 0

            long lngLenght; //to keep lenght in the array above
            long lngRow = 0;//keep track of how many row we have (are are at when we calculate stepping)


            string[] allNumbers = strLines.Split(" ");
            lngLenght = allNumbers.Length;
            for (int i = 0; i < allNumbers.Count(); i++)
            {
                lngAll[lngRow, i] = long.Parse(allNumbers[i]);
            }


            //now this one iterate until we find stepping of 0
            do
            {
                lngLenght--; //we reduce lenght by 1 for each steps
                stepping = 0;

                for (int lngCol = 0; lngCol < lngLenght; lngCol++)
                {
                    lngAll[lngRow + 1, lngCol] = lngAll[lngRow, lngCol + 1] - lngAll[lngRow, lngCol];
                    stepping += lngAll[lngRow + 1, lngCol];
                }

                //we are done with the row
                lngRow++;
            } while (stepping != 0);

            //Now we got all answer, we reverse and build the long

            lngResult = new long[lngRow + 1]; //lenght will be the amount of row we did, starting with the answer (but we in theory calculate reverse)

            lngResult[lngRow] = 0; //we start with 0
            for (long x = lngRow - 1; x >= 0; x--)
            {
                lngResult[x] = lngAll[x, 0] - lngResult[x + 1];

                lngLenght++;//as we go reverse the lenght is re-increasing
            }

            return lngResult;
        }

        private long Puzzle09_PartTwo()
        {
            string strInput09 = Advent23.Properties.Settings.Default.Puzzle_Input;
            long lngSumResult = 0;
            string[] allString = strInput09.Split("\r\n");

            foreach (string strHistory in allString)
            {
                //Now we need to do the history
                long[] lngHistory = Puzzle8Part2FindAllHistory(strHistory);
                lngSumResult += lngHistory[0];
            }

            return lngSumResult;
        }

        private void bntRun09_Click(object sender, EventArgs e)
        {
            long lngSolution09P1 = Puzzle09_PartOne();
            txt_output09P1.Text = lngSolution09P1.ToString();

            long lngSolution09P2 = Puzzle09_PartTwo();
            txt_output09P2.Text = lngSolution09P2.ToString();
        }

        #endregion

        #region Puzzle 10
        private long Puzzle10_PartOne()
        {
            //Grid is 140x140 
            char[,] charArray = new char[142, 142]; //to make it easy we pad around
            string strInput10 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput10.Split("\r\n");

            int intX = 0;
            int intY = 0;
            //we fill the array, we keep note of where we need to go in a list


            long[] lstPath = new long[500]; //500 steps max
            long lstStepAt = 0;
            int[] intLocation = new int[2]; //0 = x , 1 = y

            foreach (string strGames in allString)
            {
                intX++;
                intY = -1;
                foreach (char aChar in ("." + strGames + ".")) //we pad them to be easier to reference numbers
                {
                    intY++;
                    charArray[intX, intY] = aChar;

                    if (aChar == 'S')
                    {
                        intLocation[0] = intX;
                        intLocation[1] = intY;
                    }
                }
            }

            //We got our map now we go around and scan
            //I looked at my input, simpler, we will go north first (its valid)
            char chDirection = 'N';
            char chMapChar = 'N';

            intX = intLocation[0];
            intY = intLocation[1];
            do
            {
                lstStepAt++;
                //now we determine where we go depending of our direction and where we are

                //Depend on the direction we go
                switch (chDirection)
                {
                    case 'N':
                        intX--;
                        break;
                    case 'E':
                        intY++;
                        break;
                    case 'W':
                        intY--;
                        break;
                    case 'S':
                        intX++;
                        break;
                }
                chMapChar = charArray[intX, intY];
                chDirection = getHeading(chDirection, charArray[intX, intY]);
            } while (chMapChar != 'S');



            //now we got everything, we will

            return lstStepAt / 2;
        }

        private char getHeading(char currentHeading, char strPipe)
        {
            char newHeading = 'N';

            switch (strPipe)
            {
                case '|':
                    //stay as is
                    newHeading = currentHeading;
                    break;
                case '-':
                    //stay as is
                    newHeading = currentHeading;
                    break;
                case 'L':
                    if (currentHeading == 'S')
                    {
                        newHeading = 'E';
                    }
                    else
                    {
                        newHeading = 'N';
                    }
                    break;
                case 'J':
                    if (currentHeading == 'S')
                    {
                        newHeading = 'W';
                    }
                    else
                    {
                        newHeading = 'N';
                    }
                    break;
                case 'F':
                    if (currentHeading == 'N')
                    {
                        newHeading = 'E';
                    }
                    else
                    {
                        newHeading = 'S';
                    }
                    break;
                case '7':
                    if (currentHeading == 'N')
                    {
                        newHeading = 'W';
                    }
                    else
                    {
                        newHeading = 'S';
                    }
                    break;
            }
            return newHeading;
        }

        private long Puzzle10_PartTwo()
        {
            //we do same as 1, but we will redraw the map clear the junk and I will take a look by hand
            //Grid is 140x140
            char[,] charArray = new char[142, 142]; //to make it easy we pad around
            char[,] charCorrectedArray = new char[142, 142]; //to make it easy we pad around
            string strInput10 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput10.Split("\r\n");

            int intX = 0;
            int intY = 0;
            //we fill the array, we keep note of where we need to go in a list


            long[] lstPath = new long[500]; //500 steps max
            long lstStepAt = 0;
            int[] intLocation = new int[2]; //0 = x , 1 = y

            foreach (string strGames in allString)
            {
                intX++;
                intY = -1;
                foreach (char aChar in ("." + strGames + ".")) //we pad them to be easier to reference numbers
                {
                    intY++;
                    charArray[intX, intY] = aChar;

                    if (aChar == 'S')
                    {
                        intLocation[0] = intX;
                        intLocation[1] = intY;
                    }
                }
            }

            //We got our map now we go around and scan
            //I looked at my input, simpler, we will go north first (its valid)
            char chDirection = 'N';
            char chMapChar = 'N';

            intX = intLocation[0];
            intY = intLocation[1];
            do
            {
                charCorrectedArray[intX, intY] = charArray[intX, intY];

                lstStepAt++;
                //now we determine where we go depending of our direction and where we are

                //Depend on the direction we go
                switch (chDirection)
                {
                    case 'N':
                        intX--;
                        break;
                    case 'E':
                        intY++;
                        break;
                    case 'W':
                        intY--;
                        break;
                    case 'S':
                        intX++;
                        break;
                }
                chMapChar = charArray[intX, intY];
                chDirection = getHeading(chDirection, charArray[intX, intY]);
            } while (chMapChar != 'S');

            //we replace the S per the true of what it is a J
            //This will be needed for when we do a re-run to see outside walls
            charArray[intX, intY] = 'J';

            //now we got everything, we will
            for (int a = 0; a < 142; a++)
            {
                for (int b = 0; b < 142; b++)
                {
                    if (a == 0 || a == 141 || b == 0 || b == 141) charCorrectedArray[a, b] = '0'; //its outside
                    if (charCorrectedArray[a, b] == '\0') charCorrectedArray[a, b] = '.'; //undertermined yet
                }
            }

            /*I took a visual inspection of it and its tough We will need to redo the maze with an known exterior wall
             * we will keep the general direction of where the wall outside is and pain specific possible . along the way
             * 
             * As an example
             * if I hit 7 which is West<->South (I came from one of them)
             * if I say 'RIGHT' for outside wall and I came from South , then 
             *  all the zero are also outside (if they are dot I would repaint them Outside)
             *  Then I continue on the maze until I'm back to my Start then we can paint extension and we should be close
             *  000
             *  .70
             *  .X0
             * 
             */


            //from inspection I took and F and going EAST, outside is LEFT
            //SUPER INNEFICIENT but I don't care at this point. 
            chDirection = 'E';

            intY = 17;
            intX = 11;
            char chOutside = 'L';

            lstStepAt = 0;
            do
            {
                lstStepAt++;
                //we got a direction
                //now we determine where we go depending of our direction and where we are

                switch (chDirection)
                {
                    case 'N':
                        intX--;
                        break;
                    case 'E':
                        intY++;
                        break;
                    case 'W':
                        intY--;
                        break;
                    case 'S':
                        intX++;
                        break;
                }
                chMapChar = charArray[intX, intY];


                switch (chDirection)
                {
                    case 'N':
                        //3 possible char |, F, 7 
                        if (chMapChar == '|')
                        {
                            /*  L.R
                             *  L|R
                             *  L.R
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX, intY - 1] == '.') charCorrectedArray[intX, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                                if (charCorrectedArray[intX, intY + 1] == '.') charCorrectedArray[intX, intY + 1] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                        }
                        else if (chMapChar == 'F')
                        {
                            //3 possible char - 7 and J
                            /*  LLL
                             *  LF.
                             *  L.R
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX - 1, intY] == '.') charCorrectedArray[intX - 1, intY] = '0';
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                                if (charCorrectedArray[intX, intY - 1] == '.') charCorrectedArray[intX, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                        }
                        else if (chMapChar == '7')
                        {
                            /*  RRR
                             *  .7R
                             *  L.R
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX - 1, intY] == '.') charCorrectedArray[intX - 1, intY] = '0';
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                                if (charCorrectedArray[intX, intY + 1] == '.') charCorrectedArray[intX, intY + 1] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                        }

                        break;
                    case 'E':
                        //3 possible char - 7 and J
                        if (chMapChar == '-')
                        {
                            /*  LLL
                             *  .-.
                             *  RRR
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX - 1, intY] == '.') charCorrectedArray[intX - 1, intY] = '0';
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY] == '.') charCorrectedArray[intX + 1, intY] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                        }
                        else if (chMapChar == '7')
                        {
                            //3 possible char - 7 and J
                            /*  LLL
                             *  .7L
                             *  R.L
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX - 1, intY] == '.') charCorrectedArray[intX - 1, intY] = '0';
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                                if (charCorrectedArray[intX, intY + 1] == '.') charCorrectedArray[intX, intY + 1] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                            }
                        }
                        else if (chMapChar == 'J')
                        {
                            /*  L.R
                             *  .JR
                             *  RRR
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                                if (charCorrectedArray[intX, intY + 1] == '.') charCorrectedArray[intX, intY + 1] = '0';
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY] == '.') charCorrectedArray[intX + 1, intY] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                        }
                        break;
                    case 'W':
                        //3 possible char - L and F
                        if (chMapChar == '-')
                        {
                            /*  RRR
                             *  .-.
                             *  LLL
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY] == '.') charCorrectedArray[intX + 1, intY] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX - 1, intY] == '.') charCorrectedArray[intX - 1, intY] = '0';
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                            }
                        }
                        else if (chMapChar == 'L')
                        {
                            //3 possible char - 7 and J
                            /*  L.R
                             *  LL.
                             *  LLL
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX, intY - 1] == '.') charCorrectedArray[intX, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY] == '.') charCorrectedArray[intX + 1, intY] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                            }
                        }
                        else if (chMapChar == 'F')
                        {
                            /*  RRR
                             *  RF.
                             *  R.L
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX - 1, intY] == '.') charCorrectedArray[intX - 1, intY] = '0';
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                                if (charCorrectedArray[intX, intY - 1] == '.') charCorrectedArray[intX, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                            }
                        }
                        break;
                    case 'S':
                        //3 possible char | L and J
                        if (chMapChar == '|')
                        {
                            /*  R.L
                             *  R|L
                             *  R.L
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                                if (charCorrectedArray[intX, intY + 1] == '.') charCorrectedArray[intX, intY + 1] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX, intY - 1] == '.') charCorrectedArray[intX, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                            }
                        }
                        else if (chMapChar == 'L')
                        {
                            /*  R.L
                             *  RL.
                             *  RRR
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                                if (charCorrectedArray[intX, intY - 1] == '.') charCorrectedArray[intX, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY] == '.') charCorrectedArray[intX + 1, intY] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                        }
                        else if (chMapChar == 'J')
                        {
                            /*  R.L
                             *  .JL
                             *  LLL
                             */
                            if (chOutside == 'L')
                            {
                                if (charCorrectedArray[intX - 1, intY + 1] == '.') charCorrectedArray[intX - 1, intY + 1] = '0';
                                if (charCorrectedArray[intX, intY + 1] == '.') charCorrectedArray[intX, intY + 1] = '0';
                                if (charCorrectedArray[intX + 1, intY - 1] == '.') charCorrectedArray[intX + 1, intY - 1] = '0';
                                if (charCorrectedArray[intX + 1, intY] == '.') charCorrectedArray[intX + 1, intY] = '0';
                                if (charCorrectedArray[intX + 1, intY + 1] == '.') charCorrectedArray[intX + 1, intY + 1] = '0';
                            }
                            else
                            {
                                if (charCorrectedArray[intX - 1, intY - 1] == '.') charCorrectedArray[intX - 1, intY - 1] = '0';
                            }
                        }
                        break;
                }

                chDirection = getHeading(chDirection, charArray[intX, intY]);

            } while (intY != 17 || intX != 11);


            //Now we do a quick re-run and if undertermined but beside a 0, we repaint it as outside
            for (int a = 1; a < 141; a++)
            {
                for (int b = 1; b < 141; b++)
                {
                    if (charCorrectedArray[a, b] == '.')
                    {
                        //if there is any zero around its become O
                        for (int c = -1; c <= 1; c++)
                        {
                            for (int d = -1; d <= 1; d++)
                            {
                                if (charCorrectedArray[a + c, b + d] == '0') charCorrectedArray[a, b] = '0';
                            }
                        }
                    }
                }
            }
            //we repass reverse
            for (int a = 140; a > 0; a--)
            {
                for (int b = 140; b > 0; b--)
                {
                    if (charCorrectedArray[a, b] == '.')
                    {
                        //if there is any zero around its become O
                        for (int c = -1; c <= 1; c++)
                        {
                            for (int d = -1; d <= 1; d++)
                            {
                                if (charCorrectedArray[a + c, b + d] == '0') charCorrectedArray[a, b] = '0';
                            }
                        }
                    }
                }
            }


            long lngDotLeft = 0;
            for (int a = 1; a < 141; a++)
            {
                for (int b = 1; b < 141; b++)
                {
                    if (charCorrectedArray[a, b] == '.') lngDotLeft++;
                }
            }


            return lngDotLeft;
        }


        private void bntRun10_Click(object sender, EventArgs e)
        {
            long lngSolution10P1 = Puzzle10_PartOne();
            txt_output10P1.Text = lngSolution10P1.ToString();

            long lngSolution10P2 = Puzzle10_PartTwo();
            txt_output10P2.Text = lngSolution10P2.ToString();
        }

        #endregion

        #region Puzzle 11
        private long Puzzle11_PartOne()
        {

            //Grid is 200x200 
            char[,] charArrayInit = new char[200, 200]; //to make it easy we pad around, the baseline is 140x140
            char[,] charArrayFinal = new char[200, 200]; //to make it easy we pad around
            string strInput10 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput10.Split("\r\n");

            long lngTotalDistance = 0;
            //we will do 2, left right, place thing accordinglinghy in X, but then when we repass Y we will need to push them up
            int intX = 0;
            int intY = 0;

            //we fill the array, we keep note of where we need to go in a list



            int intGalaxyCount = 0; // incrementer for the dictionnary
            Tuple<int, int>[] tpCoordinate = new Tuple<int, int>[500]; //oversized


            bool boolHaveGalaxy = false;
            foreach (string strGames in allString)
            {
                intX = -1;
                intY++;

                boolHaveGalaxy = false;
                foreach (char aChar in (" " + strGames + " ")) //we pad them to be easier to reference numbers
                {
                    intX++;

                    if (aChar == '#')
                    {
                        boolHaveGalaxy = true;
                        charArrayInit[intY, intX] = aChar;
                    }
                }

                if (boolHaveGalaxy == false) { intY++; } //we skip next row as expansion
            }

            //now we did the Y expansion we repass and do the X one
            //As we repass we are going with the new value and recreate a new one
            int intX_Inceased = 0;
            for (intX = 0; intX < 200; intX++)
            {
                boolHaveGalaxy = false;
                intX_Inceased++;

                for (intY = 0; intY < 200; intY++)
                {
                    if (charArrayInit[intY, intX] == '#')
                    {
                        boolHaveGalaxy = true;
                        charArrayFinal[intY, intX_Inceased] = charArrayInit[intY, intX_Inceased];
                        tpCoordinate[intGalaxyCount++] = new Tuple<int, int>(intY, intX_Inceased);
                    }
                }
                if (boolHaveGalaxy == false) { intX_Inceased++; } //we skip next column as expansion
            }

            //now we got the galaxy expanded and all coordinate, we need to calculate between all
            //there might be faster way but I will just iterate
            lngTotalDistance = 0;
            for (int a = 0; a < intGalaxyCount; a++)
            {
                for (int b = a + 1; b < intGalaxyCount; b++)
                {
                    intY = tpCoordinate[a].Item1 - tpCoordinate[b].Item1;
                    if (intY < 0) intY = intY * -1;
                    intX = tpCoordinate[a].Item2 - tpCoordinate[b].Item2;
                    if (intX < 0) intX = intX * -1;

                    lngTotalDistance += (intY + intX);
                }
            }




            //now we got everything, we will

            return lngTotalDistance;
        }

        private long Puzzle11_PartTwo()
        {
            //Grid is 200x200 
            string[,] charArrayInit = new string[200, 200]; //to make it easy we pad around, the baseline is 140x140
            string[,] charArrayFinal = new string[200, 200]; //to make it easy we pad around
            string strInput10 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput10.Split("\r\n");


            //692507918828
            long lngGalaxyIncrease = 999999;

            long lngTotalDistance = 0;
            //we will do 2, left right, place thing accordinglinghy in X, but then when we repass Y we will need to push them up
            long intX = 0;
            long intY = 0;
            //we fill the array, we keep note of where we need to go in a list

            int intGalaxyCount = 0; // incrementer for the dictionnary
            Tuple<long, long>[] tpCoordinate = new Tuple<long, long>[500]; //oversized

            bool boolHaveGalaxy = false;
            int intEmptyCount = 0;
            foreach (string strGames in allString)
            {
                intX = -1;
                intY++;

                boolHaveGalaxy = false;
                foreach (char aChar in (" " + strGames + " ")) //we pad them to be easier to reference numbers
                {
                    intX++;

                    if (aChar == '#')
                    {
                        boolHaveGalaxy = true;
                        charArrayInit[intY, intX] = (intY + (intEmptyCount * lngGalaxyIncrease)).ToString();
                    }
                }

                if (boolHaveGalaxy == false) { intEmptyCount++; } //we skip next row as expansion
            }

            //now we did the Y expansion we repass and do the X one
            //As we repass we are going with the new value and recreate a new one
            int intX_Inceased = 0;
            intEmptyCount = 0;
            for (intX = 0; intX < 200; intX++)
            {
                boolHaveGalaxy = false;

                for (intY = 0; intY < 200; intY++)
                {
                    if (charArrayInit[intY, intX] != null)
                    {
                        boolHaveGalaxy = true;
                        tpCoordinate[intGalaxyCount++] = new Tuple<long, long>(long.Parse(charArrayInit[intY, intX]), (intX + (intEmptyCount * lngGalaxyIncrease)));
                    }
                }
                if (boolHaveGalaxy == false) { intEmptyCount++; } //we skip next column as expansion
            }

            //now we got the galaxy expanded and all coordinate, we need to calculate between all
            //there might be faster way but I will just iterate
            lngTotalDistance = 0;
            for (int a = 0; a < intGalaxyCount; a++)
            {
                for (int b = a + 1; b < intGalaxyCount; b++)
                {
                    intY = tpCoordinate[a].Item1 - tpCoordinate[b].Item1;
                    if (intY < 0) intY = intY * -1;
                    intX = tpCoordinate[a].Item2 - tpCoordinate[b].Item2;
                    if (intX < 0) intX = intX * -1;

                    lngTotalDistance += (intY + intX);
                }
            }




            //now we got everything, we will

            return lngTotalDistance;
        }

        private void bntRun11_Click(object sender, EventArgs e)
        {
            long lngSolution11P1 = Puzzle11_PartOne();
            txt_output11P1.Text = lngSolution11P1.ToString();

            long lngSolution11P2 = Puzzle11_PartTwo();
            txt_output11P2.Text = lngSolution11P2.ToString();
        }

        #endregion

        #region Puzzle 12
        private long Puzzle12_PartOne()
        {
            string strInput12 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput12.Split("\r\n");

            long lngTotalArragements = 0;
            foreach (string strGames in allString)
            {
                string[] strPuzzleLine = strGames.Split(" ");

                string[] strInstruction = strPuzzleLine[1].Split(",");
                List<long> lstInstruction = new List<long>();

                string strPuzzle = strPuzzleLine[0];
                string strInstructions = strPuzzleLine[1];

                //Now we do a little cleanup optimisation (to help the cache)
                //multiple dot together is useless
                //starting and ending dots are useless
                string strPuzzleOptimised = "";
                bool boolPoints = true; //for removing duplicate  (aka .... will become .)
                for (int i = 0; i < strPuzzle.Length; i++)
                {
                    if (strPuzzle[i] != '.')
                    {
                        strPuzzleOptimised += strPuzzle[i];
                        boolPoints = false;
                    }
                    else if (boolPoints == false)
                    {
                        //we do keep the first instance of .
                        strPuzzleOptimised += strPuzzle[i];
                        boolPoints = true;
                    }
                }
                strPuzzle = strPuzzleOptimised;
                strPuzzleOptimised = "";

                for (int i = strPuzzle.Length - 1; i >= 0; i--)
                {
                    if (strPuzzle[i] != '.')
                    {
                        strPuzzleOptimised = strPuzzle.Substring(0, i + 1);
                        break;
                    }
                }

                lngTotalArragements += P12_HowManyFit(strPuzzleOptimised + "|" + strInstructions);
            }

            return lngTotalArragements;
        }

        private long P12_HowManyFit(string strInput)
        {
            long lngFit = 0;
            //Refitted to use caching

            //removing any . at the start
            for (int i = 0; i < strInput.Length; i++)
            {
                if (strInput[i] != '.')
                {
                    strInput = strInput.Substring(i);
                    break;
                }
            }

            //if what we want isn't cached, we comput what we can
            if (dictPuzzle12Cache.ContainsKey(strInput))
            {
                //that one was in the cache
                lngFit = dictPuzzle12Cache[strInput];
            }
            else
            {
                //we compute it with recalling ourself
                //we need to split the instruction and puzzle
                string[] strArraySplit = strInput.Split("|");
                string strPuzzle = strArraySplit[0];

                strArraySplit = strArraySplit[1].Split(",");
                List<long> lstInstruction = new List<long>();
                for (int i = 0; i < strArraySplit.Length; i++)
                {
                    lstInstruction.Add(long.Parse(strArraySplit[i]));
                }

                //self calling function that will reduce possibility by 1 each time by sets of number etc
                if (strPuzzle.Length >= lstInstruction.Sum() + (lstInstruction.Count - 1))
                {
                    //number one it might be possible
                    //we calculate 

                    //if the first char is a wild, we recall the function with less string
                    //if the first char is # then we try to fit it direct
                    //I already filtered any other possiblity above.

                    //function is recursive and we cache sub-result for faster calculating

                    if (strPuzzle[0] == '?') lngFit += P12_HowManyFit(strInput.Substring(1)); //it was a wild, so maybe if shorter we have things (we didn'T change instruction we we can use input)

                    //now we check if what we have fitted, if not the subfunction above will cover for it
                    for (int i = 0; i < strPuzzle.Length; i++)
                    {
                        if (strPuzzle[i] == '.')
                        {
                            break;
                        } //didn't fit

                        if (lstInstruction[0] == (i + 1))
                        {
                            //it went in !
                            if (lstInstruction.Count == 1)
                            {
                                //that was the last number to fit, we make sure there is no more damage on the row
                                if (strPuzzle.Substring(i + 1).Contains('#') == false) lngFit++; //we are happy 
                            }
                            else
                            {
                                //If next character is an #, it doesn't fit (need a . or ? after), Do Nothing
                                //if its does fit and its valid, we recall the function with less instruction

                                if (strInput[i + 1] != '#')
                                {
                                    //that fitting work
                                    //let remove the instruction in the string
                                    string strInsruction = lstInstruction[0].ToString() + ",";

                                    string strLeft = strPuzzle.Substring(i + 2);
                                    strLeft += "|" + strInput.Substring(strInput.IndexOf(strInsruction, 0) + strInsruction.Length);

                                    lngFit += P12_HowManyFit(strLeft);
                                }
                                //we now re-call this method with less space/numbers
                            }
                            break;
                        }
                    }
                }

                //we cache what we had for future easier computing
                if (dictPuzzle12Cache.ContainsKey(strInput) == false) dictPuzzle12Cache.Add(strInput, lngFit);
            } //was it cached

            return lngFit;
        }


        private long Puzzle12_PartTwo()
        {
            string strInput12 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput12.Split("\r\n");

            long lngTotalArragements = 0;

            dictPuzzle12Cache = new Dictionary<string, long>();
            long lngTotalLine = 0;
            foreach (string strGames in allString)
            {
                lngTotalLine++;

                string[] strPuzzleLine = strGames.Split(" ");
                //0 = line to parse with good/bad/unknown
                //right = nonogram instruction

                string[] strInstruction = strPuzzleLine[1].Split(",");
                List<long> lstInstruction = new List<long>();

                string strFoldPuzzle = strPuzzleLine[0];
                string strFoldInstruction = strPuzzleLine[1];
                List<long> lstDefolded = new List<long>();

                for (int i = 0; i < 4; i++)
                {
                    strFoldPuzzle += "?" + strPuzzleLine[0];
                    strFoldInstruction += "," + strPuzzleLine[1];
                }


                //Now we do a little cleanup optimisation (to help the cache)
                //multiple dot together is useless
                //starting and ending dots are useless
                string strPuzzleOptimised = "";
                bool boolPoints = true; //for removing duplicate  (aka .... will become .)
                for (int i = 0; i < strFoldPuzzle.Length; i++)
                {
                    if (strFoldPuzzle[i] != '.')
                    {
                        strPuzzleOptimised += strFoldPuzzle[i];
                        boolPoints = false;
                    }
                    else if (boolPoints == false)
                    {
                        //we do keep the first instance of .
                        strPuzzleOptimised += strFoldPuzzle[i];
                        boolPoints = true;
                    }
                }
                strFoldPuzzle = strPuzzleOptimised;
                strPuzzleOptimised = "";

                for (int i = strFoldPuzzle.Length - 1; i >= 0; i--)
                {
                    if (strFoldPuzzle[i] != '.')
                    {
                        strPuzzleOptimised = strFoldPuzzle.Substring(0, i + 1);
                        break;
                    }
                }

                //doing it lazy way, we add 4 more
                Debug.WriteLine("Line " + lngTotalLine.ToString());
                lngTotalArragements += P12_HowManyFit(strPuzzleOptimised + "|" + strFoldInstruction);
            }

            return lngTotalArragements;

        }

        private void bntRun12_Click(object sender, EventArgs e)
        {
            long lngSolution12P1 = Puzzle12_PartOne();
            txt_output12P1.Text = lngSolution12P1.ToString();

            long lngSolution12P2 = Puzzle12_PartTwo();
            txt_output12P2.Text = lngSolution12P2.ToString();
        }

        #endregion

        #region Puzzle 13
        private long Puzzle13_PartOne()
        {
            string strInput13 = Advent23.Properties.Settings.Default.Puzzle_Input;
            long lnmgTotalPoint = 0;

            string[] allString = strInput13.Split("\r\n\r\n");

            foreach (string strPattern in allString)
            {
                string[] strAllLines;
                strAllLines = strPattern.Split("\r\n");
                //now we read all, and we will create two array and store it differently

                //we do until we match then we count where we were.
                int intRow = -1;

                bool intRowMirror = false;

                char[,] chCol = new char[strAllLines[0].Length, strAllLines.Length];
                char[,] chRow = new char[strAllLines.Length, strAllLines[0].Length];

                for (int a = 0; a < strAllLines[0].Length; a++)
                {
                    for (int b = 0; b < strAllLines.Length; b++)
                    {
                        chCol[a, b] = strAllLines[b][a];
                        chRow[b, a] = strAllLines[b][a];
                    }
                }

                //we go simple to identify them
                string strOldRow = "                                             "; //random high so not OOB
                foreach (string strCurrentRow in strAllLines)
                {
                    intRow++;

                    int intDifferent = 0;
                    for (int i = 0; i < strCurrentRow.Length; i++)
                    {
                        if (strCurrentRow[i] != strOldRow[i]) intDifferent++;
                    }

                    if (intDifferent == 0)
                    {
                        if (Puzzle13VerifyFullMirror(chRow, intRow, strCurrentRow.Length) == true)
                        {
                            //we got our row symetry
                            lnmgTotalPoint += (intRow * 100);
                            intRowMirror = true;
                            break;
                        }
                    }
                    else
                    {
                        strOldRow = strCurrentRow;
                    }
                }

                strOldRow = "";
                for (int a = 1; a < strAllLines[0].Length; a++)
                {

                    int intDifferent = 0;
                    for (int b = 0; b < strAllLines.Length; b++)
                    {
                        if (strAllLines[b][a] != strAllLines[b][a - 1]) intDifferent++;
                    }

                    if (intDifferent == 0)
                    {
                        if (Puzzle13VerifyFullMirror(chCol, a, strAllLines.Length) == true)
                        {
                            //we got our row symetry
                            lnmgTotalPoint += a;
                            break;
                        }
                    }
                }

            }

            return lnmgTotalPoint;
        }

        private bool Puzzle13VerifyFullMirror(char[,] chArray, int intToCheck, int intLenght, int intRequiredError = 0)
        {
            //on the array [X,Y] X = 'right' matched so [X,Y] = [X-1,Y]


            //we always want all Y to match
            //we loop until we either get a mistmatch (return null)
            //or we get out of bound

            //we already verified the row, so we adjust
            int intRow1 = intToCheck - 1;
            int intRow2 = intToCheck;
            int intMaxRow = (int)(chArray.Length / intLenght);

            int intErrorCount = 0;
            bool boolMirror = true;
            while (intRow1 >= 0 && intRow2 < intMaxRow)
            {
                for (int Y = 0; Y < intLenght; Y++)
                {
                    if (chArray[intRow1, Y] != chArray[intRow2, Y]) intErrorCount++;                        //does not match

                    if (intErrorCount > intRequiredError)
                    {
                        boolMirror = false; break;
                    }
                }

                intRow1--;
                intRow2++;
            }

            if (intErrorCount != intRequiredError) boolMirror = false; //we also need exactly X change

            return boolMirror;

        }


        private long Puzzle13_PartTwo()
        {
            //43054
            //45619
            //32854
            string strInput13 = Advent23.Properties.Settings.Default.Puzzle_Input;
            long lnmgTotalPoint = 0;

            string[] allString = strInput13.Split("\r\n\r\n");
            foreach (string strPattern in allString)
            {
                string[] strAllLines;
                strAllLines = strPattern.Split("\r\n");
                //now we read all, and we will create two array and store it differently

                //we do until we match then we count where we were.
                int intRow = -1;

                bool intRowMirror = false;

                char[,] chCol = new char[strAllLines[0].Length, strAllLines.Length];
                char[,] chRow = new char[strAllLines.Length, strAllLines[0].Length];

                for (int a = 0; a < strAllLines[0].Length; a++)
                {
                    for (int b = 0; b < strAllLines.Length; b++)
                    {
                        chCol[a, b] = strAllLines[b][a];
                        chRow[b, a] = strAllLines[b][a];
                    }
                }

                //we go simple to identify them
                string strOldRow = "                                             "; //random high so not OOB
                foreach (string strCurrentRow in strAllLines)
                {
                    intRow++;

                    int intDifferent = 0;
                    for (int i = 0; i < strCurrentRow.Length; i++)
                    {
                        if (strCurrentRow[i] != strOldRow[i]) intDifferent++;
                    }

                    if (intDifferent <= 1)
                    {
                        if (Puzzle13VerifyFullMirror(chRow, intRow, strCurrentRow.Length, 1) == true)
                        {
                            //we got our row symetry
                            lnmgTotalPoint += (intRow * 100);
                            intRowMirror = true;
                            break;
                        }
                    }
                    else
                    {
                        strOldRow = strCurrentRow;
                    }
                }

                if (intRowMirror == false)
                {
                    strOldRow = "";
                    for (int a = 1; a < strAllLines[0].Length; a++)
                    {

                        int intDifferent = 0;
                        for (int b = 0; b < strAllLines.Length; b++)
                        {
                            if (strAllLines[b][a] != strAllLines[b][a - 1]) intDifferent++;
                        }

                        if (intDifferent <= 1)
                        {
                            if (Puzzle13VerifyFullMirror(chCol, a, strAllLines.Length, 1) == true)
                            {
                                //we got our row symetry
                                lnmgTotalPoint += a;
                                break;
                            }
                        }
                    }
                }
            }

            return lnmgTotalPoint;
        }

        private void bntRun13_Click(object sender, EventArgs e)
        {
            long lngSolution13P1 = Puzzle13_PartOne();
            txt_output13P1.Text = lngSolution13P1.ToString();

            long lngSolution13P2 = Puzzle13_PartTwo();
            txt_output13P2.Text = lngSolution13P2.ToString();
        }

        #endregion

        #region Puzzle 14
        private long Puzzle14_PartOne()
        {
            //Grid is 140x140 
            char[,] charMap = new char[100, 100]; //to make it easy we pad around
            string strInput14 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput14.Split("\r\n");

            long lngLoad = 0;

            int intX = -1;
            int intY = 0;

            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    charMap[intX, intY++] = chData;
                }
            }

            charMap = P14Tilt("NORTH", charMap, 100);

            for (intX = 0; intX < 100; intX++)
            {
                for (intY = 0; intY < 100; intY++)
                {
                    if (charMap[intX, intY] == 'O') lngLoad += (100 - intX);
                }
            }

            return lngLoad;
        }

        public char[,] p14chMap;
        private long Puzzle14_PartTwo()
        {
            //Grid is 140x140 
            char[,] charMap = new char[100, 100]; //to make it easy we pad around
            string strInput14 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput14.Split("\r\n");

            long lngLoad = 0;

            int intX = -1;
            int intY = 0;

            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    charMap[intX, intY++] = chData;
                }
            }

            long[] lngSample = new long[10000];
            //we take a sample
            for (int x = 0; x < 10000; x++)
            {
                charMap = P14Tilt("NORTH", charMap, 100);
                charMap = P14Tilt("WEST", charMap, 100);
                charMap = P14Tilt("SOUTH", charMap, 100);
                charMap = P14Tilt("EAST", charMap, 100);

                lngLoad = 0;
                for (intX = 0; intX < 100; intX++)
                {
                    for (intY = 0; intY < 100; intY++)
                    {
                        if (charMap[intX, intY] == 'O') lngLoad += (100 - intX);
                    }
                }
                lngSample[x] = lngLoad;
            }

            //we got our sample, and there is a pattern in the numbers, we will fake calculate the final entry at 1000000000
            Dictionary<long, long> dictRecurrence = new Dictionary<long, long>();
            int intReccurenceStart = 0;
            List<long> lngReccurence = new List<long>();

            for (int i = 9000; i < 10000; i++)
            {
                if (!dictRecurrence.ContainsKey(lngSample[i]))
                {
                    dictRecurrence.Add(lngSample[i], 1);
                    lngReccurence.Add(lngSample[i]);
                }
                else
                {
                    intReccurenceStart = i;
                    break;
                }

            }
            lngLoad = lngReccurence[(999999999 - intReccurenceStart) % dictRecurrence.Count()];


            return lngLoad;
        }
        private char[,] P14Tilt(string strDirection, char[,] chGrid, int intSize = 100)
        {
            //The different is when we start
            //and the direction, but logic is the same
            //We will treat north/south as the same and east/west as same
            //with modifyer if we go 100 or 1

            int intModX = 0;
            int intModY = 0;
            int intDirect = 0;

            int intStartX = 0;
            int intStartY = 0;

            switch (strDirection)
            {
                case "NORTH":
                    intStartX = 1;
                    intStartY = 0;
                    intModX = 1;
                    intModY = 1;
                    intDirect = -1;
                    break;
                case "SOUTH":
                    intStartX = intSize - 2;
                    intStartY = 0;
                    intModX = -1;
                    intModY = 1;
                    intDirect = 1;
                    break;

                case "WEST":
                    intStartX = 0;
                    intStartY = 1;
                    intModX = 1;
                    intModY = 1;
                    intDirect = -1;
                    break;
                case "EAST":
                    intStartX = 0;
                    intStartY = intSize - 2;
                    intModX = 1;
                    intModY = -1;
                    intDirect = 1;
                    break;
            }



            for (int x = intStartX; x < intSize && x >= 0; x = x + intModX)
            {
                for (int y = intStartY; y < intSize && y >= 0; y = y + intModY)
                {
                    //if we hit a 0, we scan above and place it on first empty spot
                    if (chGrid[x, y] == 'O')
                    {
                        int z = 0;
                        //we play on X axis depending if we go up or down (defined above)
                        if (strDirection == "NORTH" || strDirection == "SOUTH")
                        {
                            for (z = x + intDirect; z >= 0 && z < intSize; z = z + intDirect)
                            {
                                if (chGrid[z, y] != '.') break;
                            }

                            //we hit a blocker, we stop
                            z = z - intDirect;
                            if (z != x)
                            {
                                //we swap
                                chGrid[z, y] = 'O';
                                chGrid[x, y] = '.';
                            }
                        }
                        else if (strDirection == "WEST" || strDirection == "EAST")
                        {
                            for (z = y + intDirect; z >= 0 && z < intSize; z = z + intDirect)
                            {
                                if (chGrid[x, z] != '.') break;
                            }

                            //we hit a blocker, we stop
                            z = z - intDirect;
                            if (z != y)
                            {
                                //we swap
                                chGrid[x, z] = 'O';
                                chGrid[x, y] = '.';
                            }
                        }

                    }
                }
            }
            return chGrid;


        }

        private void bntRun14_Click(object sender, EventArgs e)
        {
            long lngSolution14P1 = Puzzle14_PartOne();
            txt_output14P1.Text = lngSolution14P1.ToString();

            long lngSolution14P2 = Puzzle14_PartTwo();
            txt_output14P2.Text = lngSolution14P2.ToString();
        }

        #endregion

        #region Puzzle 15
        private long Puzzle15_PartOne()
        {


            //
            string strInput15 = Advent23.Properties.Settings.Default.Puzzle_Input;



            string[] allString = strInput15.Split(",");

            long lnmgTotalPoint = 0;
            long lngCurrentHash = 0;
            foreach (string strInstruction in allString)
            {
                lngCurrentHash = 0;
                long asciValue = 0;
                foreach (char chChar in strInstruction)
                {
                    asciValue = ((byte)chChar);

                    lngCurrentHash += asciValue;
                    lngCurrentHash *= 17;
                    lngCurrentHash = lngCurrentHash % 256;
                }
                lnmgTotalPoint += lngCurrentHash;


            }


            return lnmgTotalPoint;
        }
        private long Puzzle15_PartTwo()
        {

            string strInput15 = Advent23.Properties.Settings.Default.Puzzle_Input;

            long lngFinalPower = 0;
            List<string>[] lstBoxes = new List<string>[256];
            List<int>[] lstNums = new List<int>[256];

            //init all the list
            for (int i = 0; i < 256; i++)
            {
                lstBoxes[i] = new List<string>();
                lstNums[i] = new List<int>();
            }

            string[] allString = strInput15.Split(",");

            long lngBoxNum = 0;
            string strHASH = "";
            string strNumber = "";
            char chCommand = '\0';
            int intSpot = 0;

            foreach (string strInstruction in allString)
            {
                if (strInstruction.Contains('-') == true)
                {
                    chCommand = '-';
                    strHASH = strInstruction.Substring(0, strInstruction.IndexOf('-'));
                }
                if (strInstruction.Contains('=') == true)
                {
                    chCommand = '=';
                    strHASH = strInstruction.Substring(0, strInstruction.IndexOf('='));
                    strNumber = strInstruction.Substring(strInstruction.IndexOf('=') + 1);
                }

                lngBoxNum = P15_Hash(strHASH);


                switch (chCommand)
                {
                    case '-':
                        if (lstBoxes[lngBoxNum].Contains(strHASH) == true)
                        {
                            //we remove it (and the number) on their 'spot'
                            intSpot = lstBoxes[lngBoxNum].IndexOf(strHASH);
                            lstBoxes[lngBoxNum].RemoveAt(intSpot);
                            lstNums[lngBoxNum].RemoveAt(intSpot);
                        }
                        break;

                    case '=':
                        if (lstBoxes[lngBoxNum].Contains(strHASH) == true)
                        {
                            //we remove it (and the number) on their 'spot'
                            intSpot = lstBoxes[lngBoxNum].IndexOf(strHASH);
                            lstNums[lngBoxNum][intSpot] = int.Parse(strNumber);
                        }
                        else
                        {
                            lstBoxes[lngBoxNum].Add(strHASH);
                            lstNums[lngBoxNum].Add(int.Parse(strNumber));
                        }
                        break;

                }
            }


            lngFinalPower = 0;
            int intPosition = 0;
            //for each boxes
            for (int i = 0; i < 256; i++)
            {
                intPosition = 0;
                foreach (long lngFocal in lstNums[i])
                {
                    intPosition++;
                    lngFinalPower += ((i + 1) * intPosition * lngFocal);
                }
            }

            return lngFinalPower;
        }

        private long P15_Hash(string strHASH)
        {
            long lngCurrentHash = 0;

            lngCurrentHash = 0;
            long asciValue = 0;
            foreach (char chChar in strHASH)
            {
                asciValue = ((byte)chChar);

                lngCurrentHash += asciValue;
                lngCurrentHash *= 17;
                lngCurrentHash = lngCurrentHash % 256;
            }
            return lngCurrentHash;
        }

        private void bntRun15_Click(object sender, EventArgs e)
        {
            long lngSolution15P1 = Puzzle15_PartOne();
            txt_output15P1.Text = lngSolution15P1.ToString();

            long lngSolution15P2 = Puzzle15_PartTwo();
            txt_output15P2.Text = lngSolution15P2.ToString();
        }

        #endregion

        #region Puzzle 16
        private long Puzzle16_PartOne()
        {

            //Grid side X,Y
            int intGridSize = 110;
            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            string strInput14 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput14.Split("\r\n");

            long lngLoad = 0;

            int intX = -1;
            int intY = 0;

            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    chrGrid[intX, intY++] = chData;
                }
            }

            char[,] chrGridEnergised = new char[intGridSize, intGridSize]; //to make it easy we pad around
            Queue<Tuple<int, int, char>> quCoordinate = new Queue<Tuple<int, int, char>>();


            Tuple<int, int, char> tpNewData = new Tuple<int, int, char>(0, -1, 'R');
            Tuple<int, int, char> tpCurrent = new Tuple<int, int, char>(0, -1, 'R');


            quCoordinate.Enqueue(tpNewData);
            Dictionary<string, string> dictCached = new Dictionary<string, string>();
            string strChacheID = "0,-1,R";
            dictCached.Add(strChacheID, strChacheID);

            char charDirection = 'R';
            char charCurrentPos = '.';

            int intDirectionX = 0;
            int intDirectionY = 0;

            while (quCoordinate.Count > 0)
            {
                tpCurrent = quCoordinate.Dequeue();
                //we now do the full of this coordinate, if we split we check if it was already done (if not) then we add it in queue and 'done'
                intX = tpCurrent.Item1;
                intY = tpCurrent.Item2;
                charDirection = tpCurrent.Item3;


                while (true)
                {
                    //we are in the grid, we continue until we get 'off' the grid
                    switch (charDirection)
                    {
                        case 'U': //up
                            intDirectionX = -1;
                            intDirectionY = 0;
                            break;
                        case 'D': //down
                            intDirectionX = 1;
                            intDirectionY = 0;
                            break;
                        case 'L': //left
                            intDirectionX = 0;
                            intDirectionY = -1;
                            break;
                        case 'R': //right
                            intDirectionX = 0;
                            intDirectionY = 1;
                            break;
                    }

                    intX += intDirectionX;
                    intY += intDirectionY;

                    if (intX < 0 || intY < 0 || intX >= intGridSize || intY >= intGridSize) break; // we are now OOB

                    //we energise that grid (maybe already done, don'T carE)
                    chrGridEnergised[intX, intY] = '1';
                    charCurrentPos = chrGrid[intX, intY];

                    strChacheID = (intX).ToString() + "," + (intY).ToString() + "," + charDirection;
                    if (dictCached.ContainsKey(strChacheID) == false)
                    {
                        dictCached.Add(strChacheID, strChacheID);
                    }
                    else
                    {
                        //we have been here, we breach out 
                        break;
                    }

                    if (charCurrentPos != '.')
                    {
                        if (charCurrentPos == '-' && (charDirection == 'U' || charDirection == 'D'))
                        {
                            //we split it, current thread go left, and we queue right
                            tpNewData = new Tuple<int, int, char>(intX, intY, 'R');
                            quCoordinate.Enqueue(tpNewData);
                            strChacheID = (intX).ToString() + "," + (intY).ToString() + ",R";
                            if (dictCached.ContainsKey(strChacheID) == false) dictCached.Add(strChacheID, strChacheID);

                            //now we change our direction to down
                            charDirection = 'L';
                        }
                        else if (charCurrentPos == '|' && (charDirection == 'L' || charDirection == 'R'))
                        {
                            //we split it, and we go up, and continu this one right
                            tpNewData = new Tuple<int, int, char>(intX, intY, 'U');
                            quCoordinate.Enqueue(tpNewData);
                            strChacheID = (intX).ToString() + "," + intY.ToString() + ",U";
                            if (dictCached.ContainsKey(strChacheID) == false) dictCached.Add(strChacheID, strChacheID);

                            //now we change our direction to down
                            charDirection = 'D';
                        }
                        else if (charCurrentPos == '\\')
                        {
                            //no split but we are changing direction depending on our current direction
                            switch (charDirection)
                            {
                                case 'U': //up
                                    charDirection = 'L';
                                    break;
                                case 'D': //down
                                    charDirection = 'R';
                                    break;
                                case 'L': //left
                                    charDirection = 'U';
                                    break;
                                case 'R': //right
                                    charDirection = 'D';
                                    break;
                            }
                        }
                        else if (charCurrentPos == '/')
                        {
                            //no split but we are changing direction depending on our current direction
                            switch (charDirection)
                            {
                                case 'U': //up
                                    charDirection = 'R';
                                    break;
                                case 'D': //down
                                    charDirection = 'L';
                                    break;
                                case 'L': //left
                                    charDirection = 'D';
                                    break;
                                case 'R': //right
                                    charDirection = 'U';
                                    break;
                            }
                        }
                    }
                }
            }


            lngLoad = 0;
            for (int a = 0; a < intGridSize; a++)
            {
                for (int b = 0; b < intGridSize; b++)
                {
                    if (chrGridEnergised[a, b] == '1') lngLoad++;
                }
            }

            return lngLoad;

        }
        private long Puzzle16_PartTwo()
        {
            //I'm 'cheezing' this one, just re-use the code I did for P1 and instead queue a different start, for all possible ways

            //Grid side X,Y
            int intGridSize = 110;
            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            string strInput14 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput14.Split("\r\n");

            long lngLoad = 0;

            int intX = -1;
            int intY = 0;

            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    chrGrid[intX, intY++] = chData;
                }
            }

            Tuple<int, int, char> tpNewData = new Tuple<int, int, char>(0, -1, 'R');
            Tuple<int, int, char> tpCurrent = new Tuple<int, int, char>(0, -1, 'R');

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < intGridSize; y++)
                {
                    char[,] chrGridEnergised = new char[intGridSize, intGridSize]; //to make it easy we pad around

                    Queue<Tuple<int, int, char>> quCoordinate = new Queue<Tuple<int, int, char>>();

                    //if X = 0 , then we change on X, otherwise we start on Y
                    string strChacheID = "";
                    char charDirection = 'R';
                    char charCurrentPos = '.';

                    int intDirectionX = 0;
                    int intDirectionY = 0;

                    switch (x)
                    {
                        case 0: //up
                            strChacheID = "-1," + y.ToString() + ",D";
                            tpNewData = new Tuple<int, int, char>(-1, y, 'D');
                            break;
                        case 1: //down
                            strChacheID = intGridSize.ToString() + y.ToString() + "," + ",U";
                            tpNewData = new Tuple<int, int, char>(intGridSize, y, 'U');
                            break;
                        case 2: //left
                            strChacheID = y.ToString() + ",-1" + ",R";
                            tpNewData = new Tuple<int, int, char>(y, -1, 'R');
                            break;
                        case 3: //right
                            strChacheID = y.ToString() + "," + intGridSize.ToString() + ",L";
                            tpNewData = new Tuple<int, int, char>(y, intGridSize, 'L');
                            break;
                    }

                    quCoordinate.Enqueue(tpNewData);
                    Dictionary<string, string> dictCached = new Dictionary<string, string>();
                    dictCached.Add(strChacheID, strChacheID);

                    while (quCoordinate.Count > 0)
                    {
                        tpCurrent = quCoordinate.Dequeue();
                        //we now do the full of this coordinate, if we split we check if it was already done (if not) then we add it in queue and 'done'
                        intX = tpCurrent.Item1;
                        intY = tpCurrent.Item2;
                        charDirection = tpCurrent.Item3;


                        while (true)
                        {
                            //we are in the grid, we continue until we get 'off' the grid
                            switch (charDirection)
                            {
                                case 'U': //up
                                    intDirectionX = -1;
                                    intDirectionY = 0;
                                    break;
                                case 'D': //down
                                    intDirectionX = 1;
                                    intDirectionY = 0;
                                    break;
                                case 'L': //left
                                    intDirectionX = 0;
                                    intDirectionY = -1;
                                    break;
                                case 'R': //right
                                    intDirectionX = 0;
                                    intDirectionY = 1;
                                    break;
                            }

                            intX += intDirectionX;
                            intY += intDirectionY;

                            if (intX < 0 || intY < 0 || intX >= intGridSize || intY >= intGridSize) break; // we are now OOB

                            //we energise that grid (maybe already done, don'T carE)
                            chrGridEnergised[intX, intY] = '1';
                            charCurrentPos = chrGrid[intX, intY];

                            strChacheID = (intX).ToString() + "," + (intY).ToString() + "," + charDirection;
                            if (dictCached.ContainsKey(strChacheID) == false)
                            {
                                dictCached.Add(strChacheID, strChacheID);
                            }
                            else
                            {
                                //we have been here, we breach out 
                                break;
                            }

                            if (charCurrentPos != '.')
                            {
                                if (charCurrentPos == '-' && (charDirection == 'U' || charDirection == 'D'))
                                {
                                    //we split it, current thread go left, and we queue right
                                    tpNewData = new Tuple<int, int, char>(intX, intY, 'R');
                                    quCoordinate.Enqueue(tpNewData);
                                    strChacheID = (intX).ToString() + "," + (intY).ToString() + ",R";
                                    if (dictCached.ContainsKey(strChacheID) == false) dictCached.Add(strChacheID, strChacheID);

                                    //now we change our direction to down
                                    charDirection = 'L';
                                }
                                else if (charCurrentPos == '|' && (charDirection == 'L' || charDirection == 'R'))
                                {
                                    //we split it, and we go up, and continu this one right
                                    tpNewData = new Tuple<int, int, char>(intX, intY, 'U');
                                    quCoordinate.Enqueue(tpNewData);
                                    strChacheID = (intX).ToString() + "," + intY.ToString() + ",U";
                                    if (dictCached.ContainsKey(strChacheID) == false) dictCached.Add(strChacheID, strChacheID);

                                    //now we change our direction to down
                                    charDirection = 'D';
                                }
                                else if (charCurrentPos == '\\')
                                {
                                    //no split but we are changing direction depending on our current direction
                                    switch (charDirection)
                                    {
                                        case 'U': //up
                                            charDirection = 'L';
                                            break;
                                        case 'D': //down
                                            charDirection = 'R';
                                            break;
                                        case 'L': //left
                                            charDirection = 'U';
                                            break;
                                        case 'R': //right
                                            charDirection = 'D';
                                            break;
                                    }
                                }
                                else if (charCurrentPos == '/')
                                {
                                    //no split but we are changing direction depending on our current direction
                                    switch (charDirection)
                                    {
                                        case 'U': //up
                                            charDirection = 'R';
                                            break;
                                        case 'D': //down
                                            charDirection = 'L';
                                            break;
                                        case 'L': //left
                                            charDirection = 'D';
                                            break;
                                        case 'R': //right
                                            charDirection = 'U';
                                            break;
                                    }
                                }
                            }
                        }
                    }


                    long lngTemp = 0;
                    for (int a = 0; a < intGridSize; a++)
                    {
                        for (int b = 0; b < intGridSize; b++)
                        {
                            if (chrGridEnergised[a, b] == '1') lngTemp++;
                        }
                    }

                    if (lngLoad < lngTemp) lngLoad = lngTemp;
                }
            }

            return lngLoad;
        }

        private void bntRun16_Click(object sender, EventArgs e)
        {
            long lngSolution16P1 = Puzzle16_PartOne();
            txt_output16P1.Text = lngSolution16P1.ToString();

            long lngSolution16P2 = Puzzle16_PartTwo();
            txt_output16P2.Text = lngSolution16P2.ToString();
        }

        #endregion

        #region Puzzle 17
        private long Puzzle17_PartOne()
        {
            return 0;
            string strInput17 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput17.Split("\r\n");
            long lngAnswer = 1023;

            //If Array
            //1331
            //1028
            //1036
            //1023
            //1018
            //1013

            int intGridSize = 141;
            int inxMaxHeat = 1050;
            int intX = -1;
            int intY = 0;
            Dictionary<string, int> dictOptimisedCache = new Dictionary<string, int>();
            int[,] intGrid = new int[intGridSize, intGridSize]; //to make it easy we pad around
            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    dictOptimisedCache.Add(intX.ToString() + "," + intY.ToString() + ",V", inxMaxHeat);
                    dictOptimisedCache.Add(intX.ToString() + "," + intY.ToString() + ",H", inxMaxHeat);
                    intGrid[intX, intY++] = int.Parse(chData.ToString());

                }
            }


            lngAnswer = 99999999999999; //way too long/big, we will go with 'lowest' valid path
            //this is a weird puzzle

            //all 4 below are 'linked' by logic, we add/remove on same position
            //it will contain multiple paths




            Stack<int> quHeat = new Stack<int>();
            Stack<char> quHeading = new Stack<char>();
            Stack<int> quPathX = new Stack<int>();
            Stack<int> quPathY = new Stack<int>();

            //starting possibilities branch
            quHeat.Push(0);
            quHeading.Push('V'); //if you go right, then you can only go down or up
            quPathX.Push(0);
            quPathY.Push(0);

            quHeat.Push(0);
            quHeading.Push('H'); //if you go down, then you can only go left or right
            quPathX.Push(0);
            quPathY.Push(0);

            while (quHeat.Count > 0)
            {
                int intCurrentHeat = quHeat.Pop();
                char aHeading = quHeading.Pop();
                int X = quPathX.Pop();
                int Y = quPathY.Pop();

                if (lngAnswer > intCurrentHeat) //useless to continue this one we already have a faster solution
                {
                    //now we got what was on top of the list
                    //if we are at the bottom right, we done
                    if (X == (intGridSize - 1) && Y == (intGridSize - 1))
                    {
                        lngAnswer = intCurrentHeat;
                        inxMaxHeat = intCurrentHeat;
                    }
                    else
                    {
                        //Now for this path, we will create multiple possibility
                        //we need to go left or right (2) and move , 1 , 2 or 3 , so 6 possibilities
                        //we execute them and queue them in the list, and we loop,
                        if (aHeading == 'V')
                        {
                            //we can go LEFT or RIGHT
                            char chGoingHeading = 'H';
                            for (int b = -1; b < 2; b = b + 2)
                            {
                                int intNewHeat = intCurrentHeat;
                                int intModY = 0;

                                for (int i = 1; i < 4; i++)
                                {
                                    intModY = (i * b) + Y;

                                    if (intModY < 0 || intModY >= intGridSize) break; //OOB
                                    string strGoingLocation = X.ToString() + "," + intModY.ToString() + "," + chGoingHeading;

                                    intNewHeat += intGrid[X, intModY];
                                    if (dictOptimisedCache[strGoingLocation] > intNewHeat)
                                    {
                                        dictOptimisedCache[strGoingLocation] = intNewHeat;
                                        quHeat.Push(intNewHeat);
                                        quPathX.Push(X);
                                        quPathY.Push(intModY);
                                        quHeading.Push(chGoingHeading);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //we can go UP or DOWN
                            char chGoingHeading = 'V';
                            for (int b = -1; b < 2; b = b + 2)
                            {
                                int intNewHeat = intCurrentHeat;
                                int intModX = 0;

                                for (int i = 1; i < 4; i++)
                                {
                                    intModX = (i * b) + X;

                                    if (intModX < 0 || intModX >= intGridSize) break; //OOB
                                    string strGoingLocation = intModX.ToString() + "," + Y.ToString() + "," + chGoingHeading;

                                    intNewHeat += intGrid[intModX, Y];
                                    if (dictOptimisedCache[strGoingLocation] > intNewHeat)
                                    {
                                        //this seem shorted path to go there 'so far'
                                        dictOptimisedCache[strGoingLocation] = intNewHeat;
                                        quHeat.Push(intNewHeat);
                                        quPathX.Push(intModX);
                                        quPathY.Push(Y);
                                        quHeading.Push(chGoingHeading);
                                    }
                                }
                            }
                        }
                    }
                }
            }




            return lngAnswer;
        }


        private long Puzzle17_PartTwo()
        {
            string strInput17 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput17.Split("\r\n");
            long lngAnswer = 0;

            //If Array


            int intGridSize = 141;
            int inxMaxHeat = intGridSize * 50;
            int intX = -1;
            int intY = 0;
            Dictionary<string, int> dictOptimisedCache = new Dictionary<string, int>();
            int[,] intGrid = new int[intGridSize, intGridSize]; //to make it easy we pad around
            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    dictOptimisedCache.Add(intX.ToString() + "," + intY.ToString() + ",V", inxMaxHeat);
                    dictOptimisedCache.Add(intX.ToString() + "," + intY.ToString() + ",H", inxMaxHeat);
                    intGrid[intX, intY++] = int.Parse(chData.ToString());

                }
            }


            lngAnswer = 99999999999999; //way too long/big, we will go with 'lowest' valid path
            //this is a weird puzzle

            //all 4 below are 'linked' by logic, we add/remove on same position
            //it will contain multiple paths




            Stack<int> quHeat = new Stack<int>();
            Stack<char> quHeading = new Stack<char>();
            Stack<int> quPathX = new Stack<int>();
            Stack<int> quPathY = new Stack<int>();

            //starting possibilities branch
            quHeat.Push(0);
            quHeading.Push('V'); //if you go right, then you can only go down or up
            quPathX.Push(0);
            quPathY.Push(0);

            quHeat.Push(0);
            quHeading.Push('H'); //if you go down, then you can only go left or right
            quPathX.Push(0);
            quPathY.Push(0);

            while (quHeat.Count > 0)
            {
                int intCurrentHeat = quHeat.Pop();
                char aHeading = quHeading.Pop();
                int X = quPathX.Pop();
                int Y = quPathY.Pop();

                if (lngAnswer > intCurrentHeat) //useless to continue this one we already have a faster solution
                {
                    //now we got what was on top of the list
                    //if we are at the bottom right, we done
                    if (X == (intGridSize - 1) && Y == (intGridSize - 1))
                    {
                        lngAnswer = intCurrentHeat;
                        inxMaxHeat = intCurrentHeat;
                    }
                    else
                    {
                        //Now for this path, we will create multiple possibility
                        //we need to go left or right (2) and move , 1 , 2 or 3 , so 6 possibilities
                        //we execute them and queue them in the list, and we loop,
                        if (aHeading == 'V')
                        {
                            //we can go LEFT or RIGHT
                            char chGoingHeading = 'H';
                            for (int b = -1; b < 2; b = b + 2)
                            {
                                int intNewHeat = intCurrentHeat;
                                int intModY = 0;

                                for (int i = 1; i < 11; i++)
                                {
                                    intModY = (i * b) + Y;
                                    if (intModY < 0 || intModY >= intGridSize) break; //OOB
                                    intNewHeat += intGrid[X, intModY];

                                    if (i > 3) //we can only start to turn at 4
                                    {
                                        string strGoingLocation = X.ToString() + "," + intModY.ToString() + "," + chGoingHeading;


                                        if (dictOptimisedCache[strGoingLocation] > intNewHeat)
                                        {
                                            dictOptimisedCache[strGoingLocation] = intNewHeat;
                                            quHeat.Push(intNewHeat);
                                            quPathX.Push(X);
                                            quPathY.Push(intModY);
                                            quHeading.Push(chGoingHeading);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //we can go UP or DOWN
                            char chGoingHeading = 'V';
                            for (int b = -1; b < 2; b = b + 2)
                            {
                                int intNewHeat = intCurrentHeat;
                                int intModX = 0;

                                for (int i = 1; i < 11; i++)
                                {
                                    intModX = (i * b) + X;
                                    if (intModX < 0 || intModX >= intGridSize) break; //OOB
                                    intNewHeat += intGrid[intModX, Y];

                                    if (i > 3) //we can only start to turn at 4
                                    {
                                        string strGoingLocation = intModX.ToString() + "," + Y.ToString() + "," + chGoingHeading;
                                        if (dictOptimisedCache[strGoingLocation] > intNewHeat)
                                        {
                                            //this seem shorted path to go there 'so far'
                                            dictOptimisedCache[strGoingLocation] = intNewHeat;
                                            quHeat.Push(intNewHeat);
                                            quPathX.Push(intModX);
                                            quPathY.Push(Y);
                                            quHeading.Push(chGoingHeading);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }




            return lngAnswer;
        }

        private void bntRun17_Click(object sender, EventArgs e)
        {
            long lngSolution17P1 = Puzzle17_PartOne();
            txt_output17P1.Text = lngSolution17P1.ToString();

            long lngSolution17P2 = Puzzle17_PartTwo();
            txt_output17P2.Text = lngSolution17P2.ToString();
        }

        #endregion

        #region Puzzle 18
        private long Puzzle18_PartOne()
        {
            //string strInput18 = Advent23.Properties.Settings.Default.Puzzle_Example; int intGridSize = 100;
            string strInput18 = Advent23.Properties.Settings.Default.Puzzle_Input; int intGridSize = 1000;
            string[] allString = strInput18.Split("\r\n");
            long lngAnswer = 0;


            //If Array

            int intX = -1;
            int intY = 0;

            int startX = (int)Math.Floor((decimal)(intGridSize / 2));
            int startY = startX;

            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            for (int a = 0; a < intGridSize; a++)
            {
                for (int b = 0; b < intGridSize; b++)
                {
                    chrGrid[a, b] = ' ';
                }
            }
            chrGrid[startX, startY] = '#';


            char chDirection = ' ';
            int intDistance = 0;
            string strColor = "";
            int intModX = 0;
            int intModY = 0;
            int intCurrentPosX = startX;
            int intCurrentPosY = startY;

            int intLowestX = intGridSize;
            int intLowestY = intGridSize;
            int intHighestX = 0;
            int intHighestY = 0;

            Dictionary<string, string> dictColored = new Dictionary<string, string>();
            foreach (string strGames in allString)
            {
                string[] strInstruct = strGames.Split(" ");
                chDirection = strInstruct[0][0];
                intDistance = int.Parse(strInstruct[1]);
                strColor = strInstruct[2]; strColor = strColor.Substring(1, strColor.Length - 1);

                intModX = 0;
                intModY = 0;
                switch (chDirection)
                {
                    case 'U':
                        intModX = -1;
                        break;
                    case 'D':
                        intModX = 1;
                        break;
                    case 'L':
                        intModY = -1;
                        break;
                    case 'R':
                        intModY = 1;
                        break;
                }

                for (int i = 1; i < intDistance + 1; i++)
                {
                    intCurrentPosX += intModX;
                    intCurrentPosY += intModY;
                    dictColored.Add(intCurrentPosX.ToString() + "," + intCurrentPosY.ToString(), strColor);
                    chrGrid[intCurrentPosX, intCurrentPosY] = '#';
                }
                //we remember the grid size for later to check trench size
                if (intLowestX > intCurrentPosX) intLowestX = intCurrentPosX;
                if (intLowestY > intCurrentPosY) intLowestY = intCurrentPosY;
                if (intHighestX < intCurrentPosX) intHighestX = intCurrentPosX;
                if (intHighestY < intCurrentPosY) intHighestY = intCurrentPosY;
            }

            //now we floor from all the edge
            for (int a = intLowestX; a <= intHighestX; a++)
            {
                if (chrGrid[a, intHighestY] == ' ') chrGrid[a, intHighestY] = '0';
                if (chrGrid[a, intLowestY] == ' ') chrGrid[a, intLowestY] = '0';
            }
            for (int b = intLowestY; b <= intHighestY; b++)
            {
                if (chrGrid[intLowestX, b] == ' ') chrGrid[intLowestX, b] = '0';
                if (chrGrid[intHighestX, b] == ' ') chrGrid[intHighestX, b] = '0';
            }

            for (int i = 0; i < 5; i++)
            {
                for (int a = intLowestX; a <= intHighestX; a++)
                {
                    for (int b = intLowestY; b <= intHighestY; b++)
                    {
                        if (chrGrid[a, b] == ' ')
                        {
                            for (int c = -1; c <= 1; c++)
                            {
                                for (int d = -1; d <= 1; d++)
                                {
                                    if (chrGrid[a + c, b + d] == '0') chrGrid[a, b] = '0';
                                }
                            }
                        }
                    }
                }


                for (int b = intHighestY; b >= intLowestY; b--)
                {
                    for (int a = intHighestX; a >= intLowestX; a--)
                    {
                        if (chrGrid[a, b] != '#' && chrGrid[a, b] != '0')
                        {
                            for (int c = -1; c <= 1; c++)
                            {
                                for (int d = -1; d <= 1; d++)
                                {
                                    if (chrGrid[a + c, b + d] == '0') chrGrid[a, b] = '0';
                                }
                            }
                        }
                    }
                }

            }



            lngAnswer = 0;

            for (int a = intLowestX; a <= intHighestX; a++)
            {
                for (int b = intLowestY; b <= intHighestY; b++)
                {
                    if (chrGrid[a, b] != '0') lngAnswer++;
                }
            }


            return lngAnswer;
        }
        private string Puzzle18_PartTwo()
        {
            string strInput18 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput18.Split("\r\n");
            long lngAnswer = 0;
            //this one is ridiculous
            //Looking at math formulas... ahh Shoelace algo, there is a way... very simple I should have done that in Part1... oh well

            long intCurrentPosX = 0; long intNewPosX = 0;
            long intCurrentPosY = 0; long intNewPosY = 0; //I lost easily 1h because the PosX/PosY was integer (too small)
            char chDirection = ' ';
            string strHEX = "";
            long intNumDig = 0;

            foreach (string strGames in allString)
            {
                //let get the HEX instruction
                string[] strInstruct = strGames.Split(" ");
                strHEX = strInstruct[2];
                chDirection = strHEX[strHEX.Length - 2];
                strHEX = strHEX.Substring(2, strHEX.Length - 4);
                intNumDig = int.Parse(strHEX, System.Globalization.NumberStyles.HexNumber);

                switch (chDirection)
                {
                    case '3': //UP
                        intNewPosX = intCurrentPosX - intNumDig;
                        break;
                    case '1': //DOWN
                        intNewPosX = intCurrentPosX + intNumDig;
                        break;
                    case '2': //LEFT
                        intNewPosY = intCurrentPosY - intNumDig;
                        break;
                    case '0': //RIGHT
                        intNewPosY = intCurrentPosY + intNumDig;
                        break;
                }

                lngAnswer += ((intCurrentPosX + intNewPosX) * (intCurrentPosY - intNewPosY)) + intNumDig;

                //now we move move as we kept the line in memory
                intCurrentPosX = intNewPosX;
                intCurrentPosY = intNewPosY;
            }

            //Now we did all the Left<-->Right Lane and their height
            //We will go back and sort them by height and we traverse through them

            lngAnswer = (lngAnswer / 2);
            if (lngAnswer < 0) lngAnswer *= -1;
            lngAnswer += 1;

            return lngAnswer.ToString();
        }

        private void bntRun18_Click(object sender, EventArgs e)
        {
            long lngSolution18P1 = Puzzle18_PartOne();
            txt_output18P1.Text = lngSolution18P1.ToString();

            txt_output18P2.Text = Puzzle18_PartTwo();
        }

        #endregion

        #region Puzzle 19
        private long Puzzle19_PartOne()
        {
            string strInput19 = Advent23.Properties.Settings.Default.Puzzle_Input;
            //string strInput19 = Advent23.Properties.Settings.Default.Puzzle_Example;

            string[] allString = strInput19.Split("\r\n\r\n");
            long lngAnswer = 0;

            //we split the workflow first then instruction

            string[] strWorkflow = allString[0].Split("\r\n");
            string[] strInputs = allString[1].Split("\r\n");

            Dictionary<string, List<string>> dictFlows = new Dictionary<string, List<string>>();





            foreach (string aFlow in strWorkflow)
            {
                //first we get the caption of the workflow
                //then we split all instruction in bite size and we save them
                //we will call a function to determine Y/N on each instruction later

                string strFlowCaption = "";
                List<string> lstInstructions = new List<string>();
                string strInstructions = "";

                strFlowCaption = aFlow.Substring(0, aFlow.IndexOf("{"));
                strInstructions = aFlow.Substring(aFlow.IndexOf("{") + 1); strInstructions = strInstructions.Substring(0, strInstructions.Length - 1);

                string[] strInstSplit = strInstructions.Split(",");
                foreach (string strInst in strInstSplit)
                {
                    lstInstructions.Add(strInst);
                }
                dictFlows.Add(strFlowCaption, lstInstructions);
            }


            foreach (string strInput in strInputs)
            {
                //remove parenthesis, split then we have them
                string strInClean = strInput.Substring(1, strInput.Length - 2);
                string[] aInputClean = strInClean.Split(",");
                p19_Input aInput = new p19_Input();

                aInput.X = long.Parse(aInputClean[0].Substring(2));
                aInput.M = long.Parse(aInputClean[1].Substring(2));
                aInput.A = long.Parse(aInputClean[2].Substring(2));
                aInput.S = long.Parse(aInputClean[3].Substring(2));

                string strResponse = "in";
                List<string> lstInstructions;
                while (strResponse != "R" && strResponse != "A")
                {
                    // from the function we get either an A, R or another instruction set
                    lstInstructions = dictFlows[strResponse];
                    strResponse = P19_Validate(lstInstructions, aInput);
                }

                if (strResponse == "A") lngAnswer += aInput.sum();
            }

            return lngAnswer;
        }

        private string P19_Validate(List<string> lstInstructions, p19_Input pInput)
        {
            string strReturn = "R";
            string[] strEvaluate;
            bool boolEvaluate = false;

            //we go through each instruction and check if its pass/fail
            foreach (string strInstruction in lstInstructions)
            {
                //multiple things

                //direct to another flow or Accept/Refuse
                if (strInstruction == "A" || strInstruction == "R" || strInstruction.Contains(":") == false)
                {
                    strReturn = strInstruction;
                    break;
                }

                //then we evaluate a variable
                boolEvaluate = false;
                strEvaluate = strInstruction.Split(":");
                char chLetter = strEvaluate[0].ToUpper()[0];
                char chSign = strEvaluate[0][1];
                long lngValue = long.Parse(strEvaluate[0].Substring(2));

                //easier to check all 8 as one ligner
                if (boolEvaluate == false && chLetter == 'X' && chSign == '>' && pInput.X > lngValue) boolEvaluate = true;
                if (boolEvaluate == false && chLetter == 'X' && chSign == '<' && pInput.X < lngValue) boolEvaluate = true;
                if (boolEvaluate == false && chLetter == 'M' && chSign == '>' && pInput.M > lngValue) boolEvaluate = true;
                if (boolEvaluate == false && chLetter == 'M' && chSign == '<' && pInput.M < lngValue) boolEvaluate = true;
                if (boolEvaluate == false && chLetter == 'A' && chSign == '>' && pInput.A > lngValue) boolEvaluate = true;
                if (boolEvaluate == false && chLetter == 'A' && chSign == '<' && pInput.A < lngValue) boolEvaluate = true;
                if (boolEvaluate == false && chLetter == 'S' && chSign == '>' && pInput.S > lngValue) boolEvaluate = true;
                if (boolEvaluate == false && chLetter == 'S' && chSign == '<' && pInput.S < lngValue) boolEvaluate = true;



                if (boolEvaluate)
                {
                    strReturn = strEvaluate[1];
                    break;
                }
            }




            return strReturn;
        }



        public class p19_Input
        {
            public long X = 0;
            public long M = 0;
            public long A = 0;
            public long S = 0;

            public long sum()
            {
                return (X + M + A + S);
            }
        }

        private long Puzzle19_PartTwo()
        {
            //string strInput19 = Advent23.Properties.Settings.Default.Puzzle19_Input;
            string strInput19 = Advent23.Properties.Settings.Default.Puzzle_Example;

            string[] allString = strInput19.Split("\r\n\r\n");
            long lngAnswer = 0;

            //we split the workflow first then instruction
            string[] strWorkflow = allString[0].Split("\r\n");
            Dictionary<string, List<string>> dictFlows = new Dictionary<string, List<string>>();

            //let add them all up first
            foreach (string aFlow in strWorkflow)
            {
                //first we get the caption of the workflow
                //then we split all instruction in bite size and we save them
                //we will call a function to determine Y/N on each instruction later

                string strFlowCaption = "";
                List<string> lstInstructions = new List<string>();
                string strInstructions = "";

                strFlowCaption = aFlow.Substring(0, aFlow.IndexOf("{"));
                strInstructions = aFlow.Substring(aFlow.IndexOf("{") + 1); strInstructions = strInstructions.Substring(0, strInstructions.Length - 1);

                string[] strInstSplit = strInstructions.Split(",");
                foreach (string strInst in strInstSplit)
                {
                    lstInstructions.Add(strInst);
                }
                dictFlows.Add(strFlowCaption, lstInstructions);
            }

            //now we got a dictionary with all flows
            //let add IN in the queue and loop until we mapped all A flows

            List<string> lstHappyPaths = new List<string>();
            Queue<Tuple<string, string>> quWork = new Queue<Tuple<string, string>>();
            Tuple<string, string> tpNewItem = new Tuple<string, string>("in", ""); //Item1 = flow to go to, Item2 = what we accumulated
            Tuple<string, string> tpToDo = new Tuple<string, string>("", "");
            quWork.Enqueue(tpNewItem);

            while (quWork.Count > 0)
            {
                tpToDo = quWork.Dequeue();

                string strAccumulatedInstruction = tpToDo.Item2;
                //
                foreach (string strInst in dictFlows[tpToDo.Item1])
                {
                    //for each instruction we do all possiblities and queue extra works


                    if (strInst == "A")
                    {
                        lstHappyPaths.Add(strAccumulatedInstruction);
                    }
                    else if (strInst == "R")
                    {
                        break; //rejected do nothing, and there nothing after its always last on instruction set
                    }
                    else if (strInst.Contains(":") == false)
                    {
                        //direct go somewhere else, we queue that as another work item
                        tpNewItem = new Tuple<string, string>(strInst, strAccumulatedInstruction);
                        quWork.Enqueue(tpNewItem);
                    }
                    else
                    {
                        //this is now a formula of > or <
                        //we will add on the queue the 'if valid' and if not valid we add the reverse and continue on each instruction


                        //then we evaluate a variable
                        string[] strEvaluate = strInst.Split(":");


                        if (strEvaluate[1] == "A")
                        {
                            //this was an happy path
                            lstHappyPaths.Add(strAccumulatedInstruction + "|" + strEvaluate[0]);
                        }
                        else if (strEvaluate[1] != "R") //Reject we ignore
                        {
                            //we got redirected
                            tpNewItem = new Tuple<string, string>(strEvaluate[1], strAccumulatedInstruction + "|" + strEvaluate[0]); //Item1 = flow to go to, Item2 = what we accumulated
                            quWork.Enqueue(tpNewItem);
                        }

                        //now we need to reverse the statement and continue
                        //default >
                        char chSign = '>';
                        long lngValue = lngValue = long.Parse(strEvaluate[0].Substring(2)) - 1;
                        if (strEvaluate[0][1] == '>')
                        {
                            //oh well was the other sign we offset
                            chSign = '<';
                            lngValue += 2;
                        }
                        strAccumulatedInstruction += "|" + strEvaluate[0][0] + chSign + lngValue.ToString();
                    }
                }
            }
            lngAnswer = 0;


            foreach (string strSet in lstHappyPaths)
            {

                long lng_X_Min = 1; long lng_X_Max = 4000;
                long lng_M_Min = 1; long lng_M_Max = 4000;
                long lng_A_Min = 1; long lng_A_Max = 4000;
                long lng_S_Min = 1; long lng_S_Max = 4000;
                long lngSetAnswer = 0;

                string[] strValidation = strSet.Substring(1).Split('|');

                foreach (string strCond in strValidation)
                {
                    char chLetter = strCond.ToUpper()[0];
                    char chSign = strCond[1];
                    long lngValue = long.Parse(strCond.Substring(2));

                    //easier to check all 8 as one line
                    if (chLetter == 'X' && chSign == '>' && lng_X_Min < lngValue) lng_X_Min = lngValue + 1;
                    if (chLetter == 'X' && chSign == '<' && lng_X_Max > lngValue) lng_X_Max = lngValue - 1;
                    if (chLetter == 'M' && chSign == '>' && lng_M_Min < lngValue) lng_M_Min = lngValue + 1;
                    if (chLetter == 'M' && chSign == '<' && lng_M_Max > lngValue) lng_M_Max = lngValue - 1;
                    if (chLetter == 'A' && chSign == '>' && lng_A_Min < lngValue) lng_A_Min = lngValue + 1;
                    if (chLetter == 'A' && chSign == '<' && lng_A_Max > lngValue) lng_A_Max = lngValue - 1;
                    if (chLetter == 'S' && chSign == '>' && lng_S_Min < lngValue) lng_S_Min = lngValue + 1;
                    if (chLetter == 'S' && chSign == '<' && lng_S_Max > lngValue) lng_S_Max = lngValue - 1;
                }

                if (((lng_X_Max - lng_X_Min) + 1) > 0 &&
                    ((lng_M_Max - lng_M_Min) + 1) > 0 &&
                    ((lng_A_Max - lng_A_Min) + 1) > 0 &&
                    ((lng_S_Max - lng_S_Min) + 1) > 0)
                {
                    lngSetAnswer = ((lng_X_Max - lng_X_Min) + 1);
                    lngSetAnswer *= ((lng_M_Max - lng_M_Min) + 1);
                    lngSetAnswer *= ((lng_A_Max - lng_A_Min) + 1);
                    lngSetAnswer *= ((lng_S_Max - lng_S_Min) + 1);
                    lngAnswer += lngSetAnswer;
                }


            }



            return lngAnswer;
        }

        private void bntRun19_Click(object sender, EventArgs e)
        {
            long lngSolution19P1 = Puzzle19_PartOne();
            txt_output19P1.Text = lngSolution19P1.ToString();

            long lngSolution19P2 = Puzzle19_PartTwo();
            txt_output19P2.Text = lngSolution19P2.ToString();
        }

        #endregion


        #region Puzzle 20
        public static Queue<Tuple<string, int, bool>> p20_QuPressed = new Queue<Tuple<string, int, bool>>();
        public static List<p20Module> p20_Modules = new List<p20Module>();
        public static Dictionary<string, int> p20_DictModule = new Dictionary<string, int>();

        private long Puzzle20_PartOne()
        {
            //string strInput20 = Advent23.Properties.Settings.Default.Puzzle_Example;
            string strInput20 = Advent23.Properties.Settings.Default.Puzzle_Input;

            string[] allString = strInput20.Split("\r\n");
            long p20_lngAnswers = 0;
            long lngLowSignalCount = 0;
            long lnngHighSignalCount = 0;


            //If String type
            for (int i = 0; i < allString.Length; i++)
            {
                //we will create all Module

                //format goes like this: %rk -> gk, sb
                //we check if first character is & (for Conjunction)
                //Otherwise we consider it a flip
                //I did change the input to have broadcaster start with %

                string[] strInput = allString[i].Split(" -> ");

                bool boolConj = false;
                if (strInput[0][0] == '&') boolConj = true;

                p20Module aModule = new p20Module(strInput[0].Substring(1), strInput[1].Split(","), boolConj, i);
                p20_DictModule.Add(strInput[0].Substring(1), i);
                p20_Modules.Add(aModule);
            }

            int pBroadcast = 0;
            //now all module are started let initialize them
            for (int i = 0; i < p20_Modules.Count; i++)
            {
                p20_Modules[i].Initialize();
                if (p20_Modules[i].Label == "broadcaster") pBroadcast = i;
            }

            Tuple<string, int, bool> p20_Signal; //Item1 = from, Item2= indexTo, High/Low
            for (int i = 0; i < 1000; i++)
            {
                p20_Signal = new Tuple<string, int, bool>("", pBroadcast, false);
                p20_QuPressed.Enqueue(p20_Signal);

                while (p20_QuPressed.Count > 0)
                {
                    //we dequeue and send the signal (while counting)
                    p20_Signal = p20_QuPressed.Dequeue();

                    //we count signal H/L
                    if (p20_Signal.Item3)
                    {
                        lnngHighSignalCount++;
                    }
                    else
                    {
                        lngLowSignalCount++;
                    }

                    //now we send signal to the module which will queue what's needed
                    if (p20_Signal.Item2 != -1) p20_Modules[p20_Signal.Item2].ReceiveSignal(p20_Signal.Item1, p20_Signal.Item3);
                }
            }

            p20_lngAnswers = lnngHighSignalCount * lngLowSignalCount;
            return p20_lngAnswers;
        }

        public class p20Module
        {
            private bool boolState = false; //off by default
            private string strMyLabel = "";
            private int int_MyIndex = 0;
            private bool boolConjuction = false;
            private string[] strChildsLabel;
            private int[] int_Childs;
            private Dictionary<string, bool> dictParentState = new Dictionary<string, bool>();
            Tuple<string, int, bool> p20_Signal; //Item1 = from, Item2= indexTo, High/Low


            public string Label
            {
                get { return strMyLabel; }   // get method
            }
            public int IndexNum
            {
                get { return int_MyIndex; }   // get method
            }

            public p20Module(string strMyLabel, string[] strChildsLabel, bool boolConjuction, int int_MyIndex)
            {
                this.strMyLabel = strMyLabel.Trim();
                this.int_MyIndex = int_MyIndex;
                this.boolConjuction = boolConjuction;
                this.strChildsLabel = strChildsLabel;


                int_Childs = new int[strChildsLabel.Length];
            } //used to initialize a flop and a Conjunction


            public void ReceiveSignal(string strFrom, bool boolSignal)
            {
                //we got a signal either Low or High
                //if we are a flip, we just flip our signal and send a message (of our new signal) to all our child
                //if we are a Conjuction, we need to check the signal of all our childs (need to be high) and we become high if they all are
                if (boolConjuction == false && boolSignal == false)
                {
                    //we flip the state
                    boolState = !boolState;
                    if (strMyLabel == "broadcaster") boolState = false;

                    foreach (int intChild in int_Childs)
                    {
                        p20_Signal = new Tuple<string, int, bool>(strMyLabel, intChild, boolState);
                        p20_QuPressed.Enqueue(p20_Signal);
                    }
                }
                else if (boolConjuction == true)
                {
                    //first let update the status of this child to the status we just got
                    dictParentState[strFrom] = boolSignal;

                    //now we check all childs if all high its change itself to low otherwise its high
                    //default we will send low
                    boolState = false;
                    foreach (bool boolChild in dictParentState.Values)
                    {
                        if (boolChild == false)
                        {
                            boolState = true;
                            break;
                        }
                    }

                    foreach (int intChild in int_Childs)
                    {
                        p20_Signal = new Tuple<string, int, bool>(strMyLabel, intChild, boolState);
                        p20_QuPressed.Enqueue(p20_Signal);
                    }
                }
            }

            public void Initialize()
            {
                p20Module aModule;
                //we go throught the global list to find the child
                for (int i = 0; i < strChildsLabel.Length; i++)
                {
                    //let get the child, then save its index in our array, and send him a message to monitor for us (for Conj)
                    if (p20_DictModule.ContainsKey(strChildsLabel[i].Trim()))
                    {
                        aModule = p20_Modules[p20_DictModule[strChildsLabel[i].Trim()]];
                        int_Childs[i] = aModule.int_MyIndex;

                        aModule.init_Child(strMyLabel);
                    }
                    else
                    {
                        int_Childs[i] = -1;
                    }
                }
            }

            public void init_Child(string strParent)
            {
                //we received a message that we might have a children
                if (boolConjuction == true) dictParentState.Add(strParent, false); //lowSignal By Default
            }
        }
        private long Puzzle20_PartTwo()
        {
            //This is another one of those LCM ....

            //string strInput20 = Advent23.Properties.Settings.Default.Puzzle_Example;
            string strInput20 = Advent23.Properties.Settings.Default.Puzzle_Input;

            string[] allString = strInput20.Split("\r\n");
            long p20_lngAnswers = 0;

            p20_QuPressed = new Queue<Tuple<string, int, bool>>();
            p20_Modules = new List<p20Module>();
            p20_DictModule = new Dictionary<string, int>();

            //If String type
            for (int i = 0; i < allString.Length; i++)
            {
                //we will create all Module

                //format goes like this: %rk -> gk, sb
                //we check if first character is & (for Conjunction)
                //Otherwise we consider it a flip
                //I did change the input to have broadcaster start with %

                string[] strInput = allString[i].Split(" -> ");

                bool boolConj = false;
                if (strInput[0][0] == '&') boolConj = true;

                p20Module aModule = new p20Module(strInput[0].Substring(1), strInput[1].Split(","), boolConj, i);
                p20_DictModule.Add(strInput[0].Substring(1), i);
                p20_Modules.Add(aModule);
            }

            int pBroadcast = 0;
            //now all module are started let initialize them
            for (int i = 0; i < p20_Modules.Count; i++)
            {
                p20_Modules[i].Initialize();
                if (p20_Modules[i].Label == "broadcaster") pBroadcast = i;
            }

            Tuple<string, int, bool> p20_Signal; //Item1 = from, Item2= indexTo, High/Low

            //I looked at the input its another one of those LCM
            /*RX need to be sent a low, and there is 4 that send to HJ
             * 
             * &hj -> rx
                &ks -> hj   0
                &jf -> hj   1
                &qs -> hj   2
                &zk -> hj   3
             * 
             * They are like electronic sending pulse, and switching
             * So let keep their 'latest' status in a table and register when they 'flip' and look at the pattern and LCM it out
             */

            List<int>[] lstSwitched = new List<int>[4]; //I checked their status they change the same cycle they are put on
            lstSwitched[0] = new List<int> { 0 };
            lstSwitched[1] = new List<int> { 0 };
            lstSwitched[2] = new List<int> { 0 };
            lstSwitched[3] = new List<int> { 0 };

            for (int i = 0; i < 50000; i++) //sample
            {
                p20_lngAnswers++;
                p20_Signal = new Tuple<string, int, bool>("", pBroadcast, false);
                p20_QuPressed.Enqueue(p20_Signal);

                while (p20_QuPressed.Count > 0)
                {
                    //we dequeue and send the signal (while counting)
                    p20_Signal = p20_QuPressed.Dequeue();

                    //now we send signal to the module which will queue what's needed
                    if (p20_Signal.Item2 != -1)
                    {
                        p20_Modules[p20_Signal.Item2].ReceiveSignal(p20_Signal.Item1, p20_Signal.Item3);
                    }

                    if (p20_Signal.Item1 == "ks" && p20_Signal.Item3) lstSwitched[0].Add(i);
                    if (p20_Signal.Item1 == "jf" && p20_Signal.Item3) lstSwitched[1].Add(i);
                    if (p20_Signal.Item1 == "qs" && p20_Signal.Item3) lstSwitched[2].Add(i);
                    if (p20_Signal.Item1 == "zk" && p20_Signal.Item3) lstSwitched[3].Add(i);
                }
            }

            //they all loop on only 1
            List<long> lstInterval = new List<long>();
            for (int i = 0; i < 4; i++)
            {
                lstInterval.Add(lstSwitched[i][4] - lstSwitched[i][3]);
            }

            p20_lngAnswers = LCM(lstInterval);
            return p20_lngAnswers;


        }

        private long LCM(List<long> lstLongs)
        {
            //When trying to determine the LCM of more than two numbers, for example LCM(a, b, c) find the LCM of a and b where the result will be q.
            //Then find the LCM of c and q. The result will be the LCM of all three numbers. Using the previous example:

            long lngReturn = 0;

            if (lstLongs.Count == 1)
            {
                lngReturn = lstLongs[0];
            }
            else if (lstLongs.Count > 2)
            {
                long aGCF = 0;
                long c = lstLongs[0];

                for (int i = 1; i < lstLongs.Count; i++)
                {
                    aGCF = GCF(c, lstLongs[i]);
                    c = (c * lstLongs[i]) / aGCF;
                }

                lngReturn = c;
            }
            return lngReturn;
        }

        private long GCF(long a, long b)
        {
            long Num1 = a;
            long Num2 = b;
            long lngReturn = 1;

            //GCF(a, a) = a
            //GCF(a, b) = GCF(a-b, b), when a > b
            //GCF(a, b) = GCF(a, b-a), when b > a
            while (Num1 != Num2)
            {
                if (Num1 > Num2)
                {
                    Num1 -= Num2;
                }
                else
                {
                    Num2 -= Num1;
                }
            }

            lngReturn = Num1;
            return lngReturn;
        }
        private void bntRun20_Click(object sender, EventArgs e)
        {
            long lngSolution20P1 = Puzzle20_PartOne();
            txt_output20P1.Text = lngSolution20P1.ToString();

            long lngSolution20P2 = Puzzle20_PartTwo();
            txt_output20P2.Text = lngSolution20P2.ToString();
        }

        #endregion

        #region Puzzle 21
        private long Puzzle21_PartOne()
        {
            string strInput17 = Advent23.Properties.Settings.Default.Puzzle_Example;
            string[] allString = strInput17.Split("\r\n");
            long lngAnswer = 0;
            //If Array
            int intGridSize = 11;
            int intStep = 6;


            int intX = -1;
            int intY = 0;

            Dictionary<string, Point> dictCoordinate = new Dictionary<string, Point>();
            Dictionary<string, Point> dictNewCoordinate = new Dictionary<string, Point>();

            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    if (chData == 'S')
                    {
                        dictCoordinate.Add(intX.ToString() + "," + intY.ToString(), new Point(intX, intY));
                        chrGrid[intX, intY++] = '.';
                    }
                    else
                    {
                        chrGrid[intX, intY++] = chData;
                    }

                }
            }

            for (int a = 0; a < intStep; a++)
            {
                dictNewCoordinate = new Dictionary<string, Point>(); // we reset new location
                foreach (Point anElf in dictCoordinate.Values)
                {


                    //for each elf we take his X/Y, and move him on all possible 1 around
                    intX = anElf.X;
                    intY = anElf.Y;

                    for (int c = -1; c < 2; c = c + 2)
                    {
                        if (intX + c >= 0 && intX + c < intGridSize)
                        {
                            if (chrGrid[intX + c, intY] == '.' && dictNewCoordinate.ContainsKey((intX + c).ToString() + "," + intY.ToString()) == false)
                            {
                                dictNewCoordinate.Add((intX + c).ToString() + "," + intY.ToString(), new Point(intX + c, intY));
                            }

                        }
                        if (intY + c >= 0 && intY + c < intGridSize)
                        {
                            if (chrGrid[intX, intY + c] == '.' && dictNewCoordinate.ContainsKey(intX.ToString() + "," + (intY + c).ToString()) == false)
                            {
                                dictNewCoordinate.Add(intX.ToString() + "," + (intY + c).ToString(), new Point(intX, intY + c));
                            }
                        }
                    }
                }
                dictCoordinate = dictNewCoordinate;
            }



            //now we need to move the elf
            lngAnswer = dictCoordinate.Count;

            return lngAnswer;
        }

        private long Puzzle21_PartTwo()
        {
            //Shamefully took that from solution/internet
            //I am not very strong with quadratic
            //I found 2n^2 - 2n +1 , but I am not sure how to split Even and Odd spread etc


            var input = Advent23.Properties.Settings.Default.Puzzle_Input.Split("\r\n").ToList();
            var gridSize = input.Count == input[0].Length ? input.Count : throw new ArgumentOutOfRangeException();

            var start = Enumerable.Range(0, gridSize)
                .SelectMany(i => Enumerable.Range(0, gridSize)
                    .Where(j => input[i][j] == 'S')
                    .Select(j => new Coord(i, j)))
                .Single();

            var grids = 26501365 / gridSize;
            var rem = 26501365 % gridSize;

            var sequence = new List<int>();
            var work = new HashSet<Coord> { start };
            var steps = 0;
            for (var n = 0; n < 3; n++)
            {
                for (; steps < n * gridSize + rem; steps++)
                {
                    work = new HashSet<Coord>(work
                        .SelectMany(it => new[] { Dir.N, Dir.S, Dir.E, Dir.W }.Select(dir => it.Move(dir)))
                        .Where(dest => input[((dest.X % 131) + 131) % 131][((dest.Y % 131) + 131) % 131] != '#'));
                }

                sequence.Add(work.Count);
            }

            var c = sequence[0];
            var aPlusB = sequence[1] - c;
            var fourAPlusTwoB = sequence[2] - c;
            var twoA = fourAPlusTwoB - (2 * aPlusB);
            var a = twoA / 2;
            var b = aPlusB - a;

            long F(long n)
            {
                return a * (n * n) + b * n + c;
            }

            for (var i = 0; i < sequence.Count; i++)
            {
                Console.WriteLine($"{sequence[i]} : {F(i)}");
            }

            return (F(grids));

        }
        public record Coord(int X, int Y)
        {
            public Coord Move(Dir dir, int dist = 1)
            {
                return dir switch
                {
                    Dir.N => new Coord(this.X - dist, this.Y),
                    Dir.S => new Coord(this.X + dist, this.Y),
                    Dir.E => new Coord(this.X, this.Y + dist),
                    Dir.W => new Coord(this.X, this.Y - dist),
                };
            }
        }
        public enum Dir
        {
            N, S, E, W
        }


        public long[] P21_Spread(Point ptStart, int intStep)
        {
            long[] lstSpreadCount = new long[2]; //0 = even , 1 = odd


            string strInput21 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput21.Split("\r\n");
            int intGridSize = 131;

            long lngX = -1;
            long lngY = 0;
            List<long> lstAnswerCount = new List<long>();

            Dictionary<string, Point> dictCoordinateGlobal = new Dictionary<string, Point>();
            Dictionary<string, Point> dictCoordinateEven = new Dictionary<string, Point>();
            Dictionary<string, Point> dictCoordinateOdd = new Dictionary<string, Point>();

            Dictionary<string, Point> dictNewCoordinate = new Dictionary<string, Point>();
            Dictionary<string, Point> dictToCheck = new Dictionary<string, Point>();

            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            foreach (string strGames in allString)
            {
                lngX++;
                lngY = 0;
                foreach (char chData in strGames)
                {
                    if (chData == 'S')
                    {
                        chrGrid[lngX, lngY++] = '.';
                    }
                    else
                    {
                        chrGrid[lngX, lngY++] = chData;
                    }

                }
            }

            //starting location
            dictToCheck.Add(ptStart.X.ToString() + "," + ptStart.Y.ToString(), ptStart);
            Point newLoc;
            for (int a = 0; a < intStep; a++)
            {
                dictNewCoordinate = new Dictionary<string, Point>(); // we reset new location

                foreach (Point anElf in dictToCheck.Values)
                {
                    //for each elf we take his X/Y, and move him on all possible 1 around
                    for (int c = -1; c < 2; c = c + 2)
                    {
                        newLoc = new Point(anElf.X + c, anElf.Y);
                        if (newLoc.X >= 0 && newLoc.X < intGridSize && chrGrid[newLoc.X, newLoc.Y] == '.' && dictCoordinateGlobal.ContainsKey(newLoc.X.ToString() + "," + newLoc.Y.ToString()) == false)
                        {
                            //Then we add in all Dictionnary
                            dictCoordinateGlobal.Add(newLoc.X.ToString() + "," + newLoc.Y.ToString(), newLoc);
                            dictNewCoordinate.Add(newLoc.X.ToString() + "," + newLoc.Y.ToString(), newLoc);
                            if (a % 2 == 0)
                            {
                                dictCoordinateOdd.Add(newLoc.X.ToString() + "," + newLoc.Y.ToString(), newLoc);
                            }
                            else
                            {
                                dictCoordinateEven.Add(newLoc.X.ToString() + "," + newLoc.Y.ToString(), newLoc);
                            }
                        }

                        newLoc = new Point(anElf.X, anElf.Y + c);
                        if (newLoc.Y >= 0 && newLoc.Y < intGridSize && chrGrid[newLoc.X, newLoc.Y] == '.' && dictCoordinateGlobal.ContainsKey(newLoc.X.ToString() + "," + newLoc.Y.ToString()) == false)
                        {
                            //Then we add in all Dictionnary
                            dictCoordinateGlobal.Add(newLoc.X.ToString() + "," + newLoc.Y.ToString(), newLoc);
                            dictNewCoordinate.Add(newLoc.X.ToString() + "," + newLoc.Y.ToString(), newLoc);
                            if (a % 2 == 0)
                            {
                                dictCoordinateOdd.Add(newLoc.X.ToString() + "," + newLoc.Y.ToString(), newLoc);
                            }
                            else
                            {
                                dictCoordinateEven.Add(newLoc.X.ToString() + "," + newLoc.Y.ToString(), newLoc);
                            }
                        }
                    }
                }

                dictToCheck = dictNewCoordinate;
            }

            lstSpreadCount[0] = dictCoordinateEven.Count();
            lstSpreadCount[1] = dictCoordinateOdd.Count();


            return lstSpreadCount;
        }
        private void bntRun21_Click(object sender, EventArgs e)
        {
            int i = 13;



            long lngSolution21P1 = Puzzle21_PartOne();
            txt_output21P1.Text = lngSolution21P1.ToString();

            long lngSolution21P2 = Puzzle21_PartTwo();
            txt_output21P2.Text = lngSolution21P2.ToString();
        }

        #endregion

        #region Puzzle 22
        private long Puzzle22_PartOne()
        {
            //505
            string strInput22 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput22.Split("\r\n");
            long lngAnswer = 0;

            //If Array
            int intGridSize = 999;

            List<string> list = new List<string>();
            int[,,] GridBlock = new int[intGridSize, intGridSize, intGridSize];

            int intHighestBlock = 0;

            int intBlockNum = 1;

            int[,,] aBlock;
            //we set the floor with 1
            for (int a = 0; a < intGridSize; a++)
            {
                for (int b = 0; b < intGridSize; b++)
                {
                    GridBlock[a, b, 0] = 1;
                }
            }

            Dictionary<int, int> dictSingleDependencies = new Dictionary<int, int>();
            dictSingleDependencies.Add(1, 2);//block 1 (the ground) is fake so we eliminate  it

            PriorityQueue<string, int> quBlock = new PriorityQueue<string, int>();

            foreach (string strGames in allString)
            {
                //we just parse then put them in the Queue depending on their current heigh
                quBlock.Enqueue(strGames, int.Parse(strGames.Split("~")[0].Split(",")[2])); //first number of where the Z start
            }


            //If String type
            while (quBlock.Count > 0)
            {
                //hold the max shown in that game
                intBlockNum++;
                string strGames = quBlock.Dequeue();

                string[] Dimension = strGames.Split("~");
                string[] dmFrom = Dimension[0].Split(",");
                string[] dmTo = Dimension[1].Split(",");


                //We get the coordinate of that block and we drop it so Z go down
                int intLenghtX;
                int intLenghtY;
                int intLenghtZ;


                int[] intBlock_X = new int[2] { int.Parse(dmFrom[0]), int.Parse(dmTo[0]) };
                int[] intBlock_Y = new int[2] { int.Parse(dmFrom[1]), int.Parse(dmTo[1]) };
                int[] intBlock_Z = new int[2] { int.Parse(dmFrom[2]), int.Parse(dmTo[2]) };

                intLenghtX = intBlock_X[1] - intBlock_X[0] + 1;
                intLenghtY = intBlock_Y[1] - intBlock_Y[0] + 1;
                intLenghtZ = intBlock_Z[1] - intBlock_Z[0] + 1;


                bool dropped = false;
                int intCurrentZ = intHighestBlock;
                //Now we just need to 'drop' it to level where highest block is, then compare X/Y until we hit something

                List<int> lstSupportNum = new List<int>();

                while (dropped == false && intCurrentZ >= 0)
                {
                    //
                    for (int x = intBlock_X[0]; x <= intBlock_X[1]; x++)
                    {
                        for (int y = intBlock_Y[0]; y <= intBlock_Y[1]; y++)
                        {
                            if (GridBlock[x, y, intCurrentZ] > 0)
                            {
                                if (lstSupportNum.Contains(GridBlock[x, y, intCurrentZ]) == false) lstSupportNum.Add(GridBlock[x, y, intCurrentZ]); //we hit a support
                            }
                        }
                    }

                    if (lstSupportNum.Count > 0)
                    {
                        dropped = true;
                        if (lstSupportNum.Count == 1)
                        {
                            //now if we got just 1 supportNum we note it as he's dependent on us
                            dictSingleDependencies[lstSupportNum[0]] = dictSingleDependencies[lstSupportNum[0]] + 1;
                        }

                        //we draw this diagram with his corresponding number and add him with 'zero' dependencies for now
                        dictSingleDependencies.Add(intBlockNum, 0);

                        intBlock_Z[0] = intCurrentZ + 1; //we drop him at one level above where we just checked
                        intBlock_Z[1] = intBlock_Z[0] + (intLenghtZ - 1);

                        if (intHighestBlock < intBlock_Z[1]) intHighestBlock = intBlock_Z[1]; //we check new highest level block

                        //we draw
                        for (int x = intBlock_X[0]; x <= intBlock_X[1]; x++)
                        {
                            for (int y = intBlock_Y[0]; y <= intBlock_Y[1]; y++)
                            {
                                for (int z = intBlock_Z[0]; z <= intBlock_Z[1]; z++)
                                {
                                    GridBlock[x, y, z] = intBlockNum;
                                }
                            }

                        }
                    }
                    else
                    {
                        intCurrentZ--; //we go down one and recheck
                    }
                }
            }

            lngAnswer = 0;
            foreach (int intDependencies in dictSingleDependencies.Values)
            {
                if (intDependencies == 0) lngAnswer++;
            }

            return lngAnswer;
        }
        private long Puzzle22_PartTwo()
        {
            //1455
            //152680
            //1455
            //950
            //71002

            string strInput22 = Advent23.Properties.Settings.Default.Puzzle_Input;
            string[] allString = strInput22.Split("\r\n");
            long lngAnswer = 0;

            //If Array
            int intGridSize = 999;

            List<string> list = new List<string>();
            int[,,] GridBlock = new int[intGridSize, intGridSize, intGridSize];

            int intHighestBlock = 0;

            int intBlockNum = 1;

            int[,,] aBlock;
            //we set the floor with 1
            for (int a = 0; a < intGridSize; a++)
            {
                for (int b = 0; b < intGridSize; b++)
                {
                    GridBlock[a, b, 0] = 1;
                }
            }

            Dictionary<int, bool> dictSingleDependencies = new Dictionary<int, bool>();
            Dictionary<int, int> dictBlockLowestZ = new Dictionary<int, int>();
            Dictionary<int, List<int>> dictAllDependencies = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> dictSupportedBy = new Dictionary<int, List<int>>();

            //we create bedrock, we will delete before we check later
            dictSingleDependencies.Add(1, false);
            dictAllDependencies.Add(1, new List<int>());


            PriorityQueue<string, int> quBlock = new PriorityQueue<string, int>();

            foreach (string strGames in allString)
            {
                //we just parse then put them in the Queue depending on their current heigh
                quBlock.Enqueue(strGames, int.Parse(strGames.Split("~")[0].Split(",")[2])); //first number of where the Z start
            }


            //If String type
            while (quBlock.Count > 0)
            {
                //hold the max shown in that game
                intBlockNum++;
                string strGames = quBlock.Dequeue();

                string[] Dimension = strGames.Split("~");
                string[] dmFrom = Dimension[0].Split(",");
                string[] dmTo = Dimension[1].Split(",");


                //We get the coordinate of that block and we drop it so Z go down
                int intLenghtX;
                int intLenghtY;
                int intLenghtZ;


                int[] intBlock_X = new int[2] { int.Parse(dmFrom[0]), int.Parse(dmTo[0]) };
                int[] intBlock_Y = new int[2] { int.Parse(dmFrom[1]), int.Parse(dmTo[1]) };
                int[] intBlock_Z = new int[2] { int.Parse(dmFrom[2]), int.Parse(dmTo[2]) };

                intLenghtX = intBlock_X[1] - intBlock_X[0] + 1;
                intLenghtY = intBlock_Y[1] - intBlock_Y[0] + 1;
                intLenghtZ = intBlock_Z[1] - intBlock_Z[0] + 1;


                bool dropped = false;
                int intCurrentZ = intHighestBlock;
                //Now we just need to 'drop' it to level where highest block is, then compare X/Y until we hit something

                List<int> lstSupportNum = new List<int>();

                while (dropped == false && intCurrentZ >= 0)
                {
                    //
                    for (int x = intBlock_X[0]; x <= intBlock_X[1]; x++)
                    {
                        for (int y = intBlock_Y[0]; y <= intBlock_Y[1]; y++)
                        {
                            if (GridBlock[x, y, intCurrentZ] > 0)
                            {
                                if (lstSupportNum.Contains(GridBlock[x, y, intCurrentZ]) == false) lstSupportNum.Add(GridBlock[x, y, intCurrentZ]); //we hit a support
                            }
                        }
                    }

                    if (lstSupportNum.Count > 0)
                    {
                        dropped = true;
                        dictSingleDependencies.Add(intBlockNum, false);
                        dictAllDependencies.Add(intBlockNum, new List<int>());
                        dictSupportedBy.Add(intBlockNum, new List<int>());

                        if (lstSupportNum.Count == 1)
                        {
                            //now if we got just 1 supportNum we note it as he's dependent on us, we will target it for at minimum 1 chain
                            dictSingleDependencies[lstSupportNum[0]] = true;
                        }
                        foreach (int intSupport in lstSupportNum)
                        {
                            dictAllDependencies[intSupport].Add(intBlockNum);
                            dictSupportedBy[intBlockNum].Add(intSupport);
                        }
                        //this one have no block depending on it yet

                        intBlock_Z[0] = intCurrentZ + 1; //we drop him at one level above where we just checked
                        intBlock_Z[1] = intBlock_Z[0] + (intLenghtZ - 1);
                        dictBlockLowestZ.Add(intBlockNum, intBlock_Z[0]);

                        if (intHighestBlock < intBlock_Z[1]) intHighestBlock = intBlock_Z[1]; //we check new highest level block

                        //we draw
                        for (int x = intBlock_X[0]; x <= intBlock_X[1]; x++)
                        {
                            for (int y = intBlock_Y[0]; y <= intBlock_Y[1]; y++)
                            {
                                for (int z = intBlock_Z[0]; z <= intBlock_Z[1]; z++)
                                {
                                    GridBlock[x, y, z] = intBlockNum;
                                }
                            }

                        }
                    }
                    else
                    {
                        intCurrentZ--; //we go down one and recheck
                    }
                }
            }

            //now we 
            dictSingleDependencies.Remove(1); //we remove our bedrock
            dictAllDependencies.Remove(1);

            PriorityQueue<int, int> quNeedFall = new PriorityQueue<int, int>();
            List<int> intBlockGone = new List<int>();
            lngAnswer = 0;
            for (int i = 2; i <= intBlockNum; i++)
            {
                if (dictSingleDependencies[i] == true) //can this block drop at least one
                {
                    intBlockGone = new List<int>();
                    intBlockGone.Add(i);

                    foreach (int intChild in dictAllDependencies[i])
                    {
                        quNeedFall.Enqueue(intChild, dictBlockLowestZ[intChild]);
                    }

                    while (quNeedFall.Count > 0)
                    {
                        int intFalling = quNeedFall.Dequeue();

                        if (intBlockGone.Contains(intFalling) == false)
                        {
                            //block not gone, let check if all its supporter are gone
                            bool boolSupported = false;
                            foreach (int intSupport in dictSupportedBy[intFalling])
                            {
                                if (intBlockGone.Contains(intSupport) == false)
                                {
                                    boolSupported = true;
                                    break;
                                }
                            }

                            if (boolSupported == false)
                            {
                                lngAnswer++; //pouf
                                intBlockGone.Add(intFalling); //we don't want to double count it

                                foreach (int intChild in dictAllDependencies[intFalling])
                                {
                                    quNeedFall.Enqueue(intChild, dictBlockLowestZ[intChild]);
                                }
                            }
                        }
                    }
                }

            }



            return lngAnswer;
        }

        private void bntRun22_Click(object sender, EventArgs e)
        {
            long lngSolution22P1 = Puzzle22_PartOne();
            txt_output22P1.Text = lngSolution22P1.ToString();

            long lngSolution22P2 = Puzzle22_PartTwo();
            txt_output22P2.Text = lngSolution22P2.ToString();
        }

        #endregion

        #region Puzzle 23
        private long Puzzle23_PartOne()
        {
            string strInput23 = Advent23.Properties.Settings.Default.Puzzle_Example; int intGridSize = 23;
            //string strInput23 = Advent23.Properties.Settings.Default.Puzzle_Input; int intGridSize = 141;
            string[] allString = strInput23.Split("\r\n");
            ;


            //If Array
            Point ptPosition = new Point(-1, 0);
            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            foreach (string strGames in allString)
            {
                ptPosition.X++;
                ptPosition.Y = 0;
                foreach (char chData in (strGames))
                {
                    chrGrid[ptPosition.X, ptPosition.Y++] = chData;
                }
            }

            Point ptStart = new Point(0, 1);
            Point ptEnding = new Point(intGridSize - 1, intGridSize - 2);

            List<string> lstCaching = new List<string>();


            Queue<Tuple<List<string>, Point, long>> quFork = new Queue<Tuple<List<string>, Point, long>>();
            Tuple<List<string>, Point, long> tpData = new Tuple<List<string>, Point, long>(lstCaching, ptStart, 0);


            quFork.Enqueue(tpData);
            long lngAnswer = 0;
            long lngSteps = 0;

            //Now we start, when we find a fork we just queue it we keep maximum reached
            while (quFork.Count > 0)
            {
                tpData = quFork.Dequeue();
                lstCaching = tpData.Item1;
                ptPosition = tpData.Item2;
                lngSteps = tpData.Item3;

                //Now we just continue to move until we are out of option
                Point ptNewPosition;
                bool boolEnd = false;

                while (boolEnd == false)
                {
                    lstCaching.Add(ptPosition.X + "," + ptPosition.Y); //we cache where we just went.
                    char look = chrGrid[ptPosition.X, ptPosition.Y];

                    while (look != '.')
                    {
                        ptNewPosition = ptPosition;

                        //we are slipping
                        switch (look)
                        {
                            case '^':
                                ptNewPosition.X--;
                                break;
                            case '>':
                                ptNewPosition.Y++;
                                break;
                            case '<':
                                ptNewPosition.Y--;
                                break;
                            case 'v':
                                ptNewPosition.X++;
                                break;
                        }


                        if (lstCaching.Contains(ptNewPosition.X + "," + ptNewPosition.Y) == false)
                        {
                            //we slided, we are good
                            //we write we moved
                            lngSteps++;
                            lstCaching.Add(ptNewPosition.X + "," + ptNewPosition.Y);
                        }
                        else
                        {
                            boolEnd = true; //we slided in a position we can't go.... so this thread is gone
                        }

                        ptPosition = ptNewPosition;
                        look = chrGrid[ptNewPosition.X, ptNewPosition.Y];
                    }


                    if (boolEnd == true || (ptPosition.X == ptEnding.X && ptPosition.Y == ptEnding.Y))
                    {
                        if (ptPosition.X == ptEnding.X && ptPosition.Y == ptEnding.Y)
                        {
                            if (lngAnswer < lngSteps) lngAnswer = lngSteps;
                        }
                        break;
                    }
                    else
                    {
                        //we move (if we can) and if we moved, we will create Queue for forks
                        bool boolMoved = false;
                        Point ptMovedFinal = ptPosition;

                        for (int c = -1; c < 2; c = c + 2)
                        {
                            ptNewPosition = ptPosition;
                            ptNewPosition.X += c;

                            if (ptNewPosition.X >= 0 && ptPosition.X < intGridSize)
                            {
                                look = chrGrid[ptNewPosition.X, ptNewPosition.Y];

                                if (lstCaching.Contains((ptNewPosition.X).ToString() + "," + ptNewPosition.Y.ToString()) == false &&
                                    ((look == '^' && c == -1) || (look == 'v' && c == 1) || (look == '<' || look == '>' || look == '.')))
                                {
                                    if (boolMoved)
                                    {
                                        tpData = new Tuple<List<string>, Point, long>(new List<string>(lstCaching), ptNewPosition, lngSteps + 1);
                                        quFork.Enqueue(tpData);
                                    }
                                    else
                                    {
                                        ptMovedFinal = ptNewPosition; boolMoved = true;
                                    }
                                }
                            }

                            ptNewPosition = ptPosition;
                            ptNewPosition.Y += c;
                            if (ptNewPosition.Y >= 0 && ptPosition.Y < intGridSize)
                            {
                                look = chrGrid[ptNewPosition.X, ptNewPosition.Y];

                                if (lstCaching.Contains((ptNewPosition.X).ToString() + "," + ptNewPosition.Y.ToString()) == false &&
                                    ((look == '<' && c == -1) || (look == '>' && c == 1) || (look == '^' || look == 'v' || look == '.')))
                                {
                                    if (boolMoved)
                                    {
                                        tpData = new Tuple<List<string>, Point, long>(new List<string>(lstCaching), ptNewPosition, lngSteps + 1);
                                        quFork.Enqueue(tpData);
                                    }
                                    else
                                    {
                                        ptMovedFinal = ptNewPosition; boolMoved = true;
                                    }
                                }
                            }
                        }

                        //for the 4 points, we check if we moved, and we continue
                        if (boolMoved)
                        {
                            ptPosition = ptMovedFinal;
                            lngSteps++;
                        }
                        else
                        {
                            //we didn't move
                            boolEnd = true;
                        }
                    }
                }
            }

            return lngAnswer;
        }

        private long Puzzle23_PartTwo()
        {
            //Convoluted not most efficient, but heh... its work :P
            //We convert the maze in a branching path
            //then we calculate each steps to maximum path

            //memory of each lines
            List<List<Tuple<int, int>>> lstPathLink = new List<List<Tuple<int, int>>>();
            List<string> lstPathForkDone = new List<string>();
            int intPointCounter = 1; //where we are in the List counter
            int intPointEnding = 0;
            long lngAnswer = 0;

            Stack<Tuple<Point, Point, int>> quPointCompute = new Stack<Tuple<Point, Point, int>>();
            Tuple<Point, Point, int> tpPoint = new Tuple<Point, Point, int>(new Point(0, 1), new Point(0, 1), 0);

            //string strInput23 = Advent23.Properties.Settings.Default.Puzzle_Example; int intGridSize = 23;
            string strInput23 = Advent23.Properties.Settings.Default.Puzzle_Input; int intGridSize = 141;
            string[] allString = strInput23.Split("\r\n");

            //If Array
            Point ptPosition = new Point(-1, 0);
            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            foreach (string strGames in allString)
            {
                ptPosition.X++;
                ptPosition.Y = 0;
                foreach (char chData in (strGames))
                {
                    if (chData == '#')
                    {
                        chrGrid[ptPosition.X, ptPosition.Y] = chData;
                    }
                    else
                    {
                        chrGrid[ptPosition.X, ptPosition.Y] = '.';

                    }

                    ptPosition.Y++;
                }
            }

            Point ptNewPosition;
            Point ptEnding = new Point(intGridSize - 1, intGridSize - 2);

            //initial path of nothing, but we need to link to the starting zone.
            lstPathLink.Add(new List<Tuple<int, int>>());
            lstPathForkDone.Add("0,0");

            quPointCompute.Push(tpPoint); //we will need to remove 1 at the end as we compute the start point as 1 step.


            //Now we start, when we find a fork we just queue it we keep maximum reached
            while (quPointCompute.Count > 0)
            {
                int intPathing = 1;
                tpPoint = quPointCompute.Pop();
                ptPosition = tpPoint.Item1;
                Point ptPrevious = tpPoint.Item2;
                int intIDFrom = tpPoint.Item3;
                char look;

                bool boolEnd = false;
                while (boolEnd == false)
                {
                    {
                        //we move (if we can) and if we moved, we will create Queue for forks
                        int intPath = 0;
                        List<Point> lstMovePoint = new List<Point>();
                        for (int c = -1; c < 2; c = c + 2)
                        {
                            ptNewPosition = ptPosition;
                            ptNewPosition.X += c;

                            if (ptNewPosition.X >= 0 && ptNewPosition.X < intGridSize)
                            {
                                look = chrGrid[ptNewPosition.X, ptNewPosition.Y];
                                if ((ptNewPosition.X != ptPrevious.X || ptNewPosition.Y != ptPrevious.Y) && look == '.')
                                {
                                    intPath++;
                                    lstMovePoint.Add(ptNewPosition);
                                }
                            }

                            ptNewPosition = ptPosition;
                            ptNewPosition.Y += c;
                            if (ptNewPosition.Y >= 0 && ptNewPosition.Y < intGridSize)
                            {
                                look = chrGrid[ptNewPosition.X, ptNewPosition.Y];
                                if ((ptNewPosition.X != ptPrevious.X || ptNewPosition.Y != ptPrevious.Y) && look == '.')
                                {
                                    intPath++;
                                    lstMovePoint.Add(ptNewPosition);
                                }
                            }
                        }

                        if (intPath == 1)
                        {
                            //no fork, we continue on the path
                            intPathing++;
                            ptPrevious = ptPosition;
                            ptPosition = lstMovePoint[0];
                        }
                        else if ((ptPosition.X == ptEnding.X && ptPosition.Y == ptEnding.Y) || intPath > 0)
                        {
                            int intCurrentID;
                            bool boolNewPoint = false;
                            if (lstPathForkDone.Contains(ptPosition.X.ToString() + "," + ptPosition.Y.ToString()) == true)
                            {
                                //this point was already discovered
                                intCurrentID = lstPathForkDone.IndexOf(ptPosition.X.ToString() + "," + ptPosition.Y.ToString());
                            }
                            else
                            {
                                //new point 
                                intCurrentID = intPointCounter++;
                                boolNewPoint = true;
                                lstPathLink.Add(new List<Tuple<int, int>>());
                                lstPathForkDone.Add(ptPosition.X.ToString() + "," + ptPosition.Y.ToString());
                            }

                            //we are at the end or we forked
                            if (ptPosition.X == ptEnding.X && ptPosition.Y == ptEnding.Y) intPointEnding = intCurrentID;

                            //now we add this one with previous
                            Tuple<int, int> tpLink = new Tuple<int, int>(intCurrentID, intPathing);
                            if (lstPathLink[intIDFrom].Contains(tpLink) == false) lstPathLink[intIDFrom].Add(tpLink);

                            //and reverse
                            tpLink = new Tuple<int, int>(intIDFrom, intPathing);
                            if (lstPathLink[intCurrentID].Contains(tpLink) == false) lstPathLink[intCurrentID].Add(tpLink);


                            boolEnd = true;
                            //now we create his, if not already created
                            if (boolNewPoint)
                            {
                                //for each children in lstMovePoint we queu
                                foreach (Point ptGo in lstMovePoint)
                                {
                                    tpPoint = new Tuple<Point, Point, int>(ptGo, ptPosition, intCurrentID);
                                    quPointCompute.Push(tpPoint);
                                }
                            }
                        }
                    }
                }
            }

            // minor optimisation to remove last node as its going to the end
            int intLinkEnding = lstPathLink[intPointEnding][0].Item1;
            for (int i = lstPathLink[intLinkEnding].Count - 1; i >= 0; i--)
            {
                if (lstPathLink[intLinkEnding][i].Item1 != intPointEnding) lstPathLink[intLinkEnding].RemoveAt(i);
            }


            //now we have List of all path branching out etc
            //we start at 0 and  try all paths


            List<List<int>>[] lstFullPath = new List<List<int>>[lstPathLink.Count+1]; lstFullPath[0] = new List<List<int>>();
            List<int>[] lstPathStep = new List<int>[lstPathLink.Count+1]; lstPathStep[0] = new List<int>();
            List<int>[] lstLastStep = new List<int>[lstPathLink.Count+1]; lstLastStep[0] = new List<int>();

            lstFullPath[0].Add(new List<int>(0));
            lstPathStep[0].Add(0);
            lstLastStep[0].Add(0);

            for (int a = 0; a < lstPathLink.Count; a++) //maximum step
            {
                lstFullPath[a + 1] = new List<List<int>>();
                lstPathStep[a + 1] = new List<int>();
                lstLastStep[a + 1] = new List<int>();

                for (int b = 0; b < lstFullPath[a].Count; b++) //For each steps we did previously
                {
                    //Did it reach the ending
                    if (lstLastStep[a][b] == intPointEnding)
                    {
                        //we did rea
                        if (lngAnswer < lstPathStep[a][b]) lngAnswer = lstPathStep[a][b];
                    }
                    else
                    {
                        //we continue walking if there is valid path
                        foreach (var aPoint in lstPathLink[lstLastStep[a][b]])
                        {
                            if (lstFullPath[a][b].Contains(aPoint.Item1) == false)
                            {
                                //we add this point
                                List<int> lstNew = new List<int>(lstFullPath[a][b]);
                                lstNew.Add(aPoint.Item1);



                                lstFullPath[a + 1].Add(lstNew);
                                lstPathStep[a + 1].Add(aPoint.Item2 + lstPathStep[a][b]);
                                lstLastStep[a + 1].Add(aPoint.Item1);
                            }

                        }
                    }
                }
            }








            return lngAnswer -1;
        }

        private void bntRun23_Click(object sender, EventArgs e)
        {
            long lngSolution23P1 = Puzzle23_PartOne();
            txt_output23P1.Text = lngSolution23P1.ToString();

            long lngSolution23P2 = Puzzle23_PartTwo();
            txt_output23P2.Text = lngSolution23P2.ToString();
        }

        #endregion

        #region Puzzle 24
        private long Puzzle24_PartOne()
        {
            string strInput17 = Advent23.Properties.Settings.Default.Puzzle_Example;
            string[] allString = strInput17.Split("\r\n");
            long lngAnswer = 0;


            //If Array
            int intGridSize = 100;
            int intX = -1;
            int intY = 0;
            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    chrGrid[intX, intY++] = chData;
                }
            }



            //If String type
            foreach (string strGames in allString)
            {
                //hold the max shown in that game
                int intGameNum = int.Parse(strGames.Substring(4, strGames.IndexOf(":") - 4).Trim());
                Dictionary<int, int> dictGamesResults = new Dictionary<int, int>();
            }



            return lngAnswer;
        }
        private long Puzzle24_PartTwo()
        {
            return 0;
        }

        private void bntRun24_Click(object sender, EventArgs e)
        {
            long lngSolution24P1 = Puzzle24_PartOne();
            txt_output24P1.Text = lngSolution24P1.ToString();

            long lngSolution24P2 = Puzzle24_PartTwo();
            txt_output24P2.Text = lngSolution24P2.ToString();
        }

        #endregion

        #region Puzzle 25
        private long Puzzle25_PartOne()
        {
            string strInput17 = Advent23.Properties.Settings.Default.Puzzle_Example;
            string[] allString = strInput17.Split("\r\n");
            long lngAnswer = 0;


            //If Array
            int intGridSize = 100;
            int intX = -1;
            int intY = 0;
            char[,] chrGrid = new char[intGridSize, intGridSize]; //to make it easy we pad around
            foreach (string strGames in allString)
            {
                intX++;
                intY = 0;
                foreach (char chData in strGames)
                {
                    chrGrid[intX, intY++] = chData;
                }
            }



            //If String type
            foreach (string strGames in allString)
            {
                //hold the max shown in that game
                int intGameNum = int.Parse(strGames.Substring(4, strGames.IndexOf(":") - 4).Trim());
                Dictionary<int, int> dictGamesResults = new Dictionary<int, int>();
            }



            return lngAnswer;
        }
        private long Puzzle25_PartTwo()
        {
            return 0;
        }

        private void bntRun25_Click(object sender, EventArgs e)
        {
            long lngSolution25P1 = Puzzle25_PartOne();
            txt_output25P1.Text = lngSolution25P1.ToString();

            long lngSolution25P2 = Puzzle25_PartTwo();
            txt_output25P2.Text = lngSolution25P2.ToString();
        }

        #endregion

    }
}
