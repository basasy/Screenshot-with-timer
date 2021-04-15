using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShot
{
    public partial class Form1 : Form
    {
        private Bitmap Screenshot()
        {
            /* Englis: This method is we take a screenshot of
               Türkçe: Screenshot aldığımız method
             */

            Bitmap Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics GFX = Graphics.FromImage(Screenshot);
            GFX.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            return Screenshot;
        }
        
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * 50 / 100, Screen.PrimaryScreen.Bounds.Height * 50 / 100);
            
        }
        string Selected_File_Path = "";
        TextBox path = new TextBox();
        NumericUpDown photo_name = new NumericUpDown();
        NumericUpDown timer = new NumericUpDown();
        Button start = new Button();
        Button selectedPath = new Button();
        Label photo_number_header = new Label();
        Label timer_header = new Label();
        ListBox photos = new ListBox();
        Timer time = new Timer();
        Button stop_rec = new Button();
        Panel back1 = new Panel();
        Panel back = new Panel();
        Button c_rec = new Button();
        Button exit = new Button();
        /* English: We created the objects for the form.
           Türkçe: Form için nesneleri oluşturduk.
         
         */
                int sayac = 0;
        string sayactmp = "";
        string name = "";
        int adet = 0;
        string path_file = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            time.Interval = 2000;
            back.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.Controls.Add(back);
            

            photo_number_header.Text = "Resim isimleri hangi sayıdan başlasın";
            photo_number_header.Size = new Size(this.Width * 30 / 100, this.Height * 5 / 100);
            photo_number_header.Location = new Point(this.Width * 3 / 100, this.Height * 2 / 100);
            back.Controls.Add(photo_number_header);

            timer_header.Text = "Saniye";
            timer_header.Size = new Size(this.Width * 20 / 100, this.Height * 5 / 100);
            timer_header.Location = new Point(this.Width * 3 / 100, this.Height * 20 / 100);
            timer.Value = 1;
            back.Controls.Add(timer_header);
            

            photo_name.Size = new Size(this.Width * 10 / 100, this.Height * 20 / 100);
            photo_name.Location = new Point(this.Width * 5 / 100, this.Height * 8 / 100);
            photo_name.Maximum = decimal.MaxValue;
            back.Controls.Add(photo_name);

            timer.Size = new Size(this.Width * 10 / 100, this.Height * 20 / 100);
            timer.Location = new Point(this.Width * 5 / 100, this.Height * 26 / 100);
            timer.Maximum = decimal.MaxValue;
            back.Controls.Add(timer);


            selectedPath.Size = new Size(Screen.PrimaryScreen.Bounds.Width * 20 / 100, Screen.PrimaryScreen.Bounds.Height * 8 / 100);
            selectedPath.Location = new Point(this.Width * 50 / 100 - (selectedPath.Width / 2), this.Height * 20/ 100 - (selectedPath.Height / 2));
            selectedPath.Text = "Kayıt Yeri";
            selectedPath.Click += SelectedPath_Click;
            back.Controls.Add(selectedPath);

            
            path.Size = new Size(this.Width * 40 / 100, this.Height * 20 / 100);
            path.Location = new Point(this.Width * 30 / 100, this.Height * 32 / 100);
            path.Enabled = false;
            back.Controls.Add(path);

            
           
            start.Size = new Size(Screen.PrimaryScreen.Bounds.Width * 20 / 100, Screen.PrimaryScreen.Bounds.Height * 8 / 100);
            start.Location = new Point(this.Width * 50 / 100 - (start.Width / 2), this.Height * 50 / 100 - (start.Height / 2));
            start.Text = "Kayıda başla";
            back.Controls.Add(start);
            start.Click += Start_Click;
            stop_rec.Click += Stop_rec_Click;
            exit.Click += Exit_Click;
            c_rec.Click += C_rec_Click;

            /* Englis: We set the physical properties of the objects created in the form.
             * Türkçe: Oluşturulan nesnelerin formda ki fiziksel özelliklerini ayarladık. */
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            time.Stop();
            MessageBox.Show("Resimler Kaydedildi");
            back1.Visible = false;
            back.Visible = true;
            
        }

        private void C_rec_Click(object sender, EventArgs e)
        {
            time.Start();
            c_rec.Enabled = false;
            stop_rec.Enabled = true;
            /* English: Time starts when "Devam Et" button is active.
             * Türkçe: "Devam Et" butonu active olduğunda zaman başlar.*/
        }

        private void Stop_rec_Click(object sender, EventArgs e)
        {
            time.Stop();
            c_rec.Enabled = true;
            stop_rec.Enabled = false;

            /* English: Time stops when "DURDUR" button is active.
             * Türkçe: "Durdur" butonu active olduğunda zaman durur.
             */


        }

        private void SelectedPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog File = new FolderBrowserDialog();
            File.ShowDialog();
            Selected_File_Path = File.SelectedPath;
            path.Text = Selected_File_Path + "\\1000.jpg";
            /* English: We created the File object for the user to choose the file location where the images will be saved.
             * Türkçe: Kullanıcının resimleri kaydedeceği dosya konumunu seçmesi için File nesnesini kullandık */
            
            

        }

        private void Start_Click(object sender, EventArgs e)
        {
            /* English: Method that starts the screenshot
               Türkçe: ekran görüntüsünü başlatan method */


            if (Selected_File_Path.Equals(""))
            {
                MessageBox.Show("Lütfen Resimlerin Kaydedileceği Dosya Konumunu Seçin.");
            }
            else
            {

                
                back.Visible = false;
                time.Interval = Decimal.ToInt32(timer.Value) * 1000;
                back1.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                sayactmp = photo_name.Value.ToString();
                sayac = Int32.Parse(sayactmp);
                path_file = Selected_File_Path;
                name = photo_name.Value.ToString();
                adet = Int32.Parse(name);


                photos.Size = new Size(this.Width * 35 / 100, this.Height * 80 / 100);
                photos.Location = new Point(this.Width * 62 / 100, this.Height * 10 / 100);
                back1.Controls.Add(photos);

                Label saving = new Label();
                saving.Text = "Kaydediliyor...";
                saving.Size = new Size(this.Width * 30 / 100, this.Height * 5 / 100);
                saving.Location = new Point(this.Width * 62 / 100, this.Height * 5 / 100);

                
                stop_rec.Location = new Point(this.Width * 30 / 100, this.Height * 40 / 100);
                stop_rec.Text = "DURDUR";
                stop_rec.Size = new Size(this.Width * 20 / 100, this.Height * 10 / 100);

                c_rec.Location = new Point(this.Width * 30 / 100, this.Height * 20 / 100);
                c_rec.Text = "DEVAM ET";
                c_rec.Size = new Size(this.Width * 20 / 100, this.Height * 10 / 100);
                c_rec.Enabled = false;

                exit.Location = new Point(this.Width * 10 / 100, this.Height * 30 / 100);
                exit.Text = "ÇIK VE KAYDI DURDUR";
                exit.Size = new Size(this.Width * 20 / 100, this.Height * 10 / 100);
                

                back1.Controls.Add(c_rec);
                back1.Controls.Add(stop_rec);
                back1.Controls.Add(saving);
                back1.Controls.Add(exit);
                this.Controls.Add(back1);
                
                time.Start();
                time.Tick += Time_Tick;
                /* Englis: We created time object.
                 * Türkçe: Zaman nesnesini oluşturduk.*/
        }


    }
        
        private void Time_Tick(object sender, EventArgs e)
        {
                /* English: It takes screenshots every given second.
                 * Türkçe: Verilen saniye süresince ekran görüntüsü alır.
                 **/
                Selected_File_Path = path_file;
                Selected_File_Path = Selected_File_Path + "\\" + adet.ToString() + ".jpg";
                photos.Items.Add(Selected_File_Path);
                Console.WriteLine(Selected_File_Path);
                adet++;
                Screenshot().Save(Selected_File_Path);

        }
    }
}
