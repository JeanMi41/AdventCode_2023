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
        private int Puzzle01_PartOne()
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
        private int Puzzle01_PartTwo()
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
            int intSolution01P1 = Puzzle01_PartOne();
            txt_output01P1.Text = intSolution01P1.ToString();

            Puzzle01_PartTwo();
            int intSolution01P2 = Puzzle01_PartTwo();
            txt_output01P2.Text = intSolution01P2.ToString();
        }
        #endregion

        #region Puzzle 02
        private int Puzzle02_PartOne(int intMaxRed, int intMaxGreen, int intMaxBlue)
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

                            switch (aStoneValue.Trim().Substring(aStoneValue.Trim().IndexOf(" ")+1).Trim())
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
                    if (intTopGreen <= intMaxGreen &&  intTopRed <= intMaxRed && intTopBlue <= intMaxBlue) { intSum += intGameNum; }
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
            int intSolution02P1 = Puzzle02_PartOne(12, 13, 14);
            txt_output02P1.Text = intSolution02P1.ToString();

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
                        if (boolNumberFound == false) {
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

                for (int x = intGridValue[0]-1 ; x <= intGridValue[0] + 1; x++) 
                {
                    for (int y = intGridValue[1] - 1; y <= intGridValue[1] + 1; y++)
                    {
                        if (strArray[x,y] != null && strArray[x,y] != strLastReference)
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
            long intSolution03P1 = Puzzle03_PartOne();
            txt_output03P1.Text = intSolution03P1.ToString();

            long intSolution03P2 = Puzzle03_PartTwo();
            txt_output03P2.Text = intSolution03P2.ToString();
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
                int[] pointValue = new int[11] { 0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512};
                int intMatchCount = 0;
                List<int> intMyNumber;
                //First we get Games #
                int intGameNum = int.Parse(strGames.Substring(4, strGames.IndexOf(":") - 4).Trim());
                Dictionary<int,int> dictGamesResults = new Dictionary<int, int>();
                    

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
                if (intMatchCount < 2) { int i = 3; }

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
        private int Puzzle05_PartOne()
        {
            return 0;
        }
        private int Puzzle05_PartTwo()
        {
            return 0;
        }

        private void bntRun05_Click(object sender, EventArgs e)
        {
            int intSolution05P1 = Puzzle05_PartOne();
            txt_output05P1.Text = intSolution05P1.ToString();

            int intSolution05P2 = Puzzle05_PartTwo();
            txt_output05P2.Text = intSolution05P2.ToString();
        }

        #endregion

        #region Puzzle 06
        private int Puzzle06_PartOne()
        {
            return 0;
        }
        private int Puzzle06_PartTwo()
        {
            return 0;
        }

        private void bntRun06_Click(object sender, EventArgs e)
        {
            int intSolution06P1 = Puzzle06_PartOne();
            txt_output06P1.Text = intSolution06P1.ToString();

            int intSolution06P2 = Puzzle06_PartTwo();
            txt_output06P2.Text = intSolution06P2.ToString();
        }

        #endregion

        #region Puzzle 07
        private int Puzzle07_PartOne()
        {
            return 0;
        }
        private int Puzzle07_PartTwo()
        {
            return 0;
        }

        private void bntRun07_Click(object sender, EventArgs e)
        {
            int intSolution07P1 = Puzzle07_PartOne();
            txt_output07P1.Text = intSolution07P1.ToString();

            int intSolution07P2 = Puzzle07_PartTwo();
            txt_output07P2.Text = intSolution07P2.ToString();
        }

        #endregion

        #region Puzzle 08
        private int Puzzle08_PartOne()
        {
            return 0;
        }
        private int Puzzle08_PartTwo()
        {
            return 0;
        }

        private void bntRun08_Click(object sender, EventArgs e)
        {
            int intSolution08P1 = Puzzle08_PartOne();
            txt_output08P1.Text = intSolution08P1.ToString();

            int intSolution08P2 = Puzzle08_PartTwo();
            txt_output08P2.Text = intSolution08P2.ToString();
        }

        #endregion

        #region Puzzle 09
        private int Puzzle09_PartOne()
        {
            return 0;
        }
        private int Puzzle09_PartTwo()
        {
            return 0;
        }

        private void bntRun09_Click(object sender, EventArgs e)
        {
            int intSolution09P1 = Puzzle09_PartOne();
            txt_output09P1.Text = intSolution09P1.ToString();

            int intSolution09P2 = Puzzle09_PartTwo();
            txt_output09P2.Text = intSolution09P2.ToString();
        }

        #endregion

        #region Puzzle 10
        private int Puzzle10_PartOne()
        {
            return 0;
        }
        private int Puzzle10_PartTwo()
        {
            return 0;
        }

        private void bntRun10_Click(object sender, EventArgs e)
        {
            int intSolution10P1 = Puzzle10_PartOne();
            txt_output10P1.Text = intSolution10P1.ToString();

            int intSolution10P2 = Puzzle10_PartTwo();
            txt_output10P2.Text = intSolution10P2.ToString();
        }

        #endregion

        #region Puzzle 11
        private int Puzzle11_PartOne()
        {
            return 0;
        }
        private int Puzzle11_PartTwo()
        {
            return 0;
        }

        private void bntRun11_Click(object sender, EventArgs e)
        {
            int intSolution11P1 = Puzzle11_PartOne();
            txt_output11P1.Text = intSolution11P1.ToString();

            int intSolution11P2 = Puzzle11_PartTwo();
            txt_output11P2.Text = intSolution11P2.ToString();
        }

        #endregion

        #region Puzzle 12
        private int Puzzle12_PartOne()
        {
            return 0;
        }
        private int Puzzle12_PartTwo()
        {
            return 0;
        }

        private void bntRun12_Click(object sender, EventArgs e)
        {
            int intSolution12P1 = Puzzle12_PartOne();
            txt_output12P1.Text = intSolution12P1.ToString();

            int intSolution12P2 = Puzzle12_PartTwo();
            txt_output12P2.Text = intSolution12P2.ToString();
        }

        #endregion

        #region Puzzle 13
        private int Puzzle13_PartOne()
        {
            return 0;
        }
        private int Puzzle13_PartTwo()
        {
            return 0;
        }

        private void bntRun13_Click(object sender, EventArgs e)
        {
            int intSolution13P1 = Puzzle13_PartOne();
            txt_output13P1.Text = intSolution13P1.ToString();

            int intSolution13P2 = Puzzle13_PartTwo();
            txt_output13P2.Text = intSolution13P2.ToString();
        }

        #endregion

        #region Puzzle 14
        private int Puzzle14_PartOne()
        {
            return 0;
        }
        private int Puzzle14_PartTwo()
        {
            return 0;
        }

        private void bntRun14_Click(object sender, EventArgs e)
        {
            int intSolution14P1 = Puzzle14_PartOne();
            txt_output14P1.Text = intSolution14P1.ToString();

            int intSolution14P2 = Puzzle14_PartTwo();
            txt_output14P2.Text = intSolution14P2.ToString();
        }

        #endregion

        #region Puzzle 15
        private int Puzzle15_PartOne()
        {
            return 0;
        }
        private int Puzzle15_PartTwo()
        {
            return 0;
        }

        private void bntRun15_Click(object sender, EventArgs e)
        {
            int intSolution15P1 = Puzzle15_PartOne();
            txt_output15P1.Text = intSolution15P1.ToString();

            int intSolution15P2 = Puzzle15_PartTwo();
            txt_output15P2.Text = intSolution15P2.ToString();
        }

        #endregion

        #region Puzzle 16
        private int Puzzle16_PartOne()
        {
            return 0;
        }
        private int Puzzle16_PartTwo()
        {
            return 0;
        }

        private void bntRun16_Click(object sender, EventArgs e)
        {
            int intSolution16P1 = Puzzle16_PartOne();
            txt_output16P1.Text = intSolution16P1.ToString();

            int intSolution16P2 = Puzzle16_PartTwo();
            txt_output16P2.Text = intSolution16P2.ToString();
        }

        #endregion

        #region Puzzle 17
        private int Puzzle17_PartOne()
        {
            return 0;
        }
        private int Puzzle17_PartTwo()
        {
            return 0;
        }

        private void bntRun17_Click(object sender, EventArgs e)
        {
            int intSolution17P1 = Puzzle17_PartOne();
            txt_output17P1.Text = intSolution17P1.ToString();

            int intSolution17P2 = Puzzle17_PartTwo();
            txt_output17P2.Text = intSolution17P2.ToString();
        }

        #endregion

        #region Puzzle 18
        private int Puzzle18_PartOne()
        {
            return 0;
        }
        private int Puzzle18_PartTwo()
        {
            return 0;
        }

        private void bntRun18_Click(object sender, EventArgs e)
        {
            int intSolution18P1 = Puzzle18_PartOne();
            txt_output18P1.Text = intSolution18P1.ToString();

            int intSolution18P2 = Puzzle18_PartTwo();
            txt_output18P2.Text = intSolution18P2.ToString();
        }

        #endregion

        #region Puzzle 19
        private int Puzzle19_PartOne()
        {
            return 0;
        }
        private int Puzzle19_PartTwo()
        {
            return 0;
        }

        private void bntRun19_Click(object sender, EventArgs e)
        {
            int intSolution19P1 = Puzzle19_PartOne();
            txt_output19P1.Text = intSolution19P1.ToString();

            int intSolution19P2 = Puzzle19_PartTwo();
            txt_output19P2.Text = intSolution19P2.ToString();
        }

        #endregion

        #region Puzzle 20
        private int Puzzle20_PartOne()
        {
            return 0;
        }
        private int Puzzle20_PartTwo()
        {
            return 0;
        }

        private void bntRun20_Click(object sender, EventArgs e)
        {
            int intSolution20P1 = Puzzle20_PartOne();
            txt_output20P1.Text = intSolution20P1.ToString();

            int intSolution20P2 = Puzzle20_PartTwo();
            txt_output20P2.Text = intSolution20P2.ToString();
        }

        #endregion

        #region Puzzle 21
        private int Puzzle21_PartOne()
        {
            return 0;
        }
        private int Puzzle21_PartTwo()
        {
            return 0;
        }

        private void bntRun21_Click(object sender, EventArgs e)
        {
            int intSolution21P1 = Puzzle21_PartOne();
            txt_output21P1.Text = intSolution21P1.ToString();

            int intSolution21P2 = Puzzle21_PartTwo();
            txt_output21P2.Text = intSolution21P2.ToString();
        }

        #endregion

        #region Puzzle 22
        private int Puzzle22_PartOne()
        {
            return 0;
        }
        private int Puzzle22_PartTwo()
        {
            return 0;
        }

        private void bntRun22_Click(object sender, EventArgs e)
        {
            int intSolution22P1 = Puzzle22_PartOne();
            txt_output22P1.Text = intSolution22P1.ToString();

            int intSolution22P2 = Puzzle22_PartTwo();
            txt_output22P2.Text = intSolution22P2.ToString();
        }

        #endregion

        #region Puzzle 23
        private int Puzzle23_PartOne()
        {
            return 0;
        }
        private int Puzzle23_PartTwo()
        {
            return 0;
        }

        private void bntRun23_Click(object sender, EventArgs e)
        {
            int intSolution23P1 = Puzzle23_PartOne();
            txt_output23P1.Text = intSolution23P1.ToString();

            int intSolution23P2 = Puzzle23_PartTwo();
            txt_output23P2.Text = intSolution23P2.ToString();
        }

        #endregion

        #region Puzzle 24
        private int Puzzle24_PartOne()
        {
            return 0;
        }
        private int Puzzle24_PartTwo()
        {
            return 0;
        }

        private void bntRun24_Click(object sender, EventArgs e)
        {
            int intSolution24P1 = Puzzle24_PartOne();
            txt_output24P1.Text = intSolution24P1.ToString();

            int intSolution24P2 = Puzzle24_PartTwo();
            txt_output24P2.Text = intSolution24P2.ToString();
        }

        #endregion

        #region Puzzle 25
        private int Puzzle25_PartOne()
        {
            return 0;
        }
        private int Puzzle25_PartTwo()
        {
            return 0;
        }

        private void bntRun25_Click(object sender, EventArgs e)
        {
            int intSolution25P1 = Puzzle25_PartOne();
            txt_output25P1.Text = intSolution25P1.ToString();

            int intSolution25P2 = Puzzle25_PartTwo();
            txt_output25P2.Text = intSolution25P2.ToString();
        }

        #endregion

    }
}
