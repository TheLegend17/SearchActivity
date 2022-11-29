using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchActivity
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<Button> btns;
        List<int> path;
        List<int> disabledNumbers;
        IDictionary<int, int> fridge;
        Queue<int> myqueue;  
        int startState, goalState;
        public Form1()
        {
            InitializeComponent();
            btns = new List<Button>();
            path = new List<int>();
            disabledNumbers = new List<int>();
            btns = Controls.OfType<Button>().ToList();
            myqueue = new Queue<int>();
            fridge = new Dictionary<int, int>();
        }

        public void ChangeColorBtn(Color color)
        {
            foreach (Button b in btns)
            {
                b.Click += (send, args) =>
                {
                    if (!b.Equals(findPath) && !b.Equals(resetBtn))
                        b.BackColor = color;
                   
                };
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeColorBtn(Color.Black);
        }

        private void UnableButton_CheckedChanged(object sender, EventArgs e)
        {
            ChangeColorBtn(Color.White);
        }

        public Button ButtonGetter(String name)
        {
            foreach (var btn in btns)
            { 
                if(btn.Text.Equals(name))
                    return btn;
            }
            return null;
        }

        private void findPath_Click(object sender, EventArgs e)
        {
            //Get all the disabled numbers
            foreach (var btn in btns)
            {
                if (btn.BackColor == Color.Black)
                { 
                    int temp = Convert.ToInt32(btn.Text);
                    disabledNumbers.Add(temp);
                }
            }
            //Make sure that goal and start text has value if not display this message
            if (goalStateText.Text.Equals("") == true || startStateText.Text.Equals("") == true)
            {
                string message = "Please input start and goal state";
                string title = "Warning";
                MessageBox.Show(message, title);
                return;
            }
            //Convert the start and goal text into integer
            startState = Convert.ToInt32(startStateText.Text);
            goalState = Convert.ToInt32(goalStateText.Text);
            Console.WriteLine("Finding Path");
            int currentvalue = startState;
            while (true)
            {
                //Get the UpperValue
                if (currentvalue > 9) // If it has upperValue add to fridge
                {
                    //Check if fridge already contain this value
                    if (fridge.Keys.Contains(currentvalue - 7) == false)
                    {
                        if (disabledNumbers.Contains(currentvalue - 7) == false)
                        {
                            var btn = ButtonGetter("" + (currentvalue - 7));
                            if (btn.BackColor.Equals(Color.Black) == false)
                            {
                                fridge.Add(currentvalue - 7, currentvalue);
                                myqueue.Enqueue(currentvalue - 7);
                                Console.WriteLine("UpValueState = " + (currentvalue - 7) + " - Origin = " + (currentvalue));
                            }
                        }
                        if (currentvalue - 7 == goalState)
                        {
                            Console.WriteLine("Goal State Found");
                            pathGetter();
                            break;
                        }
                    }
                    
                }
                // else do nothing

                //Get the downvalue
                if (currentvalue < 42) // if currentValue is greaterthan 42 it has no downValue
                {
                    //Check if fridge already contain this value
                    if (fridge.Keys.Contains(currentvalue + 7) == false)
                    {
                        if (disabledNumbers.Contains(currentvalue + 7) == false)
                        {
                            fridge.Add(currentvalue + 7, currentvalue);
                            myqueue.Enqueue(currentvalue + 7);
                            Console.WriteLine("Downvalue State = " + (currentvalue + 7) + " - Origin = " + currentvalue);
                        }
                        if (currentvalue + 7 == goalState)
                        {
                            Console.WriteLine("Goal State Found");
                            pathGetter();
                            break;
                        }
                    }
                }
                // else do nothing

                //Get the left value
                //If it is not in the left side of the matrix
                //if((currentvalue-1).Equals(1) == false  || (currentvalue - 1).Equals(8) == false || (currentvalue - 1).Equals(15) == false  || (currentvalue - 1).Equals(22) == false || (currentvalue - 1).Equals(29) == false  || (currentvalue - 1).Equals(36) == false || (currentvalue - 1).Equals(43) == false )
                if (currentvalue.Equals(1) || currentvalue.Equals(8) || currentvalue.Equals(15) || currentvalue.Equals(22) || currentvalue.Equals(29) || currentvalue.Equals(36) || currentvalue.Equals(43))
                {
                    //Do Nothing
                }
                else
                {
                    //Check if fridge already contain this value
                    if (fridge.Keys.Contains(currentvalue - 1) == false)
                    {
                        if (disabledNumbers.Contains(currentvalue - 1) == false)
                        {
                            fridge.Add(currentvalue - 1, currentvalue);
                            myqueue.Enqueue(currentvalue - 1);
                            Console.WriteLine("LeftValue State = " + (currentvalue - 1) + " - Origin = " + currentvalue);
                        }
                        if (currentvalue - 1 == goalState)
                        {
                            Console.WriteLine("Goal State Found");
                            pathGetter();
                            break;
                        }
                    }
                }
                // else do nothing 

                //Get the right value
                //If it is not in the right side of the matrix
                //if ((currentvalue + 1).Equals(7) == false || (currentvalue + 1).Equals(14) == false || (currentvalue + 1).Equals(21) == false || (currentvalue + 1).Equals(28) == false || (currentvalue + 1).Equals(35) == false  || (currentvalue + 1).Equals(42) == false || (currentvalue + 1).Equals(49) == false)
                if (currentvalue.Equals(7) || currentvalue.Equals(14) || currentvalue.Equals(21) || currentvalue.Equals(28) || currentvalue.Equals(35) || currentvalue.Equals(42) || currentvalue.Equals(49))
                {
                    //Do Nothing
                }
                else
                {
                    //Check if fridge already contain this value
                    if (fridge.Keys.Contains(currentvalue + 1) == false)
                    {
                        if (disabledNumbers.Contains(currentvalue + 1) == false)
                        {
                            fridge.Add(currentvalue + 1, currentvalue);
                            myqueue.Enqueue(currentvalue + 1);
                            Console.WriteLine("RightValue State = " + (currentvalue + 1) + " - Origin = " + currentvalue);
                        }
                        if (currentvalue + 1 == goalState)
                        {
                            Console.WriteLine("Goal State Found");
                            pathGetter();
                            break;
                        }
                    }
                }
                try
                {
                    currentvalue = myqueue.Dequeue();
                }
                catch (Exception)
                {
                    string message = "Cannot find path";
                    string title = "Error";
                    MessageBox.Show(message, title);
                    resetBtn_Click();
                    return;
                }
            }
            Button s = ButtonGetter("" + startState);
            Button g = ButtonGetter("" + goalState);
            s.BackColor = Color.Green;
            g.BackColor = Color.Red;
            DisableButton.Enabled = false;
            UnableButton.Enabled = false;
            startStateText.Enabled = false;
            goalStateText.Enabled = false;
        }

        public void pathGetter()
        {
            int start = fridge[goalState];
            int temp = start;
            path.Add(start);
            while (temp != startState)
            {
                temp = fridge[temp];
                path.Add(temp);
            }
            foreach (var p in path)
            {
                Console.WriteLine(p);
            }
            DrawPath();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            //Set all the button colors to white
            foreach (Button b in btns)
            {
               b.BackColor= Color.White;
            }
            findPath.BackColor = Color.Cyan;
            resetBtn.BackColor = Color.Cyan;    
            path.Clear();
            fridge.Clear();
            myqueue.Clear();
            disabledNumbers.Clear();
            startStateText.Text = "";
            goalStateText.Text = "";
            DisableButton.Checked = false;
            UnableButton.Checked = false;
            DisableButton.Enabled = true;
            UnableButton.Enabled = true;
            startStateText.Enabled = true;
            goalStateText.Enabled = true;
        }

        private void resetBtn_Click()
        {
            //Set all the button colors to white
            foreach (Button b in btns)
            {
                b.BackColor = Color.White;
            }
            findPath.BackColor = Color.Cyan;
            resetBtn.BackColor = Color.Cyan;
            path.Clear();
            fridge.Clear();
            myqueue.Clear();
            disabledNumbers.Clear();
            startStateText.Text = "";
            goalStateText.Text = "";
            DisableButton.Checked = false;
            UnableButton.Checked = false;
            DisableButton.Enabled = true;
            UnableButton.Enabled = true;
            startStateText.Enabled = true;
            goalStateText.Enabled = true;
        }

        public void DrawPath()
        {
            foreach (var p in path)
            {
                foreach (var b in btns)
                {
                    if (b.Text.Equals("" + p))
                    {
                        b.BackColor = Color.Maroon;
                    }
                }
            }
        }

    }
}
