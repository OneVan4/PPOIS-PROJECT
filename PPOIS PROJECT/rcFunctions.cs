using Microsoft.VisualBasic;
using System;
using System.Text;
using System.Windows.Forms;


namespace PPOIS_PROJECT
{


    public partial class rcFunctions : Form
    {

        public static string submenuDeclarationText = "\n";
        static int id_Dialog = 1320; 
        public rcFunctions()
        {
            InitializeComponent();
            checkedListBox713.Items.Add("Изменить название приложения");
            checkedListBox713.Items.Add("Добавить диалоговое окно");
            checkedListBox713.Items.Add("Настроить меню");
            Form3.mode = false;
        }

        private void checkedListBox713_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox713.Items.Count; i++)
            {
                if (!checkedListBox713.GetItemChecked(i))
                {
                    checkedListBox713.SetItemChecked(i, true);
                }
            }
            Update();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string richText = Form1.t2;

            int itemCount = checkedListBox713.Items.Count;
            StringBuilder addedText = new StringBuilder();

            if (checkedListBox713.CheckedItems.Contains("Изменить название приложения"))
            {
                string newProjectName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое имя проекта:");
                string rcFileContents = richText;

                // Определение целевой строки из файла rc
                string targetString = "IDS_APP_TITLE";
                int targetStringStart = rcFileContents.IndexOf(targetString);
                if (targetStringStart >= 0)
                {
                    targetStringStart = rcFileContents.IndexOf("\"", targetStringStart) + 1;
                    int targetStringEnd = rcFileContents.IndexOf("\"", targetStringStart);
                    if (targetStringEnd >= 0)
                    {
                        string targetValue = rcFileContents.Substring(targetStringStart, targetStringEnd - targetStringStart);

                        // Замена целевой строки на новое значение
                        rcFileContents = rcFileContents.Replace(targetValue, newProjectName);

                        richText = rcFileContents;

                    }
                }
            }

            if (checkedListBox713.CheckedItems.Contains("Добавить диалоговое окно"))
            {
                
                string dialogName = Microsoft.VisualBasic.Interaction.InputBox("Введите имя для диалогового окна:");
                bool addDialogButton = MessageBox.Show("Хотите добавить кнопку в диалоговое окно?", "Добавить кнопку", MessageBoxButtons.YesNo) == DialogResult.Yes;
                id_Dialog+=13;
                addedText.AppendLine();
                addedText.AppendLine("IDD_" + dialogName.ToUpper() + " DIALOGEX 0, 0, 200, 150");
                addedText.AppendLine("STYLE DS_SETFONT | DS_MODALFRAME | DS_FIXEDSYS | WS_POPUP | WS_CAPTION | WS_SYSMENU");
                addedText.AppendLine("CAPTION \"" + dialogName + "\"");
                addedText.AppendLine("FONT 8, \"MS Shell Dlg\", 0, 0, 0x1");
                addedText.AppendLine("BEGIN");
                submenuDeclarationText += "#define  IDD_" + dialogName.ToUpper()+" "+id_Dialog.ToString()+"\n";
                if (addDialogButton)
                {
                    addedText.AppendLine("    PUSHBUTTON      \"Кнопка\", 1001, 70, 70, 60, 20");
                }

                addedText.AppendLine("END");

                // Добавление кода диалогового окна в файл ресурсов
                richText += addedText.ToString();

            }

