using System;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace PPOIS_PROJECT
{
    public partial class Form3 : Form
    {
        public static bool mode;
        

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
            MessageBox.Show("Текст был скопирован");
        }

       

        private void button3_Click(object sender, EventArgs e)
        {


            // Получаем текст из richTextBox1
            string text = richTextBox1.Text;

            // Показываем диалог сохранения файла и получаем путь, по которому нужно сохранить файл
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Сохраняем текст в файл
                File.WriteAllText(saveFileDialog.FileName, text);

                // Выводим сообщение об успешном сохранении файла
                MessageBox.Show("Файл с модифицированным кодом сохранен по адресу:\n" + saveFileDialog.FileName);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (mode)
            {
                var dialog = new FolderBrowserDialog();
                dialog.SelectedPath = Form1.x; // установить начальную папку

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;

                    // Find the C++ file
                    var files = Directory.GetFiles(selectedPath, "*.cpp", SearchOption.AllDirectories);
                    string cppFilePath = null;
                    foreach (var file in files)
                    {
                        if (File.ReadAllText(file).Contains("switch (message)") || File.ReadAllText(file).Contains("switch(message)") || File.ReadAllText(file).Contains("switch(messg)") || File.ReadAllText(file).Contains("switch (messg)"))
                        {
                            cppFilePath = file;
                            break;
                        }
                    }
                    if (cppFilePath == null)
                    {
                        MessageBox.Show("Could not find C++ file in selected folder.");
                        return;
                    }

                    if (cppFilePath != null)
                    {
                        File.WriteAllText(cppFilePath, richTextBox1.Text, Encoding.UTF8);
                        MessageBox.Show("Текст успешно изменен");
                    }
                    else
                    {
                        MessageBox.Show("Файл не найден !");
                    }
                }
            }
            else
            {
                string headertoADD = rcFunctions.submenuDeclarationText;
                // Добавить импорт текста в файл .rc
                var dialog = new FolderBrowserDialog();
                dialog.SelectedPath = Form1.x; // установить начальную папку

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                   

                    // Найти файл .rc
                    var files = Directory.GetFiles(selectedPath, "*.rc", SearchOption.AllDirectories);
                    string rcFilePath = null;
                    foreach (var file in files)
                    {
                        if (File.ReadAllText(file).Contains("IDS_APP_TITLE"))
                        {
                            rcFilePath = file;
                            break;
                        }
                    }
                    if (rcFilePath == null)
                    {
                        MessageBox.Show("Не удалось найти файл .rc в выбранной папке.");
                        return;
                    }

                    // Заменить содержимое файла .rc на новый текст с кодировкой UTF-8
                    File.WriteAllText(rcFilePath, richTextBox1.Text, Encoding.UTF8);
                   
                    // Найти файл resource.h

                    var files2 = Directory.GetFiles(selectedPath, "Resource.h", SearchOption.AllDirectories);
                    string resourceHeaderFilePath = null;
                    foreach (var file in files2)
                    {
                        if (File.ReadAllText(file).Contains("IDS_APP_TITLE"))
                        {
                            resourceHeaderFilePath = file;
                            break;
                        }
                    }
                    if (resourceHeaderFilePath == null)
                    {
                        MessageBox.Show("Не удалось найти файл resource.h в выбранной папке.");
                        return;
                    }
                    // Заменить содержимое файла .rc на новый текст с кодировкой UTF-8
                  
                    MessageBox.Show("Текст успешно заменен в файле .rc вашего проекта.");

                    // Прочитать текущее содержимое файла resource.h
                    string resourceHeaderContent = File.ReadAllText(resourceHeaderFilePath);

                    // Найти позицию после строки "#define IDS_APP_TITLE"
                    int insertPosition = resourceHeaderContent.IndexOf("#define IDS_APP_TITLE") + "#define IDS_APP_TITLE".Length+7;

                    // Вставить содержимое переменной headertoADD после найденной позиции
                    resourceHeaderContent = resourceHeaderContent.Insert(insertPosition, headertoADD);

                    // Сохранить изменения в файле resource.h
                    File.WriteAllText(resourceHeaderFilePath, resourceHeaderContent,Encoding.UTF8);

                    rcFunctions.submenuDeclarationText = "\n";
                }
            }
        }
            private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    

