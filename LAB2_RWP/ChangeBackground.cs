using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB2_RWP
{
    public partial class ChangeBackground : Form
    {
        int styleType = 0;
        Color colorRGB = Color.White;
        

        public Color ColorRGB
        {
            get { return colorRGB; }
            set { colorRGB = value; }
        }

        public int Style
        {
            get { return styleType; }
        }

        public string FileName
        {
            get;
            set;
        }

        public string TextBackGround
        {
            get { return textBacgroundBox.Text; }
        }

        public ChangeBackground()
        {
            InitializeComponent();
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonOK.DialogResult = DialogResult.OK;

        }

//-------------BUTTON---------------------------------------------------------------------------------------------------------------------------
        

        private void TextBoxVisible(bool visible)
        {
            textBacgroundBox.Visible = visible;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             switch(comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        styleType = 1;
                        TextBoxVisible(false);
                        ColorDialog bgColor = new ColorDialog();
                        if (bgColor.ShowDialog() == DialogResult.OK)
                        {
                            if (bgColor.Color != Color.Black) { colorRGB = bgColor.Color; }
                        }
                        break;
                    }
                case 1:
                    {
                        styleType = 4;
                        TextBoxVisible(false);
                        break;
                    }
                case 2:
                    {
                        styleType = 3;
                        TextBoxVisible(false);
                        break;
                    }
                case 3:
                    {
                        styleType = 2;
                        TextBoxVisible(false);
                        OpenFileDialog file = new OpenFileDialog();
                        if(file.ShowDialog() == DialogResult.OK)
                        {
                            FileName = file.FileName;
                            MessageBox.Show(FileName);
                            
                        }
                        break;
                    }
                case 4:
                    {
                        styleType = 5;
                        TextBoxVisible(true);
                        break;
                    }
                case 5:
                    {
                        styleType = 6;
                        TextBoxVisible(false);
                        break;
                    }
            }
        }
    }
}
