using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Filters
{
    public partial class Form1 : Form
    {

		Color sourcecolor;
		
        float[,] matrix = new float[3,3]   {{1, 1, 1},  //По какой матрице собирать пиксели
                                            {1, 1, 1},
                                            {1, 1, 1}};
        int koef = 1;   //Сколько клеток отступать от границ

        Bitmap newImage;   //Для отмены  
        Bitmap image;       //Для изображения

		public Form1()
        {
            InitializeComponent();
        }

        
        //открытие
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Создаем диалог для открытия файла
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.* ";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
                newImage = image;                 
            }
        
        
        
        }

        //backgroundWorker
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            image = ((Filters)e.Argument).processImage(image,backgroundWorker1);
            
            if (backgroundWorker1.CancellationPending)
                image = newImage;
            
            

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Image = image;
            pictureBox1.Refresh();
            progressBar1.Value = 0;
            newImage = image; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        //Фильтры
        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvertFilter filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеГауссToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void чёрноБелыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void яркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BrightFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void собельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new HarshnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MirrowFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Filters filter = new MedianFilter(matrix, koef);
			//Filters filter = new MathMarthology(matrix, -1);
			backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Waves();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void приюттToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new PriuttFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        //Морфология

        private void х3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matrix = new float[3, 3];
            koef = 1;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = 1;
        }

        private void х5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matrix = new float[5, 5];
            koef = 2;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i,j] = 1;
        }

        private void х7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matrix = new float[7, 7];
            koef = 3;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = 1;
        }

        private void крестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((i == matrix.GetLength(0) / 2) || (j == matrix.GetLength(1) / 2))
                        matrix[i, j] = 1;
                    else
                        matrix[i, j] = 0;
                }
        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bufkoef = koef;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((j < koef) || (j > matrix.GetLength(0) - koef-1))
                        matrix[i, j] = 0;
                    else
                        matrix[i, j] = 1;
                }
                if (i < matrix.GetLength(0) / 2) koef--;
                else koef++;
            }
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = 1;
                }
        }

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const int b = 0;
            Filters filter = new MathMarthology(matrix, b);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сужениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new Dilation(matrix,koef);
            const int a = 1;
            Filters filter = new MathMarthology(matrix, a);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void закрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {

			Filters filter = new Closing(matrix, koef);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void раскрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Filters filter = new Opening(matrix, koef);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void topHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TopHat(matrix, koef);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GreyWorld filter = new GreyWorld();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Image files|*.jpg;*.png;*.bmp|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Png);
                        break;

                    case 2:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                }

                fs.Close();
            }
        }

        private void опорныйЦветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ReferenceColor();
            backgroundWorker1.RunWorkerAsync(filter);
        }

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
			Filters filter_2 = new Pixel();
            image = (Bitmap)pictureBox1.Image;
            int x1 = image.Size.Width - 1;
            int y1 = image.Size.Height - 1;
			int x, y;
			x = filter_2.Clamp(e.X, 0, x1);
			y = filter_2.Clamp(e.Y, 0, y1);
			Color sourcecolor = image.GetPixel(x, y);
			Filters filter = new ReferenceColor(sourcecolor);
			backgroundWorker1.RunWorkerAsync(filter);
		}
		
	}
}
