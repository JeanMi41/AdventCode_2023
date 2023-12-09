using System.Data.SqlTypes;
using System.IO;
using System.Xml.Linq;

namespace Advent23
{
    public partial class frm_Advent2023 : Form
    {
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
            string strInput01 = Advent23.Properties.Settings.Default.Puzzle01_Input;
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
            string strInput01 = Advent23.Properties.Settings.Default.Puzzle01_Input;
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
            string strInput02 = Advent23.Properties.Settings.Default.Puzzle02_Input;
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
            string strInput02 = Advent23.Properties.Settings.Default.Puzzle02_Input;
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
            string strInput03 = Advent23.Properties.Settings.Default.Puzzle03_Input;
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
            string strInput03 = Advent23.Properties.Settings.Default.Puzzle03_Input;
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
            string strInput04 = Advent23.Properties.Settings.Default.Puzzle04_Input;
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

            string strInput04 = Advent23.Properties.Settings.Default.Puzzle04_Input;
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

            string strInputTransfo = Advent23.Properties.Settings.Default.Puzzle05_Input;


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

            string strInputTransfo = Advent23.Properties.Settings.Default.Puzzle05_Input; //let get the simplified input of convertion (I just removed the text and made them in 7 block

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
            string strInput07 = Advent23.Properties.Settings.Default.Puzzle07_Input;

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
            string strInput07 = Advent23.Properties.Settings.Default.Puzzle07_Input;

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
            string strInput08 = Advent23.Properties.Settings.Default.Puzzle08_Input;
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

            string strInput08 = Advent23.Properties.Settings.Default.Puzzle08_Input;
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

            //they all loop on only 1
            long[] lngStepping = new long[dictEnding.Count];
            long lngHighestStepping = 0;
            for (int i = 0; i < lstPositions.Count; i++)
            {
                lngStepping[i] = lstEndZ[i][2] - lstEndZ[i][1];
                if (lngStepping[i] > lngHighestStepping) lngHighestStepping = lngStepping[i];
            }

            //now we loop but at the speed the longest one to loopsteps
            long x = 0;
            bool valid = true;
            long lngValue;
            do
            {
                x++;
                lngValue = lngHighestStepping * x;
                valid = true;
                foreach (long lngStep in lngStepping)
                {
                    if (lngValue % lngStep != 0)
                    {
                        valid = false; break;
                    }
                }

            } while (valid == false);

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
            string strInput09 = Advent23.Properties.Settings.Default.Puzzle09_Input;
            long lngSumResult = 0;
            string[] allString = strInput09.Split("\r\n");

            foreach (string strHistory in allString)
            {
                //Now we need to do the history
                long[] lngHistory = Puzzle8FindAllHistory(strHistory);
                lngSumResult += lngHistory[0];
            }

            return lngSumResult;
        }

        private long[] Puzzle8FindAllHistory(string strLines)
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

            long[,] lngAll = new long[100,100]; //easier 100,100, won't use all
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

            lngResult = new long[lngRow+1]; //lenght will be the amount of row we did, starting with the answer (but we in theory calculate reverse)

            lngResult[lngRow] = 0; //we start with 0
            for (long x = lngRow-1; x >=0; x-- )
            {
                lngResult[x] = lngAll[x, lngLenght] + lngResult[x+1];

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
            string strInput09 = Advent23.Properties.Settings.Default.Puzzle09_Input;
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
            return 0;
        }
        private long Puzzle10_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle11_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle12_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle13_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle14_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle15_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle16_PartTwo()
        {
            return 0;
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
        }
        private long Puzzle17_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle18_PartTwo()
        {
            return 0;
        }

        private void bntRun18_Click(object sender, EventArgs e)
        {
            long lngSolution18P1 = Puzzle18_PartOne();
            txt_output18P1.Text = lngSolution18P1.ToString();

            long lngSolution18P2 = Puzzle18_PartTwo();
            txt_output18P2.Text = lngSolution18P2.ToString();
        }

        #endregion

        #region Puzzle 19
        private long Puzzle19_PartOne()
        {
            return 0;
        }
        private long Puzzle19_PartTwo()
        {
            return 0;
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
        private long Puzzle20_PartOne()
        {
            return 0;
        }
        private long Puzzle20_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle21_PartTwo()
        {
            return 0;
        }

        private void bntRun21_Click(object sender, EventArgs e)
        {
            long lngSolution21P1 = Puzzle21_PartOne();
            txt_output21P1.Text = lngSolution21P1.ToString();

            long lngSolution21P2 = Puzzle21_PartTwo();
            txt_output21P2.Text = lngSolution21P2.ToString();
        }

        #endregion

        #region Puzzle 22
        private long Puzzle22_PartOne()
        {
            return 0;
        }
        private long Puzzle22_PartTwo()
        {
            return 0;
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
            return 0;
        }
        private long Puzzle23_PartTwo()
        {
            return 0;
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
            return 0;
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
            return 0;
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
