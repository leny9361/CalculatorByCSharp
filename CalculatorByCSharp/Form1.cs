using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorByCSharp
{
    public partial class Form1 : Form
    {
        StringBuilder[] num = new StringBuilder[2];
        int currentIndex = 0; // 当前输入的数字索引
        string operatorLast = ""; // 当前运算符
        TextBox showBox = null;
        Panel panel = null;
        public Form1()
        {
            InitializeComponent();
            InitUI();
            this.KeyDown += MainForm_KeyDown;
        }
        private string GetMathKeyOutput(Keys key, bool shiftPressed)
        {
            switch (key)
            {
                // 数字键
                case Keys.D0:
                case Keys.NumPad0:
                    return shiftPressed ? ")" : "0";
                case Keys.D1:
                case Keys.NumPad1:
                    return "1";
                case Keys.D2:
                case Keys.NumPad2:
                    return shiftPressed ? "@" : "2";
                case Keys.D3:
                case Keys.NumPad3:
                    return shiftPressed ? "#" : "3";
                case Keys.D4:
                case Keys.NumPad4:
                    return shiftPressed ? "$" : "4";
                case Keys.D5:
                case Keys.NumPad5:
                    return shiftPressed ? "%" : "5";
                case Keys.D6:
                case Keys.NumPad6:
                    return shiftPressed ? "^" : "6";
                case Keys.D7:
                case Keys.NumPad7:
                    return shiftPressed ? "&" : "7";
                case Keys.D8:
                case Keys.NumPad8:
                    return shiftPressed ? "×" : "8";
                case Keys.D9:
                case Keys.NumPad9:
                    return shiftPressed ? "(" : "9";

                // 小数点
                case Keys.Decimal:
                case Keys.OemPeriod:
                    return ".";

                // 数学运算符
                case Keys.Add:
                case Keys.Oemplus:
                    return shiftPressed ? "+" : "+";
                case Keys.Subtract:
                case Keys.OemMinus:
                    return "-";
                case Keys.Multiply:
                    return "×";
                case Keys.Divide:
                case Keys.OemQuestion:
                    return "÷";
                case Keys.Enter:
                    return "=";
                case Keys.Back:
                    return "←";
                // 其他键忽略
                default:
                    return null;
            }
        }
        private void InitUI()
        {
            this.Text = "Calculator by C#";
            this.Size = new Size(300, 400);
            this.MaximumSize = this.Size;
            this.MaximizeBox = false;
            panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.KeyDown += MainForm_KeyDown;
            panel.TabIndex = 1;
            this.Controls.Add(panel);
            //按钮和文本框初始化
            string[] btnArr = new string[] { "CE", "C", "←", "÷", "7", "8", "9", "×", "4", "5", "6", "-", "1", "2", "3", "+", "±", "0", ".", "=" };
            Button button;
            int index = 0;
            int btnWith = 65, btnHeight = 50;
            foreach (var btnStr in btnArr)
            {
                button = new Button();
                button.Location = new Point(6 + index % 4 * btnWith, 100 + 50 * (index / 4));
                button.Name = btnStr;
                button.Text = btnStr;
                button.Size = new Size(btnWith, btnHeight);
                button.UseVisualStyleBackColor = true;
                button.Click += Button_Click;
                button.KeyDown += MainForm_KeyDown;
                panel.Controls.Add(button);
                index++;
            }
            showBox = new TextBox();
            showBox.Location = new Point(6, 6);
            showBox.ReadOnly = true;
            showBox.Multiline = true;
            showBox.TextAlign = HorizontalAlignment.Right;
            showBox.Font = new Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            showBox.Size = new Size(6 + 4 * btnWith, 70);
            showBox.KeyDown += MainForm_KeyDown;
            panel.Controls.Add(showBox);
            num[0] = new StringBuilder();
            num[1] = new StringBuilder();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            panel.Focus();
            var btn = sender as Button;
            string operateOrNum = btn.Text;
            ProcessButtonClick(operateOrNum);
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string operateOrNum = GetMathKeyOutput(e.KeyCode, false);
            ProcessButtonClick(operateOrNum);
        }
        private void ProcessButtonClick(string operateOrNum)
        {
            switch (operateOrNum)
            {
                case "CE":
                    //清除输入
                    break;
                case "C":
                    //清除所有
                    operatorLast = string.Empty;
                    currentIndex = 0;
                    num[0].Clear();
                    num[1].Clear();
                    showBox.Text = string.Empty;
                    break;
                case "←":
                    if (operatorLast == "=" || num[currentIndex].Length < 1)
                    {
                        break;
                    }
                    //删除最后一个字符
                    num[currentIndex].Length -= 1;
                    showBox.Text = num[currentIndex].ToString();
                    break;
                case "÷":
                case "×":
                case "-":
                case "+":
                    //处理运算符
                    currentIndex = 1;
                    if (num[0].Length > 0 && num[1].Length > 0)
                    {
                        decimal num1 = decimal.Parse(num[0].ToString());
                        decimal num2 = decimal.Parse(num[1].ToString());
                        num[0].Clear();
                        num[1].Clear();
                        switch (operateOrNum)
                        {
                            case "+":
                                num[0].Append(num1 + num2);
                                break;
                            case "-":
                                num[0].Append(num1 - num2);
                                break;
                            case "×":
                                num[0].Append(num1 * num2);
                                break;
                            case "÷":
                                num[0].Append(num1 / num2);
                                break;
                        }
                        showBox.Text = num[0].ToString();
                    }
                    operatorLast = operateOrNum;
                    break;
                case "=":
                    //计算结果
                    if (num[0].Length > 0 && num[1].Length > 0)
                    {
                        decimal num1 = decimal.Parse(num[0].ToString());
                        decimal num2 = decimal.Parse(num[1].ToString());
                        num[0].Clear();
                        num[1].Clear();
                        switch (operatorLast)
                        {
                            case "+":
                                num[0].Append(num1 + num2);
                                break;
                            case "-":
                                num[0].Append(num1 - num2);
                                break;
                            case "×":
                                num[0].Append(num1 * num2);
                                break;
                            case "÷":
                                num[0].Append(num1 / num2);
                                break;
                        }
                        showBox.Text = num[0].ToString();
                    }
                    else
                    {
                        showBox.Text = num[0].ToString();
                        operatorLast = string.Empty;
                    }
                    currentIndex = 0;
                    operatorLast = operateOrNum;
                    break;
                default:
                    //处理数字和小数点
                    if (operatorLast == "=")
                    {
                        //如果上次操作是等于号，则清空当前输入
                        num[0].Clear();
                        num[1].Clear();
                        currentIndex = 0;
                        operatorLast = string.Empty;
                    }
                    if (operateOrNum == "." && num[currentIndex].ToString().Contains("."))
                        break;
                    num[currentIndex].Append(operateOrNum);
                    showBox.Text = num[currentIndex].ToString();
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
