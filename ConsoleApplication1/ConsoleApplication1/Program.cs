using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class CellScore
    {
        public int x;
        public int y;
        public int score;

        public CellScore()
        {
            x = 0;
            y = 0;
            score = 0;
        }

        public void setScore(int xcoord, int ycoord, int scoreToSet)
        {
            this.x = xcoord;
            this.y = ycoord;
            this.score = scoreToSet;
//            throw new NotImplementedException();
        }
    }
    class Program
    {
        String strX;
        String strY;
        int insertPenalty;
        int deletePenalty;
        CellScore[][] scores;
        public bool baseCase;

        public Program()
        {
            this.strX = null;
            this.strY = null;
            this.insertPenalty = this.deletePenalty = -2;
            scores = null;
            this.baseCase = false;
        }

        public Program(String a, String b)
        {
            this.strX = a;
            this.strY = b;
            this.insertPenalty = this.deletePenalty = -2;
            this.baseCase = false;
            this.scores = new CellScore[a.Length+1][];
            for (int i = 0; i < a.Length+1; i++)
            {
                this.scores[i] = new CellScore[b.Length+1];
//                this.scores[i][i].setScore(i, i, 0);
            }
            for (int i = 0; i < strX.Length + 1; i++)
            {
                for (int j = 0; j < strY.Length + 1; j++)
                {
                    this.scores[i][j] = new CellScore();
                }
            }
        }
        void calculateScore()
        {
            for (int i = 1; i < strX.Length + 1; i++)
                for (int j = 1; j < strY.Length + 1; j++)
                    this.calculateScore(i, j);
            
        }
        void calculateScore(int xcoord, int ycoord)
        {
            // BASE CASE
            if(!baseCase)
                this.setBaseCases();
            // TERMINATING CASE
//            return 0;

            // RECURRING CASE
            this.scores[xcoord][ycoord].setScore(xcoord, ycoord, Math.Max(Math.Max(this.scores[xcoord - 1][ycoord].score +
                this.insertPenalty, this.scores[xcoord][ycoord - 1].score + this.deletePenalty),
                this.substitutionScore(xcoord, ycoord)));


        }

        private int substitutionScore(int xcoord, int ycoord)
        {
            if (this.strX[xcoord - 1] == this.strY[ycoord - 1])
            {
                return this.scores[xcoord - 1][ycoord - 1].score + 2;
            }
            else
            {
                return this.scores[xcoord - 1][ycoord - 1].score - 2;
            }

        }

        private void setBaseCases()
        {
            if ((this.strX != null) && (this.strY != null))
            {
                this.scores[0][0].setScore(0, 0, 0);
                for (int i = 1; i < strX.Length + 1; i++)
                {
                    scores[i][0].setScore(i, 0, (scores[i - 1][0].score + this.insertPenalty));
                    // cost of insertion + cost of the previous stop
                }

                for (int i = 1; i < strY.Length + 1; i++)
                {
                    scores[0][i].setScore(0, i, (scores[0][i - 1].score + this.insertPenalty));
                    // cost of insertion + cost of the previous stop
                }
            }
            this.baseCase = true;
        }

        public void print()
        {
            String textWrittenToFile = "";
            int index=0;
            for (int i = 0; i < strY.Length + 1; i++)
            {
                for (int j = 0; j < strX.Length + 1; j++)
                {
                    if (this.scores[j][i].score >= 0)
                    {
                        textWrittenToFile = textWrittenToFile + " ";
                        Console.Write(" ");
                    }
                    Console.Write(this.scores[j][i].score+"\t");
                    textWrittenToFile = textWrittenToFile + this.scores[j][i].score + "\t\t";
                }
                Console.WriteLine("\n\n");
                textWrittenToFile = textWrittenToFile + "\n\n\n";
            }
            FileStream F = new FileStream("x.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);

            File.WriteAllText(Directory.GetCurrentDirectory()+"\\Output.txt", textWrittenToFile);
        }
        static void Main(string[] args)
        {
            String str1="", str2="";
            //Console.WriteLine(Directory.GetFiles(Directory.GetCurrentDirectory()).ToString());
            //Console.WriteLine(Directory.GetCurrentDirectory().ToString());
            //string[] array2 = Directory.GetFiles(Directory.GetCurrentDirectory());
            //foreach (string name in array2)
            //{
            //    Console.WriteLine(name);
            //}

            //FileStream F = new FileStream("x.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            //File.ReadLines()
            
            str1 = File.ReadAllText("x.txt");
            str2 = File.ReadAllText("y.txt");
            //Program myProgram = new Program("ATCGAC", "CATAC");
            Program myProgram = new Program(str1, str2);
            myProgram.calculateScore();
            myProgram.print();
        }

    }
}