            if (checkedListBox713.CheckedItems.Contains("Настроить меню"))
            {
                int newMenuItemCount = 0;
                if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите количество новых пунктов меню:"), out newMenuItemCount))
                {
                    MessageBox.Show("Некорректное значение. Пожалуйста, введите целое число.");
                    return;
                }

                StringBuilder menuText = new StringBuilder();

                for (int i = 0; i < newMenuItemCount; i++)
                {
                    string menuItemName = Microsoft.VisualBasic.Interaction.InputBox($"Введите имя нового пункта меню {i + 1}:");
                    int subMenuItemCount = 0;
                    bool isSubMenu = false;

                    while (true)
                    {
                        DialogResult dialogResult = MessageBox.Show($"Пункт меню \"{menuItemName}\" является подменю?", "Подменю", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            isSubMenu = true;
                            if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox($"Введите количество пунктов подменю для \"{menuItemName}\":"),
                                out subMenuItemCount))
                            {
                                MessageBox.Show("Некорректное значение. Пожалуйста, введите целое число.");
                                continue;
                            }
                        }
                        else
                        {
                            int submenuID = i + 100;
                            string submenuIDString = $"IDM_{menuItemName.ToUpper()}";
                            submenuDeclarationText += $"#define {submenuIDString} {submenuID}\n";
                        }
                        break;

                    }

                    if (!string.IsNullOrEmpty(menuItemName)) // Skip empty menu item names
                    {
                        menuText.AppendLine();
                        string menuItemID = $"IDM_{menuItemName.ToUpper()}";
                        if (isSubMenu)
                        {
                            StringBuilder subMenuText = new StringBuilder();
                            AddSubMenu(subMenuText, menuItemName, subMenuItemCount);
                            menuText.AppendLine($"POPUP \"&{menuItemName}\"");
                            menuText.AppendLine($"BEGIN");
                            menuText.Append(subMenuText.ToString());
                            menuText.AppendLine($"END");
                        }
                        else
                        {
                            menuItemID = $"IDM_{menuItemName.ToUpper()}";
                            menuText.AppendLine($"MENUITEM \"&{menuItemName}\", {menuItemID}");
                        }
                    }
                }
                string menuText3 = "IDC_ MENU\nBEGIN\n";
                string userInput = Interaction.InputBox("Введите идентификатор для меню:", "Изменение идентификатора", "PPOISTEST");
                string newMenuText = menuText3.Insert(4, userInput);
                menuText.Insert(0, newMenuText); // Add menu declaration at the beginning
                menuText.AppendLine("END"); // Add menu closing at the end

                // Create a new resource file and save the menu text


                richText = richText.Insert(richText.Length, menuText.ToString());


             
            }
            Form3 next = new Form3();
            next.richTextBox1.Text = richText;
            next.Show();
        }

        private void AddSubMenu(StringBuilder menuText, string menuItemName, int subMenuItemCount)
        {
            for (int j = 0; j < subMenuItemCount; j++)
            {
                string subMenuItemName = Microsoft.VisualBasic.Interaction.InputBox($"Введите имя пункта подменю {j + 1} для \"{menuItemName}\":");
                int submenuID = j + 1;

                // Добавить возможность создания веток подменю
                DialogResult dialogResult = MessageBox.Show($"Пункт меню \"{subMenuItemName}\" имеет ветку подменю?", "Ветка подменю", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    menuText.AppendLine($"    POPUP \"&{subMenuItemName}\"");
                    menuText.AppendLine($"    BEGIN");
                    int subSubMenuItemCount = 0;
                    if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox($"Введите количество пунктов ветки подменю для \"{subMenuItemName}\":"),
                        out subSubMenuItemCount))
                    {
                        MessageBox.Show("Некорректное значение. Пожалуйста, введите целое число.");
                        continue;
                    }

                    StringBuilder subSubMenuText = new StringBuilder();
                    AddSubMenu(subSubMenuText, subMenuItemName, subSubMenuItemCount); // Рекурсивный вызов для создания вложенных веток подменю
                    menuText.Append(subSubMenuText.ToString());
                    menuText.AppendLine($"    END");
                }
                else
                {
                    string submenuIDString = $"IDM_{menuItemName.ToUpper()}_{submenuID}";
                    menuText.AppendLine($"    MENUITEM \"&{subMenuItemName}\", {submenuIDString}");
                    submenuDeclarationText += $"#define {submenuIDString} {submenuID}\n";
                }
            }
        }
        private void rcFunctions_Load(object sender, EventArgs e)
        {

        }
    }
}
