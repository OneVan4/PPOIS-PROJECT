using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PPOIS_PROJECT
{
   
    public partial class Form1 : Form
    {

        public static string t;
        public static string x;
        public static string t2;
        public static string x2;


        public Form1()
        {
            InitializeComponent();
            MaximizeBox= false;
            if (File.Exists("RepositoryPath.txt"))
            {
                using (StreamReader sr = new StreamReader("RepositoryPath.txt"))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        textBox2.Text = line;
                    }
                }
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Вы действительно хотите очистить поле с кодом CPP файла? ","!?",MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes) { richTextBox1.Text = ""; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            try
            {
                var text = File.ReadAllText(path,Encoding.Default);
                richTextBox1.Text= text;
                MessageBox.Show("Tекст с файла был успешно скопирован ! ");
            }
            catch { MessageBox.Show("Файл по заданному пути не был найден!", "Ошибка ",MessageBoxButtons.OK); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            t = richTextBox1.Text;
            Form next = new Form2();
            next.ShowDialog();
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1= new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            openFileDialog1.Filter = "Text files(*.txt) | *.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter= "Text files(*.txt) | *.txt|All files(*.*)|*.*";
           if(openFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            richTextBox1.Text=fileText;
            MessageBox.Show("Текст файла скопирован ");
             

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данное приложение служит для модификации кода ,написанного на языке C++.Импортировать код в приложение можно тремя способами :\n1)Импортировать напрямую (скопировать и вставить ).\n2)Имортировать ,выбрав текстовый файл\n3)Импортировать ,указав путь к файлу.\n После импортирования кода ,чтобы его модифицировать , нажмите кнопку 'Выбрать событие для добавления ' и выберите нужные вам пункты, после чего приложение добавит в ваш код выбранные события и вывод MessageBox при их вызове.  ");
        }

        private void button6_Click(object sender, EventArgs e)
        {
          using(StreamWriter sw = new StreamWriter("RepositoryPath.txt", false))
            {
                sw.WriteLine(textBox2.Text);
            }
                var dialog = new FolderBrowserDialog();
                dialog.SelectedPath = textBox2.Text; // установить начальную папку
            
                DialogResult result = dialog.ShowDialog();
            try {
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

                    // Read the contents of the C++ file
                    string cppText = File.ReadAllText(cppFilePath);
                    // Путь к папке, содержащей rc файлы
                    string rcFolderPath = Path.GetDirectoryName(cppFilePath);
                    string[] rcFiles = Directory.GetFiles(rcFolderPath, "*.rc", SearchOption.AllDirectories);

                    // Проверяем, найдены ли rc файлы
                    if (rcFiles.Length > 0)
                    {
                        // Предполагаем, что первый найденный rc файл будет использован
                        string rcFilePath = rcFiles[0];

                        // Читаем содержимое rc файла
                        string rcText = File.ReadAllText(rcFilePath);

                        // Отображаем содержимое в RichTextBox2
                        richTextBox2.Text = rcText;
                    }
                    else
                    {
                        MessageBox.Show("Could not find RC file in selected folder.");
                    }




                    // Display the contents in the richTextBox1 control
                    richTextBox1.Text = cppText;
                    using (StreamWriter sw = new StreamWriter("cppFilePath.txt", false))
                    {
                        sw.WriteLine(cppFilePath);
                    }

                    string c = textBox2.Text;
                    x = c;

                }
                
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }


        
        
        

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) Чтобы импортировать код c проекта, нажмите кнопку 'выбрать' и выберите папку с ним , после чего ,программа автоматически найдет в этой папке файл кода приложения на С++, если такой в ней существует и вставит его в поле .\n\n 2)Для дальнейшего удобства ,вы можете указать путь к папке с вашими проектами , который в дальнейшем будет открываться ,как начальный каталог .  ");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            t2 = richTextBox2.Text;
            Form next = new rcFunctions();
            next.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Вы действительно хотите очистить поле с кодом RC файла? ", "!?", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes) { richTextBox2.Text = ""; }

        }
    }
}
