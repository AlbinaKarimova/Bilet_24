
namespace Bilet_24
{
    public partial class Form1 : Form
    {
        private double result;
        private int user_id;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dbh = DBHelper.GetInstance(
                 "localhost",
                 3306,
                 "root",
                 "",
                 "ekzamen2023"
                 );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            user_id = Convert.ToInt32(numericUpDown1.Value);
            string rank = DBHelper.GetInstance().SelectRank(user_id);
            List<double> elems = DBHelper.GetInstance().SelectElems(user_id);
            Matrix matrix = new Matrix(elems, rank);

            result = matrix.Deter(matrix, Convert.ToInt32(rank));
            label2.Text += result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DBHelper.GetInstance().UpdateResult(result, user_id))
                label3.Text = "Результат успешно добавлен";

        }
    }
}