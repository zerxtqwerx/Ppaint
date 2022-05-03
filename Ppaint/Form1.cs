using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Ppaint
{
    public partial class Ppaint : Form
    {
        public Ppaint()
        {
            InitializeComponent();
            SetSize();

        }
        //Variables//

        private bool IsBrush = false;
        private bool IsEraser = false;

        private ArrayPoints arrayPoints = new ArrayPoints(2);

        Rectangle rectangle = Screen.PrimaryScreen.Bounds; 
        Bitmap map = new Bitmap(100, 100);

        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f);
        SoundPlayer Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\sewer-coin.wav");


        ///////////////////Background functions///////////////////
        //Set Image Size 
        private void SetSize()
        {
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        //Array Points for Drawing with a Pen
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;
            public ArrayPoints(int size)
            {
                if (size <= 0) { size = 2; }
                points = new Point[size];
            }
            public void SetPoint(int x, int y)
            {
                if (index >= points.Length) { index = 0; }

                points[index] = new Point(x, y);
                index++;
            }

            public void ResetPoints()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }
        ///////////////////////////////////////////////////////
        

        ///////////////////////Interface///////////////////////

        //Pallette 1
        private void colorWheel1_ColorChanged(object sender, EventArgs e)
        {
            pen.Color = colorWheel1.Color;
        }

        //Pallette 2//
        private void colorEditor1_ColorChanged(object sender, EventArgs e)
        {
            pen.Color = colorEditor1.Color;
        }
        //Saved Color Panel//
        private void SavedColor1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.White)
            {
                ((Button)sender).BackColor = pen.Color;
            }
            if (((Button)sender).BackColor != Color.White)
            {
                pen.Color = ((Button)sender).BackColor;
            }

        }
        //Drawing Tools (basic on Pen)//
        //Pen//
        private void Brush_Click(object sender, EventArgs e)
        {
            pen.Width = SizeTrackBar.Value;
            pen.Color = colorWheel1.Color;
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            IsBrush = true;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsBrush) { return; }
            arrayPoints.SetPoint(e.X, e.Y);
            if (arrayPoints.GetCountPoints() >= 2)
            {
                graphics.DrawLines(pen, arrayPoints.GetPoints());
                pictureBox0.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);
            }
        }
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            IsBrush = false;
            arrayPoints.ResetPoints();
        }

        //Eraser//
        private void Eraser_Click(object sender, EventArgs e)
        {
            pen.Width = SizeTrackBar.Value;
            pen.Color = Color.White;
        }
        private void Eraser_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsEraser) { return; }
            pen.Color = Color.White;
        }

        private void Eraser_MouseUp(object sender, MouseEventArgs e)
        {
            IsEraser = false;
            pen.Color = colorWheel1.Color;
        }

        //Size TrackBar//
        private void SizeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = SizeTrackBar.Value;
        }

        //Fill Lawyer//
        private void FillLawyer_Click(object sender, EventArgs e)
        {
            pen.Width = 5000;
            graphics.DrawLines(pen, arrayPoints.GetPoints());
            pictureBox0.Image = map;
            arrayPoints.ResetPoints();
        }
        ////////////////////////////
       
        //Delete Lawyer//
        private void DeleteLawyer_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox0.BackColor);
            pictureBox0.Image = map;
            pen.Width = SizeTrackBar.Value;
        }

        //Save Pictures//
        private void Save_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox0.Image != null)
                {
                    pictureBox0.Image.Save(saveFileDialog1.FileName);
                }
            }
        }

        ////////Music Panel//////////

        //Play Button//
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Play.Checked)
            {
                Play.Text = "Stop!";
                Sound.Play();
            }
            else
            {
                Play.Text = "Play!";
                Sound.Stop();
            }
        }
        //Sound buttons//
        private void SewerCoin_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\sewer-coin.wav");
        }

        private void River_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\river-cargo.wav");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\rtty.wav");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\building-work.wav");
        }

        private void Frogs_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\dersinnsspace_screaming-frogs-background-arrangement.wav");
        }

        private void Sewer_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\inspectorj_sewer.wav");
        }

        private void Garbage_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\noisy-garbage.wav");
        }

        private void Parking1_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\parking-garage-wind-1.wav");
        }

        private void Parking2_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\parking-garage-wind-2.wav");
        }

        private void TripRadio_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\trp_radio-frequency.wav");
        }

        private void Noise2_Click(object sender, EventArgs e)
        {
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\white-noise-low-colour-a.wav");
        }

        private void Engine_Click(object sender, EventArgs e)
        { 
            Sound = new SoundPlayer($"{Environment.CurrentDirectory}\\music\\engine-vibrations.wav");
        }
        ///////////////////////////////

        //Picture Boxes//
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP; *.JPG; *GIF; *PNG)|*.BMP; .JPG; *GIF; *PNG;|All files (*.*)|*.*";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("NO. You can't.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP; *.JPG; *GIF; *PNG)|*.BMP; .JPG; *GIF; *PNG;|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox2.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("NO. You can't.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP; *.JPG; *GIF; *PNG)|*.BMP; .JPG; *GIF; *PNG;|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox3.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("NO. You can't.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP; *.JPG; *GIF; *PNG)|*.BMP; .JPG; *GIF; *PNG;|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox4.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("NO. You can't.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP; *.JPG; *GIF; *PNG)|*.BMP; .JPG; *GIF; *PNG;|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox5.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("NO. You can't.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        ///////////////////////////
        
        //Copyright//
        private void Copyright_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Noise 1: 'White Noise, Low Colour, A.wav' by InspectorJ(www.jshaw.co.uk) of Freesound.org" +
                "\r\n\r\nSewer: 'Sewer Soundscape, A.wav' by InspectorJ(www.jshaw.co.uk) Freesound.org", "Some copyright", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        /////////////////////////////////////////////////////////////
    }
}
