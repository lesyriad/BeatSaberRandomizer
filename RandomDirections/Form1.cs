using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace RandomDirections
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "Level files|*.json";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream myStream;
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        using (var sr = new StreamReader(myStream))
                        {
                            // Insert code to read the stream here.
                            var filedata = sr.ReadToEnd();
                            var data = new JavaScriptSerializer().Deserialize<Level>(filedata);
                            //loop through notes
                            CutDirection lastRed = CutDirection.ANY;
                            CutDirection lastBlue = CutDirection.ANY;
                            Note lastNote = null;
                            double lastRedTime = 0.0;
                            double lastBlueTime = 0.0;
                            double timeDifference = double.Parse(timedifferenceTextBox.Text);
                            int seed = seedTextBox.Text == "" ? 0 : int.Parse(seedTextBox.Text);
                            bool position = randomPositionCheckBox.Checked;
                            Random rand = new Random(seed);
                            foreach (var note in data._notes)
                            {
                                //add random direction
                                if (note._type == NoteColor.BLUE)
                                {
                                    
                                    if (lastBlue == CutDirection.ANY)
                                    {
                                       
                                            int n = rand.Next(0, 9);
                                            lastBlue = (CutDirection) n;
                                            note._cutDirection = (CutDirection) n;
                                       

                                    }
                                    else if (lastBlueTime <= note._time - timeDifference)
                                    {
                                       
                                            if (lastNote._time >= note._time-.05)
                                            {
                                                //we need the same cut direction and we need it to follow the positions
                                                //right now make it go left or right
                                                int n = rand.Next(0, 2);
                                                lastBlue = (CutDirection)n+2;
                                                note._cutDirection = (CutDirection) n+2;
                                                //also make the previous note the same
                                                lastNote._cutDirection = (CutDirection) n+2;
                                            }
                                            else
                                            {
                                                int n = rand.Next(0, 9);
                                                lastBlue = (CutDirection)n;
                                                note._cutDirection = (CutDirection) n;
                                            }
                                            
                                        
                                    }
                                    else
                                    {
                                        note._cutDirection = lastBlue;

                                    }

                                   
                                    lastBlueTime = note._time;
                                }
                                else if (note._type == NoteColor.RED)
                                {
                                    if (lastRed == CutDirection.ANY)
                                    {
                                       
                                            int n = rand.Next(0, 9);
                                            lastRed = (CutDirection) n;
                                            note._cutDirection = (CutDirection) n;
                                       

                                    }
                                    else if (lastRedTime <= note._time - timeDifference)
                                    {
                                        
                                        
                                            if (lastNote._time >= note._time-.05)
                                            {
                                                //we need the same cut direction and we need it to follow the positions
                                                //right now make it go left or right
                                                int n = rand.Next(0, 2);
                                                lastRed = (CutDirection)n+2;
                                                note._cutDirection = (CutDirection) n+2;
                                                //also make the previous note the same
                                                lastNote._cutDirection = (CutDirection) n+2;
                                            }
                                            else
                                            {
                                                int n = rand.Next(0, 9);
                                                lastRed = (CutDirection)n;
                                                note._cutDirection = (CutDirection) n;
                                            }
                                            
                                        
                                    }
                                    else
                                    {
                                        note._cutDirection = lastRed;

                                    }

                                   
                                    lastRedTime = note._time;
                                }

                                if (lastNote?._time >= note._time - .05)
                                {
                                    //change the direction to left or right
                                    if (lastNote._type == note._type)
                                    {
                                        int dir = rand.Next(0, 2);
                                        note._cutDirection = (CutDirection) dir + 2;
                                        lastNote._cutDirection = note._cutDirection;
                                    }
                                    else
                                    {
                                        //change the direction to up or down
                                        int dir = rand.Next(0, 2);
                                        note._cutDirection = (CutDirection) dir;
                                        lastNote._cutDirection = note._cutDirection;
                                    }
                                }
                                lastNote = note;
                            }
                            //output the file
                            StreamWriter sw = new StreamWriter(theDialog.FileName + ".new");
                            string d2 = new JavaScriptSerializer().Serialize(data);
                            sw.Write(d2);
                            sw.Flush();
                            sw.Close();
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
