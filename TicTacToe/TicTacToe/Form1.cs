using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private List<Button> buttons = new List<Button>();
        private string[,] matrix = new string[3, 3];
        private bool sira = true;
        private bool kazanan = false;

        public Form1()
        {
            InitializeComponent();
            buttons.Add(button3);
            buttons.Add(button4);
            buttons.Add(button5);
            buttons.Add(button6);
            buttons.Add(button7);
            buttons.Add(button8);
            buttons.Add(button9);
            buttons.Add(button10);
            buttons.Add(button11);

            foreach (var button in buttons)
            {
                button.Click += Button_Click;
                button.Enabled = false;
            }

            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Font = new Font(label1.Font.FontFamily, 20);
        }


        

        private void button1_Click(object sender, EventArgs e)
        {
            ResetMatrix();
            foreach (var button in buttons)
                button.Enabled = true;

            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = true;
            button12.Enabled = true;
            button12.Visible = true;
            label3.Text = "←";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetMatrix();
            foreach (var button in buttons)
                button.Enabled = true;

            label3.Text = "←";
        }
        private void ResetMatrix()
        {
            kazanan = false;
            sira = true;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = "";
                    buttons[i * 3 + j].Text = "";
                    buttons[i * 3 + j].BackColor = SystemColors.Control;
                    buttons[i * 3 + j].Enabled = true;
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (kazanan) return;

            Button b = sender as Button;
            int index = buttons.IndexOf(b);
            int satir = index / 3;
            int sutun = index % 3;

            if (matrix[satir, sutun] != "") return;

            if (sira)
            {
                b.Text = "X";
                matrix[satir, sutun] = "X";
                sira = false;
                label3.Text = "→";
            }
            else
            {
                b.Text = "O";
                matrix[satir, sutun] = "O";
                sira = true;
                label3.Text = "←";
            }

            if (BulKazanan())
            {
                label3.TextAlign = ContentAlignment.MiddleCenter;
                label3.Font = new Font(label3.Font.FontFamily, 15);
                label3.Text = $"Winner: {(sira ? "Player 2" : "Player 1")}";
                KazananRenk();
                foreach (var btn in buttons)
                    btn.Enabled = false;
            }
            else
            {
                BeraberlikKontrol();
            }
        }

        private bool BulKazanan()
        {
            for (int i = 0; i < 3; i++)
            {
                if (matrix[i, 0] != "" && matrix[i, 0] == matrix[i, 1] && matrix[i, 1] == matrix[i, 2])
                {
                    kazanan = true;
                    return true;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                if (matrix[0, j] != "" && matrix[0, j] == matrix[1, j] && matrix[1, j] == matrix[2, j])
                {
                    kazanan = true;
                    return true;
                }
            }

            if (matrix[0, 0] != "" && matrix[0, 0] == matrix[1, 1] && matrix[1, 1] == matrix[2, 2])
            {
                kazanan = true;
                return true;
            }

            if (matrix[0, 2] != "" && matrix[0, 2] == matrix[1, 1] && matrix[1, 1] == matrix[2, 0])
            {
                kazanan = true;
                return true;
            }

            return false;
        }

        private void BeraberlikKontrol()
        {
            foreach (var btn in buttons)
            {
                if (btn.Text == "")
                    return;
            }

            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Font = new Font(label3.Font.FontFamily, 16);
            label3.Text = "Berabere";

            foreach (var btn in buttons)
                btn.Enabled = false;
        }

        private void KazananRenk()
        {
            Color color = sira ? Color.Red : Color.Green;

            for (int i = 0; i < 3; i++)
            {
                if (matrix[i, 0] != "" && matrix[i, 0] == matrix[i, 1] && matrix[i, 1] == matrix[i, 2])
                {
                    for (int j = 0; j < 3; j++)
                        buttons[i * 3 + j].BackColor = color;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                if (matrix[0, j] != "" && matrix[0, j] == matrix[1, j] && matrix[1, j] == matrix[2, j])
                {
                    for (int i = 0; i < 3; i++)
                        buttons[i * 3 + j].BackColor = color;
                }
            }

            if (matrix[0, 0] != "" && matrix[0, 0] == matrix[1, 1] && matrix[1, 1] == matrix[2, 2])
            {
                buttons[0].BackColor = color;
                buttons[4].BackColor = color;
                buttons[8].BackColor = color;
            }

            if (matrix[0, 2] != "" && matrix[0, 2] == matrix[1, 1] && matrix[1, 1] == matrix[2, 0])
            {
                buttons[2].BackColor = color;
                buttons[4].BackColor = color;
                buttons[6].BackColor = color;
            }

            foreach (var btn in buttons)
                btn.Enabled = false;
        }
    }

}