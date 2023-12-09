using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PPOIS_PROJECT
{
    public partial class Form2 : Form
    {
        public static string ToGive;
        public static List<string> PASHTET = new List<string>();
        public string RichTextBoxText { get; set; }
        void Init()
        {
            checkedListBox1.Items.Add("WM_LBUTTONDOWN");
            checkedListBox1.Items.Add("WM_RBUTTONDOWN");
            checkedListBox1.Items.Add("WM_CHAR");
            checkedListBox1.Items.Add("Добавить вызов диалогового окна через пункт меню");
           

            PASHTET.Add("WM_LBUTTONDOWN");
            PASHTET.Add("WM_RBUTTONDOWN");
            PASHTET.Add("WM_KEYDOWN");
            PASHTET.Add("Добавить вызов диалогового окна через пункт меню");
            MaximizeBox = false;

        }
        
        public Form2()
        {
            InitializeComponent();
            Init();
            Form3.mode = true;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            int position = 0;
            int position2 = 0;
            string richText = Form1.t;
            int itemCount = checkedListBox1.Items.Count;
            StringBuilder addedText = new StringBuilder();
            StringBuilder addedText2 = new StringBuilder();
            DialogResult Dres = MessageBox.Show("Хотите добавить свой текст в MessageBox?", "Добавление текста", MessageBoxButtons.YesNo);
            Form3 next = new Form3();
            // Добавление текста событий в переменную addedText
            if (richText.Contains("(messg)") || richText.Contains("message"))
            {
                for (int i = 0; i < itemCount; i++)
                {
                  
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string eventName = PASHTET[i]; // Получение текста события по индексу
                        if (eventName == "Добавить вызов диалогового окна через пункт меню")
                        {
                           
                            string menuId = Microsoft.VisualBasic.Interaction.InputBox("Введите ID пункта меню для события " + eventName + ":");
                            addedText2.Append("case " + menuId + ":\n");
                            addedText2.Append("{\n");
                            string dialogId = Microsoft.VisualBasic.Interaction.InputBox("Введите название диалогового окна для события " + eventName + ":");
                            addedText2.Append("    DialogBox(GetModuleHandle(NULL), MAKEINTRESOURCE(" + dialogId + "), hWnd, NULL);\n");
                            addedText2.Append("    break;\n");
                            addedText2.Append("}\n");
                            position2 = richText.IndexOf("switch (wmId)") + 28;
                        
                         
                            break;
                        }

                        addedText.Append("case " + eventName + ":\n");
                        addedText.Append("{\n");


            


                        if (eventName == "WM_KEYDOWN")
                        {
                            if (Dres == DialogResult.Yes)
                            {
                                string dialogId = Microsoft.VisualBasic.Interaction.InputBox("Введите название диалогового окна для события " + eventName + ":");
                                addedText.Append("    DialogBox(GetModuleHandle(NULL), MAKEINTRESOURCE(" + dialogId + "), hWnd, NULL);\n");
                            }
                            else
                            {
                                addedText.Append("MessageBox(hWnd, L\"\", L\"Заголовок\", MB_OK);\n");
                            }
                            string key = Microsoft.VisualBasic.Interaction.InputBox("Введите клавишу для события " + eventName + ":");
                            addedText.Append("    if (wParam == '" + key + "')\n");
                            addedText.Append("    {\n");
                            addedText.Append("        // Ваш обработчик для клавиши " + key + "\n");
                            addedText.Append("    }\n");
                        }
                        else
                        {
                            if (eventName != "Добавить вызов диалогового окна через пункт меню")
                            {
                                if (Dres == DialogResult.Yes)
                                {
                                    string dialogId = Microsoft.VisualBasic.Interaction.InputBox("Введите название диалогового окна для события " + eventName + ":");
                                    addedText.Append("    DialogBox(GetModuleHandle(NULL), MAKEINTRESOURCE(IDD_" + dialogId + "), hWnd, NULL);\n");
                                    addedText.Append("    MessageBox(hWnd, L\"" + Microsoft.VisualBasic.Interaction.InputBox("Введите свой текст для события " + eventName + ":") + "\", L\"Заголовок\", MB_OK);\n");
                                }
                                else
                                {
                                    addedText.Append("    MessageBox(hWnd, L\"\", L\"Заголовок\", MB_OK);\n");
                                }
                            }
                        }

                        addedText.Append("}\n");
                        addedText.Append("break;\n");
                        addedText.Append("\n");
                    }
                }

                // Вставка добавленного текста в блок кода switch (message) {
                if (richText.Contains("switch (messg)") || richText.Contains("switch (message)"))
                {
                    position = richText.IndexOf("switch (messg)") + 24;
                    if (position == 23) position = richText.IndexOf("switch (message)") + 24;
                }
                else
                {
                    position = richText.IndexOf("(messg)") + 7;
                    if (position == 6) position = richText.IndexOf("message") + 7;
                }
            }
            else
            {
                MessageBox.Show("Вероятнее всего, вы вставили некорректный код");
                return;
            }



            string result = richText;
            ToGive = result;
          
            next.richTextBox1.Text = richText;
            next.richTextBox1.SelectionStart = position;
            next.richTextBox1.SelectionLength = 0;
            next.richTextBox1.SelectionColor = Color.DarkRed;
            next.richTextBox1.SelectedText = "\n// ДОБАВЛЕННЫЕ ФУНКЦИИ : \n" + addedText;
            next.richTextBox1.SelectionColor = next.richTextBox1.ForeColor;
            next.richTextBox1.SelectionStart = position2+26;
            next.richTextBox1.SelectionLength = 0;
            next.richTextBox1.SelectionColor = Color.DarkRed;
            next.richTextBox1.SelectedText = "\n// ДОБАВЛЕННЫЕ ФУНКЦИИ : \n" + addedText2;
            next.richTextBox1.SelectionColor = next.richTextBox1.ForeColor;
            next.Show();

            MessageBox.Show("Вот ваш модифицированный код : ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (!checkedListBox1.GetItemChecked(i))
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            Update();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
